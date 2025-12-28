using FluentAssertions;
using RuleEngine.Application;
using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Rules;
using Xunit;

namespace RuleEngine.Tests;

public class DecisionEngineTests
{
    [Fact]
    public void Should_approve_request_when_all_rules_pass()
    {
        // Arrange
        var rules = new IRule[]
        {
            new MinimumAgeRule(),
            new MaxAmountRule()
        };

        var engine = new DecisionEngine(rules);

        var request = new Request
        {
            Age = 25,
            Amount = 50_000,
            Score = 650
        };

        // Act
        var result = engine.Decide(request);

        // Assert
        result.Approved.Should().BeTrue();
        result.RuleResults.Should().AllSatisfy(r =>
            r.Passed.Should().BeTrue());
    }

    [Fact]
    public void Should_reject_request_when_any_rule_fails()
    {
        var rules = new IRule[]
        {
            new MinimumAgeRule(),
            new MaxAmountRule()
        };

        var engine = new DecisionEngine(rules);

        var request = new Request
        {
            Age = 16,
            Amount = 200_000,
            Score = 400
        };

        var result = engine.Decide(request);

        result.Approved.Should().BeFalse();
        result.RuleResults.Should().Contain(r => r.Code.Value == "MIN_AGE");
        result.RuleResults.Should().Contain(r => r.Code.Value == "MAX_AMOUNT");
    }
}
