USE TOPBRAINS;
GO
CREATE TABLE USERS
(
    USER_ID INT PRIMARY KEY,
    USER_NAME VARCHAR(50),
    EMAIL VARCHAR(100)
);
GO

INSERT INTO USERS (USER_ID, USER_NAME, EMAIL) VALUES
(1, 'Amit',  'amit@gmail.com'),
(2, 'Riya',  'riya@gmail.com'),
(3, 'Karan', 'amit@gmail.com'),
(4, 'Sneha', 'sneha@gmail.com'),
(5, 'Rahul', 'riya@gmail.com'),
(6, 'Neha',  'neha@gmail.com');
GO

SELECT EMAIL, COUNT(*) AS DUPLICATE_COUNT
FROM USERS
GROUP BY EMAIL
HAVING COUNT(*) > 1;
