SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SID=LGORACLE12C)));User ID=PME_01;Password=x"
SET AssDir=i:\gitRepos\00_EXE\CoreBuild.v2\Enif.DEBUG\

rem EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifRWEPMECommon.dll" "%AssDir%EnifBaseEntities.dll" "%AssDir%EnifCommonEntities.dll" "%AssDir%EnifPMCommon.dll" "%AssDir%EnifCRMCommon.dll" "%AssDir%EnifRWEPMEOTEData.dll" --allowDropColumn -o PME.txt -m 
EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifALARMSCommon.dll" --allowDropColumn -o EnifAlarmsCommon.txt -m 

pause 

