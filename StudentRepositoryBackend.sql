create database StudentRepository


CREATE TABLE tbl_Admin
(
	AdminID int  primary key identity(100,1),
	FirstName varchar(50),
	LastName varchar(50),
	Contact varchar(10) unique,
	Username varchar(50) unique,
	Password varchar(50) not null,
	Email varchar(150) unique,
	
)
 drop table tbl_Admin
insert into tbl_Admin values('Pratik','Baser','8500222701','pbaser','p123','pratik.baser@capgemini.com')

select*from tbl_Admin

select*from tbl_Student
CREATE TABLE tbl_Graduation
(         
  GraduationID int primary key identity(1,1),
  GraduationName varchar(50),
  )


  
CREATE TABLE tbl_Student
(
    StudentID int primary key identity(1000,1),
    FirstName varchar(50),
	LastName varchar(50),
	FatherName varchar(50),
	DateofBirth date,
	GraduationID int CONSTRAINT Fk_Graduation FOREIGN KEY(GraduationID) REFERENCES tbl_Graduation(GraduationID),
	Percentage int,
	Contact varchar(10) ,
	Email varchar(150),
	Password varchar(50),
	City varchar(10)
)


insert into  tbl_Graduation values('Bsc')

insert into  tbl_Graduation values('B-tech')

insert into  tbl_Graduation values('MCA')
Select*from tbl_Graduation

insert into tbl_Student values('AnanthaNadh','Perakam','Subbarao','10-06-1995',1,73,'8328479101','saiananth135@gmail.com','a@123','Hyderabad')

select*from tbl_Student


Create table tbl_Certification(
CertificationID int primary key identity(2000,1),
CertificationName varchar(50),
About varchar(250)
)
insert into tbl_Certification values('Microsoft','Expand your IT skills. Microsoft offers a wide range of online certification programs designed to help grow your skills -- and your career.')
insert into tbl_Certification values('AWS','AWS Certification validates cloud expertise to help professionals highlight in-demand skills and organizations build effective, innovative teams for cloud ...')
Create table tbl_Status(
StatusID int primary key Identity(1,1),
Status varchar(50))

insert into tbl_Status values('Request')
insert into tbl_Status values('Approved')
insert into tbl_Status values('Cancelled')
select*from tbl_Status
Create table tbl_Apply(
ApplyId int primary key identity(1,1),
CertificationID int CONSTRAINT Fk_Ceritification FOREIGN KEY(CertificationID) REFERENCES tbl_Certification(CertificationID),
StudentID int  CONSTRAINT Fk_Student FOREIGN KEY(StudentID) REFERENCES tbl_Student(StudentID),
Comments varchar(250),
StatusID int  CONSTRAINT Fk_Status FOREIGN KEY(StatusID) REFERENCES tbl_Status(StatusID)
)
 

Select *from tbl_Student
Select*from tbl_Graduation
Select*from tbl_Certification
Select*from tbl_Status

insert into tbl_Apply values(2000,1000,'I intrested',1)

select*from tbl_Apply

create table tbl_Locations(
LocationID int primary key Identity(1,1),
LocationName varchar(50))

insert into tbl_Locations values('Mumbai')
insert into tbl_Locations values('Bangalore')
insert into tbl_Locations values('Hyderabad')
insert into tbl_Locations values('Chennai')


Create table Job(
JobID int identity(1,1) primary key not null,
CompanyName varchar(50) NOT NULL,
Details varchar(250) not null,
Designation varchar(50),
LocationID int constraint fk_Location foreign key(LocationID) references tbl_Locations(LocationID),
Number_of_Positions int,
)


insert into Job values('Capgemini','we are hiring 2017/18/19 passed out candidates and Bsc/Btech/MBA/MCA','Analyst','1',10)
create table ApplyJob(
ApplyId int identity(1,1) primary key,
JobID int constraint fk_Job foreign key(JobID) references Job(JobID),
StudentID int constraint fk_Students foreign key(StudentID) references tbl_Student(StudentID),
Skills varchar(50))

insert into ApplyJob values(1,1000,'c,java,c++')

