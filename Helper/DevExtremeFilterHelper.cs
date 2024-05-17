using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;

namespace dgFilter.Helper
{
    public class DevExtremeFilterHelper
    {
        /// <summary>
        /// Get search terms from DevExtreme loadOptions
        /// </summary>
        /// <param name="filter">Filter from DevExtreme loadOptions</param>
        /// <param name="columns">Column names in your Datagrid table</param>
        /// <returns>Dictionary with searchType and searchCriterias</returns>
        public static Dictionary<string, Dictionary<string, string>> GetSearchTerms(IList filter, IEnumerable<string> columns)
        {
            if (filter == null) throw new Exception("filter is null");
            var node = (List<object>)filter;
            var criterias = GetCriterias();
            var searchDict = criterias.ToDictionary(criteria => criteria, criteria => columns.ToDictionary(col => col, columns => ""));
            return DfsSearch(node, searchDict, columns);
        }
        public static Dictionary<string, Dictionary<string, string>> DfsSearch(
            object node,
            Dictionary<string, Dictionary<string, string>> searchDict,
            IEnumerable<string> columns
        )
        {            
            if (node is null) return searchDict;
            if (node is JArray jarray)
            {
                node = jarray.ToObject<List<object>>();
            }

            if (node is not IEnumerable<object>) return searchDict;

            // Check structure List<object>{col,"=", searchTerm}
            var current = node as List<object>;
            if (current.First() is string)
            {
                var column = (string)current[0];
                var criteria = (string)current[1];
                var searchTerm = (string)current[2];
                searchDict[criteria][column] = searchTerm;
                return searchDict;
            }

            // Recursivity
            foreach (var child in current)
            {
                searchDict = DfsSearch(child, searchDict, columns);
            }

            return searchDict;

        }
        public static List<string> GetCriterias()
        {
            List<string> criterias = new();
            var type = typeof(GridFilterOperations);
            foreach (var member in type.GetMembers())
            {
                var attribute = member.GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if (attribute.Length > 0)
                {
                    var value = member.CustomAttributes.First().NamedArguments.First().TypedValue.ToString();
                    criterias.Add(value.Replace("\"", ""));
                }
            }
            return criterias;
        }
    }
}
