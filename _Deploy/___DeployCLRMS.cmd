SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.18.6.88)(PORT=1521))(CONNECT_DATA=(SID=ORCL)));User ID=ing_lancelot;Password=x"
SET AssDir=f:\TM\PME_PMG_2.1.14.0\Server\

EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifRWEPMECommon.dll" "%AssDir%EnifBaseEntities.dll" "%AssDir%EnifCommonEntities.dll" "%AssDir%EnifPMCommon.dll" "%AssDir%EnifCRMCommon.dll" "%AssDir%EnifRWEPMEOTEData.dll" --allowDropColumn -o CLRMS.txt -m 


pause 

