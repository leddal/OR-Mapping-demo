// See https://aka.ms/new-console-template for more information

using ConsoleTest;

var attributeList = new List<AttributeMethodBase>
{
    new() { Type = "unsigned", DataFormat = null, Obis = "1-0:1.8.0" },
    // new AttributeMethodBase { Type = "octet string", DataFormat = null, Obis = "0-0:96.1.0" },
};

// 2. 模拟一组 OBIS → 原始值映射
var rawData = new Dictionary<string, object>
{
    { "1-0:1.8.0", 123 },                  // 应该转为 ushort
    { "0-0:96.1.0", new byte[] { 1, 2, 3 } } // 应该转为 byte[]
};

// 3. 执行解析
foreach (var attr in attributeList)
{
    if (rawData.TryGetValue(attr.Obis, out var raw))
    {
        var parsed = attr.ParseValue(raw);
        
        Console.WriteLine($"OBIS: {attr.Obis} : Original:{raw}({attr.Type})  =>  Parsed: {parsed} ({parsed.GetType().Name})");
    }
}
