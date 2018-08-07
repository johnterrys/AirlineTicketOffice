# Airline Ticket Office (Book Flights).
## MVVM application with WPF and Entity Framework 6. Coursework for University : 'Flight Booking Application'. Database-First Approach (EF).

---
<a href="https://github.com/VladTsiukin/AirlineTicketOffice/tree/master/AirlineTicketOffice.Main/Docs/ATOpresent.gif"><img src="https://github.com/VladTsiukin/AirlineTicketOffice/blob/master/AirlineTicketOffice.Main/Docs/ATOpresent.gif" /></a>
---
> #### Brief description of the possibilities:

> - Display of tariff data
> - Display of ticket data
> - Display of cashier data
> - Display of flight data
> - Display of passenger data
> - Create new ticket data
> - Changing and creating data about the passenger
> - Changing and creating data about the cashier


### ***REQUIREMENTS***

![alt text](https://img.shields.io/badge/.NET%20Framework-4.0%20or%20above-blue.svg)
![alt text](https://img.shields.io/badge/Visual%20Studio-2015%20or%20above-blue.svg)
![alt text](https://img.shields.io/badge/SQL%20Server-2008%20or%20above-blue.svg)
![alt text](https://img.shields.io/badge/Platform-WINDOWS%20XP%20or%20above-blue.svg)
![alt text](https://img.shields.io/badge/Dependencies-MVVM%20Light%3A%20v5.3%20%7C%20EF%20v6.1%20%7C%20internet%20connection%20required-blue.svg)


### ***RUN THE APPLICATION***

>#### The steps to start the application are as follows:

> - Download this zip and extract it to desired location.
> - Run 'AirlineTicketOffice.sln' using Visual Studio (internet connection required - for installation 'MVVM Light Toolkit ' and 'Entity Framwork').
> - In 'AirlineTicketOffice.Main' project - open file <a href="https://github.com/VladTsiukin/AirlineTicketOffice/blob/master/AirlineTicketOffice.Main/Docs/AirlineTicketOffice_Create_T-SQL_DB.sql">Create the db with t-sql</a>.
> - In this file on line 21 and 23, replace the path 'FILENAME' to desired (Sample: FILENAME=N'D:\AirlineTicketOffice.mdf'). Can not change the name ('AirlineTicketOffice.mdf' and 'AirlineTicketOffice_log.ldf') of the database. If path on the disk 'C:' - need to run VS with administrator rights.
> - Create database: run 'AirlineTicketOffice_Create_T-SQL_DB.sql' in 'new query' via SSMS sql server or VS.
> - By default, connectionString="data source=(localdb)\MSSQLLocalDB;...". If need - replace 'source' to required in app.config (AirlineTicketOffice.Main project).
