#region → Usings   .
using System;
using citPOINT.PrefApp.Data.Web;
using citPOINT.OfferApp.Data.Web.PrefAppService;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 27/02/2012   mwahab         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.PrefApp.Common
{
     
    /// <summary>
    /// Numeric Boundary that return Top Max and
    /// Top Min of any Numeric Issue.
    /// </summary>
    public class NumericBoundary : INumericBoundary
    {
        #region → Fields         .

        private CompleteNumeric mCompleteNumeric;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the complete numeric.
        /// </summary>
        /// <value>The complete numeric.</value>
        public CompleteNumeric CompleteNumeric
        {
            get { return mCompleteNumeric; }
            set { mCompleteNumeric = value; }
        }


        /// <summary>
        /// Gets the top minimum value.
        /// </summary>
        /// <value>The top minimum value.</value>
        public decimal TopMinimumValue
        {
            get { return this.CompleteNumeric.TopMin; }
        }

        /// <summary>
        /// Gets the top maximum value.
        /// </summary>
        /// <value>The top maximum value.</value>
        public decimal TopMaximumValue
        {
            get { return this.CompleteNumeric.TopMax; }
        }

        /// <summary>
        /// Gets or sets the current numeric.
        /// </summary>
        /// <value>The current numeric.</value>
        public INumericIssue CurrentNumeric
        {
            get
            {
                return this.CompleteNumeric;
            }
            set
            {
                this.CompleteNumeric = (CompleteNumeric)value;
            }
        }

        /// <summary>
        /// Gets or sets the preference set neg ID.
        /// </summary>
        /// <value>The preference set neg ID.</value>
        public Guid PreferenceSetNegID { get; set; }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericBoundary"/> class.
        /// </summary>
        /// <param name="completeNumeric">The complete numeric.</param>
        public NumericBoundary(CompleteNumeric completeNumeric)
        {
            this.CompleteNumeric = completeNumeric;
        }

        #endregion
    }
}
