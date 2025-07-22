using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class SalesContractDocTypeError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.SalesContractError;
        private const string _title = "Sales Contract Classification Not Validated";
        private const string _message = "The submitted document is not a Sales Contract.";

        private SalesContractDocTypeError() { }

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
