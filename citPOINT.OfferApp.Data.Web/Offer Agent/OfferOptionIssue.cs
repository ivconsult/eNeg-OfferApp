
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using citPOINT.OfferApp.Data.Web.PrefAppService;
using System.Text;

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
    /// Offer Option Issue.
    /// </summary>
    public class OfferOptionIssue : OfferIssueBase
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
            get
            {
                return this.IssueScore > 0 && this.CompleteOptionList.Count > 0;
            }
        }

        /// <summary>
        /// Gets the current print value.
        /// [e.g 100 EUR or Red,Green,Or any Comments]
        /// </summary>
        /// <value>The current print value.</value>
        public override string CurrentPrintValue
        {
            get
            {
                if (this.InternalOption != null)
                {
                    return InternalOption.Name;
                }
                else
                {
                    return string.Empty; ;
                }
            }
        }

        /// <summary>
        /// Gets the percentage.
        /// depend on the current value and this Issue Score.
        /// </summary>
        /// <returns></returns>
        public override decimal GetPercentage()
        {
            if (InternalOption == null)
            {
                return 0;
            }
            else
            {
                return Math.Round((this.IssueScore * InternalOption.Rate) / 100M, 2);
            }
        }

        /// <summary>
        /// Gets or sets the complete option list.
        /// </summary>
        /// <value>The complete option list.</value>
        private List<CompleteOption> CompleteOptionList { get; set; }

        /// <summary>
        /// Gets the internal option.
        /// </summary>
        /// <value>The internal option.</value>
        private CompleteOption InternalOption
        {
            get
            {
                if (!string.IsNullOrEmpty(this.CurrentValue))
                {
                    CompleteOption option = this.CompleteOptionList
                                            .Where(s => s.OptionID == Guid.Parse(this.CurrentValue))
                                            .FirstOrDefault();

                    return option;
                }
                return null;
            }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferOptionIssue"/> class.
        /// </summary>
        /// <param name="completeOptionList">The complete option list.</param>
        /// <param name="offerItem">The offer item.</param>
        public OfferOptionIssue(List<CompleteOption> completeOptionList, OfferItem offerItem)
            : base(offerItem)
        {
            this.CompleteOptionList = completeOptionList;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferOptionIssue"/> class.
        /// </summary>
        /// <param name="completeOptionList">The complete option list.</param>
        private OfferOptionIssue(List<CompleteOption> completeOptionList)
        {
            this.CompleteOptionList = completeOptionList;
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Calculates the max steps count.
        /// </summary>
        /// <param name="baseOfferValue">The base offer value.</param>
        /// <param name="increaseBaseOffer">if set to <c>true</c> [increase base offer].</param>
        /// <param name="stepSize">Size of the step.</param>
        public override void CalculateMaxStepsCount(string baseOfferValue, bool increaseBaseOffer, int stepSize)
        {
            this.BaseOfferValue = baseOfferValue;
            this.IncreaseBaseOffer = increaseBaseOffer;
            this.StepSize = stepSize;

            this.MaxStepsCount = this.CompleteOptionList.Count();
        }

        /// <summary>
        /// Gets the next offer.
        /// </summary>
        /// <param name="triesNumber">The tries number.</param>
        /// <returns></returns>
        public override string GetNextOffer(int triesNumber)
        {
            if (triesNumber < this.CompleteOptionList.Count())
            {
                return this.CompleteOptionList[triesNumber].OptionID.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override OfferIssueBase Clone()
        {
            OfferOptionIssue clone = new OfferOptionIssue(this.CompleteOptionList);

            CloneBase(clone);

            return clone;

        }
        
        /// <summary>
        /// Gets the issue details for print.
        /// </summary>
        /// <returns></returns>
        public override string GetIssueDetailsForPrint()
        {
            StringBuilder sp = new StringBuilder("");

            sp.Append(string.Format("| {0} | {1} | {2} | {3} | {4} |",
                       this.Rank.ToString().PadRight(8),
                       this.Name.PadRight(13),
                       "-".ToString().PadRight(8),
                       "-".ToString().PadRight(8),
                       this.IssueScore.ToString().PadRight(7)));


            foreach (var OptionItem in this.CompleteOptionList)
            {

                sp.Append(string.Format("\r\n| {0} |  •{1}| {2} | {3} | {4} |",
                         " ".PadRight(8),
                         OptionItem.Name.PadRight(12),
                         ("►" + OptionItem.Rate.ToString() + "%").ToString().PadRight(8),
                         " ".ToString().PadRight(8),
                         " ".ToString().PadRight(7)));

            }

            return sp.ToString();
        }

        #endregion

        #endregion


    }
}
