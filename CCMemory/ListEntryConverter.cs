using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCMemory
{
    public class ListEntryConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(String) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ListEntry && destinationType == typeof(string))
            {
                return ((ListEntry)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return new ListEntry
            {
                Length = (short)propertyValues["Length"],
                Cap = (short)propertyValues["Cap"],
                Handle = (short)propertyValues["Handle"],
                Pointer = (short)propertyValues["Pointer"],
                Segment = (short)propertyValues["Segment"]
            };
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var list = from PropertyDescriptor prop in TypeDescriptor.GetProperties(typeof(ListEntry), attributes)
                       select TypeDescriptor.CreateProperty(typeof(ListEntry), prop, new DescriptionAttribute("hello"));
            return new PropertyDescriptorCollection(list.ToArray());
        }
    }
}
