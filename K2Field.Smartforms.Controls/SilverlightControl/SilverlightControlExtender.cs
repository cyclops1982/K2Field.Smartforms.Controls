using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using System.Web.UI;


[assembly: WebResource("K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.js", "text/javascript", PerformSubstitution = true)]
namespace K2Field.Smartforms.Controls.SilverlightControl
{
    [ClientScriptResource("K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior", "K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.js")]
    [TargetControlType(typeof(System.Web.UI.WebControls.TextBox))]
    public class SilverlightControlExtender : ExtenderControlBase
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
