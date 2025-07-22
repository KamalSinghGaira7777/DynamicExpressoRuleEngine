using DynamicExpressoRuleEngine.Models;

namespace DynamicExpressoRuleEngine.ValidationError
{
    public class EnableLLMQueryError
    {
        public const int ErrorCode = (int)ErrorCodeEnum.EnableLLMQueryError;
        private const string _title = "Enable the LLM querying feature";
        private const string _message = "Please enable the LLM querying feature for this document type or organization";

        private EnableLLMQueryError() { }

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
