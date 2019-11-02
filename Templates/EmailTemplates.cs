using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qwerty1.Templates
{
    public class EmailTemplates
    {

        /// <summary>
        ///  {0}= User Display Name
        ///  {1}=username
        ///  {2}=password
        /// </summary>
        public static string WelcomeEmail = "Hi <b>{0}</b>,<br><br>Welcome to QWERTY. We are happy to see you have registered in our system.<br><br>Your registered username = {1} <br> Your Password = {2} <br><br>Regards,<br>QWERTY Team";



        /// <summary>
        ///  {0}= User Display Name
        ///  {1}=username
        ///  {2}=code
        /// </summary>
        public static string WelcomeEmail1= "Hi <b>{0}</b>,<br><br>Welcome to QWERTY. We are happy to see you have registered in our system.<br><br>Your registered username = {1} <br> Your Password = {2} <br><br>Regards,<br>QWERTY Team";




    }
}
