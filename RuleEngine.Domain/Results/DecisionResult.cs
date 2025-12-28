using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEngine.Domain.Results
{
    public class DecisionResult
    {
        public bool Approved => RuleResults.All(r => r.Passed);
        public IReadOnlyList<RuleResult> RuleResults { get; }

        public DecisionResult(IReadOnlyList<RuleResult> ruleResults)
        {
            RuleResults = ruleResults;
        }
        public static DecisionResult ApprovedResult()
        => new(Array.Empty<RuleResult>());

        public static DecisionResult RejectedResult(IEnumerable<RuleResult> failures)
            => new(failures.ToList());
    }
}
