using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    /// <summary>
    /// DTO for User Data
    /// </summary>
    public class DTO_UserData 
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public DTO_UserData(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
