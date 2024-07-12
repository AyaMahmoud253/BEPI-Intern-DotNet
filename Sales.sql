CREATE DATABASE Sales ;

CREATE TABLE Customers(
    Id INT PRIMARY KEY,
	First_name VARCHAR(50),
	Last_name  VARCHAR(50),
	Email VARCHAR(100),
	Phone_number VARCHAR(20),
	);
CREATE TABLE Suppliers(
       Id INT PRIMARY KEY,
	   Supplier_name VARCHAR(100)
);
-- Creating the Categories table
CREATE TABLE Categories (
    Id INT PRIMARY KEY,
    Category_name VARCHAR(50)
);

-- Creating the Products table
CREATE TABLE Products (
    Id INT PRIMARY KEY,
    Product_name VARCHAR(100),
    Supplier_id INT,
    Category_id INT,
	Price DECIMAL(10,2),
    FOREIGN KEY (Supplier_id) REFERENCES Suppliers(Id),
    FOREIGN KEY (Category_id) REFERENCES Categories(Id)
);
-- Creating the Employees table
CREATE TABLE Employees (
    Id INT PRIMARY KEY,
    First_name VARCHAR(50),
    Last_name VARCHAR(50),
    Reports_to INT,
    FOREIGN KEY (Reports_to) REFERENCES Employees(Id)
);
-- Creating the Orders table
CREATE TABLE Orders (
    Id INT PRIMARY KEY,
    Customer_id INT,
    Employee_id INT,
    Order_date DATE,
    FOREIGN KEY (Customer_id) REFERENCES Customers(Id),
    FOREIGN KEY (Employee_id) REFERENCES Employees(Id)
);
-- Altering the Orders table to add TotalPrice column
ALTER TABLE Orders
ADD TotalPrice DECIMAL(10, 2);

-- Creating the OrderDetails table
CREATE TABLE OrderDetails (
    Order_id INT,
    Product_id INT,
    Quantity INT,
    PRIMARY KEY (Order_id, Product_id),
    FOREIGN KEY (Order_id) REFERENCES Orders(Id),
    FOREIGN KEY (Product_id) REFERENCES Products(Id)
);
-- Creating the Shippers table
CREATE TABLE Shippers (
    Id INT PRIMARY KEY,
    Shipper_name VARCHAR(100) NOT NULL,
    Phone_number VARCHAR(20)
);
ALTER TABLE Orders
ADD Shipper_id INT;

ALTER TABLE Orders
ADD FOREIGN KEY (Shipper_id) REFERENCES Shippers(Id);

-- Inserting sample data (optional)
INSERT INTO Customers (Id, First_name, Last_name, Email, Phone_number)
VALUES (1, 'John', 'Doe', 'john.doe@example.com', '123-456-7890');

INSERT INTO Suppliers (Id, Supplier_name)
VALUES (1, 'Supplier A');

INSERT INTO Categories (Id, Category_name)
VALUES (1, 'Category A');

INSERT INTO Products (Id, Product_name, Supplier_id, Category_id)
VALUES (1, 'Product A', 1, 1);

INSERT INTO Employees (Id, First_name, Last_name, Reports_to)
VALUES (1, 'Jane', 'Smith',  NULL);

INSERT INTO Shippers (Id, Shipper_name, Phone_number)
VALUES (1, 'Shipper A', '987-654-3210');

INSERT INTO Orders (Id, Customer_id, Employee_id, Order_date, Shipper_id, TotalPrice)
VALUES (1, 1, 1, '2024-07-01', 1, 100.00);

INSERT INTO OrderDetails (Order_id, Product_id, Quantity)
VALUES (1, 1, 10);

select * from Customers;
--1
CREATE VIEW Users AS
SELECT Id, First_name, Last_name, 1 AS IsActive, 'Customer' AS UserType
FROM Customers
UNION
SELECT Id, First_name, Last_name, 1 AS IsActive, 'Employee' AS UserType
FROM Employees
UNION
SELECT Id, Supplier_name AS First_name, '' AS Last_name, 1 AS IsActive, 'Supplier' AS UserType
FROM Suppliers;
SELECT * FROM Users;
--2
CREATE FUNCTION GetFullName (@First_name VARCHAR(50), @Last_name VARCHAR(50))
RETURNS VARCHAR(100)
AS
BEGIN
    RETURN @First_name + ' ' + @Last_name;
END;
SELECT dbo.GetFullName('John', 'Doe') AS FullName;

--3
CREATE PROCEDURE GetUsersGrid
    @PageNumber INT,
    @PageSize INT,
    @SearchText VARCHAR(100)
AS
BEGIN
    DECLARE @StartRow INT = (@PageNumber - 1) * @PageSize + 1;
    DECLARE @EndRow INT = @PageNumber * @PageSize;

    WITH UsersCTE AS (
        SELECT 
            Id, 
            dbo.GetFullName(First_name, Last_name) AS FullName,
            IsActive, 
            UserType,
            ROW_NUMBER() OVER (ORDER BY First_name ASC) AS RowNum
        FROM Users
        WHERE First_name LIKE '%' + @SearchText + '%' OR Last_name LIKE '%' + @SearchText + '%'
    )
    SELECT Id, FullName, IsActive, UserType
    FROM UsersCTE
    WHERE RowNum BETWEEN @StartRow AND @EndRow;
END;
EXEC GetUsersGrid @PageNumber = 1, @PageSize = 10, @SearchText = 'Jane';

--4
ALTER TABLE Customers
ADD IsActive BIT DEFAULT 1;
CREATE TRIGGER trg_PreventDeleteActiveCustomer
ON Customers
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT * FROM deleted WHERE IsActive = 1)
    BEGIN
        RAISERROR('Cannot delete active customer.', 16, 1);
        ROLLBACK;
    END
    ELSE
    BEGIN
        DELETE FROM Customers
        WHERE Id IN (SELECT Id FROM deleted);
    END
END;
INSERT INTO Customers (Id, First_name, Last_name, Email, Phone_number, IsActive)
VALUES (2, 'Alice', 'Wonderland', 'alice@example.com', '123-456-7891', 1);
UPDATE Customers SET IsActive = 0 WHERE Id = 2;
DELETE FROM Customers WHERE Id = 2;

--5
CREATE PROCEDURE DynamicInsert
    @TableName NVARCHAR(128),
    @ColumnNames NVARCHAR(MAX),
    @Values NVARCHAR(MAX)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = 'INSERT INTO ' + @TableName + ' (' + @ColumnNames + ') VALUES (' + @Values + ')';
    EXEC sp_executesql @SQL;
END;
-- Insert into Customers table
EXEC DynamicInsert 
    @TableName = 'Customers', 
    @ColumnNames = 'Id, First_name, Last_name, Email, Phone_number', 
    @Values = '2, ''Alice'', ''Wonderland'', ''alice@example.com'', ''123-456-7891''';

--6
CREATE PROCEDURE GetCustomerTotalPrice
    @CustomerId INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT SUM(TotalPrice) AS TotalPrice
    FROM Orders
    WHERE Customer_id = @CustomerId AND Order_date BETWEEN @StartDate AND @EndDate;
END;
EXEC GetCustomerTotalPrice @CustomerId = 1, @StartDate = '2024-07-01', @EndDate = '2024-07-31';

--7
CREATE VIEW CategoriesWithProductCount AS
SELECT 
    c.Id, 
    c.Category_name, 
    COUNT(p.Id) AS ProductCount
FROM 
    Categories c
LEFT JOIN 
    Products p ON c.Id = p.Category_id
GROUP BY 
    c.Id, c.Category_name;
SELECT * FROM CategoriesWithProductCount;

--8
CREATE FUNCTION GetCustomerOrders (@CustomerId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Orders
    WHERE Customer_id = @CustomerId
);
SELECT * FROM dbo.GetCustomerOrders(1);

--9
CREATE TABLE Orders_New
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Customer_id INT,
    Employee_id INT,
    Order_date DATE,
    Shipper_id INT,
    TotalPrice DECIMAL(10, 2),
    FOREIGN KEY (Customer_id) REFERENCES Customers(Id),
    FOREIGN KEY (Employee_id) REFERENCES Employees(Id),
    FOREIGN KEY (Shipper_id) REFERENCES Shippers(Id)
);
INSERT INTO Orders_New (Customer_id, Employee_id, Order_date, Shipper_id, TotalPrice)
SELECT Customer_id, Employee_id, Order_date, Shipper_id, TotalPrice
FROM Orders;
INSERT INTO Orders_New (Customer_id, Employee_id, Order_date, Shipper_id, TotalPrice)
SELECT Customer_id, Employee_id, Order_date, Shipper_id, TotalPrice
FROM Orders;
ALTER TABLE OrderDetails DROP CONSTRAINT FK__OrderDeta__Order__440B1D61;
EXEC sp_rename 'Orders', 'Orders_Old';
EXEC sp_rename 'Orders_New', 'Orders';
ALTER TABLE OrderDetails
ADD CONSTRAINT FK__OrderDeta__Order__440B1D61 FOREIGN KEY (Order_id) REFERENCES Orders(Id);


CREATE PROCEDURE InsertOrderWithDetails
    @CustomerId INT,
    @EmployeeId INT,
    @OrderDate DATE,
    @ShipperId INT,
    @TotalPrice DECIMAL(10, 2),
    @OrderDetails OrderDetailsType READONLY
AS
BEGIN
    DECLARE @OrderId INT;

    BEGIN TRANSACTION;

    -- Insert into Orders table
    INSERT INTO Orders (Customer_id, Employee_id, Order_date, Shipper_id, TotalPrice)
    VALUES (@CustomerId, @EmployeeId, @OrderDate, @ShipperId, @TotalPrice);

    -- Retrieve the generated Order ID
    SET @OrderId = SCOPE_IDENTITY();

    -- Insert into OrderDetails table
    INSERT INTO OrderDetails (Order_id, Product_id, Quantity)
    SELECT @OrderId, Product_id, Quantity
    FROM @OrderDetails;

    COMMIT TRANSACTION;

    -- Return all orders with their details
    SELECT 
        o.Id AS OrderId,
        o.Customer_id,
        o.Employee_id,
        o.Order_date,
        o.Shipper_id,
        o.TotalPrice,
        od.Product_id,
        od.Quantity
    FROM 
        Orders o
    JOIN 
        OrderDetails od ON o.Id = od.Order_id
    ORDER BY o.Id;
END;
DECLARE @OrderDetails OrderDetailsType;

-- Insert sample data into the table variable
--INSERT INTO @OrderDetails (Product_id, Quantity)
--VALUES (1, 5), (2, 3);

-- Execute the stored procedure
EXEC InsertOrderWithDetails 
    @CustomerId = 1, 
    @EmployeeId = 1, 
    @OrderDate = '2024-07-11', 
    @ShipperId = 1, 
    @TotalPrice = 150.00, 
    @OrderDetails = @OrderDetails;
