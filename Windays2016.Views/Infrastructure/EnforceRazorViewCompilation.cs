using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Razor.Precompilation;
using Microsoft.Dnx.Compilation.CSharp;

namespace Windays2016.Views.Infrastructure
{
    public class EnforceRazorViewCompilation : RazorPreCompileModule
    {
        protected override bool EnablePreCompilation(BeforeCompileContext context)
        {
            return true;
        }
    }
}
