using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class ExpirationDateError
    {
            public const int ErrorCode = (int)ErrorCodeEnum.ExpirationDateError;
            private const string _title = "Expiration Date Error";
            private const string _message = "Expiration date validation error.";

            private ExpirationDateError() { }

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
