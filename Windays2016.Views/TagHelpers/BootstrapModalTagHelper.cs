using System.Threading.Tasks;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Razor.TagHelpers;

namespace Windays2016.Views.TagHelpers
{
    /*

ORIGINAL      
    
<div class="modal fade">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Modal title</h4>
      </div>
      <div class="modal-body">
        <p>One fine body&hellip;</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->     
         
         */

    /*

         MODAL TAG HELPER

 <modal id="simpleModal" title="Modal Title" >
     <modal-body>
         <p>One fine body&hellip;</p>
     </modal-body>
     <modal-footer>
         <button type="button" class="btn btn-primary">Save changes</button>
     </modal-footer>
 </modal>

     */

    /// <summary>
    /// A Bootstrap modal dialog
    /// </summary>
    [RestrictChildren("modal-body", "modal-footer")]
    public class BootstrapModalTagHelper : TagHelper
    {
        /// <summary>
        /// The title of the modal
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Id of the modal
        /// </summary>
        public string Id { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var modalContext = new ModalContext();
            context.Items.Add(typeof(BootstrapModalTagHelper), modalContext);

            await output.GetChildContentAsync();

            var template =
                $@"<div class='modal-dialog' role='document'>
    <div class='modal-content'>
      <div class='modal-header'>
        <button type = 'button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
        <h4 class='modal-title' id='{context.UniqueId}Label'>{Title}</h4>
      </div>
        <div class='modal-body'>";

            output.TagName = "div";
            output.Attributes["role"] = "dialog";
            output.Attributes["id"] = Id;
            output.Attributes["aria-labelledby"] = $"{context.UniqueId}Label";
            output.Attributes["tabindex"] = "-1";
            var classNames = "modal fade";
            if (output.Attributes.ContainsName("class"))
            {
                classNames = string.Format("{0} {1}", output.Attributes["class"].Value, classNames);
            }
            output.Attributes["class"] = classNames;
            output.Content.AppendHtml(template);
            if (modalContext.Body != null)
            {
                output.Content.Append(modalContext.Body);
            }
            output.Content.AppendHtml("</div>");
            if (modalContext.Footer != null)
            {
                output.Content.AppendHtml("<div class='modal-footer'>");
                output.Content.Append(modalContext.Footer);
                output.Content.AppendHtml("</div>");
            }

            output.Content.AppendHtml("</div></div>");
        }
    }

    public class ModalContext
    {
        public IHtmlContent Body { get; set; }
        public IHtmlContent Footer { get; set; }
    }

    /// <summary>
    /// The modal-body portion of a Bootstrap modal dialog
    /// </summary>
    [HtmlTargetElement("modal-body", ParentTag = "modal")]
    public class BootstrapModalBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var modalContext = (ModalContext)context.Items[typeof(BootstrapModalBodyTagHelper)];
            modalContext.Body = childContent;
            output.SuppressOutput();
        }
    }


    [HtmlTargetElement("modal-footer", ParentTag = "modal")]
    public class BootstrapModalFooterTagHelper : TagHelper
    {
        /// <summary>
        /// Whether or not to show a button to dismiss the dialog. 
        /// Default: <c>true</c>
        /// </summary>
        public bool ShowDismiss { get; set; } = true;

        /// <summary>
        /// The text to show on the Dismiss button
        /// Default: Cancel
        /// </summary>
        public string DismissText { get; set; } = "Cancel";


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (ShowDismiss)
            {
                output.PreContent.AppendFormat(@"<button type='button' class='btn btn-default' data-dismiss='modal'>{0}</button>", DismissText);
            }
            var childContent = await output.GetChildContentAsync();
            var footerContent = new DefaultTagHelperContent();
            if (ShowDismiss)
            {
                footerContent.AppendFormat(@"<button type='button' class='btn btn-default' data-dismiss='modal'>{0}</button>", DismissText);
            }
            footerContent.Append(childContent);
            var modalContext = (ModalContext)context.Items[typeof(BootstrapModalTagHelper)];
            modalContext.Footer = footerContent;
            output.SuppressOutput();
        }
    }

}