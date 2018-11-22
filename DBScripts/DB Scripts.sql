CREATE DATABASE MVCApplication
go

USE [MVCApplication]
GO

/****** Object:  Table [dbo].[Person]    Script Date: 15-08-2018 20:11:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Person](
	[User_Id] [varchar](25) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FullName] [varchar](30) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Joined] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Tweet]    Script Date: 15-08-2018 20:11:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tweet](
	[Tweet_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [varchar](25) NOT NULL,
	[Message] [varchar](140) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_Tweet] PRIMARY KEY CLUSTERED 
(
	[Tweet_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tweet]  WITH CHECK ADD  CONSTRAINT [FK_Tweet_USerID] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Person] ([User_Id])
GO

ALTER TABLE [dbo].[Tweet] CHECK CONSTRAINT [FK_Tweet_USerID]
GO

/****** Object:  Table [dbo].[Following]    Script Date: 15-08-2018 20:10:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Following](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [varchar](25) NOT NULL,
	[Following_Id] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Following] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Following]  WITH CHECK ADD  CONSTRAINT [FK_Following_FollowingId] FOREIGN KEY([Following_Id])
REFERENCES [dbo].[Person] ([User_Id])
GO

ALTER TABLE [dbo].[Following] CHECK CONSTRAINT [FK_Following_FollowingId]
GO

ALTER TABLE [dbo].[Following]  WITH CHECK ADD  CONSTRAINT [FK_Following_UserId] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Person] ([User_Id])
GO

ALTER TABLE [dbo].[Following] CHECK CONSTRAINT [FK_Following_UserId]
GO


