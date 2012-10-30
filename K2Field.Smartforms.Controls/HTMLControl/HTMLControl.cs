using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using K2Field.Smartforms.Controls.InternalControls;

namespace K2Field.Smartforms.Controls.HTMLControl
{
    [InstallHelpers.RegisterControlType("HTML Control", PropertyXMLResourceName="K2Field.Smartforms.Controls.HTMLControl.HTMLControlProperties.xml")]
    public class HTMLControl : BaseControl
    {
        public string HTML { get; set; }
        public bool ShowPlaceHolder { get; set; }
        public bool IsVisible { get; set; }

        public HTMLControl()
        {
            HTML = "No HTML configured.";
            ShowPlaceHolder = true;
        }

        protected override void CreateChildControls()
        {
            InternalTextbox tb = CreateTbControl();
            HTMLControlExtender ext = new HTMLControlExtender();
            ext.ControlID = this.ControlID;
            ext.TargetControlID = tb.ID;

            Controls.Add(tb);
            Controls.Add(ext);

            base.CreateChildControls();
        }

        private InternalTextbox CreateTbControl()
        {
            InternalTextbox tb = new InternalTextbox();
            tb.ID = this.ControlID + "_htmlcontrol";
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                //TODO: make this code work properly
                if (ShowPlaceHolder)
                {
                    tb.Text = "<div style='background:#F2F2F2;border:1px solid grey;'>HTML will appear here.</div>";
                }
            }
            else
            {
                tb.Text = this.HTML;
            }
            
            return tb;
        }


        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                CreateTbControl().RenderControl(writer);
            }

            base.RenderControl(writer);
        }
    }
}
