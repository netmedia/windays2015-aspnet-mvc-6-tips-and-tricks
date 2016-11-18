using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ViewFeatures;
using StructureMap.Attributes;
using Windays2016.ControllersAndFilters.Filters;

namespace Windays2016.ControllersAndFilters.Controllers
{
    public class HomeController
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public IModelMetadataProvider MetadataProvider;
        public IUrlHelper Url { get; set; }

        public HomeController(IHttpContextAccessor httpContextAccessor, IModelMetadataProvider metadataProvider, IUrlHelper url)
        {
            MetadataProvider = metadataProvider;
            HttpContextAccessor = httpContextAccessor;
            Url = url;
        }

        public IActionResult Index()
        {
            return new ViewResult { ViewName = "~/Views/Home/Index.cshtml" };
        }

        public IActionResult About()
        {
            var result = new ViewResult();

            result.ViewData = new ViewDataDictionary<string>(MetadataProvider, new ModelStateDictionary());
            result.ViewData["Message"] = "Your application description page.";

            result.ViewName = "~/Views/Home/About.cshtml";

            return result;
        }

        public IActionResult Contact()
        {
            var result = new ViewResult();

            result.ViewData = new ViewDataDictionary<string>(MetadataProvider, new ModelStateDictionary());
            result.ViewData["Message"] = "Your contact page.";

            result.ViewName = "~/Views/Home/Contact.cshtml";

            return result;
        }

        public IActionResult Error()
        {
            var result = new ViewResult();
            result.ViewName = "~/Views/Shared/Error.cshtml";

            return result;
        }
    }

    //public class HomeController : Controller
    //{
    //    //public IHttpContextAccessor HttpContextLocal { get; set; }

    //    //public HomeController(IHttpContextAccessor httpContext)
    //    //{
    //    //    HttpContextLocal = httpContext;
    //    //}

    //    public IActionResult Index()
    //    {
    //        //var context = HttpContextLocal;

    //        return View();
    //    }

    //    public IActionResult About()
    //    {
    //        ViewData["Message"] = "Your application description page.";

    //        return View();
    //    }

    //    //[EmailNotificationFilter]
    //    public IActionResult Contact()
    //    {
    //        ViewData["Message"] = "Your contact page.";

    //        return View();
    //    }

    //    public IActionResult Error()
    //    {
    //        return View();
    //    }
    //}
}












//[SetterProperty]























//[TypeFilter(typeof(EmailNotificationFilter))]
//[TypeFilter(typeof(EmailNotificationFilter), Arguments = new object[] { "ivan@netmedia.hr" })]
//[ServiceFilter(typeof(EmailNotificationFilter))]






























//public class HomeController
//{
//    public IHttpContextAccessor HttpContextAccessor { get; set; }
//    public IModelMetadataProvider MetadataProvider;
//    public IUrlHelper Url { get; set; }

//    public HomeController(IHttpContextAccessor httpContextAccessor, IModelMetadataProvider metadataProvider, IUrlHelper url)
//    {
//        MetadataProvider = metadataProvider;
//        HttpContextAccessor = httpContextAccessor;
//        Url = url;
//    }

//    public IActionResult Index()
//    {
//        return new ViewResult { ViewName = "~/Views/Home/Index.cshtml" };
//    }

//    public IActionResult About()
//    {
//        var result = new ViewResult();

//        result.ViewData = new ViewDataDictionary<string>(MetadataProvider, new ModelStateDictionary());
//        result.ViewData["Message"] = "Your application description page.";

//        result.ViewName = "~/Views/Home/About.cshtml";

//        return result;
//    }

//    public IActionResult Contact()
//    {
//        var result = new ViewResult();

//        result.ViewData = new ViewDataDictionary<string>(MetadataProvider, new ModelStateDictionary());
//        result.ViewData["Message"] = "Your contact page.";

//        result.ViewName = "~/Views/Home/Contact.cshtml";

//        return result;
//    }

//    public IActionResult Error()
//    {
//        var result = new ViewResult();
//        result.ViewName = "~/Views/Shared/Error.cshtml";

//        return result;
//    }
//}