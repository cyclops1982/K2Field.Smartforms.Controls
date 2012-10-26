using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using SourceCode.Forms.Controls.Web.Shared;

[assembly: WebResource("K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.css", "text/css", PerformSubstitution = true)]
namespace K2Field.Smartforms.Controls.AutocompleteTextbox
{
    [ClientScriptResource("K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox", "K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.js")]
    [TargetControlType(typeof(System.Web.UI.WebControls.TextBox))]

    public class AutocompleteTextboxExtender : ExtenderControlBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string css = "<link href=\"" + this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
            "K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.css") + "\" type=\"text/css\" rel=\"stylesheet\" />";


            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "cssFile", css, false);
        }

        [ExtenderControlProperty]
        [ClientPropertyName("_isEnabled")]
        public bool IsEnabled
        {
            get { return GetPropertyValue("IsEnabled", true); }
            set { SetPropertyValue("IsEnabled", value); }
        }

    }
}
