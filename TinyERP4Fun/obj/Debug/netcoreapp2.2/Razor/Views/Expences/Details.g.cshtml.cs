#pragma checksum "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fad1c7af784b05e59092917f71a0b5704067e73f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Expences_Details), @"mvc.1.0.view", @"/Views/Expences/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Expences/Details.cshtml", typeof(AspNetCore.Views_Expences_Details))]
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
#line 1 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\_ViewImports.cshtml"
using TinyERP4Fun;

#line default
#line hidden
#line 2 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\_ViewImports.cshtml"
using TinyERP4Fun.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fad1c7af784b05e59092917f71a0b5704067e73f", @"/Views/Expences/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"62c3a4dff41bfda2f62716864e6dedd3530d2ec2", @"/Views/_ViewImports.cshtml")]
    public class Views_Expences_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TinyERP4Fun.Models.Expenses.Expences>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(45, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
  
    ViewData["Title"] = Localization.currentLocalizatin["Details"];
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(170, 6, true);
            WriteLiteral("\r\n<h1>");
            EndContext();
            BeginContext(177, 42, false);
#line 8 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
Write(Localization.currentLocalizatin["Details"]);

#line default
#line hidden
            EndContext();
            BeginContext(219, 24, true);
            WriteLiteral("</h1>\r\n\r\n<div>\r\n    <h4>");
            EndContext();
            BeginContext(244, 43, false);
#line 11 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
   Write(Localization.currentLocalizatin["Expences"]);

#line default
#line hidden
            EndContext();
            BeginContext(287, 84, true);
            WriteLiteral("</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(372, 50, false);
#line 15 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Document Number"]);

#line default
#line hidden
            EndContext();
            BeginContext(422, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(484, 46, false);
#line 18 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.DocumentNumber));

#line default
#line hidden
            EndContext();
            BeginContext(530, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(591, 48, false);
#line 21 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Document Date"]);

#line default
#line hidden
            EndContext();
            BeginContext(639, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(701, 44, false);
#line 24 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.DocumentDate));

#line default
#line hidden
            EndContext();
            BeginContext(745, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(806, 44, false);
#line 27 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Full Name"]);

#line default
#line hidden
            EndContext();
            BeginContext(850, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(912, 47, false);
#line 30 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.Person.FullName));

#line default
#line hidden
            EndContext();
            BeginContext(959, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1020, 39, false);
#line 33 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["User"]);

#line default
#line hidden
            EndContext();
            BeginContext(1059, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1121, 42, false);
#line 36 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.User.Email));

#line default
#line hidden
            EndContext();
            BeginContext(1163, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1224, 46, false);
#line 39 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Our Company"]);

#line default
#line hidden
            EndContext();
            BeginContext(1270, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1332, 47, false);
#line 42 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.OurCompany.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1379, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1440, 42, false);
#line 45 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Company"]);

#line default
#line hidden
            EndContext();
            BeginContext(1482, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1544, 44, false);
#line 48 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.Company.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1588, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1649, 55, false);
#line 51 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Desired Payment Date"]);

#line default
#line hidden
            EndContext();
            BeginContext(1704, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1766, 50, false);
#line 54 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.DesiredPaymentDate));

#line default
#line hidden
            EndContext();
            BeginContext(1816, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1877, 56, false);
#line 57 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Approved Payment Date"]);

#line default
#line hidden
            EndContext();
            BeginContext(1933, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1995, 51, false);
#line 60 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.ApprovedPaymentDate));

#line default
#line hidden
            EndContext();
            BeginContext(2046, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(2107, 52, false);
#line 63 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Amount Of Payment"]);

#line default
#line hidden
            EndContext();
            BeginContext(2159, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(2221, 47, false);
#line 66 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.AmountOfPayment));

#line default
#line hidden
            EndContext();
            BeginContext(2268, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(2329, 43, false);
#line 69 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Currency"]);

#line default
#line hidden
            EndContext();
            BeginContext(2372, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(2434, 45, false);
#line 72 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.Currency.Code));

#line default
#line hidden
            EndContext();
            BeginContext(2479, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(2540, 43, false);
#line 75 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Approved"]);

#line default
#line hidden
            EndContext();
            BeginContext(2583, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(2645, 40, false);
#line 78 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.Approved));

#line default
#line hidden
            EndContext();
            BeginContext(2685, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(2746, 43, false);
#line 81 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Declined"]);

#line default
#line hidden
            EndContext();
            BeginContext(2789, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(2851, 40, false);
#line 84 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.Declined));

#line default
#line hidden
            EndContext();
            BeginContext(2891, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(2952, 53, false);
#line 87 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Localization.currentLocalizatin["Purpose Of Payment"]);

#line default
#line hidden
            EndContext();
            BeginContext(3005, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(3067, 48, false);
#line 90 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
       Write(Html.DisplayFor(model => model.PurposeOfPayment));

#line default
#line hidden
            EndContext();
            BeginContext(3115, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(3162, 90, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fad1c7af784b05e59092917f71a0b5704067e73f16967", async() => {
                BeginContext(3209, 39, false);
#line 95 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
                                             Write(Localization.currentLocalizatin["Edit"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 95 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
                           WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3252, 8, true);
            WriteLiteral(" |\r\n    ");
            EndContext();
            BeginContext(3260, 74, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fad1c7af784b05e59092917f71a0b5704067e73f19525", async() => {
                BeginContext(3283, 47, false);
#line 96 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Expences\Details.cshtml"
                     Write(Localization.currentLocalizatin["Back to List"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3334, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TinyERP4Fun.Models.Expenses.Expences> Html { get; private set; }
    }
}
#pragma warning restore 1591
