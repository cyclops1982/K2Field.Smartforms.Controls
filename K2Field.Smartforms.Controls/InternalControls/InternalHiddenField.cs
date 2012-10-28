using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace K2Field.Smartforms.Controls.InternalControls
{

    public class InternalHiddenField : HiddenField
    {
        #region Private Variables
        private string _controlID = "";
        #endregion

        #region Public Properties
        public string ControlID
        {
            get
            {
                return _controlID;
            }
            set
            {
                this.ID = value;
                _controlID = value;
            }
        }
        #endregion

        #region Protected Override
        ///
        /// Override to force simple IDs all around
        /// 
        public override string UniqueID
        {
            get
            {
                return this.ID;
            }
        }
        /// 
        /// Override to force simple IDs all around
        /// 
        public override string ClientID
        {
            get
            {
                return this.ID;
            }
        }
        #endregion
    }
}
