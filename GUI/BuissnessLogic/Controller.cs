using System;

namespace BuissnessLogic
{
    public class Controller
    {



        public bool checkLogin()
        {
            return DTO.isUserRegistered(UserID, UserPassword);
        }
    }

    
    
}
