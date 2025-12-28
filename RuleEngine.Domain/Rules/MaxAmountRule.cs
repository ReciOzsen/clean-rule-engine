using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Results;

namespace RuleEngine.Domain.Rules
{
    public class MaxAmountRule :IRule
    {
        public static readonly RuleCode Code = new("MAX_AMOUNT");
        public RuleResult Evaluate(Request request)
        {
            if (request.Amount > 100_000)
                return RuleResult.Fail(Code, "Talep edilen tutar çok yüksek.");
            return RuleResult.Success(Code);
        }
    }
}
