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
                //return;

                Assembly controlAssembly = Assembly.Load("K2Field.Smartforms.Controls");
                Type[] types = controlAssembly.GetTypes();
                foreach (Type type in types)
                {
                    RegisterControlType[] custAttr = type.GetCustomAttributes(typeof(RegisterControlType), true) as RegisterControlType[];
                    if (custAttr.Length == 1)
                    {
                        RegisterControlType regContType = custAttr[0];
                        Console.WriteLine("Discovered {0}", type.FullName);
                        /*
                         * 
                        object controlObj = t.Assembly.CreateInstance(t.FullName);
                       
                        PropertyInfo propInfo = t.GetProperty("Name");
                        string controlName = propInfo.GetValue(controlObj, null) as string;
                        propInfo = t.GetProperty("FriendlyName");
                        string friendlyName = propInfo.GetValue(controlObj, null) as string;
                        if (string.IsNullOrEmpty(controlName) || string.IsNullOrEmpty(friendlyName))
                        {
                            Console.WriteLine("ERROR: Not installing. Name or FriendlyName not set!");
                            continue;
                        }*/

                        

                        bool updateControl = true;
                        ControlTypeInfo controlType = explorer.ControlTypes.SingleOrDefault(s => s.Name == type.Name);
                        if (controlType == null)
                        {
                            updateControl = false;
                            controlType = new ControlTypeInfo();
                        }

                        //Set the control type information
                        controlType.Name = type.Name;
                        controlType.FullName = type.FullName + ", " + controlAssembly.FullName;
                        controlType.DisplayName = regContType.DisplayName;
                        controlType.Category = regContType.Category;
                        controlType.Group = regContType.Group;
                        controlType.Properties = regContType.GetPropertyXML();

                        // Specify runtime functions and validation
                        if (!string.IsNullOrEmpty(regContType.GetValueMethod))
                            controlType.GetValueMethod = regContType.GetValueMethod;

                        if (!string.IsNullOrEmpty(regContType.SetValueMethod))
                            controlType.SetValueMethod = regContType.SetValueMethod;

                        if (!string.IsNullOrEmpty(regContType.SetPropertyMethod) )
                            controlType.SetPropertyMethod = regContType.SetPropertyMethod;

                        if (!string.IsNullOrEmpty(regContType.GetPropertyMethod))
                            controlType.GetPropertyMethod = regContType.GetPropertyMethod;

                        if (!string.IsNullOrEmpty(regContType.GetDefaultValueMethod))
                            controlType.GetDefaultValueMethod = regContType.GetDefaultValueMethod;

                        if (!string.IsNullOrEmpty(regContType.ValidationMethod))
                            controlType.ValidationMethod = regContType.ValidationMethod;

                        if (!string.IsNullOrEmpty(regContType.SetItemsMethod))
                            controlType.SetItemsMethod = regContType.SetItemsMethod;


                        // DataTypes
                        controlType.DataTypes.Clear();
                        RegisterDataType[] dataTypes = type.GetCustomAttributes(typeof(RegisterDataType), true) as RegisterDataType[];
                        foreach (RegisterDataType dataType in dataTypes)
                        {
                            Console.WriteLine("\tAdding datatype: {0}", dataType.DataType.ToString());
                            controlType.DataTypes.Add(dataType.DataType);
                        }

                        //Events
                        controlType.Events.Clear();
                        bool defaultEventSet = false;
                        RegisterEvent[] events = type.GetCustomAttributes(typeof(RegisterEvent), true) as RegisterEvent[];
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
