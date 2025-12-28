using RuleEngine.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEngine.Domain.Results
{
    public class RuleResult
    {
        public RuleCode Code { get; }
        public bool Passed { get; }
        public string? Message { get; }

        private RuleResult(RuleCode code, bool passed, string? message)
        {
            Code = code;
            Passed = passed;
            Message = message;
        }

        public static RuleResult Success(RuleCode code)
            => new(code, true, null);

        public static RuleResult Fail(RuleCode code, string message)
            => new(code, false, message);
    }
}
