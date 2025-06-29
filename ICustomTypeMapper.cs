// using System.Windows.Forms;

using System.Windows.Forms;

namespace ConsoleTest;

public interface ICustomTypeMapper:IValueParser,IControlGenerator
{
    Type MapClrType(string dataFormat);
}

public interface IValueParser
{
    object ParseValue(object rawValue, string dataFormat);
}
public interface IControlGenerator
{
    Control GenerateControl(string labelText, object initialValue);
}