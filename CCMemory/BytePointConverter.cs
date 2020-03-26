using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace CCMemory
{
    public class BytePointConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string text = value as string;
            if (text != null)
            {
                string text2 = text.Trim();
                if (text2.Length == 0)
                {
                    return null;
                }
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }
                char c = culture.TextInfo.ListSeparator[0];
                string[] array = text2.Split(c);
                byte[] array2 = new byte[array.Length];
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(byte));
                for (int i = 0; i < array2.Length; i++)
                {
                    array2[i] = (byte)converter.ConvertFromString(context, culture, array[i]);
                }
                if (array2.Length == 2)
                {
                    return new BytePoint(array2[0], array2[1]);
                }
                throw new ArgumentException("Failed to parse point.");
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is BytePoint)
            {
                if (destinationType == typeof(string))
                {
                    BytePoint point = (BytePoint)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(byte));
                    string[] array = new string[2];
                    int num = 0;
                    array[num++] = converter.ConvertToString(context, culture, point.X);
                    array[num++] = converter.ConvertToString(context, culture, point.Y);
                    return string.Join(separator, array);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    BytePoint point2 = (BytePoint)value;
                    ConstructorInfo constructor = typeof(BytePoint).GetConstructor(new Type[2]
                    {
                        typeof(byte),
                        typeof(byte)
                    });
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[2]
                        {
                            point2.X,
                            point2.Y
                        });
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            object obj = propertyValues["X"];
            object obj2 = propertyValues["Y"];
            if (obj == null || obj2 == null || !(obj is byte) || !(obj2 is byte))
            {
                throw new ArgumentException("Invalid property value entry.");
            }
            return new BytePoint((byte)obj, (byte)obj2);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(BytePoint), attributes);
            return properties;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
