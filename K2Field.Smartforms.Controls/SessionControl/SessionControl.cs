using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using K2Field.Smartforms.Controls.InternalControls;
using System.Web.UI.WebControls;

namespace K2Field.Smartforms.Controls.SessionControl
{
    [InstallHelpers.RegisterDataType(SourceCode.Forms.Management.ControlDataType.Text)]
    [InstallHelpers.RegisterControlType("Session Reader Control", 
        PropertyXMLResourceName = "K2Field.Smartforms.Controls.SessionControl.SessionControlProperties.xml",
        SetValueMethod = "UtilitiesBehaviour.setValue",
        GetValueMethod = "UtilitiesBehaviour.getValue",
        GetPropertyMethod = "UtilitiesBehaviour.getControlProperty", 
        SetPropertyMethod = "UtilitiesBehaviour.setControlPropertyOrStyle",
        Category = SourceCode.Forms.Management.ControlTypeCategory.Input)]
    public class SessionControl : BaseControl
    {
        private System.Web.SessionState.HttpSessionState _sessionRef;
        private string _sessionvariablename = string.Empty;
        private string _sessionvariablevalue = string.Empty;
        private SessionControlExtender _extender;
        public string SessionVariableName
        {
            get { return _sessionvariablename; }
            set { _sessionvariablename = value; }
        }
        public string SessionVariableValue
        {
            get
            {
                if (!string.IsNullOrEmpty(SessionVariableName))
                {
                    if (_sessionRef[SessionVariableName] != null)
                    {
                        return _sessionRef[SessionVariableName] as string;
                    }
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(SessionVariableName))
                {
                    _sessionRef[SessionVariableName] = value;
                }
            }
        }

        public SessionControl()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _sessionRef = this.Page.Session;
        }


        protected override void CreateChildControls()
        {

            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                this.Controls.Add(new Literal() { Text = "The Session Variable Reader" });
            }

            InternalControls.InternalPanel panel = new InternalPanel();
            panel.ControlID = this.ControlID + "_hiddenPanel";
            this.Controls.Add(panel);

            _extender = new SessionControlExtender();
            _extender.ControlID = this.ControlID;
            _extender.TargetControlID = panel.ControlID;
            _extender.SessionVariableName = SessionVariableName;
            _extender.SessionVariableValue = SessionVariableValue;
            this.Controls.Add(_extender);
            base.CreateChildControls();
        }

        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderControl(writer);
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                Label label = new Label();
                label.Text = "Session Variable Reader Control";
                label.RenderControl(writer);
            }
        }
    }
}
