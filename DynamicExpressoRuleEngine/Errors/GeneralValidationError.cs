using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class GeneralError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.GeneralError;
        private const string _title = "Document Validation Error";
        private const string _message = "Document validation error.";

        private GeneralError() { }

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
