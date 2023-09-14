@echo off
setlocal

set "currentDir=%CD%"
set "targetDir=%currentDir%\src\SimpleBlazorGrid.Website"

cd /d "%targetDir%"

sass Css\_main.scss:wwwroot\css\site.css --watch

cd /d "%currentDir%"

endlocal