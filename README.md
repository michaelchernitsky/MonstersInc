# MonstersInc Management API

Simple and maintainable Management API for our intimidators!

Based on:

- ASP.NET Core 3.1
- MSSQL Server
- Identity Server 4 (OAUTH 2.0)

## How to install it?

For db migration required

​		a.    Open cmd

​		b.    Change directory to MonstersInc folder

​		c.    Run 

​			dotnet ef migrations add initial --context MonstersIncDbContext

​			dotnet ef database update --context MonstersIncDbContext

​		d.    Change directory to AuthServer.Infrastructure folder

​		e.    Run

​			dotnet ef migrations add initial --context AppIdentityDbContext

​			dotnet ef migrations add initial --context PersistedGrantDbContext

​			dotnet ef database update --context AppIdentityDbContext

​			dotnet ef database update --context PersistedGrantDbContext

 

For sql server 2017 without  SQLServer2017-KB4557397-x64   required to add  to connection string  AttachDbFilename section :

​			 ;AttachDbFilename=[date file directory]\\AuthServer.mdf



After db migration required to setup Doors list in MonstersInc database . Run from SQL Management :

  insert into Doors values('door 1' , 100)
  insert into Doors values('door 2' , 200)
  insert into Doors values('door 3' , 300)
  insert into Doors values('door 4' , 400)
  insert into Doors values('door 5' , 500)
  insert into Doors values('door 6' , 600)
  insert into Doors values('door 7' , 700)
  insert into Doors values('door 8' , 800)
  insert into Doors values('door 9' , 900)

## How to use it?

First , run both solutions  , then authorize from swagger UI and you are available to perform any intimidator activities

For register new users go to :

​	https://localhost:44385/Account/Register 