CREATE PROC [dbo].[uspNegotiationOfferSettingDelete] 
    @NegotiationOfferSettingID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationOfferSetting]
	SET [Deleted] = 1, [DeletedOn] = GETDATE()
	WHERE  [NegotiationOfferSettingID] = @NegotiationOfferSettingID

	COMMIT
GO