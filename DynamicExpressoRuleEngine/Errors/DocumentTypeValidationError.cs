using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class DocumentTypeError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.DocumentTypeError;
        private const string _title = "Secondary Classification Not Validated";
        private const string _message = "The submitted document is not an Insurance Policy or Declarations Page.";

        private DocumentTypeError() { }

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
