#pragma checksum "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "03197dabae391285e4f04f2b4a74183afb552b57"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employees_Delete), @"mvc.1.0.view", @"/Views/Employees/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Employees/Delete.cshtml", typeof(AspNetCore.Views_Employees_Delete))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03197dabae391285e4f04f2b4a74183afb552b57", @"/Views/Employees/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"62c3a4dff41bfda2f62716864e6dedd3530d2ec2", @"/Views/_ViewImports.cshtml")]
    public class Views_Employees_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TinyERP4Fun.Models.Common.Employee>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(43, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
  
    ViewData["Title"] = Localization.currentLocalizatin["Delete"];
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(167, 6, true);
            WriteLiteral("\r\n<h1>");
            EndContext();
            BeginContext(174, 41, false);
#line 8 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
Write(Localization.currentLocalizatin["Delete"]);

#line default
#line hidden
            EndContext();
            BeginContext(215, 13, true);
            WriteLiteral("</h1>\r\n\r\n<h3>");
            EndContext();
            BeginContext(229, 72, false);
#line 10 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
Write(Localization.currentLocalizatin["Are you sure you want to delete this?"]);

#line default
#line hidden
            EndContext();
            BeginContext(301, 22, true);
            WriteLiteral("</h3>\r\n<div>\r\n    <h4>");
            EndContext();
            BeginContext(324, 43, false);
#line 12 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
   Write(Localization.currentLocalizatin["Employee"]);

#line default
#line hidden
            EndContext();
            BeginContext(367, 84, true);
            WriteLiteral("</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(452, 45, false);
#line 16 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Localization.currentLocalizatin["First Name"]);

#line default
#line hidden
            EndContext();
            BeginContext(497, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(559, 48, false);
#line 19 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Person.FirstName));

#line default
#line hidden
            EndContext();
            BeginContext(607, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(668, 44, false);
#line 22 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Localization.currentLocalizatin["Last Name"]);

#line default
#line hidden
            EndContext();
            BeginContext(712, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(774, 47, false);
#line 25 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Person.LastName));

#line default
#line hidden
            EndContext();
            BeginContext(821, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(882, 41, false);
#line 28 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Localization.currentLocalizatin["Number"]);

#line default
#line hidden
            EndContext();
            BeginContext(923, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(985, 36, false);
#line 31 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1021, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1082, 45, false);
#line 34 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Localization.currentLocalizatin["Department"]);

#line default
#line hidden
            EndContext();
            BeginContext(1127, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1189, 47, false);
#line 37 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Department.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1236, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1297, 43, false);
#line 40 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Localization.currentLocalizatin["Position"]);

#line default
#line hidden
            EndContext();
            BeginContext(1340, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1402, 45, false);
#line 43 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Position.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1447, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1508, 42, false);
#line 46 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Localization.currentLocalizatin["Address"]);

#line default
#line hidden
            EndContext();
            BeginContext(1550, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1612, 39, false);
#line 49 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Address));

#line default
#line hidden
            EndContext();
            BeginContext(1651, 34, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n\r\n    ");
            EndContext();
            BeginContext(1685, 276, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03197dabae391285e4f04f2b4a74183afb552b5711673", async() => {
                BeginContext(1711, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(1721, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "03197dabae391285e4f04f2b4a74183afb552b5712066", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 54 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1757, 30, true);
                WriteLiteral("\r\n        <input type=\"submit\"");
                EndContext();
                BeginWriteAttribute("value", " value=", 1787, "", 1836, 1);
#line 55 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
WriteAttributeValue("", 1794, Localization.currentLocalizatin["Delete"], 1794, 42, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1836, 38, true);
                WriteLiteral(" class=\"btn btn-danger\" /> |\r\n        ");
                EndContext();
                BeginContext(1874, 74, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03197dabae391285e4f04f2b4a74183afb552b5714454", async() => {
                    BeginContext(1897, 47, false);
#line 56 "C:\Users\Yauheni_Tarhonski\Source\Repos\epm_lab_Yauheni_Tarhonski\TinyERP4Fun\Views\Employees\Delete.cshtml"
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
                BeginContext(1948, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1961, 10, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TinyERP4Fun.Models.Common.Employee> Html { get; private set; }
    }
}
#pragma warning restore 1591
