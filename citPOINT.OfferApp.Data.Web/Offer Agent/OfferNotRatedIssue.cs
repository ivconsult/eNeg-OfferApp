
#region → Usings   .
 
using citPOINT.OfferApp.Data.Web.PrefAppService;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 27.02.2012    mwahab         • creation
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


namespace citPOINT.OfferApp.Data.Web.OfferAgent
{
    /// <summary>
    ///  Offer Not Rated Issue
    /// </summary>
    public class OfferNotRatedIssue : OfferIssueBase
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is for calculation.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for calculation; otherwise, <c>false</c>.
        /// </value>
        public override bool IsForCalculation
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the current print value.
        /// [e.g 100 EUR or Red,Green,Or any Comments]
        /// </summary>
        /// <value>The current print value.</value>
        public override string CurrentPrintValue
        {
            get { return this.CurrentValue; }
        }

        /// <summary>
        /// Gets the percentage.
        /// depend on the current value and this Issue Score.
        /// </summary>
        /// <returns></returns>
        public override decimal GetPercentage()
        {
            return 0;
        }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferNotRatedIssue"/> class.
        /// </summary>
        /// <param name="offerItem">The offer item.</param>
        public OfferNotRatedIssue(OfferItem offerItem)
            : base(offerItem)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferNotRatedIssue"/> class.
        /// </summary>
        private OfferNotRatedIssue()
        {
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Gets the next offer.
        /// </summary>
        /// <param name="triesNumber">The tries number.</param>
        /// <returns></returns>
        public override string GetNextOffer(int triesNumber)
        {
            return this.CurrentValue;
        }

        /// <summary>
        /// Calculates the max steps count.
        /// </summary>
        /// <param name="baseOfferValue">The base offer value.</param>
        /// <param name="increaseBaseOffer">if set to <c>true</c> [increase base offer].</param>
        /// <param name="stepSize">Size of the step.</param>
        public override void CalculateMaxStepsCount(string baseOfferValue, bool increaseBaseOffer, int stepSize)
        {
            this.MaxStepsCount = 0;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override OfferIssueBase Clone()
        {
            OfferNotRatedIssue clon = new OfferNotRatedIssue();

            CloneBase(clon);

            return clon;
        }

        /// <summary>
        /// Gets the issue details for print.
        /// </summary>
        /// <returns></returns>
        public override string GetIssueDetailsForPrint()
        {
            return string.Format("| {0} | {1} | {2} | {3} | {4} |",
                                this.Rank.ToString().PadRight(8),
                                this.Name.PadRight(13),
                                "-".PadRight(8),
                                "-".ToString().PadRight(8),
                                "-".ToString().PadRight(7));
        }

        #endregion  Public

        #endregion Methods

      
    }
}
