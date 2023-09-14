@echo off
setlocal

set "currentDir=%CD%"
set "targetDir=%currentDir%\src\SimpleBlazorGrid.Core"

cd /d "%targetDir%"

sass Css\_main.scss:wwwroot\css\simpledatagrid.css

cd /d "%currentDir%"
set "targetDir=%currentDir%\src\SimpleBlazorGrid.Website"
cd /d "%targetDir%"

sass Css\_main.scss:wwwroot\css\site.css

cd /d "%currentDir%"

endlocal
