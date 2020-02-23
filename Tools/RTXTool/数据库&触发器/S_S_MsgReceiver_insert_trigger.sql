USE [FE_Base]
GO

/****** Object:  Trigger [dbo].[S_S_MsgReceiver_Insert]    Script Date: 11/12/2015 23:35:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		la_huang
-- Create date: 2015-11-09
-- Description:	将插入的消息信息同步至[FE_WorkFlow].dbo.[S_E_RTXSynchNoticeAndTask]表
-- =============================================
CREATE TRIGGER [dbo].[S_S_MsgReceiver_Insert]
   ON  [dbo].[S_S_MsgReceiver]
   FOR INSERT
AS 
BEGIN
	-- Insert statements for trigger here
	INSERT INTO [FE_WorkFlow].[dbo].[S_E_RTXSynchNoticeAndTask] (TaskExecIDOrMsgID,State,Title,Content,LinkURL,OwnerUserID,OwnerUserName,CreateDate,DataType,SendUserIDs,SendUserNames)
	select msReceiver.ID as TaskExecIDOrMsgID,'Wait' as State,msgBody.Title as Title
    ,msgBody.Content as Content,'/Base/ShortMsg/ReceiveMsg/Views?ID='+msgBody.ID+'&ReceiverID='+msReceiver.ID+'&FuncType=Receive' as LinkURL,msReceiver.UserID as OwnerUserID
    ,msReceiver.UserName as OwnerUserName,msgBody.SendTime as CreateDate,'MSG' as DataType
    ,msgBody.SenderID as SendUserIDs,msgBody.SenderName as SendUserNames
    from inserted msReceiver
    left join dbo.S_S_MsgBody msgBody on msReceiver.MsgBodyID = msgBody.ID
END




GO


