CREATE DATABASE task_1

CREATE TABLE employees (
ssn  int primary key identity(1,1),
gender varchar(20) not null,
birthdate date ,
fname varchar(20),
lname varchar(20),
hiredate date)

CREATE TABLE dependents (
  dependent_name VARCHAR(100) PRIMARY KEY, 
  emp_id INT NOT NULL,
  gender    VARCHAR(20) NOT NULL,
  birthdate DATE NOT NULL,
  FOREIGN KEY (emp_id)
    REFERENCES employees(ssn)
    ON DELETE CASCADE )


    CREATE TABLE department(
dnum  int primary key ,
dname varchar(50) not null,
location varchar(100),
ssn int not null,
 FOREIGN KEY (ssn)
    REFERENCES employees(ssn))

 
    CREATE TABLE projects(
    pnum int primary key,
    pname varchar(50) not null,
city varchar(50) ,
fname varchar(20),
lname varchar(20),
dnum int not null,
 FOREIGN KEY (dnum)
    REFERENCES department(dnum))


    insert into employees (gender,birthdate,fname,lname,hiredate)
    values
   ('female', '1999-05-05','malak','ahmed','2024-10-10'),
('female', '2005-01-20','margo','emile','2025-01-01'),
('male', '2001-09-29','pter','ibraam','2020-05-05'),
('male', '2003-12-06','hedra','andrew','2021-09-12'),
('male', '1998-10-18','ziad','hazem','2024-05-07');



insert into department(dnum,dname,location, ssn)
VALUES
(1, 'HR', 'Cairo',6),
(2, 'Finance', 'Alexandria', 5),
(3, 'IT', 'Giza', 3)
 

 select*from employees
  select*from department

  insert into dependents( dependent_name,emp_id ,gender, birthdate)
VALUES
('posy',3,'female', '1999-05-05'),
('jojo',4,'male','2001-09-29'),
('joe',6,'male', '2003-12-06')

 select*from dependents

  delete from dependents
  where emp_id =4

  select*from employees
  select*from dependents

  ALTER TABLE employees
ADD e_dnum INT;

  insert into employees (gender,birthdate,fname,lname,hiredate,e_dnum)
    values
   ('female', '1999-05-05','malak','ahmed','2024-10-10',1),
('female', '2005-01-20','margo','emile','2025-01-01',2),
('male', '2001-09-29','pter','ibraam','2020-05-05',3),
('male', '2003-12-06','hedra','andrew','2021-09-12',1),
('male', '1998-10-18','ziad','hazem','2024-05-07',3)

ALTER TABLE employees
DROP COLUMN dnum;

select*from employees

select*from department

SELECT ssn, fname, lname
FROM employees
WHERE e_dnum = (
    SELECT dnum
    FROM department
    WHERE dname = 'IT'
);

  insert into projects( pnum,pname ,city, fname,lname,dnum)
VALUES
(1, 'Website Redesign', 'Cairo', 'Ahmed', 'Ali', 1),
(2, 'Mobile App', 'Alexandria', 'Mona', 'Ali', 2)

select*from projects


CREATE TABLE emp_project (
    ssn INT ,
    pname VARCHAR(50) ,
    working_hrs INT,
    PRIMARY KEY (ssn, pname)
);

insert into emp_project(ssn,pname,working_hrs)
values
(5, 'Website Redesign', 40),
(3, 'Mobile App', 30)


select*from emp_project


select ssn,fname,lname
from employees
where ssn in(select ssn 
from emp_project
where pname='Mobile App')