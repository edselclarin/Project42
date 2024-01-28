USE ChoreBoard;

-- Create Users table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
);

-- Create Chores table
CREATE TABLE Chores (
    ChoreId INT PRIMARY KEY IDENTITY(1,1),
    ChoreName NVARCHAR(100) NOT NULL,
    ChoreDescription NVARCHAR(255) NOT NULL,
);

-- Create ChoreAssignments table
CREATE TABLE ChoreAssignments (
    AssignmentId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    ChoreId INT FOREIGN KEY REFERENCES Chores(ChoreId),
    StartDate DATE NOT NULL,
    DurationDays INT NOT NULL,
    Frequency NVARCHAR(20) CHECK (Frequency IN ('daily', 'weekly', 'bi-weekly', 'monthly', 'quarterly', 'bi-yearly', 'yearly')),
    );

-- Example of adding a constraint for unique assignments per user and chore
ALTER TABLE ChoreAssignments
ADD CONSTRAINT UC_UserChore UNIQUE (UserId, ChoreId);


CREATE VIEW ChoreAssignmentsForWeek AS
SELECT
    ca.AssignmentId,
    u.UserId,
    u.UserName,
    u.FirstName,
    u.LastName,
    c.ChoreId,
    c.ChoreName,
    c.ChoreDescription,
    ca.StartDate,
    ca.DurationDays,
    ca.Frequency,
    DATEADD(DAY, ca.DurationDays - 1, ca.StartDate) AS EndDate
FROM
    ChoreAssignments ca
JOIN Users u ON ca.UserId = u.UserId
JOIN Chores c ON ca.ChoreId = c.ChoreId
WHERE
    ca.StartDate <= GETDATE() AND DATEADD(DAY, ca.DurationDays - 1, ca.StartDate) >= GETDATE();



SELECT *
FROM ChoreAssignmentsForWeek

