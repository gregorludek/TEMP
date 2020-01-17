SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.18.6.86)(PORT=1522))(CONNECT_DATA=(SID=ORA12)));User ID=deneb2nps;Password=DNB2usrNPS"
SET AssDir=i:\TfsVyvoj\00_EXE\Enif.NET\

EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifNHCommon.dll" --allowDropColumn -o _xxx.txt -m 

rem test!sefsef
pause 

