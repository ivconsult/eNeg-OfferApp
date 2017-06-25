
#region → Usings   .


#endregion

#region → History  .

/* Date         User          Change
 * 
 * 04.03.12     Yousra Reda       • Creation
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
    /// Class reprsents the Status of query.
    /// </summary>
    public enum Status
    {
        #region → Properties     .

        /// <summary>
        /// Success.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Failed.
        /// </summary>
        Failed = 1,

        /// <summary>
        /// Date Out of Period.
        /// </summary>
        DateOutOfPeriod = 2,

        /// <summary>
        /// NO Settings.
        /// </summary>
        NoSettings = 3

        #endregion


    }
}
