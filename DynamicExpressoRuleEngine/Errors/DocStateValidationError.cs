using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class DocStateError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.DocStateError;
        private const string _title = "Document State Not Validated";
        private const string _message = "The state extracted from the document could not be verified";

        private DocStateError() { }

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
