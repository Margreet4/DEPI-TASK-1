
CREATE TABLE Department (
    dnum INT PRIMARY KEY,
    dname VARCHAR(50) NOT NULL UNIQUE,
    manager_id INT NULL,
    CONSTRAINT chk_dname CHECK (dname <> ''))


CREATE TABLE Employees (
    ssn INT PRIMARY KEY,
    fname VARCHAR(50) NOT NULL,
    lname VARCHAR(50) NOT NULL,
    gender CHAR(1) NOT NULL CHECK (gender IN ('M','F')),
    dnum INT NOT NULL,
    CONSTRAINT fk_emp_dept FOREIGN KEY (dnum)
        REFERENCES Department(dnum)
        ON DELETE NO ACTION
        ON UPDATE CASCADE)


ALTER TABLE Department
ADD CONSTRAINT fk_dept_manager FOREIGN KEY (manager_id)
REFERENCES Employees(ssn)
ON DELETE NO ACTION
ON UPDATE NO ACTION


CREATE TABLE Projects (
    pnum INT PRIMARY KEY,
    pname VARCHAR(50) NOT NULL UNIQUE,
    city VARCHAR(50),
    dnum INT NOT NULL,
    CONSTRAINT fk_proj_dept FOREIGN KEY (dnum)
        REFERENCES Department(dnum)
        ON DELETE CASCADE
        ON UPDATE CASCADE)
        -----
CREATE TABLE Emp_Project (
    ssn INT NOT NULL,
    pnum INT NOT NULL,
    working_hrs INT DEFAULT 0,
    PRIMARY KEY (ssn, pnum),
    FOREIGN KEY (ssn) REFERENCES Employees(ssn) ,
    FOREIGN KEY (pnum) REFERENCES Projects(pnum) ON UPDATE CASCADE)


CREATE TABLE Dependents (
    dependent_name VARCHAR(50) NOT NULL,
    ssn INT NOT NULL,
    gender CHAR(1) NOT NULL CHECK (gender IN ('M','F')),
    birthdate DATE ,
    PRIMARY KEY (dependent_name, ssn),
    CONSTRAINT fk_dep_emp FOREIGN KEY (ssn)
        REFERENCES Employees(ssn)
        ON DELETE CASCADE
        ON UPDATE CASCADE)

ALTER TABLE Employees
ADD birth_year INT NULL

ALTER TABLE Employees
ADD CONSTRAINT chk_birth_year CHECK (birth_year >= 1900)

ALTER TABLE Employees
DROP CONSTRAINT chk_birth_year


INSERT INTO Department (dnum, dname) 
VALUES (1, 'IT'), 
(2, 'HR')

INSERT INTO Employees (ssn, fname, lname, gender, dnum)
VALUES
(101, 'Alice', 'wael', 'F', 1),
(102, 'Bob', 'Jony', 'M', 1),
(103, 'Carol', 'lolo', 'F', 2)

UPDATE Department SET manager_id = 101 WHERE dnum = 1
UPDATE Department SET manager_id = 103 WHERE dnum = 2

INSERT INTO Projects (pnum, pname, city, dnum)
VALUES
(1001, 'Website', 'Cairo', 1),
(1002, 'Recruitment', 'Giza', 2)

INSERT INTO Emp_Project (ssn, pnum, working_hrs) 
VALUES
(101, 1001, 40),
(102, 1001, 20),
(103, 1002, 35)

INSERT INTO Dependents (dependent_name, ssn, gender, birthdate)
VALUES
('Tom', 101, 'M', '2010-05-12'),
('Lily', 103, 'F', '2012-08-20')


select*from Employees

select*from Department

select*from Emp_Project

select*from Dependents

select*from Projects