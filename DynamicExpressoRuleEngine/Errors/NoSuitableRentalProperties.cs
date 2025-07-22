using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class NoSuitableRentalProperties
    {
        public const int ErrorCode = (int)ErrorCodeEnum.NoSuitableRentalProperties;
        private const string _title = "No Suitable Rental Properties";
        private const string _message = "There are No Suitable Rental Properties on Page 1 of the Schedule E";

        private NoSuitableRentalProperties() { }

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
