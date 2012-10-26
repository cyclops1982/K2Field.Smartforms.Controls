using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Management;
using System.Reflection;
using SourceCode.Forms.Controls.Web.Shared;
using K2Field.Smartforms.Controls.InstallHelpers;

namespace K2Field.SmartForms.Controls
{

    /// <summary>
    /// This installerprogram will help to install the K2 smartforms custom controls.
    /// It's quite generic and is closely related to the K2Field.Smartforms.Control project, as there is a relation and usage of attributes from that program.
    /// The reason for this being a seperate program is because we do not want this code deployed onto the k2 smartforms server.
    /// The attributes used are deployed, but we don't want to create a seperate project for that as well.
    /// </summary>
    public class InstallerProgram
    {

        static void Main(string[] args)
        {
            using (SourceCode.Forms.Management.FormsManager manager = new FormsManager("localhost", 5555))
            {
                ControlTypeExplorer explorer = manager.GetControlTypes();
                
                //REMOVE ALL CONTROL TYPES THAT ARE NOT IN THE SOURCECODE NAMESPACE! 
                //foreach (ControlTypeInfo cType in explorer.ControlTypes)
                //{

                //    if (!cType.FullName.StartsWith("SourceCode"))
                //    {
                //        manager.DeleteControlType(cType.Name);
                //        Console.WriteLine("cType.fullname: {0}", cType.FullName);
                //    }
                //}

                Assembly controlAssembly = Assembly.Load("K2Field.Smartforms.Controls");
                Type[] types = controlAssembly.GetTypes();
                foreach (Type t in types)
                {
                    if (t.IsSubclassOf(typeof(BaseControl)))
                    {
                        Console.WriteLine("Discovered {0}", t.FullName);
                        object controlObj = t.Assembly.CreateInstance(t.FullName);

                        PropertyInfo propInfo = t.GetProperty("Name");
                        string controlName = propInfo.GetValue(controlObj, null) as string;
                        propInfo = t.GetProperty("FriendlyName");
                        string friendlyName = propInfo.GetValue(controlObj, null) as string;
                        if (string.IsNullOrEmpty(controlName) || string.IsNullOrEmpty(friendlyName))
                        {
                            Console.WriteLine("ERROR: Not installing. Name or FriendlyName not set!");
                            continue;
                        }


                        bool updateControl = true;
                        ControlTypeInfo controlType = explorer.ControlTypes.SingleOrDefault(s => s.Name == controlName);
                        if (controlType == null)
                        {
                            updateControl = false;
                            controlType = new ControlTypeInfo();
                        }

                        controlType.Name = controlName;
                        controlType.FullName = t.FullName + ", " + controlAssembly.FullName;
                        controlType.DisplayName = friendlyName;
                        controlType.Category = ControlTypeCategory.Input;
                        controlType.Group = "Custom Controls"; //TODO: erhhhhhhhhhh?

                        
                        // DataTypes
                        controlType.DataTypes.Clear();
                        RegisterDataType[] dataTypes = t.GetCustomAttributes(typeof(RegisterDataType), true) as RegisterDataType[];
                        foreach (RegisterDataType dataType in dataTypes)
                        {
                            Console.WriteLine("\tAdding datatype: {0}", dataType.DataType.ToString());
                            controlType.DataTypes.Add(dataType.DataType);
                        }

                        //Events
                        controlType.Events.Clear();
                        bool defaultEventSet = false;
                        RegisterEvent[] events = t.GetCustomAttributes(typeof(RegisterEvent), true) as RegisterEvent[];
                        foreach (RegisterEvent evt in events)
                        {
                            Console.WriteLine("\tAdding event: {0}", evt.EventName);
                            controlType.Events.Add(evt.EventName);
                            if (evt.IsDefaultEvent && !defaultEventSet)
                            {
                                controlType.DefaultEvent = evt.EventName;
                                defaultEventSet = true;
                            }
                        }
                        if (!defaultEventSet && controlType.Events.Count > 0)
                        {
                            controlType.DefaultEvent = controlType.Events[0];
                        }


                        // Properties
                        RegisterPropertiesXml[] properties = t.GetCustomAttributes(typeof(RegisterPropertiesXml), true) as RegisterPropertiesXml[];
                        if (properties.Length > 0)
                        {
                            Console.WriteLine("\tAdding properties...");
                            controlType.Properties = properties[0].GetProperties();
                        }


                        // Specify runtime functions and validation. I really have no clue what these are for and why we do this, but it was in the how-to-guide
                        controlType.GetValueMethod = "UtilitiesBehaviour.getSimpleValue";
                        controlType.SetValueMethod = "UtilitiesBehaviour.setSimpleValue";
                        controlType.ValidationMethod = "UtilitiesBehaviour.validateControl";

                        if (updateControl)
                        {
                            Console.WriteLine("Updating....");
                            manager.SaveControlType(controlType);
                        }
                        else
                        {
                            Console.WriteLine("Installing....");
                            manager.CreateControlType(controlType);
                        }
                        Console.WriteLine();

                    }
                }
            }
        }
    }
}
