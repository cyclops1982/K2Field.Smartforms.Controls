using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SourceCode.Forms.Management;

namespace K2Field.Smartforms.Controls.InstallHelpers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RegisterControlType : Attribute
    {
        public string DisplayName { get; set; }
        public ControlTypeCategory Category { get; set; }
        public string Group { get; set; }
        public string GetValueMethod { get; set; }
        public string SetValueMethod { get; set; }
        public string SetPropertyMethod {get;set;}
        public string GetPropertyMethod {get;set;}
        public string GetDefaultValueMethod {get;set;}
        public string ValidationMethod {get;set;}
        public string PropertyXMLResourceName { get; set; }


        public RegisterControlType(string displayName)
        {
            this.DisplayName = displayName;
            this.Category = ControlTypeCategory.Input;
            this.Group = "Custom Controls";
            this.GetValueMethod = "UtilitiesBehaviour.getValue";
            this.SetValueMethod = "UtilitiesBehaviour.setValue";
            this.SetPropertyMethod = string.Empty;
            this.GetPropertyMethod = string.Empty;
            this.GetDefaultValueMethod = "UtilitiesBehaviour.getDefaultValue";
            this.ValidationMethod = "UtilitiesBehaviour.validateControl";
            this.PropertyXMLResourceName = string.Empty;
        }

        public string GetPropertyXML()
        {
            if (string.IsNullOrEmpty(PropertyXMLResourceName))
                return null;

            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(PropertyXMLResourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

    }
}
