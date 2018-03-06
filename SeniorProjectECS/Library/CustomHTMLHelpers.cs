using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Library
{
    public class CustomHTMLHelpers
    {
        /// <summary>
        /// Create edit fields for all the items in an array
        /// </summary>
        /// <typeparam name="T">The type of data that is in the array.</typeparam>
        /// <param name="array">The array to create edit boxes for.</param>
        /// <returns></returns>
        public static String BuildEditFromArray(List<String> array, String name, String entryType)
        {
            String html = "";

            if (array.Count == 0)
            {
                html = "<input type=\"" + entryType + "\" class=\"form-control\" id=\"" + name + "_0_\" name=\"" + name + "[0]\" />";
            }
            else
            {
                for (int i = 0; i < array.Count; i++)
                {
                    html += html = "<input type=\"" + entryType + "\" class=\"form-control\" id=\"" + name + "_" + i + "_\" name=\"" + name + "[" + i + "]\" value=\"" + array[i] + "\" />";
                }
            }

            return html;
        }
    }
}
