CREATE PROC [dbo].[uspNegotiationOfferSettingSelect] 
    @NegotiationOfferSettingID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegotiationOfferSettingID], [NegotiationID], [BaseOfferTypeID], [TargetTypeID], [TargetValue], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegotiationOfferSetting] 
	WHERE  ([NegotiationOfferSettingID] = @NegotiationOfferSettingID OR @NegotiationOfferSettingID IS NULL) 

	COMMIT