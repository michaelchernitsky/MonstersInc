# MonstersInc Management API

Simple and maintainable Management API for our intimidators!

Based on:

- ASP.NET Core 3.1
- MSSQL Server
- Identity Server 4 (OAUTH 2.0)

## How to install it?

For db migration please use the following instructions:

1. Open cmd

2. Change directory to MonstersInc folder

3. Run the following commands in the command prompt interface (cmd):

   ```bash
   dotnet ef migrations add initial --context MonstersIncDbContext
   dotnet ef database update --context MonstersIncDbContext
   ```

4. Change directory to AuthServer.Infrastructure folder

5. Run

   ```bash
   dotnet ef migrations add initial --context AppIdentityDbContext
   dotnet ef migrations add initial --context PersistedGrantDbContext
   dotnet ef database update --context AppIdentityDbContext
   dotnet ef database update --context PersistedGrantDbContext
   ```

For sql server 2017 without  SQLServer2017-KB4557397-x64 (for migration only), it is required to add  to connection string  AttachDbFilename section:

for solution AuthServer.Infrastructure

``` c#
;AttachDbFilename=[date file directory]\\AuthServer.mdf
```

for solution MonstersInc 

``` c#
;AttachDbFilename=[date file directory]\\MonstersInc.mdf
```

After db migration required to setup Doors list in MonstersInc database . Run from SQL Management :

```sql
insert into Doors values('door 1' , 100)
insert into Doors values('door 2' , 200)
insert into Doors values('door 3' , 300)
insert into Doors values('door 4' , 400)
insert into Doors values('door 5' , 500)
insert into Doors values('door 6' , 600)
insert into Doors values('door 7' , 700)
insert into Doors values('door 8' , 800)
insert into Doors values('door 9' , 900)
```



## How to use it?

First , run both solutions  , then authorize from swagger UI and you are available to perform any intimidator activities

For register new users go to :

​	https://localhost:44385/Account/Register 

