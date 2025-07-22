using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class LosIntegrationError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.LosIntegrationError;
        private const string _title = "LOS Integration Not Detected";
        private const string _message = "No LOS integration detected. Validation may be incomplete.";

        private LosIntegrationError() { }

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
