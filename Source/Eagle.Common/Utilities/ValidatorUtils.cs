using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Eagle.Common.Utilities
{
    public class ValidatorUtils
    {
        public static bool CheckInput(String s)
        {
            string inWord = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_";
            for (int i = 0; i < s.Length; i++)
            {
                if (inWord.IndexOf(s.Substring(i, 1)) < 0)
                {
                    return false;
                }
            }

            if (s.IndexOf(" insert ") != -1 || s.IndexOf(" update ") != -1 || s.IndexOf(" delete ") != -1 || s.IndexOf(" shutdown ") != -1 || s.IndexOf(" select ") != -1 || s.IndexOf(" and ") != -1 || s.IndexOf(" execute ") != -1 || s.IndexOf(" exec ") != -1 || s.IndexOf(" union ") != -1 || s.IndexOf(" or ") != -1 || s.IndexOf(" drop ") != -1)
                return false;
            return true;
        }

        //check number
        public static bool CheckNumber(string strInput)
        {
            bool result = false;
            if (strInput != null)
            {
                //string pattern=@"^\d+$"; //The input consists of one or more decimal digits; for example "5", or "5683874674".
                string pattern = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strInput);
            }
            return result;
        }

        public static bool IsGuid(string stringValue)
        {
            //string guidPattern = @"[a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}";
            string guidPattern = @"^\{?[0-9a-fA-F]{8}-?[0-9a-fA-F]{4}-?[0-9a-fA-F]{4}-?[0-9a-fA-F]{4}-?[0-9a-fA-F]{12}\}?$";
            if (string.IsNullOrEmpty(stringValue))
                return false;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(guidPattern);
            return regex.IsMatch(stringValue);
        }



        public static bool CheckLengthOfNumberInput(string strInput, int numeric_length)
        {

            bool result = false;
            if (strInput != null)
            {
                string pattern = @"^\d{" + numeric_length + "}$";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strInput);
            }
            return result;

        }

        public static bool CheckNumberInputLetters(string strInput, int strLength)
        {
            bool result = false;
            if (strInput != null)
            {
                string pattern = @"\w{0," + strLength + "}";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strInput);
            }
            return result;
        }

        public static bool ValidateMoney(string ParMoney)
        {
            try
            {
                Convert.ToDouble(ParMoney);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidateInput(string regex, string input)
        {
            //Alphaneumeric :^[^;>;&;<;%?*!~'`;:,."";+=|]*$
            //Address :^[^;>;&;<;%?$@{}*!~'`;:"";+=|]{1,200}$
            //User ID:^[a-zA-Z0-9._]{1,50}$
            //Alphanumeric with Spaces:^[a-zA-Z0-9_]{1,100}$
            //Phone No. :^[+0-9\s,-]{1,200}$
            //Numeric : ^[0-9]{1,18}$
            //Email :^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$
            //Decimal:^\d{0,7}(\.\d{1,2})?$
            //Time:((1+[012]+)|([123456789]{1}))(((:|\s)[0-5]+[0- 9]+))?(\s)?((a|A|p|P)(m|M))
            //Date Format:dd/MM/yyyy
            //Financial Year :^[\d]{4}[\-][\d]{2}$|^[\d]{4}[\-][\d]{4}$
            //URL: ^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$
            //Email: ^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$
            //Currency (non- negative) : ^\d+(\.\d\d)?$
            //Currency : ^(-)?\d+(\.\d\d)?$
            //Charactors [a-z][A-Z] only : ^[a-zA-Z'.\s]{1,40}$
            //Number Only: ^\d*([.]\d*)?|[.]\d+$
            //Money : RegExp( /^\$?(?:\d+|\d{1,3}(?:,\d{3})*)(?:\.\d{1,2}){0,1}$/ );    <=>  ^\$?[0-9]+(,[0-9]{3})*(\.[0-9]{2})?$
            //

            // Create a new Regex based on the specified regular expression.
            Regex r = new Regex(regex);

            // Test if the specified input matches the regular expression.
            return r.IsMatch(input);
        }

        //check validate HTTP or HTTPS URL
        public static bool checkHttps(string strInput)
        {
            bool result = false;
            if (strInput != null)
            {
                string pattern = @"^https?://([\w- ]+\.)+[\w-]+(/[\w- ./ ?%=]*)?$";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strInput);
            }
            return result;
        }

        //Internet e-mail address. 
        public static bool IsValidEmailAddress(string strInput)
        {
            bool result = false;

            if (strInput != null)
            {
                string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"; //The [\w-]+ expression indicates that each address element must consist of one or more word characters or hyphens 
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strInput);
            }
            return result;
        }

        public static bool checkCreditCardNumber(string strInput)
        {
            bool result = false;
            if (strInput != null)
            {
                string pattern = @"^\d{4}-?\d{4}-?\d{4}- ?\d{4}$";  //The credit card numbers string likes "4921835221552042" or "4921-8352- 2155-2042".
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strInput);
            }
            return result;
        }

        public static bool checkSimplePassword(string strInput)
        {
            bool result = false;
            if (strInput != null)
            {
                string pattern = @"^\w{6,8}$";  //The input consists of between 6 through 8 characters; for example "ghtd6f" or "b8c7hogh".^\w{6,8}$
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strInput);
            }
            return result;
        }

        public static bool IsValidPassword(string strPassword)
        {
            //^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,40}$ =>The following regular expression is for a 6 to 40 char password and containing at least an alphabet and one Number.         

            bool result = false;
            if (strPassword != null)
            {
                string pattern = @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,15})$";  //The input consists of between 6 through 15 characters
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strPassword);
            }
            return result;
        }

        public static bool checkComplexPassword(string strPassword, int num_letters)
        {
            //The regular expression enforces the following rules:
            //    * (?=.{"+num_letters+@",}) => Passwords will contain at least num_letters 
            //    * (?=.*[a-z])(?=.*[A-Z]) => Passwords will contain at least (1) upper case letter , (1) lower case letter
            //    * (?=.*[!@#$%^&_+-*/=])  => Passwords will contain at least (1) special character [.,;:'""~^><@#$%^&{}?!|\_+-*/=]
            //      (?=.*\W) => Passwords with no spaces
            //check combination upper/lowercase, no spaces, and some special characters ($#_!@). Min 6, max 10. ^(?=[\w$#_!@]{6,10})[\w$#_!@]{6,10}$
            //By the way on MSDN ^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$ is what the MVP is talking about which is listed as a custom expresion.

            //(?=^(?=.{14,256}$)(?=.*[A-Z].*[A-Z])(?=.*[a-z].*[a-z])(?=.*[0-9].*[0-9])(?=^[A-Za-z]{1})(?=.*[!@#$%^*_:].*[!@#$%^*_:])(?!.*[\s&quot;\s&amp;()+}{;=`~:\\|'?/>.<,])).*$ ////The following regular expressions are being used for password validation requiring two uppercase characters, two lowercase characters, two numbers, no spaces and two of the following special characters !@#$%^*_: with minimum of password length of 14          
            //(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$   =>Passwords will contain at least (1) upper case letter, (1) lower case letter, (1) number or special character, (8) characters in length, and maximum length should not be arbitrarily limited
            //^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$
            //^\w*(?=\w*\d)(?=\w*[a-z])(?=\w*[A-Z])\w*$/   //discovered that the previous one allows spaces
            //^(?=.*(\d|[^a-zA-Z]))(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,12}$
            //^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$
            //(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[a-z])(?=.*[A-Z]).*$  //(1) number or special character, (8) characters in length, maximum length should not be arbitrarily limited
            //(?=^[\w$#]{6,20}$)(?=.*?\d)(?=.*?[A-Z])(?=.*?[a-z])^[\w$#]*$ 

            //To turn off case sensitivity use (?-i) and to turn on multiline use (?m). So the regular expression would be: (?-i)(?m)^(?=.*?\d)(?=.*?[a-z])(?=.*?[A-Z])[a-zA-Z].{8,20}$ (I also added code to require a password to start with a character) 
            //Simply do EnableClientScript="False" and use single backslash(\), ie. ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$" (do not change the .* position otherwise it won't work.)
            //Regular expression to validate a password. Password must contain 6 characters and no more than 20, at least one upper case letter (A-Z), one lower case letter (a-z), and one numeric character (0-9). The other characters may be from the set A-Za-z0-9$#_\ plus blank. ^(?=[\w$#_ ]{6,20})(?=.*?\d)(?=.*?[A-Z])(?=.*?[a-z])[\w$#_ ]*$

            //Password must be at least 8 characters long, contain at least one one lower case letter, one upper case letter, one digit and one special character." 
            //PasswordRegularExpression = '^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$' 
            //PasswordRegularExpressionErrorMessage="Error: Your password must be at least 8 characters long, contain at least one one lower case letter, one upper case letter, one digit and one special character, Valid special characters."


            bool result = false;
            if (strPassword != null)
            {
                //string pattern = @"^.*(?=.{" + num_letters + @",})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[.,;'""~><?!@#$%^&{}|\_+-*/=:])(?=.*\W).*$";  
                string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[.,;'""~><?!@#$%^&{}|\_+-*/=:])(?=.*\W).{" + num_letters + ",}$";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.IsMatch(strPassword);
            }
            return result;
        }
    }
}
