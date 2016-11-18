using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Filters;
using Windays2016.ControllersAndFilters.Services;

namespace Windays2016.ControllersAndFilters.Filters
{
    public class EmailNotificationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var emailSender = context.HttpContext.ApplicationServices.GetService(typeof(IEmailSender)) as IEmailSender;

            if (emailSender != null)
                emailSender.SendEmailAsync("vladan@netmedia.hr", "jako vazan subject maila", "notifikacijski email je poslan.");
        }
    }
}























//public class EmailNotificationFilter : ActionFilterAttribute
//{
//    private readonly IEmailSender _emailSender;
//    private readonly string _otherFromEmail;

//    public EmailNotificationFilter(IEmailSender emailSender)
//    {
//        _emailSender = emailSender;
//    }
//    public EmailNotificationFilter(IEmailSender emailSender, string otherFromEmail)
//    {
//        _emailSender = emailSender;
//        _otherFromEmail = otherFromEmail;
//    }

//    public override void OnActionExecuting(ActionExecutingContext context)
//    {
//        if (_emailSender != null)
//            _emailSender.SendEmailAsync("vladan@netmedia.hr", "jako vazan subject maila", "notifikacijski email je poslan.");
//    }
//}