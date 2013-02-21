using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace K2Field.Smartforms.Controls.InternalControls
{
    public class InternalPanel : Panel
    {
        private string _controlID = "";
        public string ControlID
        {
            get
            {
                return this._controlID;
            }
            set
            {
                this.ID = value;
                this._controlID = value;
            }
        }
        public override string UniqueID
        {
            get
            {
                return this._controlID;
            }
        }
        public override string ClientID
        {
            get
            {
                return this._controlID;
            }
        }
    }
}
