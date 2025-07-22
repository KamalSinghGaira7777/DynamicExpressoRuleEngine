using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class CalculationError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.CalculationError;
        private const string _title = "Calculation Error";
        private const string _message = "Calculation validation error.";

        private CalculationError() { }

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
