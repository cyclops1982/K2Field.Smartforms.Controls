using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Management;

namespace K2Field.Smartforms.Controls.InstallHelpers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class RegisterDataType : Attribute
    {
        public ControlDataType DataType;

        public RegisterDataType(ControlDataType dataType)
        {
            this.DataType = dataType;
        }
    }
}
