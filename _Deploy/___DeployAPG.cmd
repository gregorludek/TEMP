SET ConnectString="Server=localhost\SQLEXPRESS;Database=APG;User Id=APG;Password=x"
SET AssDir=i:\TfsVyvoj\00_EXE\Enif.Debug\

EnifEntityModelDeployApp.exe -t MSSQL -c %ConnectString% -a  "%AssDir%EnifApgCommon.dll" --allowDropColumn -o APG.txt
rem "%AssDir%EnifNH.dll" "%AssDir%EnifAlarmsCommon.dll" "%AssDir%EnifWFNH.dll" "%AssDir%EnifBaseEntities.dll" "%AssDir%EnifImportExportExcelCommon.dll" "%AssDir%CygLMSEnif.dll"


pause 

