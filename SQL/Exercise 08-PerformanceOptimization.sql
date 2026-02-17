use TOPBRAINS;



select * from orders;


SELECT *
FROM Orders
WHERE customer_id = 1254
  AND order_date > '2024-01-01';

CREATE NONCLUSTERED INDEX IX_Orders_CustomerId_OrderDate
ON Orders (customer_id, order_date);


--I would create a nonclustered composite index on (CustomerId, OrderDate).
--CustomerId is used with an equality condition, so it should be the leading column.
--OrderDate is a range filter, so it comes next.
--This allows SQL Server to perform an index seek instead of a table scan.