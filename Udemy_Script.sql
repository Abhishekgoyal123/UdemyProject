create database Udemy
use Udemy;
create table Users(

UserId int Primary key identity,
FistName varchar(20),
LastName varchar(20),
UserName varchar(20),
UserPassword varchar(50)
);

create table CourseTrainer(

CourseId int primary key identity,
CourseName varchar(20),
CourseDescription varchar(100),
CourseLevels varchar(20),
CourseLanguage varchar(20),
CourseSkills varchar(20),
CousrePrice int,
CourseThumbNail image
);


create table CourseUser(

CourseId int references CourseTrainer(CourseId),
CourseReviews varchar(20),
CourseRatings int,
CourseComments varchar(100)
);


create table CourseMapping(
UserId int references Users(UserId),
CourseId int references CourseTrainer(CourseId)
);


create table Roles(
RoleId int Primary key identity,
RoleName varchar(20)

);

create table RoleMapping(
UserId int references Users(UserId),
RoleId int references Roles(RoleId)
);

alter table RoleMapping add RoleMappingId int identity

alter table CourseMapping add CourseMappingId int identity

alter table CourseUser add CourseUserId int Identity
select * from RoleMapping
select * from CourseMapping
select * from CourseUser

