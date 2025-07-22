using DynamicExpresso;
using DynamicExpressoRuleEngine.Models;
using DynamicExpressoRuleEngine.ValidationError;

namespace DynamicExpressoRuleEngine
{
    public static class RuleEngineService
    {
        public static async Task RuleEngineValidateDocuments(MortgageValidatedDocument _doc, Loan? loanDetails)
        {
            try
            {
                var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();

                var rules = config.GetSection("RuleEngine:Rules").Get<List<ValidationRule>>();

                var interpreter = new Interpreter();

                foreach (var prop in _doc.ProcessedDocument.Results)
                {
                    interpreter.SetVariable(prop.Key, prop.Value);
                }

                interpreter.SetVariable("LoanDetails", loanDetails);
                
                interpreter.SetFunction("AddError", (string type, int? code, string? message, string? field) =>
                {
                    Error? error = Create(code, message);

                    if (string.IsNullOrEmpty(field))
                    {
                        _doc.ProcessedDocument.AddError(error);
                    }
                    else
                    {
                        var documentField = _doc.ProcessedDocument.Results.Where(x => x.Key == field).FirstOrDefault();

                        if (type == "note")
                            documentField?.ProcessingResult?.AddNote(message);
                        else
                            documentField?.ProcessingResult.AddError(error);
                    }
                });

                foreach (var rule in rules)
                {
                    bool result = interpreter.Eval<bool>(rule.Condition);

                    if (result)
                    {
                        interpreter.Eval(rule.Action);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static Error Create(int? errorCode, string? message, string? title = null)
        {
            return errorCode switch
            {
                (int)ErrorCodeEnum.RequiredFieldMissing => RequiredFieldMissing.Create(title, message),
                (int)ErrorCodeEnum.GeneralError => GeneralError.Create(title, message),
                (int)ErrorCodeEnum.LoanNumberMissing => LoanNumberMissing.Create(title, message),
                (int)ErrorCodeEnum.AddressMismatch => AddressMismatch.Create(title, message),
                (int)ErrorCodeEnum.NameMismatch => NameMismatch.Create(title, message),
                (int)ErrorCodeEnum.LosIntegrationError => LosIntegrationError.Create(title, message),
                (int)ErrorCodeEnum.BirthDateMismatch => BirthDateMismatch.Create(title, message),
                (int)ErrorCodeEnum.LosAddressError => LosAddressError.Create(title, message),
                (int)ErrorCodeEnum.DocAddressError => DocAddressError.Create(title, message),
                (int)ErrorCodeEnum.ExpirationDateError => ExpirationDateError.Create(title, message),
                (int)ErrorCodeEnum.EffectiveDateError => EffectiveDateError.Create(title, message),
                (int)ErrorCodeEnum.LoanNumberMismatch => LoanNumberMismatch.Create(title, message),
                (int)ErrorCodeEnum.CalculationError => CalculationError.Create(title, message),
                (int)ErrorCodeEnum.DateError => DateError.Create(title, message),
                (int)ErrorCodeEnum.ValueMismatchError => ValueMismatchError.Create(title, message),
                (int)ErrorCodeEnum.EmployerMismatch => EmployerMismatch.Create(title, message),
                (int)ErrorCodeEnum.LowConfidenceError => LowConfidenceError.Create(title, message),
                (int)ErrorCodeEnum.DriversLicenseFormatError => DriversLicenseFormatError.Create(title, message),
                (int)ErrorCodeEnum.DocStateError => DocStateError.Create(title, message),
                (int)ErrorCodeEnum.EnableLLMQueryError => EnableLLMQueryError.Create(title, message),
                (int)ErrorCodeEnum.DocumentOutdatedError => DocumentOutdatedError.Create(title, message),
                (int)ErrorCodeEnum.InvalidPayTypeError => InvalidPayTypeError.Create(title, message),
                (int)ErrorCodeEnum.MultipleDocumentsError => MultipleDocumentsError.Create(title, message),
                (int)ErrorCodeEnum.ExpirationDateBeforeCloseError => ExpirationDateBeforeCloseError.Create(title, message),
                (int)ErrorCodeEnum.ExpirationDateIssueDateMismatch => ExpirationDateIssueDateMismatch.Create(title, message),
                (int)ErrorCodeEnum.EmploymentStatusError => EmploymentStatusError.Create(title, message),
                (int)ErrorCodeEnum.NoSuitableRentalProperties => NoSuitableRentalProperties.Create(title, message),
                (int)ErrorCodeEnum.DocumentTypeError => DocumentTypeError.Create(title, message),
                (int)ErrorCodeEnum.SalesContractError => SalesContractDocTypeError.Create(title, message),
                (int)ErrorCodeEnum.InvalidTaxYearError => InvalidTaxYearError.Create(title, message),

                _ => new Error()
            };
        }
    }
}
