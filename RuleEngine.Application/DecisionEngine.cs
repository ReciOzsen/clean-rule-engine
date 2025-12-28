using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Results;
using RuleEngine.Domain.Rules;

namespace RuleEngine.Application
{
    public class DecisionEngine
    {
        private readonly IReadOnlyCollection<IRule> _rules;

        public DecisionEngine(IEnumerable<IRule> rules)
        {
            _rules = rules.ToList().AsReadOnly();
        }

        public DecisionResult Decide(Request request)
        {
            var ruleResults = new List<RuleResult>();

            foreach (var rule in _rules)
            {
                var result = rule.Evaluate(request);
                ruleResults.Add(result);
            }

            var approved = ruleResults.All(r => r.Passed);

            return new DecisionResult(
                ruleResults: ruleResults
            );
        }
    }
}
