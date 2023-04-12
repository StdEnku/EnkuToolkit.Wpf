namespace EnkuToolkit.Wpf.Converters;

using System;
using System.Globalization;
using System.Windows.Data;

/// <summary>
/// 演算操作の種類
/// </summary>
public enum MathOperations
{
    /// <summary>
    /// 和
    /// </summary>
    Add,

    /// <summary>
    /// 差
    /// </summary>
    Sub,

    /// <summary>
    /// 積
    /// </summary>
    Mul,

    /// <summary>
    /// 商
    /// </summary>
    Div,
}

/// <summary>
/// 四則演算可能なコンバーター
/// </summary>
public class MathConverter : IMultiValueConverter 
{
    // Dynamicなキャストを行うためのメソッド
    private static dynamic _DynamicCast(object obj, Type castTo)
        => System.Convert.ChangeType(obj, castTo);

    /// <summary>
    /// インスタンスを取得するためのプロパティ
    /// </summary>
    public static MathConverter Instance => new MathConverter();

    /// <summary>
    /// OneWayモードでの変換処理を記したメソッド
    /// </summary>
    /// <param name="values">バインディングソース</param>
    /// <param name="targetType">対象の型</param>
    /// <param name="parameter">演算モードを指定してください</param>
    /// <param name="culture">使用しません</param>
    /// <returns>演算結果</returns>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var operation = (MathOperations)parameter;

        var result = _DynamicCast(0, targetType);
        dynamic castedValue;

        for (int i = 0; i < values.Length; i++)
        {
            castedValue = _DynamicCast(values[i], targetType);
            if (i == 0)
            {
                result = castedValue;
            }
            else
            {
                result = operation == MathOperations.Add ? result + castedValue :
                         operation == MathOperations.Sub ? result - castedValue :
                         operation == MathOperations.Mul ? result * castedValue :
                         operation == MathOperations.Div ? result / castedValue :
                         result;
            }
        }

        return result;
    }

    /// <summary>
    /// OneWayToSourceモードでの変換処理を記したメソッド
    /// </summary>
    /// <remarks>
    /// 非対応でなので使用するとInvalidOperationExceptionが飛びます。
    /// </remarks>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new InvalidOperationException("EnkuToolkit's MathConverter does not support OneWayToSource mode.");
}