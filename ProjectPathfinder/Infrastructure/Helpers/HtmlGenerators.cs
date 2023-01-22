using ProjectPathfinder.Test.TestObjects;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjectPathfinder.Infrastructure.Helpers
{
    public static class HtmlGenerators
    {
        //-------------------------------------------------------------------------------------------

        public static List<SelectListItem> SelectList(int startValue, int endValue, string placeholder)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = placeholder, Value = "" });

            for (int i = startValue; i <= endValue; i++)
            {
                items.Add(new SelectListItem {
                                               Text = i.ToString(),
                                               Value = i.ToString()
                                             });
            }

            return items;
        }

        //-------------------------------------------------------------------------------------------

        public static List<SelectListItem> TablesSelectList(List<string> instanceList, string boxName)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select " + boxName, Value = "" });

            for (int i = 0; i < instanceList.Count; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = instanceList[i],
                    Value = i.ToString()
                });
            }

            return items;
        }

        //-------------------------------------------------------------------------------------------
    }
}