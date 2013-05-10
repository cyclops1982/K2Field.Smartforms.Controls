using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using System.Web.UI;
using System.ComponentModel;

[assembly: WebResource("K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior.js", "text/javascript", PerformSubstitution = true)]
namespace K2Field.Smartforms.Controls.SessionControl
{
    [ClientScriptResource("K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior", "K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior.js")]
    [TargetControlType(typeof(InternalControls.InternalPanel))]
    public class SessionControlExtender : ExtenderControlBase
    {

        [ExtenderControlProperty]
        [ClientPropertyName("_sessionvariablename")]
        [DefaultValue("")]
        public string SessionVariableName
        {


            get { return base.GetPropertyValue<string>("SessionVariableName", ""); }
            set { base.SetPropertyValue<string>("SessionVariableName", value); }
        }

        [ExtenderControlProperty]
        [ClientPropertyName("_sessionvariablevalue")]
        [DefaultValue("")]
        public string SessionVariableValue
        {
            get { return base.GetPropertyValue<string>("SessionVariableValue", ""); }
            set { base.SetPropertyValue<string>("SessionVariableValue", value); }
        }
    }
}
