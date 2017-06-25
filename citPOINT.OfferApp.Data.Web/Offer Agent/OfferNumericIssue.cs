
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.OfferApp.Data.Web.PrefAppService;
using citPOINT.PrefApp.Common;

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
    /// Class for NumericIssue 
    /// </summary>
    public class OfferNumericIssue : OfferIssueBase
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
                return this.IssueScore > 0;
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
                if (string.IsNullOrEmpty(this.CurrentValue))
                {
                    return this.CompleteNumericIssue.Unit;
                }
                else
                {
                    return this.CurrentValue + " " + this.CompleteNumericIssue.Unit;
                }
            }
        }

        /// <summary>
        /// Gets or sets the complete numeric issue.
        /// </summary>
        /// <value>The complete numeric issue.</value>
        private CompleteNumeric CompleteNumericIssue { get; set; }

        /// <summary>
        /// Gets or sets the change value.
        /// </summary>
        /// <value>The change value.</value>
        private int ChangeValue { get; set; }

        /// <summary>
        /// Gets or sets the top offer value.
        /// [Maximum value of numeric or minimum depend on target value of the offer and numeric structu.]
        /// </summary>
        /// <value>The top offer value.</value>
        private decimal TopOfferValue { get; set; }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferNumericIssue"/> class.
        /// </summary>
        /// <param name="completeNumericIssue">The complete numeric issue.</param>
        /// <param name="offerItem">The offer item.</param>
        public OfferNumericIssue(CompleteNumeric completeNumericIssue, OfferItem offerItem)
            : base(offerItem)
        {
            this.CompleteNumericIssue = completeNumericIssue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferNumericIssue"/> class.
        /// </summary>
        /// <param name="completeNumericIssue">The complete numeric issue.</param>
        private OfferNumericIssue(CompleteNumeric completeNumericIssue)
        {
            this.CompleteNumericIssue = completeNumericIssue;
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

            #region → Intialize Variables                          .

            this.IncreaseBaseOffer = increaseBaseOffer;
            this.StepSize = stepSize;
            int Base = 1;
            ChangeValue = 0;
            decimal baseOfferDecimalValue = 0;

            //Mean if Min =0% and Max=100%
            bool IsUpDown = true;

            #endregion

            #region → Define The Graph type                        .

            /* Updown details
             * 
             * |          *
             * |        *
             * |      *
             * |    *
             * |  *
             * |*
             * -----------------------------
             * 
             * Up=100%  Down =0 % graph
             * So it is Up Down
             * 
             * */

            if (this.CompleteNumericIssue.MaximumValue == this.CompleteNumericIssue.OptimumValueEnd)
            {
                IsUpDown = true;
            }
            else if (this.CompleteNumericIssue.MinimumValue == this.CompleteNumericIssue.OptimumValueStart)
            {
                IsUpDown = false;
            }

            #endregion

            #region → Define Start Point                           .

            if (!string.IsNullOrEmpty(baseOfferValue))
            {
                baseOfferDecimalValue = decimal.Parse(baseOfferValue);
            }
            else
            {
                if (increaseBaseOffer)
                {
                    //So put value that make score =0 (Start from begining)

                    if (IsUpDown)
                    {
                        baseOfferDecimalValue = this.CompleteNumericIssue.TopMin;
                    }
                    else
                    {
                        baseOfferDecimalValue = this.CompleteNumericIssue.TopMax;
                    }
                }
                else
                {
                    //So put value that make score =100% (Start from End)

                    if (IsUpDown)
                    {
                        baseOfferDecimalValue = this.CompleteNumericIssue.TopMax;
                    }
                    else
                    {
                        baseOfferDecimalValue = this.CompleteNumericIssue.TopMin;
                    }
                }
            }

            Base = IsUpDown ? 1 : -1;
            this.BaseOfferValue = baseOfferDecimalValue.ToString();
            #endregion

            #region → Define Change rate + StepCount + Max Value   .

            if (this.IncreaseBaseOffer)
            {
                if (IsUpDown)
                {
                    ChangeValue = (int)(Base * Math.Ceiling((((CompleteNumericIssue.TopMax - baseOfferDecimalValue) / stepSize))));
                    this.MaxStepsCount = (int)Math.Abs(Math.Floor(Math.Abs(CompleteNumericIssue.TopMax - baseOfferDecimalValue) / ChangeValue));
                    this.TopOfferValue = CompleteNumericIssue.TopMax;
                }
                else
                {
                    ChangeValue = (int)(Base * Math.Ceiling((((baseOfferDecimalValue - CompleteNumericIssue.TopMin) / stepSize))));

                    this.MaxStepsCount = (int)Math.Abs(Math.Floor(Math.Abs(CompleteNumericIssue.TopMin - baseOfferDecimalValue) / ChangeValue));
                    this.TopOfferValue = CompleteNumericIssue.TopMin;
                }

            }
            else
            {
                if (IsUpDown)
                {
                    ChangeValue = (int)(Base * Math.Ceiling(((baseOfferDecimalValue - CompleteNumericIssue.TopMin) / stepSize)));

                    ChangeValue = ChangeValue * -1;

                    this.MaxStepsCount = (int)Math.Abs(Math.Floor((baseOfferDecimalValue - CompleteNumericIssue.TopMin) / ChangeValue));

                    this.TopOfferValue = CompleteNumericIssue.TopMin;
                }
                else
                {
                    ChangeValue = (int)(Base * Math.Ceiling((((CompleteNumericIssue.TopMax - baseOfferDecimalValue) / stepSize))));

                    ChangeValue = ChangeValue * -1;

                    this.MaxStepsCount = (int)Math.Abs(Math.Floor((CompleteNumericIssue.TopMax - baseOfferDecimalValue) / ChangeValue));

                    this.TopOfferValue = CompleteNumericIssue.TopMax;
                }
            }

            //in case Step =3 start 0 ==>0 3 6 9 so need case to 10 Last Point.
            this.MaxStepsCount += 1;

            #endregion

        }

        /// <summary>
        /// Gets the percentage.
        /// </summary>
        /// <returns></returns>
        public override decimal GetPercentage()
        {
            NumericCalculation numericCalculation = new NumericCalculation(this.IssueScore, this.CompleteNumericIssue, new NumericBoundary(this.CompleteNumericIssue));

            return numericCalculation.CalcScore(this.CurrentValue.ToString());
        }

        /// <summary>
        /// Gets the next offer.
        /// </summary>
        /// <param name="triesNumber">The tries number.</param>
        /// <returns></returns>
        public override string GetNextOffer(int triesNumber)
        {
            if (triesNumber == (MaxStepsCount - 1))
            {
                return this.TopOfferValue.ToString();
            }
            else
            {
                return (decimal.Parse(this.BaseOfferValue) + (ChangeValue * triesNumber)).ToString();
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override OfferIssueBase Clone()
        {
            OfferNumericIssue clone = new OfferNumericIssue(this.CompleteNumericIssue);

            //Clone all base members.
            this.CloneBase(clone);

            clone.ChangeValue = this.ChangeValue;
            clone.TopOfferValue = this.TopOfferValue;

            return clone;
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
                           this.CompleteNumericIssue.TopMin.ToString().PadRight(8),
                           this.CompleteNumericIssue.TopMax.ToString().PadRight(8),
                           this.IssueScore.ToString().PadRight(7));
        }

        #endregion

        #endregion




    }
}
