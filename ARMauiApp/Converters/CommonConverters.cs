using System.Globalization;

namespace ARMauiApp.Converters
{
    public class CountToBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool hasItems = false;

            if (value is int count)
                hasItems = count > 0;
            else if (value is System.Collections.ICollection collection)
                hasItems = collection.Count > 0;

            // If parameter is "True", invert the result (for showing placeholder when no items)
            if (parameter?.ToString()?.ToLower() == "true")
                return !hasItems;

            return hasItems;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvertedBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;
            return true;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;
            return false;
        }
    }

    public class BoolToActiveStatusConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isActive)
                return isActive ? "Hoạt động" : "Không hoạt động";
            return "Không xác định";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToStatusColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isActive)
                return isActive ? Colors.Green : Colors.Red;
            return Colors.Gray;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool hasValue = false;

            if (value is string stringValue)
                hasValue = !string.IsNullOrEmpty(stringValue);

            // If parameter is "True", invert the result
            if (parameter?.ToString()?.ToLower() == "true")
                return !hasValue;

            return hasValue;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AllTrueMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Returns true only if ALL values are true
            foreach (var value in values)
            {
                if (value is bool boolValue && !boolValue)
                    return false;
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AllFalseMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Returns true only if ALL values are false
            foreach (var value in values)
            {
                if (value is bool boolValue && boolValue)
                    return false;
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GreaterThanOneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is int i && i > 1;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class GreaterThanZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal d) return d > 0;
            if (value is double dd) return dd > 0;
            if (value is int i) return i > 0;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class EqualsMultiConverter : IMultiValueConverter
    {
        // values[0] = Id của Plan (trong item)
        // values[1] = CurrentPlanId (từ ViewModel cha)
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is { Length: 2 } && values[0] != null && values[1] != null)
                return values[0]?.ToString() == values[1]?.ToString();
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class BoolNegateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b && !b;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class DiscountPriceConverter : IMultiValueConverter
    {
        // values[0] = Price (decimal), values[1] = DiscountPercent (decimal)
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is IConvertible && values[1] is IConvertible)
            {
                var price = System.Convert.ToDecimal(values[0], culture);
                var discount = System.Convert.ToDecimal(values[1], culture);
                var final = discount > 0 ? Math.Round(price * (100 - discount) / 100, 0) : price;
                return final;
            }
            return 0m;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
public class BrushResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = value as string;
            if (string.IsNullOrWhiteSpace(key)) key = "GradientPurple";

            var res = Application.Current?.Resources;
            if (res != null && res.TryGetValue(key, out var brush))
                return brush;

            // Fallback: luôn trả về gradient tím (không phải màu xám)
            return res?["GradientPurple"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
