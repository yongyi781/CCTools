using System;
using System.ComponentModel;
using System.Globalization;

namespace CCTools
{
    // This just makes it so you can type in strings or integers too.
    public class TileConverter : EnumConverter
    {
        public TileConverter(Type type) : base(type) { }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(int) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is int num)
                return (Tile)num;
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Tile tile)
                return tile.ToString();
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
