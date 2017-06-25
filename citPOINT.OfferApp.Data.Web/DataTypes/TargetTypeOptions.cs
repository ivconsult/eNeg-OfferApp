
#region → Usings   .

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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
    /// Class represents the Target type.
    /// </summary>
    [DataContract]
    [Serializable()]
    public enum TargetTypeOptions
    {
        #region → Properties     .

        /// <summary>
        /// Value given from StrategyApp.
        /// </summary>
        [EnumMember()]
        StrategyApp = 1,

        /// <summary>
        /// Value given from the PrefApp.
        /// </summary>
        [EnumMember()]
        PrefApp = 2,

        /// <summary>
        /// Value setted by the user
        /// </summary>
        [EnumMember()]
        UserDefined = 3,

        #endregion


    }
}
