using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    
    public class DTO_UserData 
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public DTO_UserData(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
