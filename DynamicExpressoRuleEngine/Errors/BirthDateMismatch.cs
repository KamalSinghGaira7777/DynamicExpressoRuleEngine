using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class BirthDateMismatch
    {
        public const int ErrorCode = (int)ErrorCodeEnum.BirthDateMismatch;
        private const string _title = "Date Of Birth Mismatch";
        private const string _message = "Date of birth in document and LOS do not match.";

        private BirthDateMismatch() { }

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
