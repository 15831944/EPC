USE [FE_Workflow]
GO

/****** Object:  Trigger [dbo].[InsTaskExec_Insert]    Script Date: 11/12/2015 23:34:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author: la_huang
-- Create date: 2015-11-09
-- Description:	将插入的任务记录同步到S_E_RTXSynchNoticeAndTask表
-- =============================================
CREATE TRIGGER [dbo].[InsTaskExec_Insert] 
   ON  [dbo].[S_WF_InsTaskExec]
   FOR INSERT
AS 
BEGIN
    -- Insert statements for trigger here
    
    INSERT INTO S_E_RTXSynchNoticeAndTask (TaskExecIDOrMsgID,State,Title,Content,LinkURL,OwnerUserID,OwnerUserName,CreateDate,InsFlowID,FormID,DataType,SendUserIDs,SendUserNames,TaskType)
    select insTaskExec.ID AS TaskExecIDOrMsgID,'Wait' as State,InsTask.TaskName as Title,InsTask.TaskName as Content
    ,InsDefFlow.FormUrl as LinkURL,insTaskExec.TaskUserID as OwnerUserID
    ,insTaskExec.TaskUserName as OwnerUserName, insTaskExec.CreateTime as CreateDate
    , insTaskExec.InsFlowID as InsFlowID,InsFlow.FormInstanceID as FormID,'TASK' as DataType
    ,InsTask.SendTaskUserIDs as SendUserIDs,InsTask.SendTaskUserNames as SendUserNames,InsTask.Type
    from inserted insTaskExec 
    left join S_WF_InsTask InsTask on InsTask.ID = insTaskExec.InsTaskID  
    left join S_WF_InsFlow InsFlow on insTaskExec.InsFlowID = InsFlow.ID
    left join S_WF_InsDefFlow InsDefFlow on InsFlow.InsDefFlowID = InsDefFlow.ID
END







GO


