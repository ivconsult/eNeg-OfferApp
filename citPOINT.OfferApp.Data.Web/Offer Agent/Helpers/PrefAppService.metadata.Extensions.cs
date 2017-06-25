

#region → Usings   .

using System;
using System.Collections.Generic;
using System.Linq;
using citPOINT.PrefApp.Data.Web;


#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 29.02.2012   mwahab         • creation
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

namespace citPOINT.OfferApp.Data.Web.PrefAppService
{

    /// <summary>
    /// Complete Preference Set.
    /// </summary>
    public partial class CompletePreferenceSet
    {
        /// <summary>
        /// Gets the numeric.
        /// </summary>
        /// <param name="includedList">The included list.</param>
        /// <returns></returns>
        public List<CompleteIssue> GetIssues(List<object> includedList)
        {
            var result =
              includedList
                  .Where(s => s.GetType().Equals(typeof(CompleteIssue)) &&
                      (s as CompleteIssue).PreferenceSetID == this.PreferenceSetID).ToList();


            List<CompleteIssue> resultx = new List<CompleteIssue>();

            foreach (var issue in result)
            {
                resultx.Add(issue as CompleteIssue);
            }

            return resultx;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Preference Name: {0} And Max Percentage : {1}", this.Name, this.MaxPercentage);
        }
    }

    /// <summary>
    /// Complete Issue.
    /// </summary>
    public partial class CompleteIssue
    {
        /// <summary>
        /// Gets the numeric.
        /// </summary>
        /// <param name="includedList">The included list.</param>
        /// <returns></returns>
        public CompleteNumeric GetNumeric(List<object> includedList)
        {
            var result =
              includedList
                  .Where(s => s.GetType().Equals(typeof(CompleteNumeric)) &&
                      (s as CompleteNumeric).IssueID == this.IssueID).ToList().FirstOrDefault();

            return result as CompleteNumeric;
        }
        
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="includedList">The included list.</param>
        /// <returns></returns>
        public List<CompleteOption> GetOptions(List<object> includedList)
        {
            var result =
             includedList
                 .Where(s => s.GetType().Equals(typeof(CompleteOption)) &&
                     (s as CompleteOption).IssueID == this.IssueID).ToList();

            List<CompleteOption> resultx = new List<CompleteOption>();

            foreach (var item in result)
            {
                resultx.Add(item as CompleteOption);
            }

            return resultx;
        }
        
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("==>> ({0})- Name  : {1} \r\n     Type  : {2} \r\n     Score : {3}", this.Rank, this.Name.PadRight(10), this.IssueType.ToString().PadRight(10), this.Score);
        }

    }

    /// <summary>
    /// Complete Numeric.
    /// </summary>
    public partial class CompleteNumeric : INumericIssue
    {
        /// <summary>
        /// </summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("\t   • Min       :{0} \r\n\t   • TopMin    :{1} \r\n\t   • Opt.Start :{2} \r\n\t   • Opt.End   :{3} \r\n\t   • Max       :{3} \r\n\t   • TopMax    :{4} ", this.MinimumValue, this.TopMin, this.OptimumValueStart, this.OptimumValueEnd, this.MaximumValue, this.TopMax);
        }
    }


    /// <summary>
    /// Complete Option
    /// </summary>
    public partial class CompleteOption
    {
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("\t   • Name :{0} > Rate :{1} ", this.Name.PadRight(10), this.Rate);
        }
    }


    /// <summary>
    /// Offer Item From Pref App
    /// </summary>
    public partial class OfferItem
    {
        /// <summary>
        /// Gets or sets the issue score.
        /// </summary>
        /// <value>The issue score.</value>
        public decimal IssueScore { get; set; }

        /// <summary>
        /// Gets or sets the name of the issue.
        /// </summary>
        /// <value>The name of the issue.</value>
        public String IssueName { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>The rank.</value>
        public int Rank { get; set; }
    }

}
