using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class ExpirationDateBeforeCloseError
    {
      
            public const int ErrorCode = (int)ErrorCodeEnum.ExpirationDateBeforeCloseError;
            private const string _title = "Expiration is before Closing";
            private const string _message = "The expiration date on the document is before the closing date.";

            private ExpirationDateBeforeCloseError() { }

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
