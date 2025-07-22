using DynamicExpressoRuleEngine.Models;


namespace DynamicExpressoRuleEngine.ValidationError
{
    public class RegExError
    {
        public const int ErrorCode = 123;
        private const string _title = "RegEx Validation Error";
        private const string _message = "The input does not match the required pattern or format as per the specified regular expression.";

        private RegExError() { }

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
