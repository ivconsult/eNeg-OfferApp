using System;
using System.Web;

namespace citPOINT.OfferApp.Web
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        /// <value>The IP address.</value>
        public string IPAddress { get; set; }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            #region → Initialize sessio values   .

            if (Session["SessionUserEmail"] == null)
            {
                Session["SessionUserEmail"] = string.Empty;
            }
            if (Session["SessionUserID"] == null)
            {
                Session["SessionUserID"] = string.Empty;
            }
            if (Session["SessionUserFullName"] == null)
            {
                Session["SessionUserFullName"] = string.Empty;
            }
            #endregion
        }
    }
}