using System;

namespace BMC.Security.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        int SecurityUserID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the domain.
        /// </summary>
        /// <value>The name of the domain.</value>
        string WindowsUserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the culture info.
        /// </summary>
        /// <value>The culture info.</value>
        string CultureInfo
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the currency culture.
        /// </summary>
        /// <value>The currency culture.</value>
        string CurrencyCulture
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the date culture.
        /// </summary>
        /// <value>The date culture.</value>
        string DateCulture
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        DateTime CreatedDate
        {
            get; set;
        }

        DateTime PasswordChangeDate
        { 
            get; set;
        }

        bool isReset
        {
            get; set;
        }

        bool isLocked
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the First name.
        /// </summary>
        /// <value>The First name.</value>
        string First_Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Last name.
        /// </summary>
        /// <value>The Last name.</value>
        string Last_Name
        {
            get;
            set;
        }

        string DisplayName
        { get;}

        int User_No
        { get; set; }

        bool? IsUserTerminated { get; set; }
    }
}