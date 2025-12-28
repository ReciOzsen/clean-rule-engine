using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Results;


namespace RuleEngine.Domain.Rules
{
    public class MinimumAgeRule : IRule
    {
        public static readonly RuleCode Code = new("MIN_AGE");
        public RuleResult Evaluate(Request request)
        {
            if (request.Age < 18)
                return RuleResult.Fail(Code, "Yaş 18'den küçük.");

            return RuleResult.Success(Code);
        }
    }
}
