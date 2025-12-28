namespace RuleEngine.Domain.Rules;

public readonly record struct RuleCode(string Value)
{
    public override string ToString() => Value;
}
