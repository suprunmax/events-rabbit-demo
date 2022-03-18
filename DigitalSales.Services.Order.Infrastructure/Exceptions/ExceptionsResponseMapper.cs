using System.Collections.Concurrent;
using System.Net;
using Convey;
using Convey.WebApi.Exceptions;

namespace DigitalSales.Services.Order.Infrastructure.Exceptions;

public class ExceptionsResponseMapper : IExceptionToResponseMapper
{
    private static readonly ConcurrentDictionary<Type, string> Codes = new ConcurrentDictionary<Type, string>();
    
    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            Exception ex => new ExceptionResponse(new { code = GetCode(exception), reason = exception.Message },
                HttpStatusCode.BadRequest),
        };
    
    private static string GetCode(Exception ex)
    {
        var type = ex.GetType();
        if (Codes.TryGetValue(type, out var code))
        {
            return code;
        }

        var exceptionCode = type.Name.Underscore().Replace("_exception", string.Empty);

        Codes.TryAdd(type, exceptionCode);

        return exceptionCode;
    }
}
