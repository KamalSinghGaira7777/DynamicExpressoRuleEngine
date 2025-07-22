using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class LoanNumberMismatch
    {
        public const int ErrorCode = (int)ErrorCodeEnum.LoanNumberMismatch;
        private const string _title = "Loan Number Mismatch";
        private const string _message = "Loan number in document and LOS do not match.";

        private LoanNumberMismatch() { }

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
