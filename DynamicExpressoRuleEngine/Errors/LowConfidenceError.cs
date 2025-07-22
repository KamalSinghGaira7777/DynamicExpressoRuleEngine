using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class LowConfidenceError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.LowConfidenceError;
        private const string _title = "Low Confidence";
        private const string _message = "The confidence score is below the acceptable confidence value provided.";

        private LowConfidenceError() { }

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
