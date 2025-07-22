
using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class AddressMismatch
    {
        public const int ErrorCode = (int)ErrorCodeEnum.AddressMismatch;
        private const string _title = "Address Mismatch";
        private const string _message = "Addresses in document and LOS do not match.";

        private AddressMismatch() { }

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
