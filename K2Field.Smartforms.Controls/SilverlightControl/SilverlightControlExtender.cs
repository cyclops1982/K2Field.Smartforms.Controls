using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using System.Web.UI;


[assembly: WebResource("K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("K2Field.Smartforms.Controls.SilverlightControl.SilverlightUpload.xap",  "application/x-silverlight-2")]
namespace K2Field.Smartforms.Controls.SilverlightControl
{
    [ClientScriptResource("K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior", "K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.js")]
    [TargetControlType(typeof(InternalControls.InternalPanel))]
    public class SilverlightControlExtender : ExtenderControlBase
    {
        [ExtenderControlProperty]
        public string CallbackID
        {
            get
            {
                return base.GetPropertyValue<string>("CallbackID", string.Empty);
            }
            set
            {
                base.SetPropertyValue<string>("CallbackID", value);
            }
        }


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
