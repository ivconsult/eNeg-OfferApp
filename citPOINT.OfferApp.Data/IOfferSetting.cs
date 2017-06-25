
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using citPOINT.OfferApp.Data.Web;
using System.ComponentModel;
using System.Collections.Generic;
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
    /// Interface for Offer Setting
    /// COnversation and Negotiation settings.
    /// </summary>
    public interface IOfferSetting
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the base offer type ID.
        /// </summary>
        /// <value>The base offer type ID.</value>
        int? BaseOfferTypeID { get; set; }

        /// <summary>
        /// Gets or sets the target type ID.
        /// </summary>
        /// <value>The target type ID.</value>
        int? TargetTypeID { get; set; }

        /// <summary>
        /// Gets or sets the target value.
        /// </summary>
        /// <value>The target value.</value>
        decimal? TargetValue { get; set; }

        //
        // Summary:
        //     Gets the collection of validation errors encountered during the last submit
        //     operation.
        [Display(AutoGenerateField = false)]
        ICollection<ValidationResult> ValidationErrors { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is strategy app.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is strategy app; otherwise, <c>false</c>.
        /// </value>
        bool IsStrategyApp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pref app.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is pref app; otherwise, <c>false</c>.
        /// </value>
        bool IsPrefApp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user defined.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is user defined; otherwise, <c>false</c>.
        /// </value>
        bool IsUserDefined { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is owner.
        /// </summary>
        /// <value><c>true</c> if this instance is owner; otherwise, <c>false</c>.</value>
        bool IsOwner { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is counter part.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is counter part; otherwise, <c>false</c>.
        /// </value>
        bool IsCounterPart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mixed.
        /// </summary>
        /// <value><c>true</c> if this instance is mixed; otherwise, <c>false</c>.</value>
        bool IsMixed { get; set; }


        #endregion

        #region → Methods        .

        /// <summary>
        /// Tries the validate object.
        /// </summary>
        /// <returns></returns>
        bool TryValidateObject();

        /// <summary>
        /// Tries the validate property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        bool TryValidateProperty(string propertyName);

        #endregion
    }
}
