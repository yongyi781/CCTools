using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace CCMemory
{
    public class PointConverter : TypeConverter
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
            if (value is string text)
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
                short[] array2 = new short[array.Length];
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(short));
                for (int i = 0; i < array2.Length; i++)
                {
                    array2[i] = (short)converter.ConvertFromString(context, culture, array[i]);
                }
                if (array2.Length == 2)
                {
                    return new Point(array2[0], array2[1]);
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
            if (value is Point)
            {
                if (destinationType == typeof(string))
                {
                    Point point = (Point)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(short));
                    string[] array = new string[2];
                    int num = 0;
                    array[num++] = converter.ConvertToString(context, culture, point.X);
                    array[num++] = converter.ConvertToString(context, culture, point.Y);
                    return string.Join(separator, array);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    Point point2 = (Point)value;
                    ConstructorInfo constructor = typeof(Point).GetConstructor(new Type[2]
                    {
                        typeof(short),
                        typeof(short)
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
            if (obj == null || obj2 == null || !(obj is short) || !(obj2 is short))
            {
                throw new ArgumentException("Invalid property value entry.");
            }
            return new Point((short)obj, (short)obj2);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Point), attributes);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
