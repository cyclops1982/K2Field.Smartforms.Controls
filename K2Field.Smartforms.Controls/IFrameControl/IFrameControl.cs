using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using System.Web.UI.WebControls;
using K2Field.Smartforms.Controls.InternalControls;
using System.IO;
using System.Windows.Forms;

namespace K2Field.Smartforms.Controls.IFrameControl
{
    [InstallHelpers.RegisterPropertiesXml("K2Field.Smartforms.Controls.IFrameControl.IFrameControlProperties.xml")]
    public class IFrameControl : BaseControl
    {

        private Unit _height = new Unit(0.0, UnitType.Pixel);
        private Unit _width = new Unit(0.0, UnitType.Pixel);
        public Unit Width
        {
            set { _width = value; }
        }
        public Unit Height
        {
            set { _height = value; }
        }
        public string Scrolling { get; set; }
        public bool FrameBorder { get; set; }
        public string URL { get; set; }
        public bool IsVisible { get; set; }

        public IFrameControl()
        {
            Name = "IFrameControl";
            FriendlyName = "IFrame";
            IsVisible = true;
        }



        protected override void CreateChildControls()
        {
            InternalLiteral l = CreateLiteral();
            Controls.Add(l);

            IFrameControlExtender ext = new IFrameControlExtender();
            ext.ControlID = this.ControlID;
            ext.TargetControlID = l.ID;

            Controls.Add(ext);

            base.CreateChildControls();
        }

        private InternalLiteral CreateLiteral()
        {
            if (Scrolling == null)
                Scrolling = "Auto";

            InternalLiteral l = new InternalLiteral();
            l.ID = string.Concat(this.ControlID, "_iframe");
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                StringBuilder item = new StringBuilder();
                item.AppendFormat("<div style='width:{0};height:{1};", _width, _height);
                if (IsVisible)
                    item.Append("background:#F0F0F0;");
                else
                    item.Append("background:#C0C0C0;");
                if (FrameBorder)
                    item.Append("border:2px solid #000;");
                else 
                    item.Append("border:1px dashed #000;");
                if (string.Compare(Scrolling, "auto", true) == 0 || string.Compare(Scrolling, "yes", true) == 0)
                    item.AppendFormat("overflow-y: {0};", Scrolling.ToLower());
                item.Append("'>IFrame will show here</div>");

                l.Text = item.ToString();
            }
            else if (State == ControlState.Runtime)
            {
                l.Visible = IsVisible;
                l.Text = string.Format("<iframe id='{0}_internalframe' width='{1}' height='{2}' src='{3}' scrolling='{4}' frameborder='{5}' ></iframe>", this.ControlID, _width, _height, this.URL, this.Scrolling.ToLower(), (this.FrameBorder == true) ? "1" : "0");
            }
            return l;
        }

        /// <summary>
        /// This method seems to be called always. It is even called before the page actually renders and when the designer app pool is started.
        /// This is probably to 'discover' all the controls and know what HTML to output when designing. You might have noticed that no method gets called when you drag the item
        /// onto the canvas. This is therefor cached. I also think that's why an iisreset is needed for the designer. The runtime seems to pick up the assembly by itself.
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                CreateLiteral().RenderControl(writer);
            }
            base.RenderControl(writer);
        }


    }
}
