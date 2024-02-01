USE ChoreBoard;

-- Create Users table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
);

INSERT INTO Users
	(UserName, FirstName, LastName)
VALUES
	('eaclarin', 'Edsel', 'Clarin'),
	('emclarin', 'Eileen', 'Clarin'),
	('ceclarin', 'Carina', 'Clarin'),
	('niclarin', 'Nia', 'Clarin')

-- Create Chores table
CREATE TABLE Chores (
    ChoreId INT PRIMARY KEY IDENTITY(1,1),
    ChoreName NVARCHAR(100) NOT NULL,
    ChoreDescription NVARCHAR(255) NOT NULL,
);

INSERT INTO Chores 
	(ChoreName, ChoreDescription)
VALUES 
	('Wash Dishes', 'Collect and wash all dishes, glasses, silverware, etc.'),
	('Vaccum', 'Vaccum the loft, stairs and downstairs.'),
	('Clean Bedroom', 'Clear everything from the floor and vaccum.'),
	('Pee Pad', 'Change Milo''s pee pad')

-- Create ChoreAssignments table
CREATE TABLE ChoreAssignments (
    AssignmentId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    ChoreId INT FOREIGN KEY REFERENCES Chores(ChoreId),
    DayOfTheWeek INT NOT NULL,
    DurationDays INT NOT NULL,
	WeekParity INT NOT NULL
    );

INSERT INTO ChoreAssignments 
	(UserId, ChoreId, DayOfTheWeek, DurationDays, WeekParity)
VALUES 
	(1,	1, 1, 7, 1),
	(2,	1, 1, 7, 0),
	(1,	2, 1, 7, 0),
	(2,	2, 1, 7, 1),
	(1,	3, 1, 7, -1),
	(2,	3, 1, 7, -1),
	(1,	4, 1, 3, -1),
	(2,	4, 5, 3, -1),
	(3, 4, 4, 1, -1)

-- Example of adding a constraint for unique assignments per user and chore
ALTER TABLE ChoreAssignments
ADD CONSTRAINT UC_UserChore UNIQUE (UserId, ChoreId);


CREATE VIEW ChoreAssignmentsForTheWeek AS
SELECT
    CA.AssignmentId,
    CA.DayOfTheWeek,
    CA.DurationDays,
	CA.WeekParity,
	U.UserId,
    U.UserName,
    U.FirstName,
    U.LastName,
	C.ChoreId,
    C.ChoreName,
    C.ChoreDescription,
	CAST(DATEADD(DAY, -(DATEPART(WEEKDAY, GETDATE()) - 1), GETDATE()) AS DATE) AS FirstDayOfTheWeek,
	CAST(DATEADD(DAY, CA.DayOfTheWeek, DATEADD(DAY, -(DATEPART(WEEKDAY, GETDATE())), GETDATE())) AS DATE) AS StartDate,
	CAST(DATEADD(DAY, (CA.DayOfTheWeek + CA.DurationDays) - 1, DATEADD(DAY, -(DATEPART(WEEKDAY, GETDATE())), GETDATE())) AS DATE) AS EndDate
FROM
    ChoreAssignments CA
JOIN
    Users U ON CA.UserId = U.UserId
JOIN
    Chores C ON CA.ChoreId = C.ChoreId
WHERE
	CA.WeekParity = -1 OR ( DATEPART(WEEK, CAST(DATEADD(DAY, CA.DayOfTheWeek, DATEADD(DAY, -(DATEPART(WEEKDAY, GETDATE())), GETDATE())) AS DATE)) % 2 ) = CA.WeekParity

SELECT * FROM ChoreAssignmentsForTheWeek

