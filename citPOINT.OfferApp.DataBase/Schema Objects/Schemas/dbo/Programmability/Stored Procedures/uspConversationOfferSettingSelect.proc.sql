CREATE PROC [dbo].[uspConversationOfferSettingSelect] 
    @ConversationOfferSettingID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ConversationOfferSettingID], [NegotiationOfferSettingID], [BaseOfferTypeID], [TargetTypeID], [TargetValue], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[ConversationOfferSetting] 
	WHERE  ([ConversationOfferSettingID] = @ConversationOfferSettingID OR @ConversationOfferSettingID IS NULL) 

	COMMIT