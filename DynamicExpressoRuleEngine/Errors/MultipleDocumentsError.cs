using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class MultipleDocumentsError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.MultipleDocumentsError;
        private const string _title = "Multiple Documents Detected";
        private const string _message = "Multiple documents have been detected in file.";

        private MultipleDocumentsError() { }

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
