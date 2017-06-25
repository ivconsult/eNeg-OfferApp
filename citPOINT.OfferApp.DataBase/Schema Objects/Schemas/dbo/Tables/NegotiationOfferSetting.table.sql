CREATE TABLE [dbo].[NegotiationOfferSetting]
(
	NegotiationOfferSettingID uniqueidentifier primary key,
	NegotiationID uniqueidentifier not null,
	BaseOfferTypeID int,
	TargetTypeID int, 
	TargetValue decimal,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
