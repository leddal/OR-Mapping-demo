using System.Windows.Forms;

namespace ConsoleTest;

public static class TypeMapperRegistry
{
    private static readonly Dictionary<string, ICustomTypeMapper> _mappers = new();

    static TypeMapperRegistry()
    {
        Register("unsigned", new UnsignedMapper());
        // Register("double-unsigned", new DoubleUnsignedMapper());
        // Register("octet string", new OctetStringMapper());
        // ...其他注册
    }

    private static void Register(string typeName, ICustomTypeMapper mapper, bool overwrite = false)
    {
        var key = typeName.ToLowerInvariant();
        if (_mappers.ContainsKey(key) && !overwrite)
            throw new InvalidOperationException($"Type '{typeName}' is already registered.");
        _mappers[key] = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public static Control GenerateControl(string typeName, string labelText, object initialValue)
    {
        return GetMapper(typeName).GenerateControl(labelText, initialValue);
    }
    public static Type GetClrType(AttributeMethodBase attr)
    {
        return GetMapper(attr.Type).MapClrType(attr.DataFormat);
    }

    public static object ParseValue(AttributeMethodBase attr, object rawValue)
    {
        return GetMapper(attr.Type).ParseValue(rawValue, attr.DataFormat);
    }

    private static ICustomTypeMapper GetMapper(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
            throw new ArgumentNullException(nameof(typeName));

        var key = typeName.ToLowerInvariant();
        if (_mappers.TryGetValue(key, out var mapper))
            return mapper;

        throw new NotSupportedException($"No mapper registered for type '{typeName}'");
    }
}