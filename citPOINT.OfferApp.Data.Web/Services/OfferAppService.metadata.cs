
namespace citPOINT.OfferApp.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;

    // The MetadataTypeAttribute identifies ConversationOfferSettingMetadata as the class
    // that carries additional metadata for the ConversationOfferSetting class.
    [MetadataTypeAttribute(typeof(ConversationOfferSetting.ConversationOfferSettingMetadata))]
    public partial class ConversationOfferSetting
    {

        // This class allows you to attach custom attributes to properties
        // of the ConversationOfferSetting class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ConversationOfferSettingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ConversationOfferSettingMetadata()
            {
            }

            public Nullable<int> BaseOfferTypeID { get; set; }

            public Guid ConversationID { get; set; }

            public Guid ConversationOfferSettingID { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public NegotiationOfferSetting NegotiationOfferSetting { get; set; }

            public Nullable<Guid> NegotiationOfferSettingID { get; set; }

            public Nullable<int> TargetTypeID { get; set; }

            public Nullable<decimal> TargetValue { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NegotiationOfferSettingMetadata as the class
    // that carries additional metadata for the NegotiationOfferSetting class.
    [MetadataTypeAttribute(typeof(NegotiationOfferSetting.NegotiationOfferSettingMetadata))]
    public partial class NegotiationOfferSetting
    {

        // This class allows you to attach custom attributes to properties
        // of the NegotiationOfferSetting class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegotiationOfferSettingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationOfferSettingMetadata()
            {
            }

            public Nullable<int> BaseOfferTypeID { get; set; }

            public EntityCollection<ConversationOfferSetting> ConversationOfferSettings { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid NegotiationID { get; set; }

            public Guid NegotiationOfferSettingID { get; set; }
            
            public Nullable<int> TargetTypeID { get; set; }

            public Nullable<decimal> TargetValue { get; set; }
        }
    }

    
}
