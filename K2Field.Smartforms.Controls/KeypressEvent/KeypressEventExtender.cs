using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using SourceCode.Forms.Controls.Web.Shared;


[assembly: WebResource("K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior.js", "text/javascript", PerformSubstitution = true)]

namespace K2Field.Smartforms.Controls.KeypressEvent
{
    [ClientScriptResource("K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior", "K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior.js")]
    [TargetControlType(typeof(System.Web.UI.WebControls.HiddenField))]
    public class KeyPressEventExtender : ExtenderControlBase
    {
        [ExtenderControlProperty]
        [ClientPropertyName("_isEnabled")]
        public bool IsEnabled
        {
            get { return GetPropertyValue("IsEnabled", true); }
            set { SetPropertyValue("IsEnabled", value); }
        }
    }
}