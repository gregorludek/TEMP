SET ConnectString="Server=localhost\SQLEXPRESS;User Id=CLUSTER_01;Password=x"
SET AssDir=i:\gitRepos\00_EXE\Enif.DEBUG\

EnifEntityModelDeployApp.exe -t MSSQL -c %ConnectString% -a  "%AssDir%EnifCluster.dll" "%AssDir%EnifWebApi.Security.EnifIdToken.dll" --allowDropColumn -o Cluster.txt
rem "%AssDir%EnifNH.dll" "%AssDir%EnifAlarmsCommon.dll" "%AssDir%EnifWFNH.dll" "%AssDir%EnifBaseEntities.dll" "%AssDir%EnifImportExportExcelCommon.dll" "%AssDir%CygLMSEnif.dll" "


pause 

