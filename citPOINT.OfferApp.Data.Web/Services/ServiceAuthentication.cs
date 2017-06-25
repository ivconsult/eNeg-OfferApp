
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Configuration;
using System.Web;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 23.02.12    Yousra Reda       Creation
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
    /// Helper used to can check on the authentication of the caller 
    /// through checking incoming message header and properties 
    /// </summary>
    public static class ServiceAuthentication
    {
        #region → Methods        .

        #region → Public         .
        
        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid()
        {
            bool result = false;

            #region → Check on the incoming message to see whether the caller of this service is PrefApp itself or is external caller
            var messageProperties = OperationContext.Current.IncomingMessageProperties;

            RemoteEndpointMessageProperty endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            #endregion

            #region → In case that the caller is PrefApp       .
            if (!string.IsNullOrEmpty(messageProperties.Via.LocalPath) &&
                                      messageProperties.Via.LocalPath.Contains("/binary/"))
            {
                return true;
            }
            #endregion

            #region → In case that the caller is external one  .
            MessageHeaders msgHeaderElement = OperationContext.Current.IncomingMessageHeaders;

            // check if headers exist
            // 0 = one is missing, -2 = both are missing
            Int32 id = msgHeaderElement.FindHeader("username", "http://tempori.org") + msgHeaderElement.FindHeader("password", "http://tempori.org");

            if (id > 0)
            {
                String username = msgHeaderElement.GetHeader<String>("username", "http://tempori.org");
                String password = msgHeaderElement.GetHeader<String>("password", "http://tempori.org");
                if (username == ConfigurationManager.AppSettings["username"] && password == ConfigurationManager.AppSettings["password"])
                {
                    result = true;
                }
            }
            #endregion

            return result;
        }

        #endregion

        #endregion
    }            
}
