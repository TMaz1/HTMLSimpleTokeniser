namespace HTMLSimpleTokeniser.Models;

public class Token(string type, string value)
{
    public string? Type { get; set; } = type;
    public string? Value { get; set; } = value;

    public override string ToString()
    {
        return $"[ {Type}  ] {Value}";
    }
}