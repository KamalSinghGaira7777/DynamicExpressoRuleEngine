using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class InvalidTaxYearError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.InvalidTaxYearError;
        private const string _title = "Invalid TaxYear Error";
        private const string _message = "The document tax year is not valid. The tax year must be a four digit year within the past five years, not exceeding the current year";
                                        
        private InvalidTaxYearError() { }

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
