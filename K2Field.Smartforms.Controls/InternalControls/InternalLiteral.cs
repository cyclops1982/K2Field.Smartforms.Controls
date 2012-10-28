using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using SourceCode.Forms.Controls.Web.Shared;

namespace K2Field.Smartforms.Controls.InternalControls
{
    /// <summary>
    /// This control is called a internalLiteral. Unfortunatly, extending from Literal does not work. I think this is because a literal might not have an ID (idea...)
    /// IMHO that means a literal has no clue :-)
    /// and the label DOES work. Just set the text to some lame HTML.
    /// </summary>
    public class InternalLiteral : Label
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
