using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class InvalidPayTypeError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.InvalidPayTypeError;
        private const string _title = "Invalid Pay Type";
        private const string _message = "The pay type does not match any of the valid types";

        private InvalidPayTypeError() { }

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
