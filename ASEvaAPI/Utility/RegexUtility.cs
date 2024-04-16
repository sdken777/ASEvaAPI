using System;
using System.Text.RegularExpressions;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Regular expression utility
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 正则表达式工具
    /// </summary>
    public class RegexUtil
    {
        /// \~English
        /// <summary>
        /// Whether it's Chinese string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为中文字符串
        /// </summary>
        public static bool IsChineseCh(string input)
        {
            return IsMatch(@"^[\u4e00-\u9fa5]+$", input);
        }

        /// \~English
        /// <summary>
        /// Whether it's phone number
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为电话号码
        /// </summary>
        public static bool IsPhone(string input)
        {
            string pattern = "^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7}$|^0\\d{3}[- ]?\\d{7}$";
            return IsMatch(pattern, input);
        }

        /// \~English
        /// <summary>
        /// Whether it's mobile phone number
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为手机号码
        /// </summary>
        public static bool IsMobilePhone(string input)
        {
            return IsMatch(@"^13\\d{9}$", input);
        }

        /// \~English
        /// <summary>
        /// Whether it's numeric string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为数值字符串
        /// </summary>
        public static bool IsNumber(string input)
        {
            string pattern = "^-?\\d+$|^(-?\\d+)(\\.\\d+)?$";
            return IsMatch(pattern, input);
        }

        /// \~English
        /// <summary>
        /// Whether it's non-negative numeric string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为非负数值字符串
        /// </summary>
        public static bool IsNotNegative(string input)
        {
            return IsMatch(@"^\d+$", input);
        }

        /// \~English
        /// <summary>
        /// Whether it's unsigned integer
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为无符号整型
        /// </summary>
        public static bool IsUint(string input)
        {
            return IsMatch(@"^[0-9]*[1-9][0-9]*$", input);
        }

        /// \~English
        /// <summary>
        /// Whether it's English string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为英文字符串
        /// </summary>
        public static bool IsEnglishCh(string input)
        {
            return IsMatch(@"^[A-Za-z]+$", input);
        }

        /// \~English
        /// <summary>
        /// Whether it's mail address
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为邮件字符串
        /// </summary>
        public static bool IsEmail(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return IsMatch(pattern, input);
        }

        /// \~English
        /// <summary>
        /// Whether it's mixed string of English and numbers
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为英文数字结合字符串
        /// </summary>
        public static bool IsNumAndEnCh(string input)
        {
            return IsMatch(@"^[A-Za-z0-9]+$", input);
        }

        /// \~English
        /// <summary>
        /// Whether it's URL
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为URL
        /// </summary>
        public static bool IsURL(string input)
        {
            string pattern = @"^[a-zA-Z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            return IsMatch(pattern, input);
        }

        /// \~English
        /// <summary>
        /// Whether it's URL without protocol header
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为无协议头URL
        /// </summary>
        public static bool IsNoHeadURL(string input)
        {
            string pattern = @"^(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            return IsMatch(pattern, input);
        }

        /// \~English
        /// <summary>
        /// Whether it's IPv4
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为IPv4
        /// </summary>
        public static bool IsIPv4(string input)
        {
            string[] IPs = input.Split('.');
            if (IPs.Length != 4)
            {
                return false;
            }
            for (int i = 0; i < IPs.Length; i++)
            {
                if (!IsMatch(@"^\d+$", IPs[i]))
                {
                    return false;
                }
                ushort ip;
                if (!UInt16.TryParse(IPs[i], out ip))
                {
                    return false;
                }
                if (ip > 255)
                {
                    return false;
                }
            }
            return true;
        }

        /// \~English
        /// <summary>
        /// Whether it's IPv6
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为IPv6
        /// </summary>
        public static bool IsIPv6(string input)
        {
            string pattern = "";
            string temp = input;
            string[] strs = temp.Split(':');
            if (strs.Length != 8)
            {
                return false;
            }
            int count = RegexUtil.GetStringCount(input, "::");
            if (count > 1)
            {
                return false;
            }
            else if (count == 0)
            {
                pattern = @"^([\da-f]{1,4}:){7}[\da-f]{1,4}$";
                return IsMatch(pattern, input);
            }
            else
            {
                pattern = @"^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$";
                return IsMatch(pattern, input);
            }
        }

        private static bool IsMatch(string pattern, string input)
        {
            if (input == null || input == "") return false;
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        private static int GetStringCount(string input, string compare)
        {
            int index = input.IndexOf(compare);
            if (index != -1)
            {
                return 1 + GetStringCount(input.Substring(index + compare.Length), compare);
            }
            else
            {
                return 0;
            }

        }
    }
}
