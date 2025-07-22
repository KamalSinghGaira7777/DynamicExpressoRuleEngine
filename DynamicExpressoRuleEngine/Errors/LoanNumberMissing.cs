using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class LoanNumberMissing
    {
        public const int ErrorCode = (int)ErrorCodeEnum.LoanNumberMissing;
        private const string _title = "Loan number error";
        private const string _message = "Loan number is missing or invalid.";

        private LoanNumberMissing() { }

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
