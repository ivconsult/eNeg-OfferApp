

#region → Usings   .

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using citPOINT.OfferApp.Data.Web.OfferAgent;

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
    /// Offer include base offer and Issues details
    /// </summary>
    public class Offer
    {
        #region → Fields         .
        private List<OfferIssueBase> mRatedIssues;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        /// <value>The issues.</value>
        public List<OfferIssueBase> Issues { get; set; }

        /// <summary>
        /// Gets the rated issues.
        /// </summary>
        /// <value>The rated issues.</value>
        public List<OfferIssueBase> RatedIssues
        {
            get
            {
                if (mRatedIssues == null || this.mRatedIssues.Count() != this.Issues.Count())
                {
                    mRatedIssues = this.Issues
                                       .Where(s => s.IsForCalculation)
                                       .OrderByDescending(S => S.IssueScore)
                                       .ToList();
                }

                return mRatedIssues;
            }
        }

        /// <summary>
        /// Gets or sets the Z vlaue.
        /// </summary>
        /// <value>The Z vlaue.</value>
        public Decimal ZVlaue { get; set; }

        /// <summary>
        /// Gets the total percentage.
        /// </summary>
        /// <value>The total percentage.</value>
        public decimal TotalPercentage { get; set; }

        /// <summary>
        /// Gets the total percentage integer.
        /// </summary>
        /// <value>The total percentage integer.</value>
        public int TotalPercentageInteger
        {
            get
            {
                return (int)this.TotalPercentage;
            }
        }
        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="Offer"/> class.
        /// </summary>
        public Offer()
        {
            this.Issues = new List<OfferIssueBase>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offer"/> class.
        /// </summary>
        /// <param name="issues">The issues.</param>
        public Offer(List<OfferIssueBase> issues)
            : this()
        {
            this.Issues = issues;
        }



        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Calculates the Z value.
        /// </summary>
        /// <param name="BaseOffer">The base offer.</param>
        public void CalculateZValue(Offer BaseOffer)
        {
            List<decimal> allissuesDiff = new List<decimal>();

            foreach (var issue in BaseOffer.RatedIssues)
            {
                OfferIssueBase myIssue = this.RatedIssues.Where(s => s.IssueID == issue.IssueID).FirstOrDefault();

                allissuesDiff.Add(Math.Abs(issue.Percentage - myIssue.Percentage));
            }

            //for e.g Z1=10,Z2=15,Z3=5  Then Z=15 and ZValue=15 + (0.01*(10+15+5))=15.30
            decimal Z = allissuesDiff.Max();

            ZVlaue = Math.Round(Z + 0.01M * (allissuesDiff.Sum()), 4);
        }

        /// <summary>
        /// Gets the distance from target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public int GetDistanceFromTarget(int target)
        {
            return Math.Abs(this.TotalPercentageInteger - target);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Offer Clone()
        {
            Offer newOffer = new Offer();

            foreach (var issue in this.Issues)
            {
                newOffer.Issues.Add(issue.Clone());
            }

            newOffer.ZVlaue = this.ZVlaue;
            newOffer.TotalPercentage = this.TotalPercentage;

            return newOffer;
        }

        /// <summary>
        /// Clones the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public Offer Clone(int level)
        {
            Offer newOffer = new Offer();

            for (int i = 0; i < level; i++)
            {
                OfferIssueBase tmpIssue = this.RatedIssues[i].Clone();

                newOffer.Issues.Add(tmpIssue);
                newOffer.TotalPercentage += tmpIssue.Percentage;
            }

            return newOffer;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {

            StringBuilder sp = new StringBuilder("");

            foreach (var item in this.Issues.OrderBy(s => s.Rank))
            {
                sp.Append(item.Rank.ToString().PadLeft(2) + "-" + item.Name.ToString().PadRight(10) + " - " + item.CurrentPrintValue.ToString().PadRight(20) + " - " + item.Percentage.ToString("00.00#").PadLeft(5) + "%\r\n");
            }
            sp.AppendLine("");
            sp.AppendLine(string.Format("  • Total Offer Percentage = {0} %", this.TotalPercentage.ToString()));
            sp.AppendLine(string.Format("  • Min(Z) = {0}", this.ZVlaue.ToString()));

            return sp.ToString();
        }


        /// <summary>
        /// Gets the offer for print.
        /// </summary>
        /// <returns></returns>
        public string GetOfferForPrint()
        {
            StringBuilder sp = new StringBuilder("");

            sp.AppendLine("=====================> Offer Specification <================");

            sp.AppendLine("------------------------------------------------------------");
            sp.AppendLine("|  Rank    | Name          | Min      | Max      | Score   |");
            sp.AppendLine("------------------------------------------------------------");

            foreach (var issue in this.Issues.OrderBy(s => s.Rank))
            {
                sp.AppendLine(issue.GetIssueDetailsForPrint());
            }

            sp.AppendLine("------------------------------------------------------------");

            return sp.ToString();
        }


        #endregion  Public

        #endregion Methods

    }
}
