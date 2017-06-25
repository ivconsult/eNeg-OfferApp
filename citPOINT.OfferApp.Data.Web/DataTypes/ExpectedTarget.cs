
#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
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
    /// <summary>
    /// Class reprsents the target off offer at a certain Time.
    /// </summary>
    public sealed class ExpectedTarget
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public Decimal Target { get; set; }

        #endregion

    }
}
