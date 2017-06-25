
#region → Usings   .
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.Net;
using System.Web;
using System;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 23.02.12     Yousra Reda       Creation
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
    /// Service that gets and sets a Session to or from server side
    /// </summary>
    [EnableClientAccess()]
    public class SessionService : DomainService
    {

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Get the session of a given key and returns the value casted to string
        /// </summary>
        /// <param name="sessionKey">Key of session needed</param>
        /// <returns>Value of session in string format</returns>
        public string GetSessionValue(string[] sessionKey)
        {
            string[] Values = new string[sessionKey.Length];
            for (int i = 0; i < sessionKey.Length; i++)
            {
                if (System.Web.HttpContext.Current.Session[sessionKey[i]] != null)
                {
                    Values[i] = System.Web.HttpContext.Current.Session[sessionKey[i]].ToString();
                }
                else
                {
                    Values[i] = string.Empty;
                }
            }
            string result = string.Join("\\", Values);
            return result;
        }


        /// <summary>
        /// Sets the session value.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <param name="sessionValue">The session value.</param>
        public void SetSessionValue(string[] sessionKey, string[] sessionValue)
        {
            for (int i = 0; i < sessionKey.Length; i++)
            {
                System.Web.HttpContext.Current.Session[sessionKey[i]] = sessionValue[i];
            }
        }

        #endregion

        #endregion
    }
}