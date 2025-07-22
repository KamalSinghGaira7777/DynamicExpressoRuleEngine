using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class EmployerMismatch
    {
        public const int ErrorCode = (int)ErrorCodeEnum.EmployerMismatch;
        private const string _title = "Employer Mismatch";
        private const string _message = "Employer found on document does not match employer in LOS.";

        private EmployerMismatch() { }

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
