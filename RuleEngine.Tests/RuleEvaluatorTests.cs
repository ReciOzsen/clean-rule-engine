using FluentAssertions;
using RuleEngine.Application;
using RuleEngine.Domain;
using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Results;
using RuleEngine.Domain.Rules;
using Xunit;

namespace RuleEngine.Tests;

public class RuleEvaluatorTests
{
    [Fact]
    public void Evaluate_Should_Return_Rejected_When_Multiple_Rules_Fail()
    {
        // Arrange
        var evaluator = new RuleEvaluator();

        var rules = new IRule[]
        {
            new MinimumAgeRule(),
            new MaxAmountRule()
        };

        var request = new Request
        {
            Age = 16,
            Amount = 120_000
        };

        // Act
        var result = evaluator.Evaluate(
        request,
        rules,
        RuleEngineMode.CollectAll);

        // Assert
        result.Approved.Should().BeFalse();
        result.RuleResults.Should().HaveCount(2);
    }

    [Fact]
    public void Evaluate_Should_Return_Approved_When_All_Rules_Pass()
    {
        // Arrange
        var evaluator = new RuleEvaluator();

        var rules = new IRule[]
        {
            new MinimumAgeRule(),
            new MaxAmountRule()
        };

        var request = new Request
        {
            Age = 30,
            Amount = 5_000
        };

        // Act
        var result = evaluator.Evaluate(
        request,
        rules,
        RuleEngineMode.CollectAll);

        // Assert
        result.Approved.Should().BeTrue();
        result.RuleResults.Should().BeEmpty();
    }

    [Fact]
    public void Evaluate_Should_Collect_All_Failures_By_Default()
    {
        var evaluator = new RuleEvaluator();

        var rules = new IRule[]
        {
        new MinimumAgeRule(),
        new MaxAmountRule()
        };

        var request = new Request
        {
            Age = 15,
            Amount = 120_000
        };

        var result = evaluator.Evaluate(request, rules);

        result.RuleResults.Should().HaveCount(2);
    }

}
