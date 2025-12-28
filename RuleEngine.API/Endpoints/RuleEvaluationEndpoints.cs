using RuleEngine.API.Contracts;
using RuleEngine.Application;
using RuleEngine.Domain.Entities;
using RuleEngine.Domain.Rules;

namespace RuleEngine.Api.Endpoints;

public static class RuleEvaluationEndpoints
{
    public static IEndpointRouteBuilder MapRuleEvaluationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/evaluate", Evaluate)
           .WithName("EvaluateRules")
           .WithOpenApi();

        return app;
    }

    private static IResult Evaluate(
        RuleEvaluator evaluator,
        EvaluateRequestDto dto)
    {
        var request = new Request
        {
            Age = dto.Age,
            Amount = dto.Amount
        };

        var rules = new IRule[]
        {
            new MinimumAgeRule(),
            new MaxAmountRule()
        };

        var result = evaluator.Evaluate(request, rules);

        var response = new EvaluateResponseDto
        {
            Approved = result.Approved,
            FailedRules = result.RuleResults
                .Where(r => !r.Passed)
                .Select(r => new FailedRuleDto
                {
                    Code = r.Code.Value,
                    Message = r.Message
                })
                .ToList()
        };

        return Results.Ok(response);
    }
}
