using System.Windows.Forms;

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
    public Control GenerateControl(string labelText, object initialValue)
    {
        var panel = new Panel { Width = 300, Height = 60 ,Dock = DockStyle.Top};

        var label = new Label
        {
            Text = labelText,
            Width = 100,
            Height = 25,
            Top = 10,
            Left = 10
        };

        var textBox = new TextBox
        {
            Width = 150,
            Height = 25,
            Top = 10,
            Left = 120,
            Text = initialValue?.ToString() ?? "0"
        };

        // 限制只输入数字
        textBox.KeyPress += (sender, e) =>
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        };

        panel.Controls.Add(label);
        panel.Controls.Add(textBox);

        return panel;
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
