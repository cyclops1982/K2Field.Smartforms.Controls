using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K2Field.Smartforms.Controls.InstallHelpers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class RegisterEvent : Attribute
    {
        public string EventName { get; set; }
        public bool IsDefaultEvent { get; set; }

        public RegisterEvent(string eventName)
        {
            this.EventName = eventName;
            this.IsDefaultEvent = false;
        }

        public RegisterEvent(string eventName, bool isDefault)
        {
            this.EventName = eventName;
            this.IsDefaultEvent = isDefault;
        }

    }
}
