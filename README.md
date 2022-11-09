# dvdStore

## Teck Stack
- Visual Studio (community version) latest
- MS SQL Server latest
- POCO genarator for generating view models
- Using 3Tier Achitecture (DAL, BLL, DAL) and ASP.NET Core Web API
- StartUp project is Store (ASP.NET Core Web API)
- The project solution has 4 projects

- Database connection string can be changed in the DataAccessLayer/DBHelper
- I Included the database files they will need to be attached to MS SQL Server
- When the project is running a swagger API documentation will open here https://localhost:7132/swagger/index.html
- For customer endpoint we have POST,GET Single, GET List, and PUT
- For DVDs POST,GET Single, GET List and PUT
- For Rentals POST renting a dvd, GET Single, GET List, GET rentals by status(in_progress, overdue, returned) and PUT
- There is a functionality to check and change Rental status based on returnDate, everytime a GET request is made the rentals are updated if they are overdue
- There is a functionality to check if the dvd is available before renting it, via (status and quantity)

## What was not finished
- Authontication
- Dashbored Report
- In DVD rentals the functionality to capture or allow customer to add many dvds to busket then rent them  
is not Implemented like that but a customer can rent as many dvds one by one. (I went back to the instructions and i just realized that)
- The resonses are unfinished in terms of appropriate statuses
