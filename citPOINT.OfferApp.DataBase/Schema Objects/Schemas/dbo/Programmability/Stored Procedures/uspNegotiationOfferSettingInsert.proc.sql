CREATE PROC [dbo].[uspNegotiationOfferSettingInsert] 
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
	
	INSERT INTO [dbo].[NegotiationOfferSetting] ([NegotiationOfferSettingID], [NegotiationID], [BaseOfferTypeID], [TargetTypeID], [TargetValue], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @NegotiationOfferSettingID, @NegotiationID, @BaseOfferTypeID, @TargetTypeID, @TargetValue, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationOfferSettingID], [NegotiationID], [BaseOfferTypeID], [TargetTypeID], [TargetValue], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationOfferSetting]
	WHERE  [NegotiationOfferSettingID] = @NegotiationOfferSettingID
	-- End Return Select <- do not remove
               
	COMMIT