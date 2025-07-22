using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class EffectiveDateError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.EffectiveDateError;
        private const string _title = "Effective Date Error";
        private const string _message = "Effective date validation error.";

        private EffectiveDateError() { }

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
