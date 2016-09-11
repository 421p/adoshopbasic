@echo off
title PostgreSQL Portable

set PGSQL=%cd%\..\..\..\PostgreSQLPortable\App\PgSQL
set PGDATA=%cd%\..\..\..\PostgreSQLPortable\App\PgSQL\..\..\Data\data
set PGLOG=%cd%\..\..\..\PostgreSQLPortable\App\PgSQL\..\..\Data\log.txt
set PGLOCALEDIR=%cd%\..\..\..\PostgreSQLPortable\App\PgSQL\share
set PGDATABASE=postgres
set PGPORT=5432
set PGUSER=postgres
PATH=%PATH%;%cd%\..\..\..\PostgreSQLPortable\App\PgSQL\bin

cls

:: set default code page
chcp 1252 > nul

:: initialise a new database on first use
if not exist "%PGDATA%" (
    echo.
    echo Initialising database for first use, please wait...
    "%PGSQL%\bin\initdb" -U %PGUSER% -A trust -E utf8 --locale=C >nul
)

:: startup postgres server
echo.
"%PGSQL%\bin\pg_ctl" -D "%PGDATA%" -l "%PGLOG%" -w start
cls
echo.
echo Type \q to quit and shutdown server
echo.
"%PGSQL%\bin\psql.exe"
echo.
"%PGSQL%\bin\pg_ctl" -D "%PGDATA%" stop
