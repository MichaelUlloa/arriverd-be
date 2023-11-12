using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace arriverd_be.Utilities;

public class AuthResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttributes = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

        if (authAttributes is null || !authAttributes.Any())
            return;

        var securityRequirement = new OpenApiSecurityRequirement()
        {
            {
                Constants.JWT_SECURITY_SCHEMA,
                Array.Empty<string>()
            }
        };

        operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
        operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
    }
}