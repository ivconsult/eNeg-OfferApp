

#region → Usings   .
using System;
using citPOINT.OfferApp.Data.Web.PrefAppService;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 27.02.2012      mwahab         • creation
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
    /// Class for IssueBase 
    /// </summary>
    public abstract class OfferIssueBase
    {        
        #region → Fields         .
        
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the issue ID.
        /// </summary>
        /// <value>The issue ID.</value>
        public Guid IssueID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the issue score.
        /// </summary>
        /// <value>The issue score.</value>
        public decimal IssueScore { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// For Ordering
        /// </summary>
        /// <value>The rank.</value>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>The current value.</value>
        public string CurrentValue { get; set; }

        /// <summary>
        /// Gets the current print value.
        /// [e.g 100 EUR or Red,Green,Or any Comments]
        /// </summary>
        /// <value>The current print value.</value>
        public abstract string CurrentPrintValue { get; }

        /// <summary>
        /// Gets or sets the max steps count.
        /// </summary>
        /// <value>The max steps count.</value>
        public int MaxStepsCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is for calculation.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for calculation; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsForCalculation { get; }

        /// <summary>
        /// Gets or sets the base offer value.
        /// </summary>
        /// <value>The base offer value.</value>
        protected string BaseOfferValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [increase base offer].
        /// </summary>
        /// <value><c>true</c> if [increase base offer]; otherwise, <c>false</c>.</value>
        protected bool IncreaseBaseOffer { get; set; }

        /// <summary>
        /// Gets or sets the size of the step.
        /// </summary>
        /// <value>The size of the step.</value>
        protected int StepSize { get; set; }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferIssueBase"/> class.
        /// </summary>
        /// <param name="offerItem">The offer item.</param>
        public OfferIssueBase(OfferItem offerItem)
            : this()
        {
            this.CurrentValue = offerItem.OptionID.HasValue ? offerItem.OptionID.ToString() : offerItem.Value;
            this.IssueScore = offerItem.IssueScore;
            this.IssueID = offerItem.IssueID;
            this.Percentage = offerItem.Percentage;
            this.Name = offerItem.IssueName;
            this.Rank = offerItem.Rank;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferIssueBase"/> class.
        /// </summary>
        public OfferIssueBase()
        {
        }

        #endregion

        #region → Methods        .

        #region → Private        .


        #endregion

        #region → Protected      .

        /// <summary>
        /// Clones the base.
        /// </summary>
        /// <param name="cloneIssue">The clone issue.</param>
        protected void CloneBase(OfferIssueBase cloneIssue)
        {
            cloneIssue.IssueID = this.IssueID;
            cloneIssue.Name = this.Name;
            cloneIssue.IssueScore = this.IssueScore;
            cloneIssue.Rank = this.Rank;
            cloneIssue.Percentage = this.Percentage;
            cloneIssue.CurrentValue = this.CurrentValue;
            cloneIssue.BaseOfferValue = this.BaseOfferValue;
            cloneIssue.IncreaseBaseOffer = this.IncreaseBaseOffer;
            cloneIssue.StepSize = this.StepSize;
            cloneIssue.MaxStepsCount = this.MaxStepsCount;
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the percentage.
        /// depend on the current value and this Issue Score.
        /// </summary>
        /// <returns></returns>
        public abstract decimal GetPercentage();

        /// <summary>
        /// Gets the next offer.
        /// </summary>
        /// <param name="triesNumber">The tries number.</param>
        /// <returns></returns>
        public abstract string GetNextOffer(int triesNumber);

        /// <summary>
        /// Calculates the max steps count.
        /// </summary>
        /// <param name="baseOfferValue">The base offer value.</param>
        /// <param name="increaseBaseOffer">if set to <c>true</c> [increase base offer].</param>
        /// <param name="stepSize">Size of the step.</param>
        public abstract void CalculateMaxStepsCount(string baseOfferValue, bool increaseBaseOffer, int stepSize);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public abstract OfferIssueBase Clone();

        /// <summary>
        /// Gets the issue details for print.
        /// </summary>
        /// <returns></returns>
        public abstract string GetIssueDetailsForPrint();

        #endregion  Public


        #endregion Methods

    }
}
