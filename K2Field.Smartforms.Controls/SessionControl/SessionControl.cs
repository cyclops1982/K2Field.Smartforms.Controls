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
    [InstallHelpers.RegisterControlType("Session Reader Control", PropertyXMLResourceName = "K2Field.Smartforms.Controls.SessionControl.SessionControlProperties.xml")]
    public class SessionControl : BaseControl
    {
        private System.Web.SessionState.HttpSessionState _sessionRef;
        private string _sessionvariablename = string.Empty;
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
                return "NOT OK";
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
            SessionVariableName = string.Empty;
            SessionVariableValue = string.Empty;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _sessionRef = this.Page.Session;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            _sessionRef = this.Page.Session;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
            panel.SessionVariableValue = "WAT?!";
            this.Controls.Add(panel);

            _extender = new SessionControlExtender();
            _extender.ControlID = this.ControlID;
            _extender.TargetControlID = panel.ControlID;
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
