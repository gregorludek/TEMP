REM SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.18.6.88)(PORT=1521))(CONNECT_DATA=(SID=ORCL)));User ID=PMG_01;Password=x"
SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SID=LGORACLE12C)));User ID=PMG_01;Password=x"
SET AssDir=i:\TfsVyvoj\EPRED_10\00_EXE\

EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifGASPREDCommon.dll" --allowDropColumn -o PMG.txt -m 


pause 

