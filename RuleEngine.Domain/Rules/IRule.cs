using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEngine.Domain.Rules
{
    public interface IRule
    {
        RuleResult Evaluate(Request request);
    }
}
