USE [master]
GO
/****** Object:  Database [SUMSDB]    Script Date: 2/12/2017 11:43:51 PM ******/
CREATE DATABASE [SUMSDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UniversityCourseAndResultManagementSystemDB_Inception', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.JESHADKHAN\MSSQL\DATA\UniversityCourseAndResultManagementSystemDB_Inception.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UniversityCourseAndResultManagementSystemDB_Inception_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.JESHADKHAN\MSSQL\DATA\UniversityCourseAndResultManagementSystemDB_Inception_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SUMSDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SUMSDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SUMSDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SUMSDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SUMSDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SUMSDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SUMSDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SUMSDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SUMSDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SUMSDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SUMSDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SUMSDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SUMSDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SUMSDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SUMSDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SUMSDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SUMSDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SUMSDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SUMSDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SUMSDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SUMSDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SUMSDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SUMSDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SUMSDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SUMSDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SUMSDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SUMSDB] SET  MULTI_USER 
GO
ALTER DATABASE [SUMSDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SUMSDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SUMSDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SUMSDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SUMSDB]
GO
/****** Object:  StoredProcedure [dbo].[Report_StudentResultSheet]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Report_StudentResultSheet]
(
	@ID int
)
AS
SELECT
	*
FROM
	StudentCourseView
WHERE
	Id = @ID


GO
/****** Object:  Table [dbo].[AllocateClassrooms]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllocateClassrooms](
	[DepartmentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[RoomNo] [nvarchar](50) NOT NULL,
	[Day] [nvarchar](20) NOT NULL,
	[FromTime] [nvarchar](20) NOT NULL,
	[ToTime] [nvarchar](20) NOT NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
	[Duration] [time](7) NULL,
	[AllocationDate] [datetime] NULL,
	[Status] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Courses]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Credit] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DepartmentId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CourseTeacher]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseTeacher](
	[DepartmentId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Status] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Departments]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Designations]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Designations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Designations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoomNo]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomNo](
	[RoomNo] [varchar](20) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Semesters]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Semesters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Semesters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentEnrollCourse]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentEnrollCourse](
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_StudentEnrollCourse] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentResults]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentResults](
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Grade] [varchar](5) NOT NULL,
 CONSTRAINT [PK_StudentResults] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Students]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegNo] [varchar](15) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[ContactNo] [varchar](15) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Address] [varchar](200) NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teachers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](max) NULL,
	[Email] [varchar](50) NOT NULL,
	[ContactNo] [varchar](15) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[CreditToBeTaken] [int] NOT NULL,
	[RemainingCredit] [int] NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[AllocateClassroomAndCourseView]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[AllocateClassroomAndCourseView]
AS
SELECT
	c.DepartmentId
	,c.Code AS CourseCode
	,c.Name AS CourseName
	,ac.RoomNo
	,ac.Day
	,ac.FromTime
	,ac.ToTime
	,ac.Status as ACStatus
FROM
	AllocateClassrooms AS ac
	RIGHT JOIN Courses AS c
	ON ac.CourseId = c.Id







GO
/****** Object:  View [dbo].[CourseTeacherSemestersView]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CourseTeacherSemestersView]
AS
SELECT
	c.Code
	,c.Name
	,s.Name AS SemesterName
	,t.Name AS AssignTeacherName
	,c.DepartmentId
	,ct.Status
FROM
	CourseTeacher AS ct
	RIGHT JOIN Courses AS c
	ON ct.CourseId = c.Id
	LEFT JOIN Semesters AS s
	ON c.SemesterId = s.Id
	LEFT JOIN Teachers AS t
	ON ct.TeacherId = t.Id



GO
/****** Object:  View [dbo].[StudentCourseView]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[StudentCourseView]
AS
SELECT
	s.Id
	,s.RegNo
	,s.Name
	,s.Email
	,d.Name AS DepartmentName
	,c.Id AS CourseId
	,c.Name AS CourseName
	,c.Code AS CourseCode
	,sr.Grade
	,sec.Status
FROM
	StudentEnrollCourse AS sec
	RIGHT JOIN Students AS s
	ON sec.StudentId = s.Id
	LEFT JOIN Departments AS d
	ON s.DepartmentId = d.Id
	
	LEFT JOIN Courses AS c
	ON sec.CourseId = c.Id
	LEFT JOIN StudentResults AS sr
	ON sr.StudentId = sec.StudentId





GO
/****** Object:  View [dbo].[StudentDepartmentView]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[StudentDepartmentView]
AS
SELECT
	s.Id
	,s.RegNo
	,s.Name
	,s.Email
	,dept.Id AS DepartmentId
	,dept.Name AS DepartmentName
FROM
	Students AS s
	LEFT JOIN Departments AS dept
	ON s.DepartmentId = dept.Id




GO
/****** Object:  View [dbo].[TeacherDesignationDepartmentView]    Script Date: 2/12/2017 11:43:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[TeacherDesignationDepartmentView]
AS
SELECT
	t.Id
	,t.Name
	,t.Address
	,t.Email
	,t.ContactNo
	,d.Name AS DesignationName
	,dept.Id AS DepartmentId
	,dept.Name AS DepartmentName
	,t.CreditToBeTaken
	,t.RemainingCredit
FROM
	Teachers AS t
	LEFT JOIN Designations AS d
	ON t.DesignationId = d.Id
	LEFT JOIN Departments AS dept
	ON t.DepartmentId = dept.Id





GO
SET IDENTITY_INSERT [dbo].[Designations] ON 

INSERT [dbo].[Designations] ([Id], [Name]) VALUES (1, N'Professor')
INSERT [dbo].[Designations] ([Id], [Name]) VALUES (2, N'Assistant Professor')
INSERT [dbo].[Designations] ([Id], [Name]) VALUES (3, N'Senior Lecturer')
INSERT [dbo].[Designations] ([Id], [Name]) VALUES (4, N'Lecturer')
INSERT [dbo].[Designations] ([Id], [Name]) VALUES (5, N'Instructor')
SET IDENTITY_INSERT [dbo].[Designations] OFF
SET IDENTITY_INSERT [dbo].[Semesters] ON 

INSERT [dbo].[Semesters] ([Id], [Name]) VALUES (1, N'Spring')
INSERT [dbo].[Semesters] ([Id], [Name]) VALUES (2, N'Summer')
INSERT [dbo].[Semesters] ([Id], [Name]) VALUES (3, N'Fall')
SET IDENTITY_INSERT [dbo].[Semesters] OFF
/****** Object:  Index [IX_Courses]    Script Date: 2/12/2017 11:43:51 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Courses] ON [dbo].[Courses]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Courses_1]    Script Date: 2/12/2017 11:43:51 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Courses_1] ON [dbo].[Courses]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Departments]    Script Date: 2/12/2017 11:43:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Departments] ON [dbo].[Departments]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoomNo]    Script Date: 2/12/2017 11:43:51 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_RoomNo] ON [dbo].[RoomNo]
(
	[RoomNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Students]    Script Date: 2/12/2017 11:43:51 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Students] ON [dbo].[Students]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Students_1]    Script Date: 2/12/2017 11:43:51 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Students_1] ON [dbo].[Students]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Teachers]    Script Date: 2/12/2017 11:43:51 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Teachers] ON [dbo].[Teachers]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AllocateClassrooms]  WITH CHECK ADD  CONSTRAINT [FK_AllocateClassrooms_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[AllocateClassrooms] CHECK CONSTRAINT [FK_AllocateClassrooms_Courses]
GO
ALTER TABLE [dbo].[AllocateClassrooms]  WITH CHECK ADD  CONSTRAINT [FK_AllocateClassrooms_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[AllocateClassrooms] CHECK CONSTRAINT [FK_AllocateClassrooms_Departments]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Departments]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Semesters] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semesters] ([Id])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Semesters]
GO
ALTER TABLE [dbo].[CourseTeacher]  WITH CHECK ADD  CONSTRAINT [FK_CourseTeacher_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[CourseTeacher] CHECK CONSTRAINT [FK_CourseTeacher_Departments]
GO
ALTER TABLE [dbo].[CourseTeacher]  WITH CHECK ADD  CONSTRAINT [FK_CourseTeacher_Teachers] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[CourseTeacher] CHECK CONSTRAINT [FK_CourseTeacher_Teachers]
GO
ALTER TABLE [dbo].[StudentEnrollCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentEnrollCourse_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[StudentEnrollCourse] CHECK CONSTRAINT [FK_StudentEnrollCourse_Students]
GO
ALTER TABLE [dbo].[StudentResults]  WITH CHECK ADD  CONSTRAINT [FK_StudentResults_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[StudentResults] CHECK CONSTRAINT [FK_StudentResults_Students]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Departments]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Departments]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Designations] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designations] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Designations]
GO
USE [master]
GO
ALTER DATABASE [SUMSDB] SET  READ_WRITE 
GO
