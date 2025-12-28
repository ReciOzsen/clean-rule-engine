using FluentAssertions;
using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Rules;
using Xunit;

namespace RuleEngine.Tests.Rules;

public class MinimumAgeRuleTests
{
    private readonly MinimumAgeRule _rule = new();

    [Fact]
    public void Should_fail_when_age_is_less_than_18()
    {
        // Arrange
        var request = new Request
        {
            Age = 17,
            Amount = 10_000,
            Score = 600
        };

        // Act
        var result = _rule.Evaluate(request);

        // Assert
        result.Passed.Should().BeFalse();
        result.Code.Value.Should().Be("MIN_AGE");
        result.Message.Should().NotBeNull();
    }

    [Fact]
    public void Should_pass_when_age_is_18_or_more()
    {
        var request = new Request
        {
            Age = 18,
            Amount = 10_000,
            Score = 600
        };

        var result = _rule.Evaluate(request);

        result.Passed.Should().BeTrue();
        result.Message.Should().BeNull();
    }
}
