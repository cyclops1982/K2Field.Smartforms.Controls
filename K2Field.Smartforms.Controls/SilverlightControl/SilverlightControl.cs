using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using K2Field.Smartforms.Controls.InternalControls;

namespace K2Field.Smartforms.Controls.SilverlightControl
{

    [InstallHelpers.RegisterDataType(SourceCode.Forms.Management.ControlDataType.Text)]
    [InstallHelpers.RegisterControlType("SilverlightControl", PropertyXMLResourceName = "K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlProperties.xml")]
    public class SilverlightControl : BaseControl
    {
        public String Text { get; set; }

        public SilverlightControl()
        {

        }

        protected override void CreateChildControls()
        {
            
            InternalTextbox tb = new InternalTextbox();
            tb.Text = Text;
            tb.ID = this.ControlID + "_textboxinternal";
            tb.TextMode = System.Web.UI.WebControls.TextBoxMode.Password;
            this.Controls.Add(tb);

            SilverlightControlExtender extender = new SilverlightControlExtender();
            extender.ControlID = this.ControlID;
            extender.TargetControlID = tb.ID;
            this.Controls.Add(extender);

            base.CreateChildControls();
        }

        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderControl(writer);
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                InternalTextbox tb = new InternalTextbox();
                tb.Text = Text;
                tb.ID = this.ControlID + "_textboxinternal";
                tb.TextMode = System.Web.UI.WebControls.TextBoxMode.Password;
                tb.RenderControl(writer);
            }
            
        }

    }
}
