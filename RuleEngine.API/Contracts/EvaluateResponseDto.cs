namespace RuleEngine.API.Contracts
{
    public class EvaluateResponseDto
    {
        public bool Approved { get; init; }
        public List<FailedRuleDto> FailedRules { get; init; } = [];
    }
}
