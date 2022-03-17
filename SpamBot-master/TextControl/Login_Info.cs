using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSwitch.TextControl
{
    class Login_Info
    {
        static int User_ID;
        public static int ID
        {
            get
            {
                return User_ID;
            }
            set
            {
                User_ID = value;
            }
        }

        static string Username;
        public static string U_Name
        {
            get
            {
                return Username;
            }
            set
            {
                Username = value;
            }
        }

        static string E_Mail;
        public static string email
        {
            get
            {
                return E_Mail;
            }
            set
            {
                E_Mail = value;
            }
        }

        static bool Is_Admin;
        public static bool admin
        {
            get
            {
                return Is_Admin;
            }
            set
            {
                Is_Admin = value;
            }
        }
    }
}
