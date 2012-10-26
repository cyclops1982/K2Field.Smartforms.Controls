using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace K2Field.Smartforms.Controls.InstallHelpers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public class RegisterPropertiesXml : Attribute
    {
        public string ResourceFullName;

        public RegisterPropertiesXml(string resourceFullname)
        {
            this.ResourceFullName = resourceFullname;
        }

        public string GetProperties()
        {
            string result = string.Empty;
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(ResourceFullName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
