USE TOPBRAINS;
GO


CREATE TABLE SalesWF
(
    ProductId  INT,
    SaleMonth  DATE,
    Amount     DECIMAL(10,2)
);

INSERT INTO SalesWF (ProductId, SaleMonth, Amount)
VALUES
(1, '2024-01-01', 1000),
(1, '2024-02-01', 1500),
(1, '2024-03-01', 1200),
(1, '2024-04-01', 1800),

(2, '2024-01-01', 2000),
(2, '2024-02-01', 2200),
(2, '2024-03-01', 2100);

SELECT
    ProductId,
    SaleMonth,
    Amount,
    SUM(Amount) OVER (
        PARTITION BY ProductId
        ORDER BY SaleMonth
        ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
    ) AS CumulativeSales
FROM SalesWF
ORDER BY ProductId, SaleMonth;





