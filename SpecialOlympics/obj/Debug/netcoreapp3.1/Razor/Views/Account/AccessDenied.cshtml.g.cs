#pragma checksum "C:\Users\jorge\source\repos\SpecialOlympics\SpecialOlympics\Views\Account\AccessDenied.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a0eb83ac65170510417630219a082eb88da86e83"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_AccessDenied), @"mvc.1.0.view", @"/Views/Account/AccessDenied.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\jorge\source\repos\SpecialOlympics\SpecialOlympics\Views\_ViewImports.cshtml"
using SpecialOlympics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\jorge\source\repos\SpecialOlympics\SpecialOlympics\Views\_ViewImports.cshtml"
using SpecialOlympics.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\jorge\source\repos\SpecialOlympics\SpecialOlympics\Views\_ViewImports.cshtml"
using SpecialOlympics.Models.StoredProcedures;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\jorge\source\repos\SpecialOlympics\SpecialOlympics\Views\_ViewImports.cshtml"
using SpecialOlympics.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\jorge\source\repos\SpecialOlympics\SpecialOlympics\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0eb83ac65170510417630219a082eb88da86e83", @"/Views/Account/AccessDenied.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"251075eae78bf16f3b4e4347a5c15982d638d783", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_AccessDenied : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\jorge\source\repos\SpecialOlympics\SpecialOlympics\Views\Account\AccessDenied.cshtml"
  
    ViewBag.Title = "Acceso denegado";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Acceso denegado</h1>\r\n<hr />\r\n<div class=\"text-justify\">\r\n    <h4 class=\"text-danger\">No tiene permisos para acceder a esta página.</h4>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
