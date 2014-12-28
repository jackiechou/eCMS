using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Security.Cryptography;

namespace Eagle.Common.Utilities
{
    public static class StringUtils
    {  
        /// <summary>
        /// Convert string "on" to Bool
        /// </summary>
        /// <param name="stron"></param>
        /// <returns></returns>
        public static bool ConvertOnOffToBool(object obj)
        {
            if (obj == null)
                return false;

            if ((obj.ToString().ToLower() == "on") || (obj.ToString().ToLower() == "true"))
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Replace last occurance of string in a main string
        /// </summary>
        /// <param name="Source">Main String</param>
        /// <param name="Find">What To Search</param>
        /// <param name="Replace">What To Replace</param>
        /// <returns>Replaced String </returns>
        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public static string FormatDays(double mDays)
        {
            string Ret = string.Empty;
            int Year = 0;
            int Month = 0;
            int Weeak = 0;
            int Days = 0;
            int Reminder = 0;
            if (mDays > 360 || mDays == 360)
            {
                Reminder = (int)mDays % 360;
                Year = (int)mDays / 360;
                if (Reminder > 30 || Reminder == 30)
                {

                    Month = Reminder / 30;
                    Reminder = Reminder % 30;
                    if (Reminder > 7)
                    {
                        Weeak = Reminder / 7;
                        Reminder = Reminder % 7;
                        if (Reminder > 0)
                        {
                            Days = Reminder;
                        }
                    }
                    else
                    {
                        Days = Reminder;
                    }
                }
                else
                {
                    if (Reminder > 7)
                    {
                        Weeak = Reminder / 7;
                        Reminder = Reminder % 7;
                        if (Reminder > 0)
                        {
                            Days = Reminder;
                        }
                    }
                    else
                    {
                        Days = Reminder;
                    }
                }
            }
            else if (mDays > 30 || mDays == 30)
            {
                Reminder = (int)mDays % 30;
                Month = (int)mDays / 30;
                if (Reminder > 7 || Reminder == 7)
                {
                    Weeak = Reminder / 7;
                    Reminder = Reminder % 7;
                    if (Reminder > 0)
                    {
                        Days = Reminder;
                    }
                }
                else
                {
                    Days = Reminder;
                }
            }
            else if (mDays > 7 || mDays == 7)
            {
                Reminder = (int)mDays % 7;
                Weeak = (int)mDays / 7;
                if (Reminder > 0)
                {
                    Days = Reminder;
                }
            }
            else
            {
                Days = (int)mDays;
            }

            if (Year > 0)
            {
                if (Year > 1)
                    Ret += Year + " Years  ";
                else
                    Ret += Year + " Year  ";

            }
            if (Month > 0)
            {
                if (Month > 1)
                    Ret += Month + " Months  ";
                else
                    Ret += Month + " Month  ";
            }
            if (Weeak > 0)
            {
                if (Weeak > 1)
                    Ret += Weeak + " Weeaks  ";
                else
                    Ret += Weeak + " Weeak  ";
            }
            if (Days > 0)
            {
                if (Days > 1)
                    Ret += Days + " Days  ";
                else
                    Ret += Days + " Day  ";
            }
            return Ret;
        }

        public static string FormatRating(int RateValue)
        {
            string ret = string.Empty;
            switch (RateValue)
            {
                case 1:
                    ret = "Basic level";
                    break;
                case 2:
                    ret = "Normal";
                    break;
                case 3:
                    ret = "Good";
                    break;
                case 4:
                    ret = "Advance";
                    break;
                case 5:
                    ret = "Fluient";
                    break;
            }
            return ret;
        }

        public static string FormatbitValue(string bitValue)
        {
            string ret = "No";
            if (bitValue == "1")
            {
                ret = "Yes";
            }
            return ret;
        }

        public static bool CheckIgnorWords(string SearchString)
        {
            string IgnorWords = string.Empty;
            IgnorWords = "^ , ; : [] ] [ {} () } { ) ( _ = < > . + - \\ / \" \"\" ' ! % * @~ @# @& &? & # ? about 1 after 2 all also 3 an 4 and 5 another 6 any 7 are 8 as 9 at 0 be $ because been before being between both but by came can come could did do each for from get got has had he have her here him himself his how if in into is it like make many me might more most much must my never now of on only or other our out over said same see should since some still such take than that the their them then there these they this those through to too under up very was way we well were what where which while who with would you your a b c d e f g h i j k l m n o p q r s t u v w x y z";
            SearchString = RemoveSpecialSymbols(SearchString);
            if (IgnorWords.Contains(SearchString) || SearchString.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string RemoveUnnessaryKeywords(string SearchString)
        {
            string IgnorWords = string.Empty;
            IgnorWords = "^ , ; : [] ] [ {} () } { ) ( _ = < > . + - \\ / \" \"\" ' ! % * @~ @# @& &? & # ? about 1 after 2 all also 3 an 4 and 5 another 6 any 7 are 8 as 9 at 0 be $ because been before being between both but by came can come could did do each for from get got has had he have her here him himself his how if in into is it like make many me might more most much must my never now of on only or other our out over said same see should since some still such take than that the their them then there these they this those through to too under up very was way we well were what where which while who with would you your a b c d e f g h i j k l m n o p q r s t u v w x y z";
            SearchString = RemoveSpecialSymbols(SearchString);
            return SearchString;
        }

        public static string RemoveSpecialSymbols(string SearchString)
        {
            SearchString = SearchString.Replace("\"", "");
            SearchString = SearchString.Replace("@", "");
            SearchString = SearchString.Replace("?", "");
            SearchString = SearchString.Replace(":", "");
            SearchString = SearchString.Replace(";", "");
            SearchString = SearchString.Replace("_", "");
            SearchString = SearchString.Replace("=", "");
            SearchString = SearchString.Replace("<", "");
            SearchString = SearchString.Replace(">", "");
            SearchString = SearchString.Replace("[", "");
            SearchString = SearchString.Replace("]", "");
            SearchString = SearchString.Replace("{", "");
            SearchString = SearchString.Replace("}", "");
            SearchString = SearchString.Replace("!", "");
            SearchString = SearchString.Replace("#", "");
            SearchString = SearchString.Replace(",", "");
            SearchString = SearchString.Replace("-", "");
            SearchString = SearchString.Replace(".", "");
            SearchString = SearchString.Replace("^", "");
            SearchString = SearchString.Replace("(", "");
            SearchString = SearchString.Replace(")", "");
            SearchString = SearchString.Replace("/", "");
            SearchString = SearchString.Replace("~", "");
            SearchString = SearchString.Replace("|", "");
            SearchString = SearchString.Replace("$", "");
            SearchString = SearchString.Replace("%", "");
            SearchString = SearchString.Replace("&", "");
            SearchString = SearchString.Replace("*", "");
            SearchString = SearchString.Replace("and", "");
            return SearchString;
        }

        public static string Trim(string info)
        {
            if (String.IsNullOrEmpty(info) || String.IsNullOrWhiteSpace(info))
            {
                return null;
            }
            return info.Trim();
        }
        public static string EncodePassword(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            byte[] binarySource = Encoding.UTF8.GetBytes(source);
            System.Security.Cryptography.SymmetricAlgorithm rijn = System.Security.Cryptography.SymmetricAlgorithm.Create();
            MemoryStream ms = new MemoryStream();
            byte[] rgbIV = Encoding.ASCII.GetBytes("lkjhasdfyuiwhcnt");
            byte[] key = Encoding.ASCII.GetBytes("tkw123aaaa");
            CryptoStream cs = new CryptoStream(ms, rijn.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);
            cs.Write(binarySource, 0, binarySource.Length);
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecodePassword(string source)
        {
            byte[] binarySource = Convert.FromBase64String(source);
            MemoryStream ms = new MemoryStream();
            System.Security.Cryptography.SymmetricAlgorithm rijn = System.Security.Cryptography.SymmetricAlgorithm.Create();
            byte[] rgbIV = Encoding.ASCII.GetBytes("lkjhasdfyuiwhcnt");
            byte[] key = Encoding.ASCII.GetBytes("tkw123aaaa");
            CryptoStream cs = new CryptoStream(ms, rijn.CreateDecryptor(key, rgbIV),
            CryptoStreamMode.Write);
            cs.Write(binarySource, 0, binarySource.Length);
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        //Get MD5
        public static string GetMd5Sum(string str)
        {
            // First we need to convert the string into bytes, which
            // means using a text encoder.

            Encoder enc = System.Text.Encoding.Unicode.GetEncoder();
            // Create a buffer large enough to hold the string

            byte[] unicodeText = new byte[str.Length * 2];

            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            // Now that we have a byte array we can ask the CSP to hash it

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] result = md5.ComputeHash(unicodeText);

            // Build the final string by converting each byte

            // into hex and appending it to a StringBuilder

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {

                sb.Append(result[i].ToString("X2"));

            }
            // And return it
            return sb.ToString();
        }

        public static string[] ConvertStringToArray(string strInput)
        {
            string[] arrayString = strInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return arrayString;
        }

        public static string ConvertToNoMarkString(string text)
        {
            try
            {
                //Ky tu dac biet

                for (int i = 32; i < 48; i++)
                {
                    text = text.Replace(((char)i).ToString(), "-");
                }
                text = text.Replace(".", "");
                text = text.Replace("?", "");
                text = text.Replace("\"", "");
                text = text.Replace(" ", "-");
                text = text.Replace(",", "-");
                text = text.Replace(";", "-");
                text = text.Replace(":", "-");

                text = text.Replace("–", "");
                text = text.Replace("“", "");
                text = text.Replace("”", "");

                text = text.Replace("(", "-");
                text = text.Replace(")", "-");
                text = text.Replace("@", "-");
                text = text.Replace("&", "-");
                text = text.Replace("*", "-");
                text = text.Replace("\\", "-");
                text = text.Replace("+", "-");
                text = text.Replace("/", "-");
                text = text.Replace("#", "-");
                text = text.Replace("$", "-");
                text = text.Replace("%", "-");
                text = text.Replace("^", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                if (text.Substring(0, 1) == "-")
                {
                    text = text.Substring(1);
                }
                if (text.Substring(text.Length - 1) == "-")
                {
                    text = text.Substring(0, text.Length - 1);
                }
                //'Dấu Ngang
                text = text.Replace("A", "A");
                text = text.Replace("a", "a");
                text = text.Replace("Ă", "A");
                text = text.Replace("ă", "a");
                text = text.Replace("Â", "A");
                text = text.Replace("â", "a");
                text = text.Replace("E", "E");
                text = text.Replace("e", "e");
                text = text.Replace("Ê", "E");
                text = text.Replace("ê", "e");
                text = text.Replace("I", "I");
                text = text.Replace("i", "i");
                text = text.Replace("O", "O");
                text = text.Replace("o", "o");
                text = text.Replace("Ô", "O");
                text = text.Replace("ô", "o");
                text = text.Replace("Ơ", "O");
                text = text.Replace("ơ", "o");
                text = text.Replace("U", "U");
                text = text.Replace("u", "u");
                text = text.Replace("Ư", "U");
                text = text.Replace("ư", "u");
                text = text.Replace("Y", "Y");
                text = text.Replace("y", "y");

                //    'Dấu Huyền
                text = text.Replace("À", "A");
                text = text.Replace("à", "a");
                text = text.Replace("Ằ", "A");
                text = text.Replace("ằ", "a");
                text = text.Replace("Ầ", "A");
                text = text.Replace("ầ", "a");
                text = text.Replace("È", "E");
                text = text.Replace("è", "e");
                text = text.Replace("Ề", "E");
                text = text.Replace("ề", "e");
                text = text.Replace("Ì", "I");
                text = text.Replace("ì", "i");
                text = text.Replace("Ò", "O");
                text = text.Replace("ò", "o");
                text = text.Replace("Ồ", "O");
                text = text.Replace("ồ", "o");
                text = text.Replace("Ờ", "O");
                text = text.Replace("ờ", "o");
                text = text.Replace("Ù", "U");
                text = text.Replace("ù", "u");
                text = text.Replace("Ừ", "U");
                text = text.Replace("ừ", "u");
                text = text.Replace("Ỳ", "Y");
                text = text.Replace("ỳ", "y");

                //'Dấu Sắc
                text = text.Replace("Á", "A");
                text = text.Replace("á", "a");
                text = text.Replace("Ắ", "A");
                text = text.Replace("ắ", "a");
                text = text.Replace("Ấ", "A");
                text = text.Replace("ấ", "a");
                text = text.Replace("É", "E");
                text = text.Replace("é", "e");
                text = text.Replace("Ế", "E");
                text = text.Replace("ế", "e");
                text = text.Replace("Í", "I");
                text = text.Replace("í", "i");
                text = text.Replace("Ó", "O");
                text = text.Replace("ó", "o");
                text = text.Replace("Ố", "O");
                text = text.Replace("ố", "o");
                text = text.Replace("Ớ", "O");
                text = text.Replace("ớ", "o");
                text = text.Replace("Ú", "U");
                text = text.Replace("ú", "u");
                text = text.Replace("Ứ", "U");
                text = text.Replace("ứ", "u");
                text = text.Replace("Ý", "Y");
                text = text.Replace("ý", "y");

                //'Dấu Hỏi
                text = text.Replace("Ả", "A");
                text = text.Replace("ả", "a");
                text = text.Replace("Ẳ", "A");
                text = text.Replace("ẳ", "a");
                text = text.Replace("Ẩ", "A");
                text = text.Replace("ẩ", "a");
                text = text.Replace("Ẻ", "E");
                text = text.Replace("ẻ", "e");
                text = text.Replace("Ể", "E");
                text = text.Replace("ể", "e");
                text = text.Replace("Ỉ", "I");
                text = text.Replace("ỉ", "i");
                text = text.Replace("Ỏ", "O");
                text = text.Replace("ỏ", "o");
                text = text.Replace("Ổ", "O");
                text = text.Replace("ổ", "o");
                text = text.Replace("Ở", "O");
                text = text.Replace("ở", "o");
                text = text.Replace("Ủ", "U");
                text = text.Replace("ủ", "u");
                text = text.Replace("Ử", "U");
                text = text.Replace("ử", "u");
                text = text.Replace("Ỷ", "Y");
                text = text.Replace("ỷ", "y");

                //'Dấu Ngã   
                text = text.Replace("Ã", "A");
                text = text.Replace("ã", "a");
                text = text.Replace("Ẵ", "A");
                text = text.Replace("ẵ", "a");
                text = text.Replace("Ẫ", "A");
                text = text.Replace("ẫ", "a");
                text = text.Replace("Ẽ", "E");
                text = text.Replace("ẽ", "e");
                text = text.Replace("Ễ", "E");
                text = text.Replace("ễ", "e");
                text = text.Replace("Ĩ", "I");
                text = text.Replace("ĩ", "i");
                text = text.Replace("Õ", "O");
                text = text.Replace("õ", "o");
                text = text.Replace("Ỗ", "O");
                text = text.Replace("ỗ", "o");
                text = text.Replace("Ỡ", "O");
                text = text.Replace("ỡ", "o");
                text = text.Replace("Ũ", "U");
                text = text.Replace("ũ", "u");
                text = text.Replace("Ữ", "U");
                text = text.Replace("ữ", "u");
                text = text.Replace("Ỹ", "Y");
                text = text.Replace("ỹ", "y");

                //'Dẫu Nặng
                text = text.Replace("Ạ", "A");
                text = text.Replace("ạ", "a");
                text = text.Replace("Ặ", "A");
                text = text.Replace("ặ", "a");
                text = text.Replace("Ậ", "A");
                text = text.Replace("ậ", "a");
                text = text.Replace("Ẹ", "E");
                text = text.Replace("ẹ", "e");
                text = text.Replace("Ệ", "E");
                text = text.Replace("ệ", "e");
                text = text.Replace("Ị", "I");
                text = text.Replace("ị", "i");
                text = text.Replace("Ọ", "O");
                text = text.Replace("ọ", "o");
                text = text.Replace("Ộ", "O");
                text = text.Replace("ộ", "o");
                text = text.Replace("Ợ", "O");
                text = text.Replace("ợ", "o");
                text = text.Replace("Ụ", "U");
                text = text.Replace("ụ", "u");
                text = text.Replace("Ự", "U");
                text = text.Replace("ự", "u");
                text = text.Replace("Ỵ", "Y");
                text = text.Replace("ỵ", "y");
                text = text.Replace("Đ", "D");
                text = text.Replace("đ", "d");
            }
            catch
            {
            }
            return text.ToLower();

        }

        public static bool CheckValidName(string strInput){
	        string pattern = @"[^a-zA-Z\s]$";
	        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
	        Match match = regex.Match(strInput);
	        int matchCount = 0;
	        bool isValid = false;
	        if (match.Success) 
	           ++matchCount;
            if (matchCount > 0)
                isValid = false;
            else
                isValid = true;
	         return isValid;
        }

        public static string GetEmailAccount(string strInput)
        {
            int length = strInput.IndexOf('@');
            string email_account = strInput.Substring(0, length);
            return email_account;
        }

        public static string ConvertToUnSign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');

            sb = sb.Replace('á', 'a');
            sb = sb.Replace('à', 'a');
            sb = sb.Replace('ả', 'a');
            sb = sb.Replace('ã', 'a');
            sb = sb.Replace('ạ', 'a');

            sb = sb.Replace('ă', 'a');
            sb = sb.Replace('ắ', 'a');
            sb = sb.Replace('ằ', 'a');
            sb = sb.Replace('ẳ', 'a');
            sb = sb.Replace('ẵ', 'a');
            sb = sb.Replace('ặ', 'a');

            sb = sb.Replace('é', 'e');
            sb = sb.Replace('è', 'e');
            sb = sb.Replace('ẻ', 'e');
            sb = sb.Replace('ẽ', 'e');
            sb = sb.Replace('ẹ', 'e');

            sb = sb.Replace('ê', 'e');
            sb = sb.Replace('ế', 'e');
            sb = sb.Replace('ề', 'e');
            sb = sb.Replace('ể', 'e');
            sb = sb.Replace('ễ', 'e');
            sb = sb.Replace('ệ', 'e');


            sb = sb.Replace('í', 'i');
            sb = sb.Replace('ì', 'i');
            sb = sb.Replace('ỉ', 'i');
            sb = sb.Replace('ĩ', 'i');
            sb = sb.Replace('ị', 'i');

            sb = sb.Replace('ó', 'o');
            sb = sb.Replace('ò', 'o');
            sb = sb.Replace('ỏ', 'o');
            sb = sb.Replace('õ', 'o');
            sb = sb.Replace('ọ', 'o');

            sb = sb.Replace('ô', 'o');
            sb = sb.Replace('ố', 'o');
            sb = sb.Replace('ồ', 'o');
            sb = sb.Replace('ổ', 'o');
            sb = sb.Replace('ỗ', 'o');
            sb = sb.Replace('ộ', 'o');

            sb = sb.Replace('ú', 'u');
            sb = sb.Replace('ù', 'u');
            sb = sb.Replace('ủ', 'u');
            sb = sb.Replace('ũ', 'u');
            sb = sb.Replace('ụ', 'u');

            sb = sb.Replace('ý', 'y');
            sb = sb.Replace('ỳ', 'y');
            sb = sb.Replace('ỷ', 'y');
            sb = sb.Replace('ỹ', 'y');
            sb = sb.Replace('ỵ', 'y');

            //Capital letter
            sb = sb.Replace('Á', 'A');
            sb = sb.Replace('À', 'A');
            sb = sb.Replace('Ả', 'A');
            sb = sb.Replace('Ã', 'A');
            sb = sb.Replace('Ạ', 'A');

            sb = sb.Replace('Ă', 'A');
            sb = sb.Replace('Ắ', 'A');
            sb = sb.Replace('Ằ', 'A');
            sb = sb.Replace('Ẳ', 'A');
            sb = sb.Replace('Ẵ', 'A');
            sb = sb.Replace('Ặ', 'A');

            sb = sb.Replace('É', 'E');
            sb = sb.Replace('È', 'E');
            sb = sb.Replace('Ẻ', 'E');
            sb = sb.Replace('Ẽ', 'E');
            sb = sb.Replace('Ẹ', 'E');

            sb = sb.Replace('Ê', 'E');
            sb = sb.Replace('Ế', 'E');
            sb = sb.Replace('Ề', 'E');
            sb = sb.Replace('Ể', 'E');
            sb = sb.Replace('Ễ', 'E');
            sb = sb.Replace('Ệ', 'E');

            sb = sb.Replace('Í', 'I');
            sb = sb.Replace('Ì', 'I');
            sb = sb.Replace('Ỉ', 'I');
            sb = sb.Replace('Ĩ', 'I');
            sb = sb.Replace('Ị', 'I');

            sb = sb.Replace('Ó', 'O');
            sb = sb.Replace('Ò', 'O');
            sb = sb.Replace('Ỏ', 'O');
            sb = sb.Replace('Õ', 'O');
            sb = sb.Replace('Ọ', 'O');

            sb = sb.Replace('Ô', 'O');
            sb = sb.Replace('Ố', 'O');
            sb = sb.Replace('Ồ', 'O');
            sb = sb.Replace('Ổ', 'O');
            sb = sb.Replace('Ỗ', 'O');
            sb = sb.Replace('Ộ', 'O');


            sb = sb.Replace('Ú', 'U');
            sb = sb.Replace('Ù', 'U');
            sb = sb.Replace('Ủ', 'U');
            sb = sb.Replace('Ũ', 'U');
            sb = sb.Replace('Ụ', 'U');

            sb = sb.Replace('Ý', 'Y');
            sb = sb.Replace('Ỳ', 'Y');
            sb = sb.Replace('Ỷ', 'Y');
            sb = sb.Replace('Ỹ', 'Y');
            sb = sb.Replace('Ỵ', 'Y');

            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        public static string cutSubString(string content, int num)
        {
            content = removeHTMLtab(content, "");
            content = CutLimitText(content, num);
            return content;
        }

        public static string createTags(string tags)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(tags.ToLower());
            sb.Replace("\"", " ");
            sb.Replace(",", " ");
            sb.Replace("&amp", " ");
            sb.Replace("&", " ");
            string result = UTF8_Encode(removeHTMLtab(sb.ToString(), ""));
            return result;
        }

        public static string convertTitle2Alias(string title)
        {
            string result = string.Empty;
            string strSource = removeHTMLtab(title, "");

            System.Text.StringBuilder sb = new System.Text.StringBuilder(strSource.ToLower().TrimStart());
            sb.Replace(",", "-");
            sb.Replace(' ', '-');
            sb.Replace(":", "");
            sb.Replace("\"", "");
            sb.Replace("%", "");
            sb.Replace("?", "");
            sb.Replace("&amp", "-");
            sb.Replace("&", "-");
            sb.Replace("--", "-");
            result = RemoveSign4VietnameseString(sb.ToString());
            return result;
        }

        public static string convertTitle2Link(string title)
        {
            string result = string.Empty;
            string strSource = removeHTMLtab(title, "");

            System.Text.StringBuilder sb = new System.Text.StringBuilder(strSource.ToLower().TrimStart());
            sb.Replace("-", "");
            sb.Replace(' ', '-');
            sb.Replace(":", "");
            sb.Replace("\"", "");
            sb.Replace("%", "");
            sb.Replace("?", "");
            sb.Replace("&amp", "-");
            sb.Replace("&", "-");
            sb.Replace("--", "-");
            result = RemoveSign4VietnameseString(sb.ToString());
            return result;
        }

        public static string convertName2Link(string title)
        {
            string result = string.Empty;
            string strSource = removeHTMLtab(title, "");
            System.Text.StringBuilder sb = new System.Text.StringBuilder(strSource.ToLower());
            sb.Replace(' ', '_');
            sb.Replace("\"", "");
            sb.Replace(",", "_");
            sb.Replace("&amp", "_");
            sb.Replace("&", "_");
            result = RemoveSign4VietnameseString(sb.ToString());
            return result;
        }

        public static string ConvertVNIToUnicode(string VNI)
        {
            string sUnicode = "";
            int iChieuDai = 0;
            if (VNI.Length == 0) return "";
            iChieuDai = VNI.Length - 1;
            int i = 0;
            while (i <= iChieuDai)
            {
                switch ((byte)VNI[i])
                {
                    case 97: //, 101, 105, 111, 117 'a,e,i,o,u
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 249: sUnicode += "á"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "à"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ả"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ã"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ạ"; i += 2; //dấu nặng
                                    break;
                                case 226: sUnicode += "â"; i += 2; //^
                                    break;
                                case 225: sUnicode += "ấ"; i += 2; //ấ
                                    break;
                                case 224: sUnicode += "ầ"; i += 2; //ầ
                                    break;
                                case 229: sUnicode += "ẩ"; i += 2; //ẩ
                                    break;
                                case 227: sUnicode += "ẫ"; i += 2; //ẫ
                                    break;
                                case 228: sUnicode += "ậ"; i += 2; //ậ
                                    break;
                                case 234: sUnicode += "ă"; i += 2; //ă
                                    break;
                                case 233: sUnicode += "ắ"; i += 2; //ắ
                                    break;
                                case 232: sUnicode += "ằ"; i += 2; //ằ
                                    break;
                                case 250: sUnicode += "ẳ"; i += 2; //ẳ
                                    break;
                                case 252: sUnicode += "ẵ"; i += 2; //ẵ
                                    break;
                                case 235: sUnicode += "ặ"; i += 2; //ặ
                                    break;
                                default: sUnicode += "a"; i += 1;
                                    break;
                            }
                        }
                        else
                        {
                            sUnicode += "a"; i += 1;
                        }
                        break;
                    case 65: //A
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 217: sUnicode += "Á"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "À"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ả"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ã"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ạ"; i += 2; //dấu nặng
                                    break;
                                case 194: sUnicode += "Â"; i += 2; //^
                                    break;
                                case 193: sUnicode += "Ấ"; i += 2; //ấ
                                    break;
                                case 192: sUnicode += "Ầ"; i += 2; //ầ
                                    break;
                                case 197: sUnicode += "Ẩ"; i += 2; //ẩ
                                    break;
                                case 195: sUnicode += "Ẫ"; i += 2; //ẫ
                                    break;
                                case 196: sUnicode += "Ậ"; i += 2; //ậ
                                    break;
                                case 202: sUnicode += "Ă"; i += 2; //ă
                                    break;
                                case 201: sUnicode += "Ắ"; i += 2; //ắ
                                    break;
                                case 200: sUnicode += "Ằ"; i += 2; //ằ
                                    break;
                                case 218: sUnicode += "Ẳ"; i += 2; //ẳ
                                    break;
                                case 220: sUnicode += "Ẵ"; i += 2; //ẵ
                                    break;
                                case 203: sUnicode += "Ặ"; i += 2; //ặ
                                    break;
                                //Trường hợp bị lỗi
                                case 249: sUnicode += "Á"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "À"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "Ả"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "Ã"; i += 2;  //dấu ngã
                                    break;
                                case 239: sUnicode += "Ạ"; i += 2; //dấu nặng
                                    break;
                                case 226: sUnicode += "Â"; i += 2; //^
                                    break;
                                case 225: sUnicode += "Ấ"; i += 2; //ấ
                                    break;
                                case 224: sUnicode += "Ầ"; i += 2; //ầ
                                    break;
                                case 229: sUnicode += "Ẩ"; i += 2; //ẩ
                                    break;
                                case 227: sUnicode += "Ẫ"; i += 2; //ẫ
                                    break;
                                case 228: sUnicode += "Ậ"; i += 2; //ậ
                                    break;
                                case 234: sUnicode += "Ă"; i += 2; //ă
                                    break;
                                case 233: sUnicode += "Ắ"; i += 2; //ắ
                                    break;
                                case 232: sUnicode += "Ằ"; i += 2; //ằ
                                    break;
                                case 250: sUnicode += "Ẳ"; i += 2; //ẳ
                                    break;
                                case 252: sUnicode += "Ẵ"; i += 2; //ẵ
                                    break;
                                case 235: sUnicode += "Ặ"; i += 2; //ặ
                                    break;
                                default: sUnicode += "A"; i += 1; break;
                            }
                        }
                        else { sUnicode += "A"; i += 1; } break;
                    case 101: //e
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 249: sUnicode += "é"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "è"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ẻ"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ẽ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ẹ"; i += 2; //dấu nặng
                                    break;
                                case 226: sUnicode += "ê"; i += 2; //^
                                    break;
                                case 225: sUnicode += "ế"; i += 2; //ấ
                                    break;
                                case 224: sUnicode += "ề"; i += 2; //ầ
                                    break;
                                case 229: sUnicode += "ể"; i += 2; //ẩ
                                    break;
                                case 227: sUnicode += "ễ"; i += 2; //ẫ
                                    break;
                                case 228: sUnicode += "ệ"; i += 2; //ậ
                                    break;
                                default: sUnicode += "e"; i += 1;
                                    break;
                            }
                        }
                        else { sUnicode += "e"; i += 1; } break;
                    case 69:
                        //E
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 217: sUnicode += "É"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "È"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ẻ"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ẽ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ẹ"; i += 2; //dấu nặng
                                    break;
                                case 194: sUnicode += "Ê"; i += 2; //^
                                    break;
                                case 193: sUnicode += "Ế"; i += 2; //ấ
                                    break;
                                case 192: sUnicode += "Ề"; i += 2; //ầ
                                    break;
                                case 197: sUnicode += "Ể"; i += 2; //ẩ
                                    break;
                                case 195: sUnicode += "Ễ"; i += 2; //ẫ
                                    break;
                                case 196: sUnicode += "Ệ"; i += 2; //ậ
                                    break;
                                default: sUnicode += "E"; i += 1;
                                    break;
                            }
                        }
                        else { sUnicode += "E"; i += 1; } break;
                    case 111:
                        //o
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 249: sUnicode += "ó"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ò"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ỏ"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "õ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ọ"; i += 2; //dấu nặng
                                    break;
                                case 226: sUnicode += "ô"; i += 2; //^
                                    break;
                                case 225: sUnicode += "ố"; i += 2; //ấ
                                    break;
                                case 224: sUnicode += "ồ"; i += 2; //ầ
                                    break;
                                case 229: sUnicode += "ổ"; i += 2; //ẩ
                                    break;
                                case 227: sUnicode += "ỗ"; i += 2; //ẫ
                                    break;
                                case 228: sUnicode += "ộ"; i += 2; //ậ
                                    break;
                                default: sUnicode += "o"; i += 1;
                                    break;
                            }
                        }
                        else { sUnicode += "o"; i += 1; } break;
                    case 79: //O
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 217: sUnicode += "Ó"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ò"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ỏ"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Õ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ọ"; i += 2; //dấu nặng
                                    break;
                                case 194: sUnicode += "Ô"; i += 2; //^
                                    break;
                                case 193: sUnicode += "Ố"; i += 2; //ấ
                                    break;
                                case 192: sUnicode += "Ồ"; i += 2; //ầ
                                    break;
                                case 197: sUnicode += "Ổ"; i += 2; //ẩ
                                    break;
                                case 195: sUnicode += "Ỗ"; i += 2; //ẫ
                                    break;
                                case 196: sUnicode += "Ộ"; i += 2; //ậ
                                    break;
                                default: sUnicode += "O"; i += 1; break;
                            }
                        }
                        else { sUnicode += "O"; i += 1; } break;
                    case 117: //u
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 249: sUnicode += "ú"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ù"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ủ"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ũ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ụ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "u"; i += 1; break;
                            }
                        }
                        else { sUnicode += "u"; i += 1; } break;
                    case 85: //U
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 217: sUnicode += "Ú"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ù"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ủ"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ũ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ụ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "U"; i += 1; break;
                            }
                        }
                        else { sUnicode += "U"; i += 1; } break;
                    case 244: //ơ
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 249: sUnicode += "ớ"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ờ"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ở"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ỡ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ợ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "ơ"; i += 1; break;
                            }
                        }
                        else
                        {
                            sUnicode += "ơ";
                            i += 1;
                        }
                        break;
                    case 212: //Ơ
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 217: sUnicode += "Ớ"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ờ"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ở"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ỡ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ợ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "Ơ"; i += 1; break;
                            }
                        }
                        else { sUnicode += "Ơ"; i += 1; } break;
                    case 246: //ư
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 249: sUnicode += "ứ"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ừ"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ử"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ữ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ự"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "ư"; i += 1;
                                    break;
                            }
                        }
                        else { sUnicode += "ư"; i += 1; } break;
                    case 214: //Ư
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 217: sUnicode += "Ứ"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ừ"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ử"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ữ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ự"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "Ư"; i += 1; break;
                            }
                        }
                        else { sUnicode += "Ư"; i += 1; } break;
                    case 121: //y
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 249: sUnicode += "ý"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ỳ"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ỷ"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ỹ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ỵ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "y"; i += 1; break;
                            }
                        }
                        else { sUnicode += "y"; i += 1; }
                        break;
                    case 89: //Y
                        if (i < iChieuDai)
                        {
                            switch ((byte)VNI[i + 1])
                            {
                                case 217: sUnicode += "Ý"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ỳ"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ỷ"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ỹ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ỵ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "Y"; i += 1; break;
                            }
                        }
                        else { sUnicode += "Y"; i += 1; } break;
                    case 237: sUnicode += "í"; i += 1; break;
                    case 236: sUnicode += "ì"; i += 1; break;
                    case 230: sUnicode += "ỉ"; i += 1; break;
                    case 243: sUnicode += "ĩ"; i += 1; break;
                    case 242: sUnicode += "ị"; i += 1; break;
                    case 205: sUnicode += "Í"; i += 1; break;
                    case 204: sUnicode += "Ì"; i += 1; break;
                    case 198: sUnicode += "Ỉ"; i += 1; break;
                    case 211: sUnicode += "Ĩ"; i += 1; break;
                    case 210: sUnicode += "Ị"; i += 1; break;
                    case 241: sUnicode += "đ"; i += 1; break;
                    case 209: sUnicode += "Đ"; i += 1; break;
                    case 238:
                    case 255: sUnicode += "ỵ"; i += 1; break;
                    case 159:
                    case 206: sUnicode += "Ỵ"; i += 1; break;
                    default:
                        sUnicode += VNI[i]; i += 1; break;
                }
            }
            return sUnicode;
        }

        public static string CutLimitText(string source, int limit)
        {
            if (source.Length < limit)
            {
                return source;
            }
            else
            {
                int endtext2 = 0;
                endtext2 = source.IndexOf(" ", limit);

                if (endtext2 > 0)
                {
                    source = source.Substring(0, endtext2);
                }
                else
                {
                    source = source.ToString() + " ...";
                }
                return source;
            }
        }

        public static string FixCode(string html)
        {
            html = html.Replace("  ", "&nbsp; ");
            html = html.Replace("  ", " &nbsp;");
            html = html.Replace("\t", "&nbsp; &nbsp;&nbsp;");
            html = html.Replace("[", "&#91;");
            html = html.Replace("]", "&#93;");
            html = html.Replace("<", "&lt;");
            html = html.Replace(">", "&gt;");
            html = html.Replace("\r\n", "<br/>");
            return html;
        }

        public static string FormatDate(string strDate)
        {
            int pos = strDate.IndexOf(" ");
            string strRe = "";
            string strChar = "";
            if (pos > 0)
            {
                string Temp = strDate.Substring(0, pos);
                string[] arrDate = Temp.Split('/');

                for (int i = 0; i < arrDate.Length; i++)
                {
                    if (i != 0)
                    {
                        strChar = "-";
                    }
                    if (arrDate[i].Length == 1)
                    {
                        strRe += strChar + "0" + arrDate[i];
                    }
                    else if (arrDate[i].Length == 2)
                    {
                        strRe += strChar + arrDate[i];
                    }
                    else if (arrDate[i].Length >= 4)
                    {
                        strRe += strChar + arrDate[i].Substring(2, 2);
                    }
                }
            }
            else
            {
                strRe = strDate;
            }

            string[] fm = strRe.Split('-');
            if (fm.Length > 1)
                strRe = fm[1] + "-" + fm[0] + "-" + fm[2];
            return strRe;
        }

        public static string GetAlphanumericString(string s)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in s)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string GenerateCode(string strInput, int max_number)
        {
            int total = 0;
            if (strInput.Length <= max_number)
            {
                total = max_number - strInput.Length;
                for (int i = 0; i < total; i++)
                {
                    strInput = "0" + strInput;
                }
            }
            return strInput;
        }

        public static string GenerateCodeWithMillisecond(string strInput, int max_number)
        {
            int total = 0;
            //strInput = String.Concat(strInput, "-", DateTime.Now.Millisecond.ToString());
            strInput = String.Concat(strInput, DateTime.Now.Millisecond.ToString());
            if (strInput.Length <= max_number)
            {
                total = max_number - strInput.Length;
                for (int i = 0; i < total; i++)
                {
                    strInput = "0" + strInput;
                }
            }
            else
                strInput.Substring(0, max_number);
            return strInput;
        }

        public static string GetEncodeString(string strInput)
        {
            //Random String 
            Random rnd = new Random();
            string strRandom = rnd.Next(10000, 99999).ToString() + GetRandomString();

            strInput = new Regex(@"[\s+\\\/:\*\?\&""\''<>|]").Replace(ConvertToUnSign(strInput), string.Empty);
            string encode_result = strRandom + "_" + strInput;
            return encode_result;
        }

        public static string GetEncodeStringWithDate(string strInput)
        {
            //Current Date
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            culture.DateTimeFormat.DateSeparator = string.Empty;
            culture.DateTimeFormat.ShortDatePattern = "yyyyMMdd";
            culture.DateTimeFormat.LongDatePattern = "yyyyMMdd";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            string current_date_yyyymmdd_hhmmss_mmm = DateTime.Now.ToString("yyyyMMdd_hhmmss") + DateTime.Now.Millisecond.ToString();

            //Random String 
            Random rnd = new Random();
            string strRandom = rnd.Next(10000, 99999).ToString() + GetRandomString();

            strInput = new Regex(@"[\s+\\\/:\*\?\&""\''<>|]").Replace(ConvertToUnSign(strInput), string.Empty);
            string encode_result = current_date_yyyymmdd_hhmmss_mmm + "_" + strRandom + "_" + strInput;
            return encode_result;
        }

        public static string CreateEncodedStringWithDateGuid(string strInput)
        {
            //Current Date
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            culture.DateTimeFormat.DateSeparator = string.Empty;
            culture.DateTimeFormat.ShortDatePattern = "yyyyMMdd";
            culture.DateTimeFormat.LongDatePattern = "yyyyMMdd";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            string current_date_yyyymmdd_hhmmss_mmm = DateTime.Now.ToString("yyyyMMdd-hhmmss") + DateTime.Now.Millisecond.ToString();

            strInput = new Regex(@"[\s+\\\/:\*\?\@\&\#\$\^\%\--\+\;\,\“\”\""\''<>|]").Replace(ConvertToUnSign(strInput), string.Empty);
            string encode_result = current_date_yyyymmdd_hhmmss_mmm + "-" + Guid.NewGuid() + "-" + strInput;
            return encode_result;
        }

        public static string GetFirstCapitalizedLetterOfWords(string strInput)
        {
            string result = null;
            string[] words = strInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                //first_capitialized_letter_of_words
                result += word.Substring(0, 1).ToUpper();

            }
            return result;
        }

        public static string GeneratePageTitle(string title, string rootTitle, string pathSeparator)
        {
            List<string> path = new List<string>();
            SiteMapNode currentNode = null;

            currentNode = SiteMap.CurrentNode;

            if (currentNode == null)
            {
                if (String.IsNullOrEmpty(title))
                {
                    return rootTitle;
                }

                return title + pathSeparator + rootTitle;
            }

            if (currentNode != SiteMap.RootNode)
            {
                if (!String.IsNullOrEmpty(title) && title != rootTitle)
                {
                    path.Add(title);
                }
                else
                {
                    path.Add(currentNode.Title);
                }
            }
            else
            {
                path.Add(rootTitle);
            }

            currentNode = currentNode.ParentNode;

            while (!(currentNode == null))
            {

                // Use our own root title for the <title> tag
                if (currentNode.Title != SiteMap.RootNode.Title)
                {
                    path.Add(currentNode.Title);
                }
                else
                {
                    path.Add(rootTitle);
                }

                currentNode = currentNode.ParentNode;
            }

            string[] paths = path.ToArray();

            return string.Join(pathSeparator, paths);

        }

        public static string getRandom(int min, int max)
        {
            Random random = new Random();
            int result = random.Next(min, max);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append(result);
            return builder.ToString();
        }

        public static string GenerateRandomString(int size)
        {

            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString().ToLower();
        }

        public static string RandomString(int size)
        {
            string strCaptcha = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string strTemp = "";
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                int iRan = random.Next(0, 61);
                strTemp += strCaptcha.Substring(iRan, 1);
            }
            return strTemp;
        }

        public static string GetRandomString()
        {
            //use the following string to control your set of alphabetic characters to choose from
            //for example, you could include uppercase too
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";

            // Random is not truly random, 
            // so we try to encourage better randomness by always changing the seed value
            Random rnd = new Random(DateTime.Now.Millisecond);

            // basic 5 digit random number
            string result = rnd.Next(10000, 99999).ToString();

            // single random character in ascii range a-z
            string alphaChar = alphabet.Substring(rnd.Next(0, alphabet.Length - 1), 1);

            // random position to put the alpha character
            int replacementIndex = rnd.Next(0, (result.Length - 1));
            result = result.Remove(replacementIndex, 1).Insert(replacementIndex, alphaChar);

            return result;
        }

        public static string GetRandomString(int length)
        {
            int intZero = 0;
            int intNine = 0;
            int intA = 0;
            int intZ = 0;
            int intCount = 0;
            int intRandomNumber = 0;
            string strRandomString = null;
            Random rRandom = new Random(DateTime.Now.Millisecond);

            intZero = Convert.ToInt32('0');
            intNine = Convert.ToInt32('9');

            intA = Convert.ToInt32('A');
            intZ = Convert.ToInt32('Z');
            strRandomString = "";

            while (intCount < length)
            {
                intRandomNumber = rRandom.Next(intZero, intZ);
                if (((intRandomNumber >= intZero) & (intRandomNumber <= intNine)) | ((intRandomNumber >= intA) & (intRandomNumber <= intZ)))
                {
                    strRandomString = strRandomString + ((char)(intRandomNumber)).ToString();
                }
                else
                {
                    strRandomString = strRandomString + ((char)(rRandom.Next(intZero, intNine))).ToString();
                }
                intCount = intCount + 1;
            }

            return strRandomString;

        }

        public static string GenerateFriendlyString(string phrase)
        {
            string str = RemoveSign4VietnameseString(phrase.ToLower());

            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces/hyphens into one space       
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();
            // trim it
            str = str.Trim();
            // hyphens
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }

        public static string GenerateFriendlyString(string phrase, int maxLength = 50)
        {
            string str = RemoveSign4VietnameseString(phrase.ToLower());

            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces/hyphens into one space       
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();
            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();
            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;

            //string title = @"A bunch of ()/*++\'#@$&*^!%     invalid URL characters  ";
            //title.Slugify();
            // outputs : a-bunch-of-invalid-url-characters
        }

        private static string GetMD5Hash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        public static string GetUniqueKey()
        {
            int maxSize = 8;
            //int minSize = 5 ;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        //public string HighlightKeyWords(string text, string keywords, string cssClass)
        //{
        //    if (text == String.Empty || keywords == String.Empty || cssClass == String.Empty)
        //        return text;
        //    var words = keywords.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //    //return words.Select(word => word.Trim()).Aggregate(text,(current, pattern) => Regex.Replace(current,pattern,string.Format("<span style=\"background-color:{0}\">{1}</span>",cssClass,"$0"),RegexOptions.IgnoreCase));
        //    //return words.Select(word => "\\b" + word.Trim() + "\\b").Aggregate(text, (current, pattern) => Regex.Replace(current, pattern, string.Format("<span style=\"background-color:{0}\">{1}</span>", cssClass, "$0"), RegexOptions.IgnoreCase));
        //    return words.Select(word => word.Trim()).Aggregate(text, (current, pattern) => Regex.Replace(current, pattern, string.Format("<span class=\"{0}\">{1}</span>", cssClass, "$0"), RegexOptions.IgnoreCase));
        //    //return words.Select(word => "\\b" + word.Trim() + "\\b").Aggregate(text, (current, pattern) => Regex.Replace(current, pattern, string.Format("<span class=\"{0}\">{1}</span>", cssClass, "$0"), RegexOptions.IgnoreCase));
        //}       

      

        public static string RemoveExtraText(string value)
        {
            var allowedChars = "01234567890.,";
            return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
        }

        public static string RemoveExtraTextWithoutPointOrComma(string value)
        {
            var allowedChars = "01234567890";
            return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
        }

        public static readonly string[] VietnameseSigns = new string[]    
        {   "aAeEoOuUiIdDyY",        
            "áàạảãâấầậẩẫăắằặẳẵ",        
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",        
            "éèẹẻẽêếềệểễ",        
            "ÉÈẸẺẼÊẾỀỆỂỄ",        
            "óòọỏõôốồộổỗơớờợởỡ",        
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",        
            "úùụủũưứừựửữ",        
            "ÚÙỤỦŨƯỨỪỰỬỮ",        
            "íìịỉĩ",        
            "ÍÌỊỈĨ",        
            "đ",        
            "Đ",        
            "ýỳỵỷỹ",        
            "ÝỲỴỶỸ",            
        };

        public static string RemoveSign4VietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi        
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
        
        //meta characters : \ | ( ) [  {  ^ $ * + ? .
        public static string RemoveDuplicateWhiteSpace(string input)
        {   
            return Regex.Replace(input, @"[\s]+", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }

        /*** Remove white space*/
        public static string RemoveWhiteSpace(string strIput)
        {
            string result = null;
            if (strIput != null)
            {
                string pattern = @"\s+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                result = regex.Replace(strIput, String.Empty);
            }
            return result;
        }        

        public static string RemoveWhitespaceWithSplit(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            string[] parts = inputString.Split(new char[] { ' ', '\n', '\t', '\r', '\f', '\v' }, StringSplitOptions.RemoveEmptyEntries);
            int size = parts.Length;
            for (int i = 0; i < size; i++)
            sb.AppendFormat("{0} ", parts[i]);
            return sb.ToString();
        }

        public static String ReverseString(String InString)
        {
            // Check null String
            if (InString == null) return null;

            Int32 intSize = InString.Length;
            char[] arrayInString = InString.ToCharArray();
            char[] arrayOutString = new char[intSize];

            for (Int32 i = 0; i < intSize; ++i)
                arrayOutString[i] = arrayInString[intSize - i - 1];

            return new String(arrayOutString);
        }      

        /* SQL INJECTION ==============================================
            /((\%3D)|(=))[^\n]*((\%27)|(\')|(\-\-)|(\%3B)|(;))/i 
            /\w*((\%27)|(\'))((\%6F)|o|(\%4F))((\%72)|r|(\%52))/ix 
            /((\%27)|(\'))union/ix
            /exec(\s|\+)+(s|x)p\w+/ix 
            /((\%3C)|<)((\%2F)|\/)*[a-z0-9\%]+((\%3E)|>)/ix
         =============================================================== */
        public static string RemoveFixedMetaCharacters(string strIput)
        {
            string pattern = @"&quot;|union|exec|['!~=+<>()""\[\]^&@$?%\.*:#/\\_;]|[-+]";
            Regex regex = new Regex(pattern);
            string q = Regex.Replace(strIput, pattern, " ").Trim();
            string result = Regex.Replace(q, @"\s+", "-").Trim();
            return result;
        }


        public static string[] RemoveDuplicates(string[] items)
        {
            ArrayList noDupsArrList = new ArrayList();
            for (int i = 0; i <= items.Length - 1; i++)
            {
                if (!noDupsArrList.Contains(items[i].Trim()))
                {
                    noDupsArrList.Add(items[i].Trim());
                }
            }

            string[] uniqueItems = new string[noDupsArrList.Count];
            noDupsArrList.CopyTo(uniqueItems);
            return uniqueItems;
        }

        public static string RemoveRepeatWords(string source)
        {
            Dictionary<string, bool> listofUniqueWords = new Dictionary<string, bool>();
            StringBuilder destBuilder = new StringBuilder();
            string[] spilltedwords = source.Split(new char[] { ' ', ',', ';', '.' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in spilltedwords)
            {
                string lower = item.ToLower();
                if (!listofUniqueWords.ContainsKey(lower))
                {
                    destBuilder.Append(item).Append(' ');
                    listofUniqueWords.Add(lower, true);
                }
            }

            return destBuilder.ToString().Trim();
        }

            

        private static ArrayList RemoveDuplicate(ArrayList sourceList)
        {
            ArrayList list = new ArrayList();
            foreach (string item in sourceList)
            {
                if (!list.Contains(item)) { list.Add(item); }
            } 
            return list;
        }

        /// <summary>
        /// Replaces and  and Quote characters to HTML safe equivalents.
        /// </summary>
        /// <param name="html">HTML to convert</param>
        /// <returns>Returns an HTML string of the converted text</returns>
        public static string FixHTMLForDisplay(string html)
        {
            html = html.Replace("<", "&lt;");
            html = html.Replace(">", "&gt;");
            html = html.Replace("\"", "&quot;");
            return html;
        }

        public static string removeHTMLtab(string content, string replace)
        {
            string strSource = Regex.Replace(content, @"<(.|\n)*?>", replace);
            strSource = Regex.Replace(strSource, "\r|\t|\n", replace);
            return strSource;
        }

        public static System.Collections.ArrayList SplitSentences(string sSourceText)
        {
            // create a local string variable
            // set to contain the string passed it
            string sTemp = sSourceText;

            // create the array list that will be used to hold the sentences
            System.Collections.ArrayList al = new System.Collections.ArrayList();

            // split the sentences with a regular expression
            string[] splitSentences = Regex.Split(sTemp, @"(?<=['""A-Za-z0-9][\.\!\?])\s+(?=[A-Z])");

            // loop the sentences
            for (int i = 0; i < splitSentences.Length; i++)
            {
                // clean up the sentence one more time, trim it, and add it to the array list
                string sSingleSentence = splitSentences[i].Replace(Environment.NewLine, string.Empty);
                al.Add(sSingleSentence.Trim());
            }
            // return the arraylist with all sentences added
            return al;
        }


        /*** Strips accents off words = Loai bo dau trong tieng viet*/
        public static string StripDiacritics(string accented)
        {
            string result = null;
            if (accented != null)
            {
                string pattern = @"\\pp{IsCombiningDiacriticalMarks}+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                string strFormD = accented.Normalize(System.Text.NormalizationForm.FormD);
                result = regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            }
            return result;
        }  

        /// <summary>
        /// Strips HTML tags out of an HTML string and returns just the text.
        /// </summary>
        /// <param name="html">Html String</param>
        /// <returns></returns>
        public static string StripHtml(string html)
        {
            html = Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
            html = html.Replace("\t", " ");
            html = html.Replace("\r\n", string.Empty);
            html = html.Replace("   ", " ");
            return html.Replace("  ", " ");
        }
        

        public static string UTF8_Encode(string strInput)
        {
            string result = null;
            try
            {
                System.Text.Encoding enc = System.Text.Encoding.UTF8;
                byte[] byte_array = enc.GetBytes(strInput.Replace("'", "''"));
                result = enc.GetString(byte_array);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

        #region XU LY HTML TAGS ===========================================================
        /// <summary>
        /// Fixes a plain text field for display as HTML by replacing carriage returns 
        /// with the appropriate br and p tags for breaks.
        /// </summary>
        /// <param name="htmlText">Input string</param>
        /// <returns>Fixed up string</returns>
        public static string DisplayMemo(string htmlText)
        {
            if (htmlText == null)
                return string.Empty;

            htmlText = htmlText.Replace("\r\n", "\r");
            htmlText = htmlText.Replace("\n", "\r");
            //HtmlText = HtmlText.Replace("\r\r","<p>");
            htmlText = htmlText.Replace("\r", "<br />\r\n");
            return htmlText;
        }

        /// <summary>
        /// Method that handles handles display of text by breaking text.
        /// Unlike the non-encoded version it encodes any embedded HTML text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DisplayMemoEncoded(string text)
        {
            if (text == null)
                return string.Empty;

            bool PreTag = false;
            if (text.Contains("<pre>"))
            {
                text = text.Replace("<pre>", "__pre__");
                text = text.Replace("</pre>", "__/pre__");
                PreTag = true;
            }


            // fix up line breaks into <br><p>
            text = DisplayMemo(HtmlEncode(text)); //HttpUtility.HtmlEncode(Text));

            if (PreTag)
            {
                text = text.Replace("__pre__", "<pre>");
                text = text.Replace("__/pre__", "</pre>");
            }

            return text;
        }

        /// <summary>
        /// HTML-encodes a string and returns the encoded string.
        /// </summary>
        /// <param name="text">The text string to encode. </param>
        /// <returns>The HTML-encoded text.</returns>
        public static string HtmlEncode(string text)
        {
            if (text == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder(text.Length);

            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                switch (text[i])
                {

                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '"':
                        sb.Append("&quot;");
                        break;
                    case '&':
                        sb.Append("&amp;");
                        break;
                    default:
                        if (text[i] > 159)
                        {
                            // decimal numeric entity
                            sb.Append("&#");
                            sb.Append(((int)text[i]).ToString(CultureInfo.InvariantCulture));
                            sb.Append(";");
                        }
                        else
                            sb.Append(text[i]);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Expands links into HTML hyperlinks inside of text or HTML.
        /// </summary>
        /// <param name="text">The text to expand</param>
        /// <param name="target">Target frame where links are displayed</param>
        /// <param name="parseFormattedLinks">Allows parsing of links in the following format [text|www.site.com]</param>
        /// <returns></returns>
        public static string ExpandUrls(string text, string target = null, bool parseFormattedLinks = false)
        {
            if (target == null)
                target = string.Empty;

            ExpandUrlsParser Parser = new ExpandUrlsParser();
            Parser.Target = target;
            Parser.ParseFormattedLinks = parseFormattedLinks;
            return Parser.ExpandUrls(text);
        }

        /// <summary>
        /// Create an Href HTML link
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        /// <param name="target"></param>
        /// <param name="additionalMarkup"></param>
        /// <returns></returns>
        public static string Href(string text, string url, string target = null, string additionalMarkup = null)
        {
            if (url.StartsWith("~"))
                url = ResolveUrl(url);

            return "<a href=\"" + url + "\" " +
                (string.IsNullOrEmpty(target) ? string.Empty : "target=\"" + target + "\" ") +
                (string.IsNullOrEmpty(additionalMarkup) ? string.Empty : additionalMarkup) +
                ">" + text + "</a>";
        }

        /// <summary>
        /// Creates an HREF HTML Link
        /// </summary>
        /// <param name="url"></param>
        public static string Href(string url)
        {
            return Href(url, url, null, null);
        }

        /// <summary>
        /// Returns an IMG link as a string. If the image is null
        /// or empty a blank string is returned.
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="additionalMarkup">any additional attributes added to the element</param>
        /// <returns></returns>
        public static string ImgRef(string imageUrl, string additionalMarkup = null)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return string.Empty;

            if (imageUrl.StartsWith("~"))
                imageUrl = ResolveUrl(imageUrl);

            string img = "<img src=\"" + imageUrl + "\" ";

            if (!string.IsNullOrEmpty("additionalMarkup"))
                img += additionalMarkup + " ";

            img += "/>";
            return img;
        }
        #endregion XU LY HTML TAGS ========================================================
      
        #region XU LY LINK ================================================================
        /// <summary>
        /// Resolves a URL based on the current HTTPContext
        /// 
        /// Note this method is added here internally only
        /// to support the HREF() method and ~ expansion
        /// on urls.
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        internal static string ResolveUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
                return string.Empty;

            // Absolute path - just return
            if (originalUrl.IndexOf("://") != -1)
                return originalUrl;

            // Fix up image path for ~ root app dir directory
            if (originalUrl.StartsWith("~"))
            {
                //return VirtualPathUtility.ToAbsolute(originalUrl);
                string newUrl = "";
                if (HttpContext.Current != null)
                {
                    newUrl = HttpContext.Current.Request.ApplicationPath +
                          originalUrl.Substring(1);
                    newUrl = newUrl.Replace("//", "/"); // must fix up for root path
                }
                else
                    // Not context: assume current directory is the base directory
                    throw new ArgumentException("Invalid URL: Relative URL not allowed.");

                // Just to be sure fix up any double slashes
                return newUrl;
            }

            return originalUrl;
        }
        #endregion XU LY LINK =============================================================


        ///
        /// Converts the String to UTF8 Byte array and is used in De-serialization
        ///      
        public static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        

        /// <summary>
        /// Extracts a string from between a pair of delimiters. Only the first 
        /// instance is found.
        /// </summary>
        /// <param name="source">Input String to work on</param>
        /// <param name="StartDelim">Beginning delimiter</param>
        /// <param name="endDelim">ending delimiter</param>
        /// <param name="CaseInsensitive">Determines whether the search for delimiters is case sensitive</param>
        /// <returns>Extracted string or ""</returns>
        public static string ExtractString(string source,
                                           string beginDelim,
                                           string endDelim,
                                           bool caseSensitive = false,
                                           bool allowMissingEndDelimiter = false,
                                           bool returnDelimiters = false)
        {
            int at1, at2;

            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (caseSensitive)
            {
                at1 = source.IndexOf(beginDelim);
                if (at1 == -1)
                    return string.Empty;

                if (!returnDelimiters)
                    at2 = source.IndexOf(endDelim, at1 + beginDelim.Length);
                else
                    at2 = source.IndexOf(endDelim, at1);
            }
            else
            {
                //string Lower = source.ToLower();
                at1 = source.IndexOf(beginDelim, 0, source.Length, StringComparison.OrdinalIgnoreCase);
                if (at1 == -1)
                    return string.Empty;

                if (!returnDelimiters)
                    at2 = source.IndexOf(endDelim, at1 + beginDelim.Length, StringComparison.OrdinalIgnoreCase);
                else
                    at2 = source.IndexOf(endDelim, at1, StringComparison.OrdinalIgnoreCase);
            }

            if (allowMissingEndDelimiter && at2 == -1)
                return source.Substring(at1 + beginDelim.Length);

            if (at1 > -1 && at2 > 1)
            {
                if (!returnDelimiters)
                    return source.Substring(at1 + beginDelim.Length, at2 - at1 - beginDelim.Length);
                else
                    return source.Substring(at1, at2 - at1 + endDelim.Length);
            }

            return string.Empty;
        }



        /// <summary>
        /// String replace function that support
        /// </summary>
        /// <param name="origString">Original input string</param>
        /// <param name="findString">The string that is to be replaced</param>
        /// <param name="replaceWith">The replacement string</param>
        /// <param name="instance">Instance of the FindString that is to be found. if Instance = -1 all are replaced</param>
        /// <param name="caseInsensitive">Case insensitivity flag</param>
        /// <returns>updated string or original string if no matches</returns>
        public static string ReplaceStringInstance(string origString, string findString,
                                                   string replaceWith, int instance,
                                                   bool caseInsensitive)
        {
            if (instance == -1)
                return ReplaceString(origString, findString, replaceWith, caseInsensitive);

            int at1 = 0;
            for (int x = 0; x < instance; x++)
            {

                if (caseInsensitive)
                    at1 = origString.IndexOf(findString, at1, origString.Length - at1, StringComparison.OrdinalIgnoreCase);
                else
                    at1 = origString.IndexOf(findString, at1);

                if (at1 == -1)
                    return origString;

                if (x < instance - 1)
                    at1 += findString.Length;
            }

            return origString.Substring(0, at1) + replaceWith + origString.Substring(at1 + findString.Length);
        }

        /// <summary>
        /// Replaces a substring within a string with another substring with optional case sensitivity turned off.
        /// </summary>
        /// <param name="origString">String to do replacements on</param>
        /// <param name="findString">The string to find</param>
        /// <param name="replaceString">The string to replace found string wiht</param>
        /// <param name="caseInsensitive">If true case insensitive search is performed</param>
        /// <returns>updated string or original string if no matches</returns>
        public static string ReplaceString(string origString, string findString,
                                           string replaceString, bool caseInsensitive)
        {
            int at1 = 0;
            while (true)
            {
                if (caseInsensitive)
                    at1 = origString.IndexOf(findString, at1, origString.Length - at1, StringComparison.OrdinalIgnoreCase);
                else
                    at1 = origString.IndexOf(findString, at1);

                if (at1 == -1)
                    break;

                origString = origString.Substring(0, at1) + replaceString + origString.Substring(at1 + findString.Length);

                at1 += replaceString.Length;
            }

            return origString;
        }



        /// <summary>
        /// Trims a sub string from a string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textToTrim"></param>
        /// <returns></returns>
        public static string TrimStart(string text, string textToTrim, bool caseInsensitive)
        {
            while (true)
            {
                string match = text.Substring(0, textToTrim.Length);

                if (match == textToTrim ||
                    (caseInsensitive && match.ToLower() == textToTrim.ToLower()))
                {
                    if (text.Length <= match.Length)
                        text = "";
                    else
                        text = text.Substring(textToTrim.Length);
                }
                else
                    break;
            }
            return text;
        }

        /// <summary>
        /// Replicates an input string n number of times
        /// </summary>
        /// <param name="input"></param>
        /// <param name="charCount"></param>
        /// <returns></returns>
        public static string Replicate(string input, int charCount)
        {
            return new StringBuilder().Insert(0, "input", charCount).ToString();
        }

        /// <summary>
        /// Replicates a character n number of times and returns a string
        /// </summary>
        /// <param name="charCount"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static string Replicate(char character, int charCount)
        {
            return new string(character, charCount);
        }

        /// <summary>
        /// Return a string in proper Case format
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string ProperCase(string Input)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Input);
        }

        /// <summary>
        /// Takes a phrase and turns it into CamelCase text.
        /// White Space, punctuation and separators are stripped
        /// </summary>
        /// <param name="?"></param>
        public static string ToCamelCase(string phrase)
        {
            if (phrase == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder(phrase.Length);

            // First letter is always upper case
            bool nextUpper = true;

            foreach (char ch in phrase)
            {
                if (char.IsWhiteSpace(ch) || char.IsPunctuation(ch) || char.IsSeparator(ch))
                {
                    nextUpper = true;
                    continue;
                }

                if (nextUpper)
                    sb.Append(char.ToUpper(ch));
                else
                    sb.Append(char.ToLower(ch));

                nextUpper = false;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Tries to create a phrase string from CamelCase text.
        /// Will place spaces before capitalized letters.
        /// 
        /// Note that this method may not work for round tripping 
        /// ToCamelCase calls, since ToCamelCase strips more characters
        /// than just spaces.
        /// </summary>
        /// <param name="camelCase"></param>
        /// <returns></returns>
        public static string FromCamelCase(string camelCase)
        {
            if (camelCase == null)
                throw new ArgumentException("Null is not allowed for StringUtils.FromCamelCase");

            StringBuilder sb = new StringBuilder(camelCase.Length + 10);
            bool first = true;
            char lastChar = '\0';

            foreach (char ch in camelCase)
            {
                if (!first &&
                     (char.IsUpper(ch) ||
                       char.IsDigit(ch) && !char.IsDigit(lastChar)))
                    sb.Append(' ');

                sb.Append(ch);
                first = false;
                lastChar = ch;
            }

            return sb.ToString(); ;
        }

        /// <summary>
        /// Terminates a string with the given end string/character, but only if the
        /// value specified doesn't already exist and the string is not empty.
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string TerminateString(string value, string terminator)
        {
            if (string.IsNullOrEmpty(value) || value.EndsWith(terminator))
                return value;

            return value + terminator;
        }

        /// <summary>
        /// Trims a string to a specific number of max characters
        /// </summary>
        /// <param name="value"></param>
        /// <param name="charCount"></param>
        /// <returns></returns>
        public static string TrimTo(string value, int charCount)
        {
            if (value == null)
                return string.Empty;

            if (value.Length > charCount)
                return value.Substring(0, charCount);

            return value;
        }

        /// <summary>
        /// Strips any common white space from all lines of text that have the same
        /// common white space text. Effectively removes common code indentation from
        /// code blocks for example so you can get a left aligned code snippet.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NormalizeIndentation(string code)
        {
            // normalize tabs to 3 spaces
            string text = code.Replace("\t", "   ");

            string[] lines = text.Split(new string[3] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // keep track of the smallest indent
            int minPadding = 1000;

            foreach (var line in lines)
            {
                if (line.Length == 0)  // ignore blank lines
                    continue;

                int count = 0;
                foreach (char chr in line)
                {
                    if (chr == ' ' && count < minPadding)
                        count++;
                    else
                        break;
                }
                if (count == 0)
                    return code;

                minPadding = count;
            }

            string strip = new String(' ', minPadding);

            StringBuilder sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine(StringUtils.ReplaceStringInstance(line, strip, "", 1, false));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns an abstract of the provided text by returning up to Length characters
        /// of a text string. If the text is truncated a ... is appended.
        /// </summary>
        /// <param name="text">Text to abstract</param>
        /// <param name="length">Number of characters to abstract to</param>
        /// <returns>string</returns>
        public static string TextAbstract(string text, int length)
        {
            if (text == null)
                return string.Empty;

            if (text.Length <= length)
                return text;

            text = text.Substring(0, length);

            text = text.Substring(0, text.LastIndexOf(" "));
            return text + "...";
        }

        /// <summary>
        /// Creates an Abstract from an HTML document. Strips the 
        /// HTML into plain text, then creates an abstract.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlAbstract(string html, int length)
        {
            return TextAbstract(StripHtml(html), length);
        }

        /// <summary>
        /// Simple Logging method that allows quickly writing a string to a file
        /// </summary>
        /// <param name="output"></param>
        /// <param name="filename"></param>
        public static void LogString(string output, string filename)
        {
            StreamWriter Writer = new StreamWriter(filename, true);
            Writer.WriteLine(DateTime.Now.ToString() + " - " + output);
            Writer.Close();
        }

        /// <summary>
        /// Creates short string id based on a GUID hashcode.
        /// Not guaranteed to be unique across machines, but unlikely
        /// to duplicate in medium volume situations.
        /// </summary>
        /// <returns></returns>
        public static string NewStringId()
        {
            return Guid.NewGuid().ToString().GetHashCode().ToString("x");
        }


        /// <summary>
        /// Parses an string into an integer. If the value can't be parsed
        /// a default value is returned instead
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public static int ParseInt(string input, int defaultValue, IFormatProvider numberFormat)
        {
            int val = defaultValue;
            int.TryParse(input, NumberStyles.Any, numberFormat, out val);
            return val;
        }

        /// <summary>
        /// Parses an string into an integer. If the value can't be parsed
        /// a default value is returned instead
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ParseInt(string input, int defaultValue)
        {
            return ParseInt(input, defaultValue, CultureInfo.CurrentCulture.NumberFormat);
        }

        /// <summary>
        /// Parses an string into an decimal. If the value can't be parsed
        /// a default value is returned instead
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ParseDecimal(string input, decimal defaultValue, IFormatProvider numberFormat)
        {
            decimal val = defaultValue;
            decimal.TryParse(input, NumberStyles.Any, numberFormat, out val);
            return val;
        }

        /// <summary>
        /// Strips all non digit values from a string and only
        /// returns the numeric string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripNonNumber(string input)
        {
            var chars = input.ToCharArray();
            StringBuilder sb = new StringBuilder();
            foreach (var chr in chars)
            {
                if (char.IsNumber(chr) || char.IsSeparator(chr))
                    sb.Append(chr);
            }

            return sb.ToString();
        }


        /// <summary>
        /// Creates a Stream from a string. Internally creates
        /// a memory stream and returns that.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Stream StringToStream(string text, Encoding encoding)
        {
            MemoryStream ms = new MemoryStream(text.Length * 2);
            byte[] data = encoding.GetBytes(text);
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Creates a Stream from a string. Internally creates
        /// a memory stream and returns that.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Stream StringToStream(string text)
        {
            return StringToStream(text, Encoding.Default);
        }

        /// <summary>
        /// Turns a BinHex string that contains raw byte values
        /// into a byte array
        /// </summary>
        /// <param name="hex">BinHex string (just two byte hex digits strung together)</param>
        /// <returns></returns>
        public static byte[] BinHexToString(string hex)
        {
            int offset = hex.StartsWith("0x") ? 2 : 0;
            if ((hex.Length % 2) != 0)
                throw new ArgumentException();

            byte[] ret = new byte[(hex.Length - offset) / 2];

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = (byte)((ParseHexChar(hex[offset]) << 4)
                                 | ParseHexChar(hex[offset + 1]));
                offset += 2;
            }
            return ret;
        }

        static int ParseHexChar(char c)
        {
            if (c >= '0' && c <= '9')
                return c - '0';
            if (c >= 'A' && c <= 'F')
                return c - 'A' + 10;
            if (c >= 'a' && c <= 'f')
                return c - 'a' + 10;

            throw new ArgumentException();
        }

        /// <summary>
        /// Retrieves a value from an XML-like string
        /// </summary>
        /// <param name="propertyString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetProperty(string propertyString, string key)
        {
            return StringUtils.ExtractString(propertyString, "<" + key + ">", "</" + key + ">");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyString"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SetProperty(string propertyString, string key, string value)
        {
            string extract = StringUtils.ExtractString(propertyString, "<" + key + ">", "</" + key + ">");

            if (string.IsNullOrEmpty(value) && extract != string.Empty)
            {
                return propertyString.Replace(extract, "");
            }

            string xmlLine = "<" + key + ">" + value + "</" + key + ">";

            // replace existing
            if (extract != string.Empty)
                return propertyString.Replace(extract, xmlLine);

            // add new
            return propertyString + xmlLine + "\r\n";
        }

        public static string SetRandomCaptchaString(int size, bool lowerCase)
        {
            string s = null;
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * r.NextDouble() + 65)));
                s += ch.ToString();
            }
            // storing the password in the DataSet
            s = ((lowerCase ? s.ToString().ToLower() : s.ToString()));
            return s;
        }

        public static string Truncate(string input, int characterLimit)
        {
            string output = input;

            // Check if the string is longer than the allowed amount otherwise do nothing
            if (output.Length > characterLimit && characterLimit > 0)
            {
                // cut the string down to the maximum number of characters
                output = output.Substring(0, characterLimit);

                // Check if the character right after the truncate point was a space
                // if not, we are in the middle of a word and need to remove the rest of it
                if (input.Substring(output.Length, 1) != " ")
                {
                    int LastSpace = output.LastIndexOf(" ");

                    // if we found a space then, cut back to that space
                    if (LastSpace != -1)
                    {
                        output = output.Substring(0, LastSpace);
                    }
                }
                // Finally, add the "..."
                output += "...";
            }
            return output;
        }
        /*
        public static string ToUTF8Xml()
        {
            string result;

            MemoryStream stream = new MemoryStream(); // The writer closes this for us
            using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
            {
                writer.Indentation = 4;
                writer.IndentChar = '\t';
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                writer.WriteStartElement("Root");
                writer.WriteAttributeString("myattrib", "123");
                writer.WriteEndElement();

                writer.WriteEndDocument();
                writer.Flush();

                // Make sure you flush or you only get half the text
                writer.Flush();

                // Use a StreamReader to get the byte order correct
                StreamReader reader = new StreamReader(stream, Encoding.UTF8, true);
                stream.Seek(0, SeekOrigin.Begin);
                result = reader.ReadToEnd();

                // #2 - doesn't write the byte order reliably
                //result = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);
            }

            // Make sure you use File.WriteAllText("myfile", xml, Encoding.UTF8);
            // or equivalent to write your file back.
            return result;
        }
         *   public static string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(builder))
            {
                using (XmlTextWriter writer = new XmlTextWriter(stringWriter))
                {
                    // This produces UTF16 XML
                    writer.Indentation = 4;
                    writer.IndentChar = '\t';
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartDocument();

                    writer.WriteStartElement("Root");
                    writer.WriteAttributeString("myattrib", "123");
                    writer.WriteEndElement();

                    writer.WriteEndDocument();
                }
            }


            return builder.ToString();
        }

        
         * */
        ///
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        ///
        /// Unicode Byte Array to be converted to String /// String converted from Unicode Byte Array
        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }
        public static int CountRepeatedCharInString(string str, char c)
        {
            int num_slash = 0;
            for (int index = 0; index < str.Length; index++)
            {
                if (str[index] == c)
                {
                    num_slash++;
                }
            }
            return num_slash;
        }

        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();

            // select all that starts with the value string (in this case with "*")
            //string value = "*";
            // the dataView.RowFilter will be: "Name LIKE '[*]*'"
            //DataTable dt=new DataTable();
            //DataView dv = new DataView(dt);
            //dataView.RowFilter = String.Format("Name LIKE '{0}*'", EscapeLikeValue(value));
        }
       
      
        //=====================================================
        public static string ConverId_Id_rewrite(string id)
        {
            string result = "";
            if (id != "")
            {
                if (id.IndexOf("-") > -1)
                    result = id.Substring(0, id.IndexOf("-"));
                else
                    result = id;
            }
            return result;
        }

        public static string Conver_rewrite_GetId(string link)
        {
            string result = "";

            if (link.IndexOf("_") > -1)
                result = link.Substring(4, link.IndexOf("_") - 1);

            return result;
        }

        #region UrlEncoding and UrlDecoding without System.Web
        /// <summary>
        /// UrlEncodes a string without the requirement for System.Web
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        // [Obsolete("Use System.Uri.EscapeDataString instead")]
        public static string UrlEncode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Sytem.Uri provides reliable parsing
            return System.Uri.EscapeDataString(text);
        }

        /// <summary>
        /// UrlDecodes a string without requiring System.Web
        /// </summary>
        /// <param name="text">String to decode.</param>
        /// <returns>decoded string</returns>
        public static string UrlDecode(string text)
        {
            // pre-process for + sign space formatting since System.Uri doesn't handle it
            // plus literals are encoded as %2b normally so this should be safe
            text = text.Replace("+", " ");
            string decoded = System.Uri.UnescapeDataString(text);
            return decoded;
        }

        /// <summary>
        /// Retrieves a value by key from a UrlEncoded string.
        /// </summary>
        /// <param name="urlEncoded">UrlEncoded String</param>
        /// <param name="key">Key to retrieve value for</param>
        /// <returns>returns the value or "" if the key is not found or the value is blank</returns>
        public static string GetUrlEncodedKey(string urlEncoded, string key)
        {
            urlEncoded = "&" + urlEncoded + "&";

            int Index = urlEncoded.IndexOf("&" + key + "=", StringComparison.OrdinalIgnoreCase);
            if (Index < 0)
                return string.Empty;

            int lnStart = Index + 2 + key.Length;

            int Index2 = urlEncoded.IndexOf("&", lnStart);
            if (Index2 < 0)
                return string.Empty;

            return UrlDecode(urlEncoded.Substring(lnStart, Index2 - lnStart));
        }

        /// <summary>
        /// Allows setting of a value in a UrlEncoded string. If the key doesn't exist
        /// a new one is set, if it exists it's replaced with the new value.
        /// </summary>
        /// <param name="urlEncoded">A UrlEncoded string of key value pairs</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SetUrlEncodedKey(string urlEncoded, string key, string value)
        {
            if (!urlEncoded.EndsWith("?") && !urlEncoded.EndsWith("&"))
                urlEncoded += "&";

            Match match = Regex.Match(urlEncoded, "[?|&]" + key + "=.*?&");

            if (match == null || string.IsNullOrEmpty(match.Value))
                urlEncoded = urlEncoded + key + "=" + UrlEncode(value) + "&";
            else
                urlEncoded = urlEncoded.Replace(match.Value, match.Value.Substring(0, 1) + key + "=" + UrlEncode(value) + "&");

            return urlEncoded.TrimEnd('&');
        }



        static char[] base36CharArray = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
        static string base36Chars = "0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Encodes an integer into a string by mapping to alpha and digits (36 chars)
        /// chars are embedded as lower case
        /// 
        /// Example: 4zx12ss
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Base36Encode(long value)
        {
            string returnValue = "";
            bool isNegative = value < 0;
            if (isNegative)
                value = value * -1;

            do
            {
                returnValue = base36CharArray[value % base36CharArray.Length] + returnValue;
                value /= 36;
            } while (value != 0);

            return isNegative ? returnValue + "-" : returnValue;
        }

        /// <summary>
        /// Decodes a base36 encoded string to an integer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long Base36Decode(string input)
        {
            bool isNegative = false;
            if (input.EndsWith("-"))
            {
                isNegative = true;
                input = input.Substring(0, input.Length - 1);
            }

            char[] arrInput = input.ToCharArray();
            Array.Reverse(arrInput);
            long returnValue = 0;
            for (long i = 0; i < arrInput.Length; i++)
            {
                long valueindex = base36Chars.IndexOf(arrInput[i]);
                returnValue += Convert.ToInt64(valueindex * Math.Pow(36, i));
            }
            return isNegative ? returnValue * -1 : returnValue;
        }


        #endregion





    }
}
