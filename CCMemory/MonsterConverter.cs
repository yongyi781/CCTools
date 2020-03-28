using CCTools;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;

namespace CCMemory
{
    public class MonsterConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Monster && destinationType == typeof(string))
            {
                return ((Monster)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return new Monster
            {
                Tile = (Tile)propertyValues["Tile"],
                Location = (Point)propertyValues["Location"],
                Direction = (Point)propertyValues["Direction"],
                IsSlipping = (short)propertyValues["IsSlipping"]
            };
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var list = from PropertyDescriptor prop in TypeDescriptor.GetProperties(typeof(Monster), attributes)
                       select TypeDescriptor.CreateProperty(typeof(Monster), prop, new DescriptionAttribute("A monster property"));
            return new PropertyDescriptorCollection(list.ToArray());
        }
    }
}
