CREATE PROC [dbo].[uspConversationOfferSettingUpdate] 
    @ConversationOfferSettingID uniqueidentifier,
    @NegotiationOfferSettingID uniqueidentifier,
	@ConversationID uniqueidentifier,
    @BaseOfferTypeID int,
    @TargetTypeID int,
    @TargetValue decimal(18, 0),
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ConversationOfferSetting]
	SET    [ConversationOfferSettingID] = @ConversationOfferSettingID, [NegotiationOfferSettingID] = @NegotiationOfferSettingID, [ConversationID] = @ConversationID ,[BaseOfferTypeID] = @BaseOfferTypeID, [TargetTypeID] = @TargetTypeID, [TargetValue] = @TargetValue, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [ConversationOfferSettingID] = @ConversationOfferSettingID
	
	-- Begin Return Select <- do not remove
	SELECT [ConversationOfferSettingID], [NegotiationOfferSettingID], [ConversationID], [BaseOfferTypeID], [TargetTypeID], [TargetValue], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[ConversationOfferSetting]
	WHERE  [ConversationOfferSettingID] = @ConversationOfferSettingID	
	-- End Return Select <- do not remove

	COMMIT TRAN