SET ConnectString="Server=localhost\SQLEXPRESS;Database=en4g;User Id=en4g;Password=x"
SET AssDir=i:\TfsVyvoj\00_EXE\Enif.Debug\

EnifEntityModelDeployApp.exe -t MSSQL -c %ConnectString% -a "%AssDir%EnifN4GRPSCommon.dll" "%AssDir%EnifN4GVIPCommon.dll" --allowDropColumn -o EN4G.txt -m 


pause 

