#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.OfferApp.Data.Web.OfferAgent;
using citPOINT.OfferApp.Data.Web.PrefAppService;
using System.Text;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 2/29/2012 3:07:38 PM      mwahab         • creation
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
    /// Class for Offer Agent .
    /// </summary>
    public class OfferAgent
    {
        #region → Fields         .

        /// <summary>
        /// Load the distance between the current offer Percentage and The Target.
        /// </summary>
        private decimal lastOfferDiff = UInt16.MaxValue;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the base offer.
        /// </summary>
        /// <value>The base offer.</value>
        public Offer BaseOffer { get; set; }

        /// <summary>
        /// Gets or sets the next offer.
        /// </summary>
        /// <value>The next offer.</value>
        public Offer NextOffer { get; set; }

        /// <summary>
        /// Gets or sets the size of the step.
        /// </summary>
        /// <value>The size of the step.</value>
        public int StepSize { get; set; }

        /// <summary>
        /// Gets or sets the number of tries.
        /// </summary>
        /// <value>The number of tries.</value>
        public int NumberOfTries { get; set; }

        /// <summary>
        /// Gets or sets the number of offers.
        /// </summary>
        /// <value>The number of offers.</value>
        public int NumberOfOffers { get; set; }

        /// <summary>
        /// Gets or sets the skip offer count.
        /// </summary>
        /// <value>The skip offer count.</value>
        private int SkipOfferCount { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        private DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        private int Target { get; set; }

        /// <summary>
        /// Gets or sets the default max resoulution.
        /// </summary>
        /// <value>The default max resoulution.</value>
        private Int64 DefaultMaxResoulution { get; set; }

        #endregion

        #region → Constractor    .



        /// <summary>
        /// Initializes a new instance of the <see cref="OfferAgent"/> class.
        /// </summary>
        /// <param name="defaultMaxIteration">The default max iteration.</param>
        /// <param name="preferenceSet">The preference set.</param>
        /// <param name="preferenceSetDetails">The preference set details.</param>
        /// <param name="baseOfferFromUser">The base offer.</param>
        public OfferAgent(Int64 defaultMaxIteration, CompletePreferenceSet preferenceSet, List<object> preferenceSetDetails, List<OfferItem> baseOfferFromUser)
        {
            #region Intialize Variables       .

            this.StartTime = DateTime.Now;

            //Number of maximum iteration.
            defaultMaxIteration = defaultMaxIteration == 0 ? 1000000 : defaultMaxIteration;

            this.DefaultMaxResoulution = defaultMaxIteration;


            BaseOffer = new Offer();
            //NextOffer = new Offer();

            int numericIssuesCount = 0;
            int optionIssuesCount = 0;
            this.StepSize = 10;

            #endregion

            #region Adapt all to be One Offer .

            OfferItem currentOfferItem;

            foreach (var issue in preferenceSet.GetIssues(preferenceSetDetails))
            {
                currentOfferItem = baseOfferFromUser.Where(s => s.IssueID == issue.IssueID).FirstOrDefault();

                #region → Adapt offer Item

                if (currentOfferItem == null)
                {

                    currentOfferItem = new OfferItem()
                    {
                        IssueID = issue.IssueID,
                        IssueScore = issue.Score,
                        Value = string.Empty,
                        OptionID = null,
                        Percentage = 0,
                        IssueName = issue.Name,
                        Rank = issue.Rank
                    };

                    baseOfferFromUser.Add(currentOfferItem);
                }
                else
                {
                    currentOfferItem.IssueScore = issue.Score;
                    currentOfferItem.IssueName = issue.Name;
                    currentOfferItem.Rank = issue.Rank;
                }

                #endregion

                CompleteIssue cIssue = preferenceSet.GetIssues(preferenceSetDetails).Where(search => search.IssueID == currentOfferItem.IssueID).FirstOrDefault();

                OfferIssueBase issueBase = null;

                switch (cIssue.IssueType)
                {
                    case CompleteIssueType.Numeric:
                        issueBase = new OfferNumericIssue(cIssue.GetNumeric(preferenceSetDetails), currentOfferItem);
                        if (cIssue.Score > 0)
                            numericIssuesCount++;
                        break;
                    case CompleteIssueType.Option:
                        issueBase = new OfferOptionIssue(cIssue.GetOptions(preferenceSetDetails), currentOfferItem);
                        if (cIssue.Score > 0)
                            optionIssuesCount += cIssue.GetOptions(preferenceSetDetails).Count();
                        break;
                    case CompleteIssueType.LaterRated:
                        issueBase = new OfferOptionIssue(cIssue.GetOptions(preferenceSetDetails), currentOfferItem);
                        if (cIssue.Score > 0)
                            optionIssuesCount += cIssue.GetOptions(preferenceSetDetails).Count();
                        break;
                    case CompleteIssueType.NotRated:
                        issueBase = new OfferNotRatedIssue(currentOfferItem);
                        break;
                    default:
                        break;
                }

                issueBase.Percentage = issueBase.GetPercentage();

                this.BaseOffer.Issues.Add(issueBase);
            }


            //Calc Total once..
            this.BaseOffer.TotalPercentage = this.BaseOffer.Issues.Sum(s => s.Percentage);

            if (optionIssuesCount > 0)
            {
                this.StepSize = (int)(defaultMaxIteration / optionIssuesCount);
            }

            if (numericIssuesCount > 0)
            {
                this.StepSize = (int)Math.Ceiling(Math.Pow((double)this.StepSize, (double)(1.0 / (double)numericIssuesCount)));
            }

            if (this.StepSize <= 0)
            {
                this.StepSize = 2;
            }

            #endregion

        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Generates the next offer.
        /// </summary>
        /// <param name="currentOffer">The current offer.</param>
        /// <param name="target">The target.</param>
        /// <param name="increaseBaseOffer">if set to <c>true</c> [increase base offer].</param>
        /// <param name="level">The level.</param>
        private void GenerateNextOffer(Offer currentOffer, int target, bool increaseBaseOffer, int level)
        {
            #region → For Full Offer Generated        .

            ////If offer exceed the target so no need to complete.
            ////e.g. target 70 % and Offer is 80 % so no need to complete for all levels
            if (currentOffer.TotalPercentageInteger > target &&
                currentOffer.GetDistanceFromTarget(target) > lastOfferDiff)
            {
                this.SkipOfferCount++;
                return;
            }


            //In case the level exceed all issues count
            if (level >= this.BaseOffer.RatedIssues.Count())
            {

                //to calculate Z
                currentOffer.CalculateZValue(this.BaseOffer);

                this.NumberOfOffers++;

                //if you reach your Target
                if (currentOffer.GetDistanceFromTarget(target) <= lastOfferDiff)
                {
                    lastOfferDiff = currentOffer.GetDistanceFromTarget(target);

                    if (this.NextOffer != null)
                    {
                        if (currentOffer.GetDistanceFromTarget(target) <= this.NextOffer.GetDistanceFromTarget(target))
                        {
                            if ((int)currentOffer.TotalPercentageInteger == (int)this.NextOffer.TotalPercentageInteger)
                            {
                                if (this.NextOffer.ZVlaue > currentOffer.ZVlaue)
                                {
                                    //Save the Offer with minimum Z
                                    this.NextOffer = currentOffer.Clone();
                                }
                            }
                            else
                            {
                                //Save the Offer with minimum Z
                                this.NextOffer = currentOffer.Clone();
                            }
                        }
                    }
                    else
                    {
                        this.NextOffer = currentOffer.Clone();
                    }
                }
                return;
            }

            #endregion

            #region → Real Calculation                .

            for (int j = 0; j < this.BaseOffer.RatedIssues[level].MaxStepsCount; j++)
            {
                this.NumberOfTries++;

                Offer innerOffer = currentOffer.Clone(level);

                OfferIssueBase issue1 = this.BaseOffer.RatedIssues[level].Clone();

                //we decide to use from 0 to include the base offer also
                //this e.g. 500 base offer + 25(add percentage)*3(j)
                issue1.CurrentValue = issue1.GetNextOffer(j).ToString();

                issue1.Percentage = issue1.GetPercentage();

                //With Issue 2
                innerOffer.Issues.Add(issue1);

                innerOffer.TotalPercentage += issue1.Percentage;

                //get resoulution for next offer with current one.
                GenerateNextOffer(innerOffer, target, increaseBaseOffer, level + 1);

            }

            #endregion
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Generates the next offer.
        /// </summary>
        /// <param name="target">The target.</param>
        public void GenerateNextOffer(int target)
        {
            #region → Calculate Maximum Step Count for each Issue  .

            this.Target = target;

            bool increaseBaseOffer = target > this.BaseOffer.TotalPercentage;

            //Calculate The Max steps Count
            foreach (var issue in this.BaseOffer.Issues)
            {
                issue.CalculateMaxStepsCount(issue.CurrentValue, increaseBaseOffer, this.StepSize);
            }

            #endregion

            //Core Function calling
            GenerateNextOffer(this.BaseOffer, target, increaseBaseOffer, 0);

            #region → Adding UnRated Issues or Issues with Score 0 .

            if (this.NextOffer == null)
            {
                this.NextOffer = new Offer();
            }

            //Adding Un calculated Issues..
            foreach (var missedIssue in BaseOffer.Issues.Where(s => s.IsForCalculation == false))
            {
                if (this.NextOffer.Issues.Where(s => s.IssueID == missedIssue.IssueID).FirstOrDefault() == null)
                {
                    this.NextOffer.Issues.Add(missedIssue.Clone());
                }
            }

            #endregion

        }

        /// <summary>
        /// Gets the agent debug info.
        /// </summary>
        /// <returns></returns>
        public string GetAgentDebugInfo()
        {
            StringBuilder sp = new StringBuilder();

            if (this.BaseOffer != null)
            {
                sp.AppendLine(BaseOffer.GetOfferForPrint());
            }

            TimeSpan span = DateTime.Now.Subtract(this.StartTime);

            sp.AppendLine(string.Format("Target         ={0} %", this.Target));
            sp.AppendLine(string.Format("MaxResoulution ={0} #", this.DefaultMaxResoulution));
            sp.AppendLine(string.Format("Offers         ={0} #", this.NumberOfOffers));
            sp.AppendLine(string.Format("Iteration      ={0} #", this.NumberOfTries));
            sp.AppendLine(string.Format("Step Size      ={0} #", this.StepSize));
            sp.AppendLine(string.Format("Used Time      ={0}:{1}.{2} (Minutes:Second.Millisecond)\r\n", span.Minutes.ToString("00"), span.Seconds.ToString("00"), span.Milliseconds.ToString("000")));

            if (this.BaseOffer != null)
            {
                sp.AppendLine("=====================> Base Offer <=========================");

                sp.Append(this.BaseOffer.ToString());
            }

            sp.AppendLine("");

            if (this.NextOffer != null)
            {
                sp.AppendLine("=====================> Next Offer <=========================");

                sp.Append(this.NextOffer.ToString());
            }

            sp.AppendLine("============================================================");

            return sp.ToString();
        }

        #endregion  Public

        #endregion Methods

    }
}
