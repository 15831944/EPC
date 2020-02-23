USE [FE_Workflow]
GO

/****** Object:  Table [dbo].[S_E_RTXSynchNoticeAndTask]    Script Date: 11/12/2015 23:32:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[S_E_RTXSynchNoticeAndTask](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaskExecIDOrMsgID] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Title] [nvarchar](2000) NULL,
	[Content] [nvarchar](2000) NULL,
	[LinkURL] [nvarchar](500) NULL,
	[OwnerUserID] [nvarchar](50) NULL,
	[OwnerUserName] [nvarchar](50) NULL,
	[SendUserIDs] [nvarchar](500) NULL,
	[SendUserNames] [nvarchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[InsFlowID] [nvarchar](50) NULL,
	[FormID] [nvarchar](50) NULL,
	[TaskType] [nvarchar](50) NULL,
	[DataType] [nvarchar](50) NULL,
	[ExecTime] [datetime] NULL,
	[ErrorMsg] [nvarchar](2000) NULL,
 CONSTRAINT [PK_S_E_RTXSynchNoticeAndTask] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态(Wait,Finish,Failed)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'State'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'Title'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'Content'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面链接地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'LinkURL'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务所属人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'OwnerUserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务所属人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'OwnerUserName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据类型(消息(MSG),任务(TASK))' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'DataType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务执行时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'ExecTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'错误信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_E_RTXSynchNoticeAndTask', @level2type=N'COLUMN',@level2name=N'ErrorMsg'
GO


