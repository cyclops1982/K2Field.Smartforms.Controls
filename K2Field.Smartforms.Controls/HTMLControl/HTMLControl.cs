using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using K2Field.Smartforms.Controls.InternalControls;

namespace K2Field.Smartforms.Controls.HTMLControl
{
    [InstallHelpers.RegisterControlType("HTML Control", PropertyXMLResourceName = "K2Field.Smartforms.Controls.HTMLControl.HTMLControlProperties.xml")]
    public class HTMLControl : BaseControl
    {
        private string _html;
        public string HTML
        {
            get
            {
                return _html;
            }
            set
            {
                _html = value;
            }
        }
        public bool IsVisible { get; set; }

        public HTMLControl()
        {
            HTML = "No HTML configured.";
        }

        protected override void CreateChildControls()
        {
            InternalLiteral tb = CreateLiteralControl();
            HTMLControlExtender ext = new HTMLControlExtender();
            ext.ControlID = this.ControlID;
            ext.TargetControlID = tb.ID;

            Controls.Add(tb);
            Controls.Add(ext);

            base.CreateChildControls();
        }

        private InternalLiteral CreateLiteralControl()
        {
            InternalLiteral l = new InternalLiteral();
            l.ID = string.Concat(this.ControlID, "_htmlcontrol");
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                l.Text = "<div>HTML will be output here</div>";
            }
            else
            {
                l.Text = HTML;
            }
            return l;
        }


        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                CreateLiteralControl().RenderControl(writer);
            }

            base.RenderControl(writer);
        }
    }
}
