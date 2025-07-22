using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class LosAddressError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.LosAddressError;
        private const string _title = "LOS Address Not Validated";
        private const string _message = "LOS address could not be validated. .";

        private LosAddressError() { }

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
