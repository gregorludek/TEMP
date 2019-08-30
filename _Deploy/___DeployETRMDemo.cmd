SET ConnectString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SID=LGORACLE12C)));User ID=ETRM_IS_04;Password=x"
SET AssDir=I:\gitRepos\uu_lancelotetrm_lancelotg01\uu_lancelotetrm_lancelotg01-server\artifacts\debug\

EnifEntityModelDeployApp.exe -t ORA -c %ConnectString% -a "%AssDir%EnifETRMCommon.dll" --allowDropColumn -o EtrmDemo.txt -m 


pause 

