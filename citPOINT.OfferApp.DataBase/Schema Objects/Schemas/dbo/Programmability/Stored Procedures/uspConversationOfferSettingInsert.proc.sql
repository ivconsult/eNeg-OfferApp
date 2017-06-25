CREATE PROC [dbo].[uspConversationOfferSettingInsert] 
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
	
	INSERT INTO [dbo].[ConversationOfferSetting] ([ConversationOfferSettingID], [NegotiationOfferSettingID], [ConversationID], [BaseOfferTypeID], [TargetTypeID], [TargetValue], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @ConversationOfferSettingID, @NegotiationOfferSettingID, @ConversationID ,@BaseOfferTypeID, @TargetTypeID, @TargetValue, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [ConversationOfferSettingID], [NegotiationOfferSettingID],[ConversationID], [BaseOfferTypeID], [TargetTypeID], [TargetValue], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[ConversationOfferSetting]
	WHERE  [ConversationOfferSettingID] = @ConversationOfferSettingID
	-- End Return Select <- do not remove
               
	COMMIT