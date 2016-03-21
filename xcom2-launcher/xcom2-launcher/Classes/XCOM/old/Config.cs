using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace XCOM2Launcher.XCOM.Config
{
    [TypeConverter(typeof(ConfigSettingsObjectConverter))]
    public class ConfigFile
    {
        public string Name { get; set; }

        [Browsable(false)]
        public List<ConfigSetting> Settings { get; set; }


        public ConfigFile()
        {
            Settings = new List<ConfigSetting>();
        }

    }

    internal class ConfigSettingsObjectConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            // Config Name
            ConfigFile obj = value as ConfigFile;

            List<ConfigSetting> customProps = obj.Settings;
            PropertyDescriptorCollection stdProps = base.GetProperties(context, value, attributes);

            PropertyDescriptor[] props = new PropertyDescriptor[stdProps.Count + obj.Settings.Count];
            stdProps.CopyTo(props, 0);

            int index = stdProps.Count;
            foreach (ConfigSetting prop in customProps)
            {
                props[index++] = new ConfigFilePropertyDescriptor(prop);
            }

            return new PropertyDescriptorCollection(props);
        }
    }


    internal class ConfigFilePropertyDescriptor : PropertyDescriptor
    {
        public ConfigSetting prop { get; set; }
        public ConfigFilePropertyDescriptor(ConfigSetting prop)
            : base(prop.Name, null)
        {
            this.prop = prop;
        }

        public override string Category
        {
            get { return prop.Category; }
        }

        public override string Description
        {
            get { return prop.Name; }
        }

        public override string Name
        {
            get { return prop.Name; }
        }

        public override object GetValue(object component)
        {
            return (component as ConfigFile).Settings.Find(s => s.Name == prop.Name).Value;
        }

        public override void SetValue(object component, object value)
        {
            (component as ConfigFile).Settings.Find(s => s.Name == prop.Name).Value = value;
        }

        public override bool ShouldSerializeValue(object component) { return false; }// ((ConfigFile)component).Settings[prop.Name] != null; }

        public override void ResetValue(object component) { }
        public override bool IsReadOnly { get { return false; } }
        public override Type PropertyType { get { return prop.Type; } }
        public override bool CanResetValue(object component) { return true; }
        public override Type ComponentType
        {
            get { return typeof(ConfigFile); }
        }
    }

    public class ConfigSetting
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public object Value { get; set; }
        public object DefaultValue { get; set; }
        public string Category { get; set; }

        public Type Type
        {
            get { return Value.GetType(); }
            // set { Value = null; }// Activator.CreateInstance(value); }
        }

    }
}
