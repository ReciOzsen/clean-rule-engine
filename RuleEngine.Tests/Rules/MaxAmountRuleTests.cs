using FluentAssertions;
using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Rules;
using Xunit;

namespace RuleEngine.Tests.Rules;

public class MaxAmountRuleTests
{
    private readonly MaxAmountRule _rule = new();

    [Fact]
    public void Should_fail_when_amount_exceeds_limit()
    {
        var request = new Request
        {
            Age = 30,
            Amount = 150_000,
            Score = 700
        };

        var result = _rule.Evaluate(request);

        result.Passed.Should().BeFalse();
        result.Code.Value.Should().Be("MAX_AMOUNT");
    }

    [Fact]
    public void Should_pass_when_amount_is_within_limit()
    {
        var request = new Request
        {
            Age = 30,
            Amount = 50_000,
            Score = 700
        };

        var result = _rule.Evaluate(request);

        result.Passed.Should().BeTrue();
    }

    [Fact]
    public void MaxAmountRule_Should_Fail_When_Amount_Is_Too_High()
    {
        // Arrange
        var rule = new MaxAmountRule();
        var request = new Request
        {
            Amount = 120_000
        };

        // Act
        var result = rule.Evaluate(request);

        // Assert
        result.Passed.Should().BeFalse();
        result.Code.Value.Should().Be("MAX_AMOUNT");
    }
}
