namespace ConsoleTest;

public interface ICustomTypeMapper:IValueParser
{
    Type MapClrType(string dataFormat);
}

public interface IValueParser
{
    object ParseValue(object rawValue, string dataFormat);
}