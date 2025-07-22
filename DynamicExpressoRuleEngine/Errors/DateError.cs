using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class DateError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.DateError;
        private const string _title = "Date Error";
        private const string _message = "Date is not accurate.";

        private DateError() { }

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
