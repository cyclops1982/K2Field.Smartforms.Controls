using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using K2Field.Smartforms.Controls.InternalControls;

namespace K2Field.Smartforms.Controls.AutocompleteTextbox
{
    [InstallHelpers.RegisterDataType(SourceCode.Forms.Management.ControlDataType.Text)]
    [InstallHelpers.RegisterControlType("Autocomplete Text Box", PropertyXMLResourceName = "K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextboxProperties.xml")]
    public class AutocompleteTextbox : BaseControl
    {

        public AutocompleteTextbox()
        {
            Name = "AutocompleteTextbox";
            FriendlyName = "Autocomplete Text Box";
        }

        private InternalTextbox CreateTextBox()
        {
            InternalTextbox tb = new InternalTextbox();
            tb.ID = this.ControlID + "_textbox";
            tb.TextMode = System.Web.UI.WebControls.TextBoxMode.SingleLine;
            tb.CssClass = "funkytextbox";
            return tb;
        }

        protected override void CreateChildControls()
        {
            this.Controls.Add(CreateTextBox());

            AutocompleteTextboxExtender ext = new AutocompleteTextboxExtender();
            ext.TargetControlID = this.ControlID + "_textbox";
            ext.ControlID = this.ControlID;
            this.Controls.Add(ext);

            base.CreateChildControls();
        }

        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderControl(writer);
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                InternalTextbox tb = CreateTextBox();
                tb.RenderControl(writer);
            }
        }
    }
}
