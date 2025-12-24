use StoreDB
GO

--1--
declare @total_AMOUNT int
declare @id int 
set @id = ( select [customer_id] 
from [sales].[customers]
where [customer_id]=1)

set @total_AMOUNT =(select COUNT(*) From  [production].[products])
print @total_AMOUNT
print @id
if @total_AMOUNT >5000
begin 
print 'VIP CUSTOMER'
end
else
print 'REGULAR CUSTOMER'


--2--
declare @price int =1500
declare @product int 
SELECT  @product = count(*)
FROM [production].[products]
WHERE list_price > @price

print 'price = ' + cast( @price AS VARCHAR)
print 'product_id = '+cast( @product AS VARCHAR)

--3--!

declare @total_sales int
declare @staff int =2
declare @date date='2017-01-01'
(select @total_sales =count (*)
from [sales].[orders]
where[staff_id] = @staff
and[order_date] =@date)

print @total_sales
print @staff
print @date

--4--
select @@SERVERNAME
select @@VERSION
select @@ROWCOUNT

--5--

declare @quantity int =19

(select @quantity =  [quantity]
from [production].[stocks]
where [store_id]=1 and [product_id]=1)

if @quantity > 20
begin
print 'Well stocked'
end
else if @quantity >= 10 AND @quantity <= 20
begin
print 'Moderate  stocked'
end
else
print 'reorder needed'

--6--
declare @quantity_upd int

select @quantity_upd=count (*)
from [production].[stocks]
where [quantity]<5

while @quantity_upd >0
begin
update top (3)  [production].[stocks]
set quantity=quantity+10
where quantity <5

 print '3 products updated'

 select @quantity_upd=count (*)
from [production].[stocks]
where [quantity]<5
 end

 --7--
select [list_price],
CASE 
 when list_price<300
 then 'LOW BUDGET'
  when list_price between 300 and 800
 then 'MID RANGE'
  when list_price between 801 and 2000
 then 'PREMIUM'
  when list_price>2000
 then 'LUXURY'
 end as'product price'
 from [production].[products]
 ORDER BY list_price

 --8-
 declare @cus_id int=5
 declare  @order_count int 

 select @order_count = count(*)
from sales.orders
where customer_id = @cus_id

if @order_count >0
begin 
select [customer_id], count(*) as'ordercount'
from [sales].[orders]
where customer_id=5
group by [customer_id]
end 
else
print 'there is no orders'

--9--
CREATE FUNCTION CalculateeShipping (@OrderTotal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @ShippingCost DECIMAL(10,2)

    IF @OrderTotal > 100
        SET @ShippingCost = 0
    ELSE IF @OrderTotal >= 50 AND @OrderTotal <= 99
        SET @ShippingCost = 5.99
    ELSE
        SET @ShippingCost = 12.99

    RETURN @ShippingCost
END

select [dbo].[CalculateeShipping](120) AS ShippingCost
select[dbo].[CalculateeShipping]  (75) AS ShippingCost   
select[dbo].[CalculateeShipping](30) AS ShippingCost   

--10--
create function GetProductsByPriceRange(@MinPrice int , @MaxPrice int)
returns table
as
return(
select p.product_name,p.list_price,  b.brand_name, c.category_name
from [production].[products] p
join [production].[brands] b on p.brand_id = b.brand_id
join  [production].[categories] c on p.category_id = c.category_id
where  p.list_price between @minPrice and @maxPrice)

select * from [dbo].[GetProductsByPriceRange](100,600)

--11--
create function GetCustomerYearlySummary(@CustId int )
returns @YearlySummary table (SalesYear int ,TotaolOrders int ,TotalSpent decimal(10,2)  , Avgorders decimal(10,2) )
as
begin
insert into @YearlySummary
select
YEAR(o.order_date) AS SalesYear,
        count(*) AS TotalOrders,
        sum(s.list_price*s.quantity) AS TotalSpent,
        avg(s.list_price*s.quantity) AS AverageOrders
    FROM Sales.Orders o
    join [sales].[order_items] s on o.order_id=s.order_id
    where customer_id=@CustId
    group by YEAR(o.order_date)
    order by SalesYear
    return 
    end

    select * from dbo.GetCustomerYearlySummary(3)
  
  --12--
  CREATE FUNCTION CalculateBulkDiscount (@quantity int)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @discount DECIMAL(10,2)
    if @quantity between 1 and 2
        set @discount = 0
        else if @quantity between 3 and 5
        set @discount =0.5
        else if @quantity between 6 and 9
        set @discount =0.10
        else if  @quantity >=10
        set @discount =0.15
        return @discount
        end

 select [dbo].[CalculateBulkDiscount](4) as 'quantity'
        
     --17--
 DECLARE @StartDate DATE, @EndDate DATE
DECLARE @LowBonus DECIMAL(10,2) = 0.02
DECLARE @MidBonus DECIMAL(10,2) = 0.05
DECLARE @HighBonus DECIMAL(10,2) = 0.10

SET @StartDate = '2025-01-01'
SET @EndDate = '2025-09-31'

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


