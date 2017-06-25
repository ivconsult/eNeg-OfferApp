
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.ComponentModel;

#endregion

#region → History  .

/* Date         User          Change
 * 
 * 28.02.12     Yousra Reda       • Creation
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
    [Serializable()]
    public class OfferDetails
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>The rank.</value>
        [DataMember()]
        [Key]
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the issue name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember()]
        public string IssueName { get; set; }

        /// <summary>
        /// Gets or sets the value of the issue.
        /// </summary>
        /// <value>The value.</value>
        [DataMember()]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the max percentage.
        /// </summary>
        /// <value>The max percentage.</value>
        [DataMember()]
        public decimal MaxPercentage { get; set; }

        #endregion Properties
    }
}
