using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class DocumentOutdatedError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.DocumentOutdatedError;
        private const string _title = "Document Outdated Error";
        private const string _message = "Document date is too old for validation.";

        private DocumentOutdatedError() { }

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
