@echo off
setlocal

set "currentDir=%CD%"
set "targetDir=%currentDir%\src\SimpleBlazorGrid.Core"

cd /d "%targetDir%"

sass Css\_main.scss:wwwroot\css\simpledatagrid.css --watch

cd /d "%currentDir%"

endlocal
