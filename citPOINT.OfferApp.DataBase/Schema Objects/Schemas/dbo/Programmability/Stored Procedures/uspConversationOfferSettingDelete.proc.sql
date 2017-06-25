
CREATE PROC [dbo].[uspConversationOfferSettingDelete] 
    @ConversationOfferSettingID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ConversationOfferSetting]
	SET [Deleted] = 1, [DeletedOn] = GETDATE()
	WHERE  [ConversationOfferSettingID] = @ConversationOfferSettingID

	COMMIT