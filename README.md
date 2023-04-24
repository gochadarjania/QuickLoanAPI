# QuickLoanAPI

* ბაზის და ცხრილების შესაქმნელი კოდი
    
<pre>
create database QuickLoan;
go
use [QuickLoan]
create table Users (
    Id int primary key identity(1,1),
	Email nvarchar(50) NOT NULL,
	Password nvarchar(255) NOT NULL,
	UserType int not null
);
go
create table UserInfos (
    Id int primary key,
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,
    PersonalNumber nvarchar(50) NOT NULL,
    DateOfBirth datetime NOT NULL,   
    CreatedAt datetime2 NOT NULL DEFAULT getdate(),
    UpdatedAt datetime2 NOT NULL DEFAULT getdate(),
    CONSTRAINT FK_UserInfos_Users foreign key (Id) REFERENCES Users(Id)
);
go
create table LoanRequests (
    Id int primary key identity(1,1),
    Period INT NOT NULL,
    Money DECIMAL(18,2) NOT NULL,
    RequestedAt datetime2 NOT NULL DEFAULT getdate(),
    UpdatedAt datetime2 NOT NULL DEFAULT getdate(),
    UserId int NOT NULL,
    LoanTypeId int NOT NULL,
    CurrencyTypeId INT NOT NULL,
    StatusId INT NOT NULL,
    foreign key (UserId) references Users(Id)
);
go
	create table Logs (
    Id bigint primary key identity(1,1),
	LogLevel nvarchar(50) NULL,
	Trace nvarchar(max) NULL,
	Message nvarchar(max) NULL,
	RequestData nvarchar(max) NULL,
	RequestHeader nvarchar(max) NULL,
	Path nvarchar(max) NULL,
	HttpStatusCode nvarchar(16) NULL,
	Created datetime NULL
)

go
INSERT INTO Users(Email, Password, UserType)
VALUES
(N'admin@test.com', N'BjG53MMBi/H0gAKJr+r3/swkYGFk3ZFS97eGSLEDvHBNANKm', 1),
(N'customer@test.com', N'BjG53MMBi/H0gAKJr+r3/swkYGFk3ZFS97eGSLEDvHBNANKm', 2),
(N'customer2@test.com', N'BjG53MMBi/H0gAKJr+r3/swkYGFk3ZFS97eGSLEDvHBNANKm', 2); 
--password Abc123.
go
INSERT INTO LoanRequests(Period, Money, LoanTypeId, CurrencyTypeId, StatusId, UserId)
VALUES
(10, 2500, 1, 2, 1, 2),
(25, 10000, 3, 1, 3, 2),
(35, 25000, 2, 1, 4, 2),
(25, 10000, 3, 1, 3, 2),
(25, 10000, 3, 1, 3, 2),
(15, 8500, 2, 2, 2, 2),
(25, 10000, 3, 1, 3, 2),
(15, 8500, 2, 2, 2, 2),
(25, 10000, 3, 1, 3, 2),
(35, 25000, 2, 1, 4, 2),
(25, 10000, 3, 1, 3, 2),
(15, 8500, 2, 2, 2, 2),
(25, 10000, 3, 1, 3, 3),
(35, 25000, 2, 1, 4, 3),
(25, 10000, 3, 1, 3, 3),
(15, 8500, 2, 2, 2, 3),
(25, 10000, 3, 1, 3, 3),
(25, 10000, 3, 1, 3, 3),
(15, 8500, 2, 2, 2, 3),
(25, 10000, 3, 1, 3, 3),
(35, 25000, 2, 1, 4, 3),
(25, 10000, 3, 1, 3, 3),
(15, 8500, 2, 2, 2, 3),
(35, 25000, 2, 1, 4, 3),
(25, 10000, 3, 1, 3, 3),
(35, 25000, 2, 1, 4, 3),
(12, 22500, 1, 1, 1, 3),
(12, 22500, 1, 1, 1, 3),
(24, 21500, 1, 2, 1, 3); 

</pre>
