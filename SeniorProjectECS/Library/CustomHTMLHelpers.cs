using Dapper;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public static HtmlString BuildEditFromArray(List<String> array, String name, String entryType)
        {
            if(entryType.Equals("select"))
            {
                return BuildSelectFromArray(array, name);
            }

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

            return new HtmlString(html);
        }

        public static HtmlString BuildSelectFromArray(List<String> array, String name)
        {
            // Get the select options
            List<string> optionList = new List<string>();
            using (var con = DBHandler.GetSqlConnection())
            {
                var data = con.Query("GetSelectLists", commandType: CommandType.StoredProcedure).ToList();

                foreach (dynamic dataLine in data)
                {
                   foreach(dynamic item in dataLine)
                   {
                        if(item.Key.Equals(name) && item.Value != null)
                        {
                            optionList.Add(item.Value);
                        }
                   }//end inner foreach
                }//end outer foreach
            }//end using

            // Build the html
            String html = "";

            if (array.Count == 0)
            {
                html = "<select class=\"form-control\" id=\"" + name + "_0_\" name=\"" + name + "[0]\" >";
                html += BuildSelectOptions(optionList);
                html += "</select>";
            }
            else
            {
                for (int i = 0; i < array.Count; i++)
                {
                    html += html = "<select class=\"form-control\" id=\"" + name + "_" + i + "_\" name=\"" + name + "[" + i + "]\" value=\"" + array[i] + "\" >";
                    html += BuildSelectOptions(optionList, array[i]);
                    html += "</select>";
                }
            }

            return new HtmlString(html);
        }

        private static string BuildSelectOptions(List<string> optionList, string defaultOption = "")
        {
            // Add the default blank option
            string html = "<option></option>";

            foreach(string option in optionList)
            {
                if(option.Equals(defaultOption))
                {
                    html += "<option value=\"" + option + "\" selected=\"selected\">" + option + "</option>";
                } else
                {
                    html += "<option value=\"" + option + "\">" + option + "</option>";
                }
            }

            return html;
        }
    }
}
