-------------------------------------------
USE [master]
GO

/****** Object:  Database [AirlineTicketOffice] ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'AirlineTicketOffice')
DROP DATABASE [AirlineTicketOffice]
GO

USE [master]
GO

----------------------------------------------------------------------
PRINT ' '
PRINT 'Started - ' + CONVERT(varchar, GETDATE(), 121);
GO
-----------------------------------------------------------------------

/****** Object:  Database [AirlineTicketOffice] ******/
CREATE DATABASE [AirlineTicketOffice] ON  PRIMARY 
( NAME = N'AirlineTicketOffice', FILENAME = N'D:\AirlineTicketOffice.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
LOG ON 
( NAME = N'AirlineTicketOffice_log', FILENAME = N'D:\AirlineTicketOffice_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [AirlineTicketOffice] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AirlineTicketOffice].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [AirlineTicketOffice] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET ARITHABORT OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET AUTO_CLOSE OFF
GO

--ALTER DATABASE [AirlineTicketOffice] SET CHANGE_TRACKING = ON (CHANGE_RETENTION = 2 DAYS, AUTO_CLEANUP = ON)
--GO

ALTER DATABASE [AirlineTicketOffice] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [AirlineTicketOffice] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [AirlineTicketOffice] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET  DISABLE_BROKER 
GO

ALTER DATABASE [AirlineTicketOffice] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [AirlineTicketOffice] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [AirlineTicketOffice] SET  READ_WRITE 
GO

ALTER DATABASE [AirlineTicketOffice] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [AirlineTicketOffice] SET  MULTI_USER 
GO

ALTER DATABASE [AirlineTicketOffice] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [AirlineTicketOffice] SET DB_CHAINING OFF 
GO
-------------------------------------------------------

use AirlineTicketOffice
go


CREATE TYPE [dbo].[phone] FROM [varchar](16) NULL
GO

CREATE TABLE Ticket (     
       TicketID      int IDENTITY(1,1),
       FlightID      int NOT NULL,  
       PassengerID   int NOT NULL,
       CashierID     int NOT NULL,
       RateID        int NOT NULL,
       SaleDate      date NOT NULL,
       TotalCost     decimal NOT NULL,
)
go


ALTER TABLE Ticket
       ADD CONSTRAINT PK_Ticket PRIMARY KEY NONCLUSTERED (TicketID)
go


CREATE TABLE Aircraft (
	   AircraftID       int IDENTITY(1,1),
       TailNumber       varchar(10) NOT NULL,
       DateOfIssue      date NOT NULL,
       TypeOfAircraft   varchar(50) NOT NULL
)
go


ALTER TABLE Aircraft
       ADD CONSTRAINT PK_Aircraft PRIMARY KEY NONCLUSTERED (
              AircraftID)
go


CREATE TABLE Cashier (
       CashierID             int IDENTITY(1,1),
       NumberOfOffices       int NOT NULL,
       FullName              varchar(50) NOT NULL
)
go


ALTER TABLE Cashier
       ADD CONSTRAINT PK_Cashier PRIMARY KEY NONCLUSTERED (CashierID)
go


CREATE TABLE Route (
       RouteID             int IDENTITY(1,1),
       NameRoute           varchar(50) NOT NULL,
       AirportOfDeparture  varchar(50) NOT NULL,
       AirportOfArrival    varchar(50) NOT NULL,
	   TravelTime          time(0) NOT NULL,
       Distance            float NOT NULL,
       Cost                decimal NOT NULL
)
go


ALTER TABLE Route
       ADD CONSTRAINT PK_Route PRIMARY KEY NONCLUSTERED (RouteID)
go


CREATE TABLE PlaceInAircraft (
       TypePlace         varchar(1) NOT NULL,
       AircraftID        int NOT NULL,
       Amount            int NOT NULL
)
go


ALTER TABLE PlaceInAircraft
       ADD CONSTRAINT PK_PlaceInAircraft PRIMARY KEY NONCLUSTERED (TypePlace, AircraftID)
go


CREATE TABLE PlaceInFlight(
       TypePlace          varchar(1) NOT NULL,
       FlightID           int NOT NULL,
       Amount             int NOT NULL
)
go


ALTER TABLE PlaceInFlight
       ADD CONSTRAINT PK_PlaceInFlight PRIMARY KEY NONCLUSTERED (TypePlace, FlightID)
go


CREATE TABLE Passenger (
       PassengerID           int IDENTITY(1,1),
       Citizenship           varchar(50) NOT NULL,
       PassportNumber        varchar(14) NOT NULL, 
       Sex                   varchar(1) NOT NULL,
       FullName              varchar(100) NOT NULL,
       DateOfBirth           date NOT NULL,
       TermOfPassportDate    date NOT NULL,
       CountryOfResidence    varchar(50) NOT NULL,
       PhoneMobile           phone NULL,
       Email                 varchar(50) NULL
)
go


ALTER TABLE Passenger
       ADD CONSTRAINT PK_Passenger PRIMARY KEY NONCLUSTERED (PassengerID)
go


CREATE TABLE Flight (
	   FlightID           int IDENTITY(1,1),
       FlightNumber       varchar(10) NOT NULL,
       RouteID            int NOT NULL,
       DateOfDeparture    date NOT NULL,
       DepartureTime      time(0) NOT NULL,
       TimeOfArrival      time(0) NOT NULL,
       AircraftID         int NOT NULL
)
go


ALTER TABLE Flight
       ADD CONSTRAINT PK_Flight PRIMARY KEY NONCLUSTERED (FlightID)
go

CREATE TABLE Rate (
       RateID				int IDENTITY(1,1),
       RateName				varchar(50) NOT NULL,
       TicketRefund			varchar(1) NOT NULL,
       BookingChanges		varchar(1) NOT NULL,
       BaggageAllowance		float NOT NULL,
       FreeFood				varchar(1) NOT NULL,
       TypeOfPlace			varchar(1) NOT NULL
)
go


ALTER TABLE Rate
       ADD CONSTRAINT PK_Rate PRIMARY KEY NONCLUSTERED (RateID)
go



ALTER TABLE Ticket
       ADD CONSTRAINT FK_Ticket_RateID
              FOREIGN KEY (RateID)
                             REFERENCES Rate
go


ALTER TABLE Ticket
       ADD CONSTRAINT FK_Ticket_CashierID
              FOREIGN KEY (CashierID)
                             REFERENCES Cashier
go


ALTER TABLE Ticket
       ADD CONSTRAINT FK_Ticket_PassengerID
              FOREIGN KEY (PassengerID)
                             REFERENCES Passenger
go


ALTER TABLE Ticket
       ADD CONSTRAINT FK_Ticket_
              FOREIGN KEY (FlightID)
                             REFERENCES Flight
go


ALTER TABLE PlaceInAircraft
       ADD CONSTRAINT FK_PlaceInAircraft_Aircraft
              FOREIGN KEY (AircraftID)
                             REFERENCES Aircraft
go


ALTER TABLE PlaceInFlight
       ADD CONSTRAINT FK_PlaceInFlight_Flight
              FOREIGN KEY (FlightID)
                             REFERENCES Flight
go


ALTER TABLE Flight
       ADD CONSTRAINT FK_Flight_Aircraft
              FOREIGN KEY (AircraftID)
                             REFERENCES Aircraft
go


ALTER TABLE Flight
       ADD CONSTRAINT FK_Flight_Route
              FOREIGN KEY (RouteID)
                             REFERENCES Route
go

----------------------------------------------ADDITIONAL CONSTRAINTS------------------------------------------------------------


ALTER TABLE dbo.Passenger ADD  CONSTRAINT DF_PassportNumber  UNIQUE(PassportNumber);
GO

ALTER TABLE dbo.Passenger ADD  CONSTRAINT CH_TermOfPassportDate CHECK(TermOfPassportDate > DateOfBirth);
GO
---------
ALTER TABLE dbo.Ticket ADD  CONSTRAINT CH_TotalCost CHECK(TotalCost >= 0);
GO

----------
ALTER TABLE dbo.Route ADD  CONSTRAINT CH_Route CHECK(LEN(NameRoute) > 3);
GO
----------

ALTER TABLE dbo.Ticket ADD  CONSTRAINT DF_SaleDate_Ticket  DEFAULT(GETDATE()) FOR SaleDate
GO

---------------------------------------------------------------- VIEW ---------------------------------
-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------


use AirlineTicketOffice
go

IF OBJECT_ID ('BoughtTickets_ATO') IS NOT NULL
    DROP VIEW BoughtTickets_ATO;
GO

/* Select all tickets. */

CREATE VIEW BoughtTickets_ATO AS  
select dbo.Passenger.FullName,
       dbo.Passenger.PassportNumber,
       dbo.Flight.FlightNumber,
       dbo.Ticket.TotalCost,
	   dbo.Ticket.SaleDate,
       dbo.Rate.RateName,
       dbo.Flight.DateOfDeparture,
       dbo.Flight.DepartureTime,
       dbo.Flight.TimeOfArrival,
       dbo.Route.NameRoute,
       dbo.Route.AirportOfDeparture,
       dbo.Route.AirportOfArrival,
	   dbo.Route.TravelTime,
	   dbo.Route.Distance,
       dbo.Aircraft.TypeOfAircraft,
	   dbo.Cashier.FullName as 'CashierFullName',
	   dbo.Cashier.NumberOfOffices
from   dbo.Passenger,
       dbo.Ticket,
       dbo.Rate,
       dbo.Flight,
       dbo.Route,
	   dbo.Aircraft,
	   dbo.Cashier
where  dbo.Passenger.PassengerID = dbo.Ticket.PassengerID
  and  TypeOfPlace = (select TypeOfPlace
                   from   dbo.Rate 
                   where  dbo.Rate.RateID = dbo.Ticket.RateID)
  and  dbo.Ticket.FlightID = dbo.Flight.FlightID
  and  dbo.Flight.RouteID = dbo.Route.RouteID
  and  dbo.Flight.AircraftID = dbo.Aircraft.AircraftID
  and  dbo.Ticket.CashierID = dbo.Cashier.CashierID;

go

-------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------
use [AirlineTicketOffice];
go


IF OBJECT_ID ('Ticket_ATO') IS NOT NULL
    DROP VIEW Ticket_ATO;
GO

/* Select all tickets. */

CREATE VIEW Ticket_ATO AS 
SELECT  t.[TicketID],
		t.[FlightID],
		t.[PassengerID],
		t.[CashierID],
		t.[RateID],
		t.[SaleDate],
		t.[TotalCost]	
FROM	[dbo].[Ticket] as t	
go

-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
use [AirlineTicketOffice];
go


IF OBJECT_ID ('Passenger_ATO') IS NOT NULL
    DROP VIEW Passenger_ATO;
GO

/* Select all Passenger. */

CREATE VIEW Passenger_ATO AS
SELECT	p.[PassengerID],
		p.[Citizenship],
	    p.[PassportNumber],
		p.[Sex],
		p.[FullName],
		p.[DateOfBirth],
		p.[TermOfPassportDate],
		p.[CountryOfResidence],
		p.[PhoneMobile],
		p.[Email]
FROM [dbo].[Passenger] as p
go
-------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------


IF OBJECT_ID ('PassangerTicket_ATO') IS NOT NULL
    DROP VIEW PassangerTicket_ATO;
GO

/* Select all tickets. */

CREATE VIEW PassangerTicket_ATO AS 
SELECT 	t.[TicketID],
		t.[FlightID],
		t.[PassengerID],
		t.[CashierID],
		t.[RateID],
		t.[SaleDate],
		t.[TotalCost],
	    p.[Citizenship],
	    p.[PassportNumber],
		p.[Sex],
		p.[FullName],
		p.[DateOfBirth],
		p.[TermOfPassportDate],
		p.[CountryOfResidence],
		p.[PhoneMobile],
		p.[Email]
		
FROM	[dbo].[Ticket] t INNER JOIN [dbo].[Passenger] p
ON		t.PassengerID = p.PassengerID 	
go



-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
  
use [AirlineTicketOffice];
go


IF OBJECT_ID ('GetTariffs_ATO') IS NOT NULL
    DROP VIEW GetTariffs_ATO;
GO

CREATE VIEW GetTariffs_ATO AS
SELECT  [RateID],
		[RateName],
		[TicketRefund],
		[BookingChanges],
		[BaggageAllowance],
		[FreeFood],
		[TypeOfPlace]
FROM   [dbo].[Rate]

go
------------------------------------------------------------------------------ FUNCTION ---------------------------

use [AirlineTicketOffice];
go

if object_id ('f_GetTimeOfArrival') is not null
    drop function f_GetTimeOfArrival;
go

create function f_GetTimeOfArrival
	(@departuretime varchar(121),
	 @routeid int)
returns time
as
begin 
declare @time as time;
	declare @total as bigint;
	declare @restime as time;
	set @time = (select dbo.route.traveltime from dbo.route where routeid = @routeid);
	select @total = (datepart(ss,@time) + 60 * datepart(mi,@time) + 3600 * datepart(hh,@time))/60;
    set @restime = dateadd(minute, @total,@departuretime);
    return @restime;
end
go

--select [dbo].[f_GetTimeOfArrival]('2017-06-15 02:00:00', 2);

------------------------------------------------------------------------------ PROCEDURE ---------------------

-- uspPrintError prints error information about the error that caused 
-- execution to jump to the CATCH block of a TRY...CATCH construct. 
-- Should be executed from within the scope of a CATCH block otherwise 
-- it will return without printing any error information.

use [AirlineTicketOffice];
go

if object_id ('ATO_PrintError') is not null
    drop PROCEDURE ATO_PrintError;
go

CREATE PROCEDURE ATO_PrintError 
AS
BEGIN
    SET NOCOUNT ON;

    -- Print error information. 
    PRINT 'Error ' + CONVERT(varchar(50), ERROR_NUMBER()) +
          ', Severity ' + CONVERT(varchar(5), ERROR_SEVERITY()) +
          ', State ' + CONVERT(varchar(5), ERROR_STATE()) + 
          ', Procedure ' + ISNULL(ERROR_PROCEDURE(), '-') + 
          ', Line ' + CONVERT(varchar(5), ERROR_LINE());
    PRINT ERROR_MESSAGE();
END;
GO

--------------------------------------------------------------------------------------- TRIGGER ----------------------

use AirlineTicketOffice;
go

IF OBJECT_ID ('t_ReplaceOnFlightAfterBuyTicket') IS NOT NULL
    DROP TRIGGER t_ReplaceOnFlightAfterBuyTicket;
GO

CREATE TRIGGER t_ReplaceOnFlightAfterBuyTicket ON [dbo].[Ticket] 
AFTER INSERT AS 
BEGIN
	DECLARE @TicketID int;
    DECLARE @Count int;
	DECLARE @FlightID int;
	DECLARE @RateID int;
	DECLARE @TypeOfPlace varchar(1);
	--cursor:
	DECLARE ticket_cursor CURSOR FOR
    SELECT ins.TicketID, ins.FlightID, ins.RateID FROM INSERTED ins;

    SET @Count = @@ROWCOUNT;
    IF @Count = 0 
		BEGIN
			PRINT 'Error insert'
			RETURN;
		END;
	
    SET NOCOUNT ON;

    BEGIN TRY
        -- If inserting or updating these columns
        IF UPDATE([TicketID])	
			BEGIN
				OPEN ticket_cursor;
				FETCH NEXT FROM ticket_cursor
					INTO @TicketID, @FlightID, @RateID
				WHILE @@FETCH_STATUS = 0
				BEGIN
					SELECT DISTINCT @TypeOfPlace = dbo.Rate.TypeOfPlace FROM dbo.Rate WHERE 
					dbo.Rate.RateID =  @RateID;

					-- Update Place In Flight 
					UPDATE [dbo].[PlaceInFlight]
					SET Amount = Amount - 1
					WHERE FlightID = @FlightID
					AND   TypePlace = @TypeOfPlace
					AND   Amount >= 1;

					PRINT 'Success after inset ticket №' + (CAST(@TicketID AS varchar(1)));            
		
					FETCH NEXT FROM ticket_cursor
					INTO @TicketID, @FlightID, @RateID
				END
	
				CLOSE ticket_cursor;
				DEALLOCATE ticket_cursor;					
			END
		ELSE
			BEGIN
				PRINT 'Error after inset ticket!';
				RETURN;		
			END
    END TRY
    BEGIN CATCH
        EXECUTE [dbo].[ATO_PrintError];
		DEALLOCATE ticket_cursor;
        -- Rollback any active or uncommittable transactions before
        -- inserting information in the ErrorLog
        IF @@TRANCOUNT > 0
				
        ROLLBACK TRANSACTION;

    END CATCH
END;
GO

------------------------------------------------------ flight inserting function helper -----------------
----------------------------------------------------------------------------------------------------------

use [AirlineTicketOffice];
go

if object_id ('f_FlightInsertingHelper_ATO') is not null
    drop function f_FlightInsertingHelper_ATO;
go

create function f_FlightInsertingHelper_ATO
	(@typeOfPlace varchar(1),
	 @flightId int)
returns int
as
begin 
declare @amount as int;	
	set @amount = (select [Amount] 
	               from [dbo].[PlaceInAircraft]
				   where [TypePlace] = @typeOfPlace
				   and   [AircraftID] = (select [AircraftID]
	                                     from [dbo].[Flight]
										 where [FlightID] = @flightId));

    return @amount;
end
go

-------select [dbo].[f_FlightInsertingHelper_ATO]('A', 2);

----------------------------------------------------------------------------------------------------------
---------------------------------------------------INSERT DATA--------------------------------------------

use AirlineTicketOffice
go

PRINT 'dbo.Aircraft inserting'
go

INSERT INTO dbo.Aircraft (TailNumber, DateOfIssue, TypeOfAircraft)
VALUES ( 'EW-250PA', '2010-02-14', 'Embraer 195'),                          
       ( 'EW-770PA', '2006-08-17', 'Boeing 737-200'),                         
       ( 'EW-880PA', '2009-07-20', 'Airbus A319');
GO

PRINT 'dbo.PlaceInAircraft inserting'
go

INSERT INTO dbo.PlaceInAircraft (TypePlace, AircraftID, Amount)
VALUES  ( 'A', 1, 30),
		( 'B', 1, 100),
		( 'A', 2, 40),
		( 'B', 2, 120),
		( 'A', 3, 50),
		( 'B', 3, 160);
GO

PRINT 'dbo.Rate inserting'
go

INSERT INTO dbo.Rate ( RateName, TicketRefund, BookingChanges, BaggageAllowance, FreeFood, TypeOfPlace)
VALUES ( 'Business', '+', '+', 32, '+', 'A'),
       ( 'Economy', '-', '-', 20, '+', 'B');
GO

PRINT 'dbo.Cashier inserting'
go

INSERT INTO dbo.Cashier (NumberOfoffices, FullName)
VALUES ( 777, 'Muhammad Ali'),
       ( 888, 'Bud Abbott'),
       ( 999, 'Mary Anderson');
GO

PRINT 'dbo.Route inserting'
go

INSERT INTO dbo.Route (NameRoute, AirportOfDeparture, AirportOfArrival, Distance, TravelTime, Cost)
VALUES ( 'Moscow-Berlin', 'Moscow, Domodedovo(DME)', 'Berlin (Schonefeld)', 900, '01:55:00', 150),
       ( 'Karaganda-Brussels', 'Karaganda (KGF)', 'Brussels (Charleroi)', 1200, '02:45:00', 170),
       ( 'Minsk-Stockholm', 'Minsk(MSQ)',  'Stockholm (Arlanda)', 836, '01:45:00', 140),
       ( 'Baku-London', 'Baku (BAK)', 'London (Gatwick)', 1300, '02:30:00', 200),
       ( 'Minsk-Amsterdam', 'Minsk(MSQ)', 'Amsterdam, Schiphol(AMS)', 1610, '02:45:00', 220),
       ( 'Minsk-Milan', 'Minsk(MSQ)', 'Milan, Malpensa(MXP)', 1580, '02:35:00', 210);
GO

PRINT 'dbo.Passenger inserting'
go

INSERT INTO dbo.Passenger (Citizenship, PassportNumber, Sex, FullName, DateOfBirth, 
							TermOfPassportDate, CountryOfResidence, PhoneMobile, Email) 
VALUES ( 'Belarus', '0748777E998EV8', 'M', 'Ray Bolger', '1990-03-17', '2032-03-17', 'Belarus',
         '+37529-330-30-30', 'bolger@mail.com'),
       ( 'Austria', '0778977E998EV8', 'M', 'Javier Bardem', '1995-08-20', '2020-05-14', 'Austria',
         '+37529-335-37-37', 'bardem@mail.net'),
       ( 'Argentina', '0768977E998EV8', 'W', 'Alice Brady', '1985-04-10', '2029-05-14', 'Bangladesh',
         '+37533-337-66-36', 'alice@mail.net'),
       ( 'Canada', '0728977E998EV8', 'M', 'Les Brown', '1985-01-12', '2029-05-14', 'Canada',
         '+37529-339-55-38', 'brown@mail.net'),
       ( 'Belarus', '0718977E998EV8', 'W', 'Maria Callas', '1990-05-19', '2032-05-14', 'Belarus',
         '+37544-338-22-37', 'callas@gmail.com'),
       ( 'Belarus', '0748977E888EV8', 'M', 'Lou Costello', '1989-05-14', '2030-08-23', 'USA',
         '+37529-339-88-39', 'costello@gmail.com');
GO

PRINT 'dbo.Flight inserting'
go
---------------------------------------------------------------------------
INSERT INTO dbo.Flight (FlightNumber, RouteID, DateOfDeparture, DepartureTime, TimeOfArrival, AircraftID)
VALUES  (  'B2-0891', 1, '2017-05-15', '2017-05-15 21:15:00', (select [dbo].[f_GetTimeOfArrival]('2017-05-15 21:15:00', 1)), 1),
		(  'BB-0999', 2, '2017-11-16', '2017-11-16 02:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-16 02:00:00', 2)), 2),
		(  'BB-0999', 2, '2017-11-03', '2017-11-03 02:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-03 02:00:00', 2)), 1),
		(  'BA-0777', 3, '2017-11-10', '2017-11-10 18:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-10 18:00:00', 3)), 3),
		(  'BA-0777', 3, '2017-12-14', '2017-12-14 18:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-12-14 18:00:00', 3)), 1),
		(  'BA-0777', 3, '2017-12-08', '2017-12-08 18:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-12-08 18:00:00', 3)), 1),
		(  'BA-0777', 3, '2017-12-17', '2017-12-17 18:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-12-17 18:00:00', 3)), 2),
		(  'BA-0778', 3, '2017-12-20', '2017-12-20 10:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-12-20 10:00:00', 3)), 3),
		(  'BA-0778', 3, '2017-11-18', '2017-11-10 10:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-18 10:00:00', 3)), 2),
		(  'BA-0778', 3, '2017-11-24', '2017-11-24 10:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-24 10:00:00', 3)), 2),
		(  'B2-0892', 6, '2017-11-30', '2017-11-30 02:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-30 02:00:00', 6)), 1),
		(  'B2-0892', 6, '2017-11-29', '2017-11-29 02:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-29 02:00:00', 6)), 2),
		(  'B2-0892', 6, '2017-11-21', '2017-11-21 02:00:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-21 02:00:00', 6)), 1),
		(  'BB-0441', 4, '2017-11-25', '2017-11-25 16:35:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-25 16:35:00', 4)), 2),
		(  'BB-0441', 4, '2017-11-13', '2017-11-13 16:35:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-13 16:35:00', 4)), 2),
		(  'BB-0441', 4, '2017-11-28', '2017-11-28 16:35:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-28 16:35:00', 4)), 3),
		(  'BB-0441', 4, '2017-11-23', '2017-11-23 16:35:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-23 16:35:00', 4)), 2),
		(  'BB-0441', 4, '2017-11-12', '2017-11-12 16:35:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-12 16:35:00', 4)), 1),
		(  'BB-0441', 4, '2017-11-06', '2017-11-06 16:35:00', (select [dbo].[f_GetTimeOfArrival]('2017-11-06 16:35:00', 4)), 3);
GO


-------------



declare @idf int 

PRINT 'dbo.PlaceInFlight inserting'
go

INSERT INTO dbo.PlaceInFlight (TypePlace, FlightID, Amount)
VALUES  ( 'A', 1, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 1))),
		( 'B', 1, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 1))),
		( 'A', 2, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 2))),
		( 'B', 2, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 2))),
		( 'A', 3, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 3))),
		( 'B', 3, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 3))),
		( 'A', 4, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 4))),
		( 'B', 4, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 4))),
		( 'A', 5, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 5))),
		( 'B', 5, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 5))),
		( 'A', 6, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 6))),
		( 'B', 6, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 6))),
		( 'A', 7, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 7))),
		( 'B', 7, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 7))),
		( 'A', 8, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 8))),
		( 'B', 8, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 8))),
		( 'A', 9, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 9))),
		( 'B', 9, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 9))),
		( 'A', 10, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 10))),
		( 'B', 10, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 10))),
		( 'A', 11, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 11))),
		( 'B', 11, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 11))),
		( 'A', 12, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 12))),
		( 'B', 12, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 12))),
		( 'A', 13, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 13))),
		( 'B', 13, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 13))),
		( 'A', 14, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 14))),
		( 'B', 14, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 14))),
		( 'A', 15, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 15))),
		( 'B', 15, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 15))),
		( 'A', 16, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 16))),
		( 'B', 16, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 16))),
		( 'A', 17, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 17))),
		( 'B', 17, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 17))),
		( 'A', 18, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 18))),
		( 'B', 18, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 18))),
		( 'A', 19, (select [dbo].[f_FlightInsertingHelper_ATO]('A', 19))),
		( 'B', 19, (select [dbo].[f_FlightInsertingHelper_ATO]('B', 19)));
GO

---------------------------------------------
PRINT 'dbo.Ticket inserting'
go
---------------------------------------------

INSERT INTO dbo.Ticket (FlightID, PassengerID, CashierID, RateID, SaleDate, TotalCost)
VALUES
       ( 1, 3, 2, 1, GETDATE(), 150),
       ( 3, 3, 2, 2, GETDATE(), 140),
       ( 3, 4, 1, 2, GETDATE(), 140),
       ( 2, 5, 1, 1, GETDATE(), 170),
       ( 2, 6, 2, 2, GETDATE(), 170),
       ( 5, 2, 3, 1, GETDATE(), 210),
	   ( 6, 4, 1, 1, GETDATE(), 200);
 GO  
 

GO
	   

------------------------------------------------------------------------------------------------------------------------------
PRINT ' '
PRINT 'Finished - ' + CONVERT(varchar, GETDATE(), 121);
GO
------------------------------------------------------------------------------------------------------------------------------