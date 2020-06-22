SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SID=LGORACLE12C)));User ID=PME_PMG;Password=x"
SET AssDir=f:\TM\PME_PMG_2.1.14.0\Server\

EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifRWEPMECommon.dll" "%AssDir%EnifBaseEntities.dll" "%AssDir%EnifCommonEntities.dll" "%AssDir%EnifPMCommon.dll" "%AssDir%EnifCRMCommon.dll" "%AssDir%EnifRWEPMEOTEData.dll" --allowDropColumn -o PME_PMG.txt -m 


pause 

