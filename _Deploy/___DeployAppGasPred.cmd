REM SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SID=LGORACLE12C)));User ID=PMG_01;Password=x"
SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=LANCEDDB01)(PORT=1522))(CONNECT_DATA=(SID=ORA12)));User ID=COLOREMSYS;Password=Deneb2"
SET AssDir=i:\TfsVyvoj\EPRED_10\00_EXE\

EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifGASPREDCommon.dll" --allowDropColumn -o GasPred.txt -m 


pause 

