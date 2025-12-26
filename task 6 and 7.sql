use StoreDB
GO

---ASSIGNMENT (6)13 TO 20  & (7)------

--13--

create proc sp_GetCustomersOrders
    @customer_id INT,
    @start_date DATE = NULL,
    @end_date DATE = NULL
AS
BEGIN

    SELECT

        o.order_id,

        o.order_date,

        o.order_status,

        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) as order_total

    FROM sales.orders o

     JOIN sales.order_items oi ON o.order_id = oi.order_id

    WHERE o.customer_id = @customer_id

        AND (@start_date IS NULL OR o.order_date >= @start_date)

        AND (@end_date IS NULL OR o.order_date <= @end_date)

    GROUP BY o.order_id, o.order_date, o.order_status

    ORDER BY o.order_date DESC;

END

EXEC sp_GetCustomersOrders @customer_id =3

--14--
create proc sp_RestockProducts

    @storeID int , @productID int , @restockQuantity int 
    ,@newQuantity int  output,@oldQuantity int output ,@sucess int output
as
begin
set @sucess=0

select @oldQuantity=o.quantity
from [sales].[order_items] o
join [sales].[orders] s on o.order_id = s.order_id
where o.product_id=@productID  and s.store_id=@storeID

if @oldQuantity is not null
begin
update o  
SET o.quantity = o.quantity + @restockQuantity

from [sales].[order_items] o
join [sales].[orders] s on o.order_id = s.order_id
where o.product_id=@productID  and s.store_id=@storeID
 SET @newQuantity = @oldQuantity + @restockQuantity
        SET @sucess = 1
end
end

DECLARE @newQuantity INT, @oldQuantity INT,@sucess INT;

EXEC sp_RestockProducts 
    2, 5,15,
    @newQuantity OUTPUT,@oldQuantity OUTPUT,@sucess OUTPUT;

SELECT   @oldQuantity AS 'Old Quantity' ,@newQuantity AS 'New Quantity', @sucess AS 'Success Status'

--15--
 CREATE PROC sp_ProcessNewtrans
    @customerID INT,
    @productID INT,
    @quantity INT,
    @storeID INT,
    @staffId INT,
    @orderStatus INT,
    @orderdate DATE,
    @requireddate DATE
AS
BEGIN
  

    BEGIN TRY
        BEGIN TRANSACTION

        INSERT INTO sales.orders (customer_id, store_id, staff_id, order_status, order_date, required_date)
        VALUES (@customerID, @storeID, @staffId, @orderStatus, @orderdate, @requireddate)

        
        DECLARE @orderId INT;
        SET @orderId = SCOPE_IDENTITY();

        INSERT INTO sales.order_items(order_id, quantity, product_id)
        VALUES (@orderId, @quantity, @productID);

        
        update production.stocks
set quantity= quantity - @quantity
where product_id=@productID and store_id=@storeID

commit transaction
print 'transaction  successfully'
  END TRY
    BEGIN CATCH
rollback transaction
print 'error is :'+error_message()
end catch
 end

EXEC sp_ProcessNewtrans 1,2,1,5,1,4,'2025-1-1','2025-1-30'

--16--
CREATE PROCEDURE sp_SearchProductss
    @productname VARCHAR(50) = NULL,
    @categoryID INT = NULL,
    @min DECIMAL(10,2) = NULL,
    @max DECIMAL(10,2) = NULL,
    @sortColumn VARCHAR(50) = 'product_name' 
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX)
    DECLARE @params NVARCHAR(MAX)
    
    SET @sql = N'
        SELECT * 
        FROM production.products 
        WHERE 1=1'
   
    IF @productname IS NOT NULL
        SET @sql = @sql + N' AND product_name LIKE ''%'' + @productname + ''%'''
    
    IF @categoryID IS NOT NULL
        SET @sql = @sql + N' AND category_id = @categoryID'
    
    IF @min IS NOT NULL
        SET @sql = @sql + N' AND list_price >= @min'
    
    IF @max IS NOT NULL
        SET @sql = @sql + N' AND list_price <= @max'
    
    SET @sql = @sql + N' ORDER BY ' + QUOTENAME(@sortColumn)
    
    
    SET @params = N'
        @productname VARCHAR(50),
        @categoryID INT,
        @min DECIMAL(10,2),
        @max DECIMAL(10,2)'
  
    EXEC sp_executesql @sql, @params,
        @productname,
        @categoryID,
        @min,
        @max
END

exec sp_SearchProductss

--17--

DECLARE @LowBonus DECIMAL(10,2) = 0.02
DECLARE @MidBonus DECIMAL(10,2) = 0.05
DECLARE @HighBonus DECIMAL(10,2) = 0.10
DECLARE @StartDate DATE, @EndDate DATE

SET @StartDate = '2025-01-01'
SET @EndDate = '2025-09-30' 

SELECT 
    o.staff_id,
    SUM(s.list_price * s.quantity) AS TotalSales,
    CASE
        WHEN SUM(s.list_price * s.quantity) < 5000
        THEN @LowBonus
        WHEN SUM(s.list_price * s.quantity) BETWEEN 5000 AND 10000 
        THEN @MidBonus
        ELSE @HighBonus
    END AS BonusRate,
     SUM(s.list_price * s.quantity) * 
    CASE
        WHEN SUM(s.list_price * s.quantity) < 5000 
        THEN @LowBonus
        WHEN SUM(s.list_price * s.quantity) BETWEEN 5000 AND 10000 
        THEN @MidBonus
        ELSE @HighBonus
    END AS BonusAmount
FROM Sales.Orders o
JOIN Sales.order_items s ON o.order_id = s.order_id
WHERE o.order_date BETWEEN @StartDate AND @EndDate
GROUP BY o.staff_id
ORDER BY o.staff_id

SELECT *
FROM Sales.Orders o
JOIN Sales.order_items s ON o.order_id = s.order_id

--18--
declare @catId int ,@stock int

select @stock=quantity
from [production].[stocks]

if @stock >=1 
print ' u have products '
else 
print'u do not have '

--19--
create proc sp_loyal @cusId int 
as
begin
select  c.customer_id,
    CASE 
        WHEN SUM(oi.list_price * oi.quantity) IS NULL THEN 'No Orders'
        WHEN SUM(oi.list_price * oi.quantity) >= 2000 THEN 'Premium'
        WHEN SUM(oi.list_price * oi.quantity) >= 1000 THEN 'Gold'
        WHEN SUM(oi.list_price * oi.quantity) >= 500  THEN 'Silver'
        ELSE 'Bronze'
    END AS LoyaltyLevels
FROM sales.customers c
 JOIN sales.orders o ON c.customer_id = o.customer_id
 JOIN sales.order_items oi ON o.order_id = oi.order_id
WHERE c.customer_id = @cusId
GROUP BY c.customer_id
end
exec sp_loyal 7

--20---
create proc sp_handels @ProductID int,
    @ReplacementID INT = NULL,   
    @StatusMessage NVARCHAR(100) OUTPUT
AS
BEGIN
   
    IF @ReplacementID IS NOT NULL
    BEGIN
        UPDATE sales.order_items
        SET product_id = @ReplacementID
        WHERE product_id = @ProductID;
    END

    UPDATE production.stocks
    SET quantity = 0
    WHERE product_id =@ProductID

    SET @StatusMessage = ' inventory cleared';
END

DECLARE @Status VARCHAR(100)

EXEC sp_handels 1, 3, @Status OUTPUT

PRINT @Status

---1--
create NONCLUSTERED  index index_mail on sales.customers (email)

--2--
create index ix_cabr on production.products (category_id,brand_id)

--3--
create  NONCLUSTERED index ix_orders on sales.orders (order_date )
include(customer_id,store_id,  order_status)

--4--
create table customer_log (
log_id int primary key identity(1,1) ,
customer_id int ,
log_date  date default getdate(),
log_mess varchar(100))


create trigger tr_welcome on sales.customers
after insert 
as
begin 
insert into customer_log(customer_id,log_mess)
select customer_id,
  'Welcome new customer'
    from inserted
end

insert into  sales.customers(first_name, last_name, email)
VALUES ('MOHAMMED', 'Hassan', 'ali@test.com')

--5--
create table production.price_history
( history_id int identity(1,1) primary key ,
    product_id int,
    old_price decimal(10,2),
    new_price decimal(10,2),
    change_date datetime default GETDATE())

create trigger tr_price on  production.products
after update 
as
begin
    insert into production.price_history (product_id,old_price,new_price,change_date)
        SELECT d.product_id,d.list_price as'old price',i.list_price as'new price',getdate()
        from deleted d
        inner join inserted i
        on i.product_id=d.product_id
        where d.list_price !=i.list_price
        end

        UPDATE production.products
SET list_price=list_price+90
WHERE product_id = 1

SELECT * FROM production.price_history

--6--
create trigger tr_nodelete
on production.categories
instead of delete
as
begin
if exists (select 1
from deleted d
 join production.products p
   ON d.category_id = p.category_id)

 begin
print 'u cannot delete it'
return
   end

     delete from  production.categories
     where category_id in (select category_id from deleted)
end

delete from production.categories where category_id in  (2,5)
--7--
create trigger tr_quan on sales.order_items
after insert
as
begin
update s 
    set s.quantity = s.quantity - i.quantity
    from production.stocks s
    inner join  sales.order_items i
    on s.product_id = i.product_id
    end

    UPDATE production.stocks
SET  quantity=2
WHERE product_id = 5


--8--
CREATE TABLE sales.order_audit (
    audit_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
    customer_id INT,
    store_id INT,
    staff_id INT,
    order_date DATE,
    audit_timestamp DATETIME DEFAULT GETDATE()
);

create trigger tr_new
on sales.orders 
after insert 
as
begin
insert into sales.order_audit (order_id,store_id ,order_date)
        SELECT i.order_id,i.store_id ,getdate()
        from inserted i
end

insert into sales.orders (order_id,order_status,required_date,store_id,staff_id)
 values (3,4,GETDATE(),1,2)