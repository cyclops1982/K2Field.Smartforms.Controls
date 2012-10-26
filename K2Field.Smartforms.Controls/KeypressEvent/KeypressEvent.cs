using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using System.Web.UI.WebControls;

namespace K2Field.Smartforms.Controls.KeypressEvent
{

    [InstallHelpers.RegisterEvent("Keypressed", true)]
    public class KeypressEvent : BaseControl
    {
        public KeypressEvent()
        {
            Name = "KeyPressEvent";
            FriendlyName = "KeyPress Event";
            IsEnabled = true;
        }

        protected override void CreateChildControls()
        {
            InternalHiddenField l = new InternalHiddenField();
            l.ID = this.ControlID + "_textboxInternal";
            this.Controls.Add(l);

            KeyPressEventExtender ext = new KeyPressEventExtender();
            ext.ControlID = this.ControlID;
            ext.TargetControlID = this.ControlID + "_textboxInternal";
            this.Controls.Add(ext);

            base.CreateChildControls();
        }

        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderControl(writer);
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                InternalHiddenField l = new InternalHiddenField();
                l.ID = this.ControlID + "_textboxInternal";      
                l.RenderControl(writer);

                Label label = new Label();
                label.Text = "Keypress event placeholder";
                label.RenderControl(writer);
                
            }
        }
    }
}
