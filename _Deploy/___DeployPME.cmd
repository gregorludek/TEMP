SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.18.6.88)(PORT=1521))(CONNECT_DATA=(SID=ORCL)));User ID=PME_01;Password=x"
REM SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SID=LGORACLE12C)));User ID=PME_01;Password=x"
SET AssDir=i:\TfsVyvoj\ERWE\PME\00_EXE\Enif.Debug\

EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifRWEPMECommon.dll" --allowDropColumn -o PME.txt -m 


pause 

