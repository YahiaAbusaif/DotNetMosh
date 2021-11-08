using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.Models
{
    public static class RoleName
    {
        public const string CanMangeMovies = "canMangeMovie";

        public const string CanMangeCustomers = "canMangeCustomers";


        //this function for handel if there are multi roles can access function
        public static string MultiRoles(string[] array)
        { 
            
            if(array == null || array.Length==0)
                return "";
            string result= "";
            var first=true;
            foreach(var element in array)
            {
                if(!first)
                    result+=", ";
                result+=element;
                first=false;
                }

            return result;
        }
    }
}