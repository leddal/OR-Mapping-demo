namespace ConsoleTest;

public class UnsignedMapper:ICustomTypeMapper
{
    public Type MapClrType(string dataFormat)
    {
        dataFormat = dataFormat?.ToLowerInvariant();
        return dataFormat switch
        {
            "enum" or "enumbit" => typeof(ushort),
            _ => typeof(double)
        };
    }

    public object ParseValue(object rawValue, string dataFormat)
    {
        dataFormat = dataFormat?.ToLowerInvariant();

        if (dataFormat == "enum" | dataFormat == "enumbit")
            return Convert.ToUInt16(rawValue);
        else
            return Convert.ToDouble(rawValue);
    }
}

public class AttributeMethodBase
{
    public string Type { get; set; }
    public string DataFormat { get; set; }
    public string Obis { get; set; }

    public Type GetClrType()
        => TypeMapperRegistry.GetClrType(this);

    public object ParseValue(object rawValue)
        => TypeMapperRegistry.ParseValue(this, rawValue);
}
