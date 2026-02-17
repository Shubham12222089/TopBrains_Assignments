USE TOPBRAINS;
GO

CREATE TABLE Attendance
(
    EmpId INT,
    [Month] VARCHAR(10),
    TotalPresent INT
);

INSERT INTO Attendance (EmpId, [Month], TotalPresent)
VALUES
(101, 'Jan', 22),
(101, 'Feb', 20),
(101, 'Mar', 23),
(101, 'Apr', 21),

(102, 'Jan', 21),
(102, 'Feb', 22),
(102, 'Mar', 20),
(102, 'Apr', 22);

SELECT EmpId, [Jan], [Feb], [Mar], [Apr]
FROM
(
    SELECT EmpId, [Month], TotalPresent
    FROM Attendance
) AS SourceTable
PIVOT( SUM(TotalPresent) FOR [Month] IN ([Jan], [Feb], [Mar], [Apr]) ) AS PivotTable;
