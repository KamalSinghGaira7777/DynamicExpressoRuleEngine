using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class RequiredFieldMissing
    {
        public const int ErrorCode = (int)ErrorCodeEnum.RequiredFieldMissing;
        private const string _title = "Required Field(s) Missing";
        private const string _message = "Field is required.";

        private RequiredFieldMissing() { }

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
