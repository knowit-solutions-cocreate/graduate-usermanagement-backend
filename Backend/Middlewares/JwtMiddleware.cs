using Backend.Utility;

namespace Backend.Middlewares;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class JwtMiddleware {
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) {
        var path = context.Request.Path.Value?.ToLower();
        var method = context.Request.Method.ToLower();

        // Skip token validation for user creation
        if (path == "/api/user" && method == "post") {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

        if (!string.IsNullOrWhiteSpace(token)) {
            var principal = JwtToken.Validate(token);
            var userId = principal?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userId, out var guid)) {
                context.Items["UserId"] = guid;
            }
        }

        await _next(context);
    }
}