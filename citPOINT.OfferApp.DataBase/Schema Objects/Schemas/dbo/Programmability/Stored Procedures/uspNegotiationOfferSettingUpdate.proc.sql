CREATE PROC [dbo].[uspNegotiationOfferSettingUpdate] 
    @NegotiationOfferSettingID uniqueidentifier,
    @NegotiationID uniqueidentifier,
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

	UPDATE [dbo].[NegotiationOfferSetting]
	SET    [NegotiationOfferSettingID] = @NegotiationOfferSettingID, [NegotiationID] = @NegotiationID, [BaseOfferTypeID] = @BaseOfferTypeID, [TargetTypeID] = @TargetTypeID, [TargetValue] = @TargetValue, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [NegotiationOfferSettingID] = @NegotiationOfferSettingID
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationOfferSettingID], [NegotiationID], [BaseOfferTypeID], [TargetTypeID], [TargetValue], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationOfferSetting]
	WHERE  [NegotiationOfferSettingID] = @NegotiationOfferSettingID	
	-- End Return Select <- do not remove

	COMMIT TRAN