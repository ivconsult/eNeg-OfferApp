CREATE TABLE [dbo].[ConversationOfferSetting]
(
	ConversationOfferSettingID uniqueidentifier primary key,
	NegotiationOfferSettingID uniqueidentifier references NegotiationOfferSetting(NegotiationOfferSettingID),
	ConversationID uniqueidentifier not null,
	BaseOfferTypeID int,
	TargetTypeID int, 
	TargetValue decimal,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
