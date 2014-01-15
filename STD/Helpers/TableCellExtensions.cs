using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace STD.Helpers
{
    public static class TableCellExtensions
    {
        public static string LengthyText(this HtmlHelper helper, string input, int length)
        {
            if (input != null && input.Length > length + 3)
            {
                StringBuilder shortenedString = new StringBuilder();
                shortenedString.Append(input.Substring(0, length));
                shortenedString.Append("...");
                return shortenedString.ToString();
            }
            else
            {
                return input;
            }
            
            
        }
    }
}