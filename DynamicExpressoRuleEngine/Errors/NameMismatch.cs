using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class NameMismatch
    {
        public const int ErrorCode = (int)ErrorCodeEnum.NameMismatch;
        private const string _title = "Name Mismatch";
        private const string _message = "Name in document and LOS do not match.";

        private NameMismatch() { }

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
