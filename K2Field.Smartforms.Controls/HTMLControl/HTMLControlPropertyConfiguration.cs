using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web;
using SourceCode.Forms.Web.Controls.Input;
using System.Web.UI;


[assembly: WebResource("K2Field.Smartforms.Controls.HTMLControl.HTMLControlPropertyConfiguration.js", "text/javascript", PerformSubstitution = true)]

namespace K2Field.Smartforms.Controls.HTMLControl
{
    public class HTMLControlPropertyConfiguration : SourceCode.Forms.Controls.Web.Shared.PropertyConfigurationBase
    {
        public HTMLControlPropertyConfiguration()
        {
            base.CodePaths.Add("HTMLControlPropertyConfiguration", "K2Field.Smartforms.Controls.HTMLControl.HTMLControlPropertyConfiguration.js");
            base.Name = "HTMLControlPropertyConfiguration";
        }


        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            PanelInternal pi = new PanelInternal();
            pi.ControlID = "HTMLControlPropertyConfiguration";
            TextArea textArea = new TextArea();
            textArea.Width = 590;
            textArea.Rows = 33;
            pi.Controls.Add(textArea);
            pi.RenderControl(writer);
        }
    }
}
