using DynamicExpressoRuleEngine.Models;


namespace DynamicExpressoRuleEngine.ValidationError
{
    public class EmploymentStatusError
    {
        public const int ErrorCode = 131;
        private const string _title = "Employment Status Error";
        private const string _message = "The employee is either inactive, not currently employed, or on leave.";

        private EmploymentStatusError() { }

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
