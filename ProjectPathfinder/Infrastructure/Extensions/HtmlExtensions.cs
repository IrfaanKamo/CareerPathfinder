using System.Web;
using System.Web.Mvc;

namespace ProjectPathfinder.Infrastructure.Extensions
{
    public static class HtmlExtensions
    {
        //------------------------------------------------------------------------------------------------------

        public static IHtmlString ModalDialog(this HtmlHelper html, string id, string title, string description)
        {
            string htmlString = string.Format (
                @"<div id=""{0}"" class=""modal fade"" role=""dialog"">
                    <div class=""modal-dialog modal-lg"">
                        <div class=""modal-content"">

                            <div class=""modal-header"">
                                <button type = ""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                                <h4 class=""modal-title"" align=""center"">{1}</h4>
                            </div>

                            <div class=""modal-body"">
                                {2}
                                <div>

                                    <div class=""modal-footer"">
                                        <button type = ""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>", id, title, description);

            return new HtmlString(htmlString);
        }

        //------------------------------------------------------------------------------------------------------

        public static IHtmlString ResourceItem(this HtmlHelper html, string url, string imagePath, string title, string description)
        {
            string htmlString = string.Format(
                @"
                <div class=""resource-item"">
                    <img src = ""{0}""/>
                    <div class=""info"">
                        <p class=""title"">{1}</p>
                        <p class=""desc"">{2}</p>
                        <ul>
                            <li><a href = ""{3}"" target=""blank""> View PDF</a></li>
                        </ul>
                    </div>
                </div>", imagePath, title, description, url);

            return new HtmlString(htmlString);
        }


        //------------------------------------------------------------------------------------------------------

        public static IHtmlString TestScreenButtons(this HtmlHelper html, string stepName)
        {
            string htmlString = string.Format(
                @"  <ul class=""list-unstyled"">
                        <li class=""pull-right"">
                            <button type=""submit"" name=""action:{0}Next"" class=""btn btn-primary test-button"">
                                Next Step <i class=""glyphicon glyphicon-chevron-right""></i>
                            </button>
                        </li>
                        <li class=""pull-left"">
                            <button type=""submit"" name=""action:{0}Previous"" class=""btn btn-primary test-button cancel"">
                                <i class=""glyphicon glyphicon-chevron-left""></i> Previous Step
                            </button>
                        </li>
                    </ul>
                    <ul class=""list-unstyled text-center"">
                        <li>
                            <button type=""submit"" name=""action:SaveAndExit"" class=""btn btn-grey test-button cancel"" data-toggle = ""tooltip"" data-placement = ""top"" title = ""This will only save all previous steps. To save the current step you will need to complete it, then proceed to the next step and then click save."">
                                <i class=""glyphicon glyphicon-save""></i> Save all Completed Steps and Exit
                            </button>
                        </li>
                    </ul>", stepName);

            

            return new HtmlString(htmlString);
        }

        //------------------------------------------------------------------------------------------------------

        public static IHtmlString SubjectMarks(this HtmlHelper html, MvcHtmlString name, MvcHtmlString mark1, MvcHtmlString mark2)
        {
            string htmlString = string.Format(
                @"<tr>
                    <td>{0}</td>
                    <td>
                        <div class=""percent-text"">                            
                            {1}
                            <i>%</i>
                        </div>
                    </td>
                    <td>
                        <div class=""percent-text"">                            
                            {2}
                            <i>%</i>
                        </div>
                    </td>
                </tr>", name, mark1, mark2);

            return new HtmlString(htmlString);
        }

        //------------------------------------------------------------------------------------------------------
    }
}