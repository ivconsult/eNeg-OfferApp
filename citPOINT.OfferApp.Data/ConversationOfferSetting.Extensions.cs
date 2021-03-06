
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using citPOINT.OfferApp.Data.Web.PrefAppService;
#endregion

#region → History  .

/* Date         User            Change
 * 
 *04.03.12     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.OfferApp.Data.Web
{
    /// <summary>
    /// ConversationOfferSetting class client-side extensions
    /// </summary>
    public partial class ConversationOfferSetting : IOfferSetting
    {
        #region → Fields         .

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is strategy app.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is strategy app; otherwise, <c>false</c>.
        /// </value>
        public bool IsStrategyApp
        {
            get
            {
                return (this.TargetTypeID.HasValue && this.TargetTypeID.Value == (int)TargetTypeOptions.StrategyApp);
            }
            set
            {
                if (value)
                {
                    this.TargetTypeID = (int)TargetTypeOptions.StrategyApp;
                    this.RaisePropertyChanged("IsStrategyApp");
                    this.RaisePropertyChanged("IsPrefApp");
                    this.RaisePropertyChanged("IsUserDefined");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pref app.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is pref app; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrefApp
        {
            get
            {
                return (this.TargetTypeID.HasValue && this.TargetTypeID.Value == (int)TargetTypeOptions.PrefApp);
            }
            set
            {
                if (value)
                {
                    this.TargetTypeID = (int)TargetTypeOptions.PrefApp;
                    this.RaisePropertyChanged("IsStrategyApp");
                    this.RaisePropertyChanged("IsPrefApp");
                    this.RaisePropertyChanged("IsUserDefined");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user defined.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is user defined; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserDefined
        {
            get
            {
                return (this.TargetTypeID.HasValue && this.TargetTypeID.Value == (int)TargetTypeOptions.UserDefined);
            }
            set
            {
                if (value)
                {
                    this.TargetTypeID = (int)TargetTypeOptions.UserDefined;
                    this.RaisePropertyChanged("IsStrategyApp");
                    this.RaisePropertyChanged("IsPrefApp");
                    this.RaisePropertyChanged("IsUserDefined");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is owner.
        /// </summary>
        /// <value><c>true</c> if this instance is owner; otherwise, <c>false</c>.</value>
        public bool IsOwner
        {
            get
            {
                return (this.BaseOfferTypeID.HasValue && this.BaseOfferTypeID.Value == (int)OfferType.Own);
            }
            set
            {
                if (value)
                {
                    this.BaseOfferTypeID = (int)OfferType.Own;
                    this.RaisePropertyChanged("IsOwner");
                    this.RaisePropertyChanged("IsCounterPart");
                    this.RaisePropertyChanged("IsMixed");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is counter part.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is counter part; otherwise, <c>false</c>.
        /// </value>
        public bool IsCounterPart
        {
            get
            {
                return (this.BaseOfferTypeID.HasValue && this.BaseOfferTypeID.Value == (int)OfferType.Counterpart);
            }
            set
            {
                if (value)
                {
                    this.BaseOfferTypeID = (int)OfferType.Counterpart;
                    this.RaisePropertyChanged("IsOwner");
                    this.RaisePropertyChanged("IsCounterPart");
                    this.RaisePropertyChanged("IsMixed");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mixed.
        /// </summary>
        /// <value><c>true</c> if this instance is mixed; otherwise, <c>false</c>.</value>
        public bool IsMixed
        {
            get
            {
                return (this.BaseOfferTypeID.HasValue && this.BaseOfferTypeID.Value == (int)OfferType.Mixed);
            }
            set
            {
                if (value)
                {
                    this.BaseOfferTypeID = (int)OfferType.Mixed;
                    this.RaisePropertyChanged("IsOwner");
                    this.RaisePropertyChanged("IsCounterPart");
                    this.RaisePropertyChanged("IsMixed");
                }
            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the ConversationOfferSetting class
        /// </summary>
        /// <returns>True Or False </returns>
        public bool TryValidateObject()
        {

            ValidationContext context = new ValidationContext(this, null, null);
            var validationResults = new Collection<ValidationResult>();

            if (Validator.TryValidateObject(this, context, validationResults, false) == false)
            {
                foreach (ValidationResult error in validationResults)
                {
                    this.ValidationErrors.Add(error);
                }
                return false;
            }


            return true;
        }

        /// <summary>    
        /// Try Try Validate by Property name  
        /// </summary> 
        /// <returns>True Or False </returns> 
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "ConversationOfferSettingID"
             || propertyName == "NegotiationOfferSettingID"
             || propertyName == "ConversationID"
             || propertyName == "BaseOfferTypeID"
             || propertyName == "TargetTypeID"
             || propertyName == "TargetValue"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "ConversationOfferSettingID")
                    return Validator.TryValidateProperty(this.ConversationOfferSettingID, context, validationResults);
                if (propertyName == "NegotiationOfferSettingID")
                    return Validator.TryValidateProperty(this.NegotiationOfferSettingID, context, validationResults);
                if (propertyName == "ConversationID")
                    return Validator.TryValidateProperty(this.ConversationID, context, validationResults);
                if (propertyName == "BaseOfferTypeID")
                    return Validator.TryValidateProperty(this.BaseOfferTypeID, context, validationResults);
                if (propertyName == "TargetTypeID")
                    return Validator.TryValidateProperty(this.TargetTypeID, context, validationResults);
                if (propertyName == "TargetValue")
                    return Validator.TryValidateProperty(this.TargetValue, context, validationResults);
                if (propertyName == "Deleted")
                    return Validator.TryValidateProperty(this.Deleted, context, validationResults);
                if (propertyName == "DeletedBy")
                    return Validator.TryValidateProperty(this.DeletedBy, context, validationResults);
                if (propertyName == "DeletedOn")
                    return Validator.TryValidateProperty(this.DeletedOn, context, validationResults);
            }
            return false;
        }
        
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>return new Instance of ConversationOfferSetting</returns>
        public ConversationOfferSetting Clone()
        {
            ConversationOfferSetting mConversationOfferSetting = new ConversationOfferSetting
                                        {
                                            ConversationOfferSettingID = this.ConversationOfferSettingID,
                                            NegotiationOfferSettingID = this.NegotiationOfferSettingID,
                                            ConversationID = this.ConversationID,
                                            BaseOfferTypeID = this.BaseOfferTypeID,
                                            TargetTypeID = this.TargetTypeID,
                                            TargetValue = this.TargetValue,
                                            Deleted = this.Deleted,
                                            DeletedBy = this.DeletedBy,
                                            DeletedOn = this.DeletedOn,


                                        };

            return mConversationOfferSetting;
        }
                
        #endregion Methods
        
    }

}
