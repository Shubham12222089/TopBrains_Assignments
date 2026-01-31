use Capgemini;
CREATE TABLE Sales_Raw
(
    OrderID INT,
    OrderDate VARCHAR(20),
    CustomerName VARCHAR(100),
    CustomerPhone VARCHAR(20),
    CustomerCity VARCHAR(50),
    ProductNames VARCHAR(200),   
    Quantities VARCHAR(100),     
    UnitPrices VARCHAR(100),     
    SalesPerson VARCHAR(100)
);

INSERT INTO Sales_Raw VALUES
(101, '2024-01-05', 'Ravi Kumar', '9876543210', 'Chennai', 'Laptop,Mouse', '1,2', '55000,500', 'Anitha'),
(102, '2024-01-06', 'Priya Sharma', '9123456789', 'Bangalore','Keyboard,Mouse', '1,1', '1500,500', 'Anitha'),
(103, '2024-01-10', 'Ravi Kumar', '9876543210', 'Chennai','Laptop', '1', '54000', 'Suresh'),
(104, '2024-02-01', 'John Peter', '9988776655', 'Hyderabad','Monitor,Mouse', '1,1', '12000,500', 'Anitha'),
(105, '2024-02-10', 'Priya Sharma', '9123456789', 'Bangalore','Laptop,Keyboard', '1,1', '56000,1500', 'Suresh');

select * from Sales_Raw;

create table Customers(
    CustomerID int identity primary key,
    CustomerName varchar(100),
    CustomerPhone varchar(20),
    CustomerCity varchar(50)
);

create table SalesPersons(
    SalesPersonID int identity primary key,
    SalesPersonName varchar(100)
);

create table orders(
    OrderID INT PRIMARY KEY,
    OrderDate DATE,
    CustomerID INT,
    SalesPersonID INT,
    foreign key (CustomerID) references Customers(CustomerID),
    foreign key (SalesPersonID) references SalesPersons(SalesPersonID)
);

create table Products(
    ProductID int identity primary key,
    ProductName varchar(100),
    UnitPrice Numeric(10,2)
);

create table OrderDetails(
    OrderID INT,
    ProductID INT,
    Quantity INT,
    primary key (OrderID, ProductID),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

select name from sys.tables;

-- question 2
WITH OrderTotals AS (
    SELECT OrderID,
           SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) AS TotalSales
    FROM Sales_Raw
    CROSS APPLY STRING_SPLIT(Quantities, ',') q
    CROSS APPLY STRING_SPLIT(UnitPrices, ',') p
    GROUP BY OrderID
)
SELECT OrderID, TotalSales
FROM (
    SELECT OrderID, TotalSales,
           DENSE_RANK() OVER (ORDER BY TotalSales DESC) AS rnk
    FROM OrderTotals
) t
WHERE rnk = 3;

--question 3
SELECT SalesPerson,
       SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) AS TotalSales
FROM Sales_Raw
CROSS APPLY STRING_SPLIT(Quantities, ',') q
CROSS APPLY STRING_SPLIT(UnitPrices, ',') p
GROUP BY SalesPerson
HAVING SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) > 60000;

--question 4

SELECT CustomerName,
       SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) AS TotalSpent
FROM Sales_Raw
CROSS APPLY STRING_SPLIT(Quantities, ',') q
CROSS APPLY STRING_SPLIT(UnitPrices, ',') p
GROUP BY CustomerName
HAVING SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) >
(
    SELECT AVG(TotalSpent)
    FROM (
        SELECT SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) AS TotalSpent
        FROM Sales_Raw
        CROSS APPLY STRING_SPLIT(Quantities, ',') q
        CROSS APPLY STRING_SPLIT(UnitPrices, ',') p
        GROUP BY CustomerName
    ) x
);

-- question 5
SELECT UPPER(CustomerName) AS CustomerName,
    MONTH(OrderDate) AS OrderMonth,
    OrderDate
FROM Sales_Raw
WHERE OrderDate >= '2026-01-01'
  AND OrderDate <  '2026-02-01';
