using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class DocAddressError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.DocAddressError;
        private const string _title = "Document Address Not Validated";
        private const string _message = "Document address could not be validated. .";

        private DocAddressError() { }

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
