using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class ValueMismatchError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.ValueMismatchError;
        private const string _title = "Values Mismatch";
        private const string _message = "Values must match.";

        private ValueMismatchError() { }

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
