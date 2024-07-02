using Domain.Abstractions;

namespace API.Extensions;

public static class ResultExtensions
{
    public static IResult FailureResult<T>(Result<T> data) where T : class
    {

        return Results.Problem(
                statusCode: 400,
                title: "An error occured",
                extensions: new Dictionary<string, object?>()
                {
                        {"errors", data.Error }
                });
    }
}
