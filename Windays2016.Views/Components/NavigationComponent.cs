using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;

namespace Windays2016.Views.Components
{
    public class NavigationComponent : ViewComponent
    {
        private readonly IHostingEnvironment _environment;

        public NavigationComponent(IHostingEnvironment environment)
        {
            _environment = environment;
        }


        //public async Task<IViewComponentResult> InvokeAsync()
        public IViewComponentResult Invoke()
        {
            return View(_GetNavigationItems());
        }

        private List<ItemViewModel> _GetNavigationItems()
        {
            var navigationItems = new List<ItemViewModel>
            {
                new ItemViewModel("Home", Url.Action("Index", "Home")),
                new ItemViewModel("Contact", Url.Action("Contact", "Home")),
                new ItemViewModel("About", Url.Action("About", "Home"))
            };

            if (_environment.IsDevelopment())
                navigationItems.Add(new ItemViewModel("Development stranice", "/dev"));

            return navigationItems;
        }
    }
}
