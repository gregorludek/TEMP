using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Enif;
using Enif.Naos;
using Enif.Naos.Fx;

namespace InnoMigration
{
  public static class Program
  {
    static void Main(string[] args)
    {
      var step = 3;
      if (step == 0)
      {
        TruncateUATConfig();
      }
      if (step == 1)
      {
        ReplaceIds();
      }
      if (step == 2)
      {
        ConsolidateIds();
      }
      if (step == 3)
      {
        ConsolidateGridConfigs();
      }
    }

    private static void TruncateUATConfig()
    {
      var srcFile = @"f:\tmp\InnoMigration\UATConfig.xml";
      var destFile = @"f:\tmp\InnoMigration\UATConfig_Truncated.xml";

      var outIdsFile = @"f:\tmp\InnoMigration\outIds.txt";
      var inIdsFile = @"f:\tmp\InnoMigration\inIds.txt";
      var inIdsEntities = @"f:\tmp\InnoMigration\inIdsEntities.txt";

      var formulas = LoadFxStorageFromXmlFile(srcFile);
      var outIds = ReadDataIdsFromFile(outIdsFile);
      var formulasNew = formulas.Where(a => outIds.Intersect(a.Fxs.SelectMany(b => ReadOuts(b))).Count() > 0).ToList();
      SaveFxStorageToXmlFile(destFile, formulasNew);
      var ins = formulasNew.SelectMany(a => a.Fxs.SelectMany(b => ReadIns(b)))
        .Where(c => !outIds.Contains(c))
        .Where(d => int.Parse(d.Id) > 1000)
        .Distinct().OrderBy(a => a.TemplateId).ThenBy(a => a.Id).ToList();
      SaveDataIdsToFile(inIdsFile, ins);
      SaveEntityIdsToFile(inIdsEntities, ins);
    }
    private static void ReplaceIds()
    {
      var srcFile = @"f:\tmp\InnoMigration\UATConfig_Truncated.xml";
      var destFile = @"f:\tmp\InnoMigration\UATConfig_Truncated_Replaced.xml";
      var formulas = LoadFxStorageFromXmlFile(srcFile);
      ReplaceInFormulas(formulas, @"f:\tmp\InnoMigration\Mapping\", "TS");
      ReplaceInFormulas(formulas, @"f:\tmp\InnoMigration\Mapping\", "MeteoData");
      ReplaceInFormulas(formulas, @"f:\tmp\InnoMigration\Mapping\", "PMEContractABPDT");
      ReplaceInFormulas(formulas, @"f:\tmp\InnoMigration\Mapping\", "PMECustomer");
      ReplaceInFormulas(formulas, @"f:\tmp\InnoMigration\Mapping\", "PMEMMProduct");
      SaveFxStorageToXmlFile(destFile, formulas);
    }
    private static void ReplaceInFormulas(List<CNaosFormula> formulas, string directory, string entity)
    {
      var oldId2Key = ReadMapping(Path.Combine(directory, entity, "Old.Txt"), true);
      var key2NewId = ReadMapping(Path.Combine(directory, entity, "New.Txt"), false); ;

      foreach (var formula in formulas)
      {
        foreach (var fx in formula.Fxs)
        {
          for (var a = 0; a < fx.Data.Length; a++)
          {
            if (!fx.Data[a].TreeId.IsTemporary() && fx.Data[a].TreeId.EntityName() == entity)
            {
              fx.Data[a].TreeId = ReplaceTreeId(fx.Data[a].TreeId, oldId2Key, key2NewId);
            }
          }
        }
      }
    }
    private static TTreeId ReplaceTreeId(TTreeId treeId, Dictionary<string, string> oldId2Key, Dictionary<string, string> key2NewId)
    {
      var key = oldId2Key[treeId.Id];
      string newId;
      if (!key2NewId.TryGetValue(key, out newId)) newId = "NOT_FOUND_" + key + "_" + treeId.Id;
      return new TTreeId(newId, treeId.TemplateId, treeId.ConfigName, treeId.Kind);
    }

    private static void ConsolidateIds()
    {
      var inFiles = new[]
      {
        @"f:\tmp\InnoMigration\ProdConfig_Original.xml",
        @"f:\tmp\InnoMigration\UATConfig_Truncated_Replaced.xml"
      };

      var allFormulas = new List<CNaosFormula>();
      foreach (var inFile in inFiles) allFormulas.AddRange(LoadFxStorageFromXmlFile(inFile));
      for (var a = 0; a < allFormulas.Count; a++) allFormulas[a].Id = new TFormulaId((a + 1).ToString(), allFormulas[a].Id.TemplateId, allFormulas[a].Id.ConfigName);
      SaveFxStorageToXmlFile(@"f:\tmp\InnoMigration\Result.xml", allFormulas);
    }

    private static Dictionary<string, string> ReadMapping(string fileName, bool idIsKey)
    {
      var lines = File.ReadAllLines(fileName);
      var ret = new Dictionary<string, string>();
      foreach (var line in lines.Select(a => a.Split('\t')))
      {
        if (idIsKey) ret.Add(line[0], line[1]);
        else ret.Add(line[1], line[0]);
      }
      return ret;
    }
    static List<CNaosFormula> LoadFxStorageFromXmlFile(string sFileName)
    {
      XElement xE = XElement.Load(sFileName);
      List<CNaosFormula> arF = new List<CNaosFormula>();
      foreach (XElement xEF in xE.Elements())
      {
        CNaosFormula rNew = new CNaosFormula();
        rNew = (CNaosFormula)((IXmlSerializable)rNew).Deserialize(xEF, null);
        arF.Add(rNew);
      }
      return arF;
    }
    static void SaveFxStorageToXmlFile(string sFileName, List<CNaosFormula> storage)
    {
      XElement xE = new XElement("Formulas");
      foreach (CNaosFormula a in storage)
      {
        XElement xF = new XElement("Formula");
        foreach (var b in a.Fxs) if (b.Params == null) b.Params = new byte[0];
        ((IXmlSerializable)a).Serialize(xF);
        xE.Add(xF);
      }
      xE.Save(sFileName);
    }
    static void SaveDataIdsToFile(string sFileName, List<TDataId> dataIds)
    {
      File.WriteAllLines(sFileName, dataIds.Select(a => a.ToString()).ToArray());
    }
    static void SaveEntityIdsToFile(string sFileName, List<TDataId> dataIds)
    {
      File.WriteAllLines(sFileName, dataIds.Select(a => a.EntityId()).ToArray());
    }
    static string EntityId(this TDataId dataId)
    {
      return dataId.TemplateId.Split('.')[0] + "\t" + dataId.Id;
    }
    static string EntityName(this TTreeId dataId)
    {
      return dataId.TemplateId.Split('.')[0];
    }

    static HashSet<TDataId> ReadDataIdsFromFile(string sFileName)
    {
      var lines = File.ReadAllLines(sFileName);
      var ret = new HashSet<TDataId>();
      foreach (var line in lines) ret.Add(new TDataId(line));
      return ret;
    }
    static IEnumerable<TDataId> ReadIns(CNaosFx r)
    {
      for (int i = 0; i < r.DataInCount; i++)
      {
        TTreeId tId = r.Data[i].TreeId;
        if (!tId.IsTemporary()) yield return (TDataId)tId;
      }
    }
    static IEnumerable<TDataId> ReadOuts(CNaosFx r)
    {
      for (int i = r.DataInCount; i < r.DataAllCount; i++)
      {
        TTreeId tId = r.Data[i].TreeId;
        if (!tId.IsTemporary()) yield return (TDataId)tId;
      }
    }

    private static void ConsolidateGridConfigs()
    {
      var srcDir = @"f:\tmp\InnoMigration\Grid\";
      var targetDir = @"f:\tmp\InnoMigration\GridOut\";

      foreach (var file in Directory.GetFiles(srcDir, "*.xml", SearchOption.TopDirectoryOnly))
      {
        var newFileName = Path.Combine(targetDir, "9" + Path.GetFileName(file).Substring(2));
        File.Copy(file, newFileName, true);
      }
      ReplaceInFiles(targetDir, @"f:\tmp\InnoMigration\Mapping\", "PMEMMContractTypeTS");
      ReplaceInFiles(targetDir, @"f:\tmp\InnoMigration\Mapping\", "PMEMMProduct");
    }

    private static void ReplaceInFiles(string srcDir, string mappingDir, string entity)
    {
      var oldId2Key = ReadMapping(Path.Combine(mappingDir, entity, "Old.Txt"), true);
      var key2NewId = ReadMapping(Path.Combine(mappingDir, entity, "New.Txt"), false); ;

      foreach (var file in Directory.GetFiles(srcDir, "*.xml", SearchOption.TopDirectoryOnly))
      {
        var element = XElement.Load(file);
        foreach (var shapeElement in element.ReadElement("Shapes").ReadElement("Shape").ReadElement("Shapes").ReadElements("Shape"))
        {
          if (shapeElement.Attribute("Name").Value != shapeElement.Attribute("TreeId").Value) throw new Exception();
          var treeId = new TTreeId(shapeElement.Attribute("Name").Value);
          if (treeId.EntityName() == entity)
          {
            treeId = ReplaceTreeId(treeId, oldId2Key, key2NewId);
            shapeElement.Attribute("Name").Value = shapeElement.Attribute("TreeId").Value = treeId.ToString();
          }
        }
        element.Save(file);
      }
    }
  }
}
