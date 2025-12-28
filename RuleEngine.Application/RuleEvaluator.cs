using RuleEngine.Domain;
using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Results;
using RuleEngine.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEngine.Application
{
    public sealed class RuleEvaluator
    {
        public DecisionResult Evaluate(
            Request request,
            IEnumerable<IRule> rules,
            RuleEngineMode mode = RuleEngineMode.CollectAll)
        {
            var failures = new List<RuleResult>();

            foreach (var rule in rules)
            {
                var result = rule.Evaluate(request);

                if (!result.Passed)
                {
                    failures.Add(result);

                    if (mode == RuleEngineMode.FirstFailure)
                        break;
                }
            }

            return failures.Count == 0
                ? DecisionResult.ApprovedResult()
                : DecisionResult.RejectedResult(failures);
        }
    }
}
