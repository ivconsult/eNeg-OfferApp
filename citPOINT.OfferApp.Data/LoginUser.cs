
#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using citPOINT.eNeg.Apps.Common.Interfaces;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 23.04.12     Yousra Reda       Creation
 * 23.04.12     Yousra Reda       Put needed properties related to User
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.OfferApp.Data
{
    /// <summary>
    /// LoginUser class derives from User class and implements IUser interface,
    /// it only exposes the following three data members to the client:
    /// </summary>
    public sealed partial class LoginUser : IUserInfo
    {
        #region → Properties     .

        /// <summary>
        /// Represent the UserID of current User
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName { get; set; }

        /// <summary>
        /// Represent the EmailAddress of current User
        /// </summary>]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Represent the whether the current User is locked or not
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// Represent the whether the current User is disabled or not
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Represent the CultureID of the current User 
        /// </summary>
        public int? CultureID { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the country ID.
        /// </summary>
        /// <value>The country ID.</value>
        public Guid? CountryID { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get { return this.EmailAddress; }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public bool? Gender { get; set; }

        /// <summary>
        /// Gets or sets the has public profile.
        /// </summary>
        /// <value>The has public profile.</value>
        public bool? HasPublicProfile { get; set; }

        /// <summary>
        /// Gets or sets the LCID.
        /// </summary>
        /// <value>The LCID.</value>
        public int? LCID { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>The mobile.</value>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the profile description.
        /// </summary>
        /// <value>The profile description.</value>
        public string ProfileDescription { get; set; }

        #endregion
    }
}
