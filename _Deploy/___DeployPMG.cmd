REM SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.18.6.88)(PORT=1521))(CONNECT_DATA=(SID=ORCL)));User ID=PMG_01;Password=x"
SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SID=LGORACLE12C)));User ID=PMG_01;Password=x"
SET AssDir=I:\gitRepos\usy_lancelot_pmg01-innogy-net\build\

EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifGASPREDCommon.dll" "%AssDir%EnifPredAuditCommon.dll" --allowDropColumn -o PMG.txt -m 


pause 

