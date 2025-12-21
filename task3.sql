USE StoreDB;
GO
--1--
SELECT COUNT(*) AS total_products
FROM production.products

--2--
select avg([list_price]) as 'avg', MIN(list_price) as 'min',MAX(list_price) as 'max', SUM(list_price) as'sum'
from[production].[products]

--3--
select COUNT([category_id]) as'category product'
from[production].[products] 
group by category_id

--4--
select COUNT(*) as'total orders'
from [sales].[orders]
--5--
select top 10  UPPER([first_name])as'firstName',LOWER([last_name])as'lastName'
from[sales].[customers]
--6--
select top 10 [product_name],  LEN([product_name])as'length'
from [production].[products]
--7--
select [customer_id] ,SUBSTRING([phone],1,3)
from [sales].[customers]
where [customer_id]=1 or [customer_id]=15