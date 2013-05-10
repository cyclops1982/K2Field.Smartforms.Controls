using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using K2Field.Smartforms.Controls.InternalControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

namespace K2Field.Smartforms.Controls.SilverlightControl
{

    [InstallHelpers.RegisterDataType(SourceCode.Forms.Management.ControlDataType.File)]
    [InstallHelpers.RegisterControlType("SilverlightControl",
        GetValueMethod="UtilitiesBehaviour.getFileUploaderValue",
        SetValueMethod="UtilitiesBehaviour.setFileUploaderValue", 
        GetPropertyMethod="UtilitiesBehaviour.getControlProperty",
        SetPropertyMethod = "UtilitiesBehaviour.setControlPropertyOrStyle", 
        Group = "Silverlight",
        PropertyXMLResourceName = "K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlProperties.xml")]
    public class SilverlightControl : BaseControl, ICallbackEventHandler
    {
        public String Text { get; set; }
        private SilverlightControlExtender _extender;
        private ClientScriptManager _clientScriptManager;
        private string _returnFromEvent;
        public SilverlightControl()
        {

        }
   
 
        public string GetCallbackResult()
        {
            return this._returnFromEvent;
        }

        // SourceCode.Forms.Controls.Web.File
        public void RaiseCallbackEvent(string eventArgument)
        {
            FileUploadEventArgs fileUploadEventArgs = new FileUploadEventArgs(eventArgument);
            string newFileName = fileUploadEventArgs.NewFileName;
            string fileContents = fileUploadEventArgs.FileContents;
             StreamWriter streamWriter;
            if (string.IsNullOrEmpty(newFileName))
            {
                string oldFileName = fileUploadEventArgs.OldFileName;
                string oldFileExtension = oldFileName.Substring(oldFileName.IndexOf('.') + 1);
                newFileName = string.Concat(base.FilePath, "\\", Guid.NewGuid(), ".", oldFileExtension);
                streamWriter = new StreamWriter(newFileName);
            }
            else
            {
                streamWriter = new StreamWriter(newFileName, true);
            }
            string[] chunks = fileContents.Split(',');
            BinaryWriter binaryWriter = new BinaryWriter(streamWriter.BaseStream);
            for (int i = 1; i < chunks.Length; i++)
            {
                binaryWriter.Write(byte.Parse(chunks[i]));
            }
            streamWriter.Flush();
            streamWriter.Close();
            this._returnFromEvent = newFileName;

        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _clientScriptManager = this.Page.ClientScript;
            _clientScriptManager.GetCallbackEventReference(this, "", "", "");
            EnsureChildControls();
            _extender.CallbackID = this.UniqueID;
        }

        protected override void CreateChildControls()
        {
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                this.Controls.Add(new Literal() { Text = "The silverlight control" });
            }
            else
            {
                 string resourceUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(K2Field.Smartforms.Controls.SilverlightControl.SilverlightControl), "K2Field.Smartforms.Controls.SilverlightControl.SilverlightUpload.xap");
                Literal l = new Literal();
                StringBuilder controlThing = new StringBuilder();
                controlThing.AppendFormat("<object id=\"{0}\" data=\"data:application/x-silverlight-2,\" type=\"application/x-silverlight-2\" width=\"200px\" height=\"200px\">", this.ControlID + "_SilverlightControl");
                controlThing.AppendFormat("<param name=\"source\" value=\"{0}\"/>", resourceUrl);
                controlThing.Append("<param name=\"minRuntimeVersion\" value=\"4.0.50826.0\" />");
                controlThing.Append("<param name=\"autoUpgrade\" value=\"true\" />");
                controlThing.Append("<param name=\"enablehtmlaccess\" value=\"true\" />");
                controlThing.AppendFormat("<param name=\"initParams\" value=\"objectID={0}\" />", this.ControlID);
                controlThing.Append("<a href=\"http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50826.0\" style=\"text-decoration:none\">");
                controlThing.Append("<img src=\"http://go.microsoft.com/fwlink/?LinkId=161376\" alt=\"Get Microsoft Silverlight\" style=\"border-style:none\"/>");
                controlThing.Append("</a>");
                controlThing.Append("</object></div>");
                l.Text = controlThing.ToString();
                this.Controls.Add(l);
            }

            InternalControls.InternalPanel panel = new InternalPanel();
            panel.ControlID = this.ControlID + "_filePanel";
            this.Controls.Add(panel);

            _extender = new SilverlightControlExtender();
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
                Label l = new Label();
                l.Text = "Silverlight control, design and preview mode.";
                l.RenderControl(writer);
            }

        }



    }
}
