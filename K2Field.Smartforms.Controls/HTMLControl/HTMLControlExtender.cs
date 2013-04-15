using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using System.Web.UI;

[assembly: WebResource("K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior.js", "text/javascript", PerformSubstitution = true)]

namespace K2Field.Smartforms.Controls.HTMLControl
{

    [ClientScriptResource("K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior", "K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior.js")]
    [TargetControlType(typeof(System.Web.UI.WebControls.Label))]
    public class HTMLControlExtender : ExtenderControlBase
    {
        [ClientPropertyName("_isVisible")]
        [ExtenderControlProperty]
        public bool IsVisible
        {
            get
            {
                return base.GetPropertyValue<bool>("IsVisible", true);
            }
            set
            {
                base.SetPropertyValue<bool>("IsVisible", value);
            }
        }
    }
}
