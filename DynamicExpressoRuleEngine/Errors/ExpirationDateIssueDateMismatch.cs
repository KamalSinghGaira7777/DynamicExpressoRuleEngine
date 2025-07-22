using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class ExpirationDateIssueDateMismatch
    {
      
            public const int ErrorCode = (int)ErrorCodeEnum.ExpirationDateIssueDateMismatch;
            private const string _title = "Expiration is before Issue Date";
            private const string _message = "The expiration date on the document is before the issue date.";

            private ExpirationDateIssueDateMismatch() { }

            public static Error Create(string? title = _title, string? message = _message)
            {
                return new Error()
                {
                    Code = ErrorCode,
                    Level = LogLevel.Error,
                    Title = title,
                    Message = message
                };
            }
        
    }
}
