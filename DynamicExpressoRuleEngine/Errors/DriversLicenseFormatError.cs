using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class DriversLicenseFormatError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.DriversLicenseFormatError;
        private const string _title = "Driver's License Error";
        private const string _message = "The driver's license number provided doesnt match the borrowers state format";

        private DriversLicenseFormatError() { }

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
