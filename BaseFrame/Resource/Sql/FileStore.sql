


--  创建数据库对象-------------------------------------------------------------------------------

USE [FileStore]
GO
/****** 对象:  Table [dbo].[FsLog]    脚本日期: 11/04/2011 13:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FsLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[FileName] [varchar](200) NULL,
	[FileSize] [int] NULL,
	[FileRelateId] [varchar](50) NULL,
	[Operation] [varchar](50) NULL,
	[OperatorId] [varchar](50) NULL,
	[OperatorName] [varchar](50) NULL,
	[OperatorIp] [varchar](50) NULL,
	[OperateTime] [datetime] NULL,
	[Description] [varchar](200) NULL,
 CONSTRAINT [PK_FsLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

GO
/****** 对象:  Table [dbo].[UserProfile]    脚本日期: 11/04/2011 10:48:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[AttrKey] [varchar](50) NOT NULL,
	[AttrValue] [varchar](200) NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[FsServer]    脚本日期: 11/04/2011 10:48:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FsServer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServerName] [varchar](50) NULL,
	[IsMaster] [bit] NULL,
	[IsCache] [bit] NULL,
	[HttpUrl] [varchar](200) NULL,
	[Description] [varchar](500) NULL,
 CONSTRAINT [PK_FsServer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[FsFile]    脚本日期: 11/04/2011 10:48:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FsFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FolderId] [int] NULL,
	[Name] [nvarchar](200) NULL,
	[ExtName] [varchar](50) NULL,
	[Size] [bigint] NULL,
	[RelateId] [varchar](50) NULL,
	[Code] [nvarchar](50) NULL,
	[Version] [varchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_FsFile_IsDeleted]  DEFAULT ('False'),
	[DeleteTime] [datetime] NULL,
	[DeleteReason] [nvarchar](200) NULL,
	[Src] [varchar](50) NULL,
	[OnSrc] [bit] NULL,
	[OnMaster] [bit] NULL,
	[ServerName] [varchar](50) NULL,
	[FileLocation] [varchar](50) NULL,
	[Guid] [varchar](50) NULL,
 CONSTRAINT [PK_FsFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FsFile', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FsFile', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件来源' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FsFile', @level2type=N'COLUMN',@level2name=N'Src'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件是否已经按来源归类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FsFile', @level2type=N'COLUMN',@level2name=N'OnSrc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件是否在主服务器' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FsFile', @level2type=N'COLUMN',@level2name=N'OnMaster'
GO
/****** 对象:  Table [dbo].[FsFileAttr]    脚本日期: 11/04/2011 10:48:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FsFileAttr](
	[AttrId] [int] IDENTITY(1,1) NOT NULL,
	[FsFileId] [int] NULL,
	[AttrKey] [varchar](50) NULL,
	[AttrValue] [varchar](50) NULL,
 CONSTRAINT [PK_FsFileAttr] PRIMARY KEY CLUSTERED 
(
	[AttrId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[FsRootFolder]    脚本日期: 11/04/2011 10:48:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FsRootFolder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServerId] [int] NULL,
	[RootFolderPath] [varchar](500) NULL,
	[UserName] [varchar](50) NULL,
	[Pwd] [varchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[IsFull] [bit] NULL,
	[SrcFilter] [varchar](200) NULL,
	[ExtNameFilter] [varchar](200) NULL,
	[AllowEncrypt] [bit] NULL,
 CONSTRAINT [PK_FsRootFolder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[FsFolder]    脚本日期: 11/04/2011 10:48:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FsFolder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FsRootFolderId] [int] NULL,
	[FolderPath] [varchar](500) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_FsFolder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  ForeignKey [FK_FsFolder_FsRootFolder]    脚本日期: 11/04/2011 10:48:35 ******/
ALTER TABLE [dbo].[FsFolder]  WITH CHECK ADD  CONSTRAINT [FK_FsFolder_FsRootFolder] FOREIGN KEY([FsRootFolderId])
REFERENCES [dbo].[FsRootFolder] ([Id])
GO
ALTER TABLE [dbo].[FsFolder] CHECK CONSTRAINT [FK_FsFolder_FsRootFolder]
GO
/****** 对象:  ForeignKey [FK_FsRootFolder_FsServer]    脚本日期: 11/04/2011 10:48:35 ******/
ALTER TABLE [dbo].[FsRootFolder]  WITH CHECK ADD  CONSTRAINT [FK_FsRootFolder_FsServer] FOREIGN KEY([ServerId])
REFERENCES [dbo].[FsServer] ([Id])
GO
ALTER TABLE [dbo].[FsRootFolder] CHECK CONSTRAINT [FK_FsRootFolder_FsServer]
GO


--FsFile表建RelateID索引
/****** Object:  Index [RelateID]    Script Date: 04/05/2012 14:10:30 ******/
CREATE NONCLUSTERED INDEX [RelateID] ON [dbo].[FsFile] 
(
	[RelateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

--FsServer和FsRootFolder数据
INSERT [FsServer] ([ServerName], [IsMaster], [IsCache], [HttpUrl], [Description]) VALUES (N'Base', 1, 0, N'http://10.10.1.129:8181/FileStore/FileStoreService/FileService.asmx', N'')
INSERT [FsRootFolder] ([ServerId], [RootFolderPath], [UserName], [Pwd], [CreateTime], [IsFull], [SrcFilter], [ExtNameFilter], [AllowEncrypt]) VALUES (1, N'D:\UploadFiles', N'', N'', CAST(0x00009FC700000000 AS DateTime), 0, N'', N'', 0)



