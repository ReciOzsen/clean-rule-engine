namespace RuleEngine.API.Contracts
{
    public class FailedRuleDto
    {
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
    }
}
