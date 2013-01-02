using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using SourceCode.Forms.Controls.Web.Shared;
using System.ComponentModel;

[assembly: WebResource("K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.css", "text/css", PerformSubstitution = true)]
namespace K2Field.Smartforms.Controls.AutocompleteTextbox
{
    [ClientScriptResource("K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox", "K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.js")]
    [TargetControlType(typeof(System.Web.UI.WebControls.TextBox))]

    public class AutocompleteTextboxExtender : ExtenderControlBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string css = "<link href=\"" + this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
            "K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.css") + "\" type=\"text/css\" rel=\"stylesheet\" />";


            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "cssFile", css, false);
        }


        [ClientPropertyName("_isVisible"), DefaultValue(true), ExtenderControlProperty]
        public bool IsVisible
        {
            get { return base.GetPropertyValue<bool>("IsVisible", true); }
            set { base.SetPropertyValue<bool>("IsVisible", value); }
        }


        [ExtenderControlProperty]
        [ClientPropertyName("_isReadOnly")]
        [DefaultValue(false)]
        public bool IsReadOnly
        {
            get { return base.GetPropertyValue<bool>("IsReadOnly", false); }
            set { base.SetPropertyValue<bool>("IsReadOnly", value); }
        }


        [ExtenderControlProperty]
        [ClientPropertyName("_isEnabled")]
        public bool IsEnabled
        {
            get { return base.GetPropertyValue<bool>("IsEnabled", true); }
            set { base.SetPropertyValue<bool>("IsEnabled", value); }
        }

        [ExtenderControlProperty]
        [ClientPropertyName("_dataSourceType")]
        [DefaultValue("")]
        public string DataSourceType
        {
            get { return base.GetPropertyValue<string>("DataSourceType", ""); }
            set { base.SetPropertyValue<string>("DataSourceType", value); }
        }

        [ExtenderControlProperty]
        [ClientPropertyName("_fixedListItems")]
        public string FixedListItems
        {
            get { return base.GetPropertyValue<string>("FixedListItems", ""); }
            set { base.SetPropertyValue<string>("FixedListItems", value); }
        }

        [ExtenderControlProperty]
        [ClientPropertyName("_displaytemplate")]
        public string DisplayTemplate
        {
            get { return base.GetPropertyValue<string>("DisplayTemplate", ""); }
            set { base.SetPropertyValue<string>("DisplayTemplate", value); }
        }


        [ExtenderControlProperty]
        [ClientPropertyName("_valueproperty")]
        public string ValueProperty
        {
            get { return base.GetPropertyValue<string>("ValueProperty", ""); }
            set { base.SetPropertyValue<string>("ValueProperty", value); }
        }

        [ExtenderControlProperty]
        [ClientPropertyName("_associationSO")]
        public string AssociationSO
        {
            get { return base.GetPropertyValue<string>("AssociationSO", ""); }
            set { base.SetPropertyValue<string>("AssociationSO", value); }
        }

        [ExtenderControlProperty]
        [ClientPropertyName("_associationMethod")]
        public string AssociationMethod
        {
            get { return base.GetPropertyValue<string>("AssociationMethod", ""); }
            set { base.SetPropertyValue<string>("AssociationMethod", value); }
        }

    }
}
