using DynamicExpresso;
using DynamicExpressoRuleEngine.Models;
using DynamicExpressoRuleEngine.ValidationError;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace DynamicExpressoRuleEngine
{
    public class RuleEngineService
    {
        private static readonly MemoryCache _ruleCache = new MemoryCache(new MemoryCacheOptions());

        public static async Task RuleEngineValidateDocuments(MortgageValidatedDocument _doc, Interpreter interpreter)
        {
            try
            {
                var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();

                var rules = config.GetSection("RuleEngine:Rules").Get<List<ValidationRule>>();

                foreach (var prop in _doc.ProcessedDocument.Results)
                {
                    interpreter.SetVariable(prop.Key, prop?.Value == null ? string.Empty : prop.Value.ToString());
                }

                interpreter.SetFunction("AddValue", (dynamic? value, string? field) =>
                {
                    var documentField = _doc.ProcessedDocument.Results.Where(x => x.Key == field).FirstOrDefault();
                    documentField.Value = value;
                });

                interpreter.SetFunction("AddError", (int code, string? message, string? field, string? title = null) =>
                {
                    Error? error = Create(code, message, title);

                    if (string.IsNullOrEmpty(field))
                    {
                        _doc.ProcessedDocument.AddError(error);
                    }
                    else
                    {
                        var documentField = _doc.ProcessedDocument.Results.Where(x => x.Key == field).FirstOrDefault();
                        documentField?.ProcessingResult.AddError(error);
                    }
                });

                interpreter.SetFunction("AddNotes", (string? message, string? field) =>
                {
                    var documentField = _doc.ProcessedDocument.Results.Where(x => x.Key == field).FirstOrDefault();
                    documentField?.ProcessingResult.AddNote(message);
                });

                interpreter.SetFunction("AddPotentialValues", (dynamic? value, string? field) =>
                {
                    var documentField = _doc.ProcessedDocument.Results.Where(x => x.Key == field).FirstOrDefault();
                    documentField?.ProcessingResult.AddPotentialValues(value);
                });

                interpreter.SetFunction("AddPotentialValues", (List<object>? value, string? field) =>
                {
                    var documentField = _doc.ProcessedDocument.Results.Where(x => x.Key == field).FirstOrDefault();
                    documentField?.ProcessingResult.AddPotentialValues(value);
                });

                interpreter.SetFunction("SetVariable", (dynamic? value, string? field) =>
                {
                    interpreter.SetVariable(field, value);
                });

                interpreter.SetFunction("ToStringFormat", new Func<dynamic, string>(ToStringFormat));

                interpreter.SetFunction("ContainsAny", (List<string> list, string value) =>
                {
                    return list.Any(sub => value.ToLower().Contains(sub));
                });

                foreach (var rule in rules)
                {
                    // Cache and evaluate condition
                    var conditionKey = $"rule:condition:{rule.Condition}";
                    var conditionLambda = _ruleCache.GetOrCreate(conditionKey, entry =>
                    {
                        entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                        return interpreter.Parse(rule.Condition);
                    });

                    bool result = (bool)conditionLambda.Invoke();

                    if (result)
                    {
                        var actions = rule.Action.Split('#');

                        foreach (var action in actions)
                        {
                            var actionKey = $"rule:action:{action.Trim()}";
                            var actionLambda = _ruleCache.GetOrCreate(actionKey, entry =>
                            {
                                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                                return interpreter.Parse(action.Trim());
                            });

                            actionLambda.Invoke();
                        }
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
                (int)ErrorCodeEnum.GeneralError => title == null ? GeneralError.Create(message: message) : GeneralError.Create(title, message),
                (int)ErrorCodeEnum.RequiredFieldMissing => title == null ? RequiredFieldMissing.Create(message: message) : RequiredFieldMissing.Create(title, message),
                (int)ErrorCodeEnum.LoanNumberMissing => title == null ? LoanNumberMissing.Create(message: message) : LoanNumberMissing.Create(title, message),
                (int)ErrorCodeEnum.AddressMismatch => title == null ? AddressMismatch.Create(message: message) : AddressMismatch.Create(title, message),
                (int)ErrorCodeEnum.NameMismatch => title == null ? NameMismatch.Create(message: message) : NameMismatch.Create(title, message),
                (int)ErrorCodeEnum.LosIntegrationError => title == null ? LosIntegrationError.Create(message: message) : LosIntegrationError.Create(title, message),
                (int)ErrorCodeEnum.BirthDateMismatch => title == null ? BirthDateMismatch.Create(message: message) : BirthDateMismatch.Create(title, message),
                (int)ErrorCodeEnum.LosAddressError => title == null ? LosAddressError.Create(message: message) : LosAddressError.Create(title, message),
                (int)ErrorCodeEnum.DocAddressError => title == null ? DocAddressError.Create(message: message) : DocAddressError.Create(title, message),
                (int)ErrorCodeEnum.ExpirationDateError => title == null ? ExpirationDateError.Create(message: message) : ExpirationDateError.Create(title, message),
                (int)ErrorCodeEnum.EffectiveDateError => title == null ? EffectiveDateError.Create(message: message) : EffectiveDateError.Create(title, message),
                (int)ErrorCodeEnum.LoanNumberMismatch => title == null ? LoanNumberMismatch.Create(message: message) : LoanNumberMismatch.Create(title, message),
                (int)ErrorCodeEnum.CalculationError => title == null ? CalculationError.Create(message: message) : CalculationError.Create(title, message),
                (int)ErrorCodeEnum.DateError => title == null ? DateError.Create(message: message) : DateError.Create(title, message),
                (int)ErrorCodeEnum.ValueMismatchError => title == null ? ValueMismatchError.Create(message: message) : ValueMismatchError.Create(title, message),
                (int)ErrorCodeEnum.EmployerMismatch => title == null ? EmployerMismatch.Create(message: message) : EmployerMismatch.Create(title, message),
                (int)ErrorCodeEnum.LowConfidenceError => title == null ? LowConfidenceError.Create(message: message) : LowConfidenceError.Create(title, message),
                (int)ErrorCodeEnum.DriversLicenseFormatError => title == null ? DriversLicenseFormatError.Create(message: message) : DriversLicenseFormatError.Create(title, message),
                (int)ErrorCodeEnum.DocStateError => title == null ? DocStateError.Create(message: message) : DocStateError.Create(title, message),
                (int)ErrorCodeEnum.EnableLLMQueryError => title == null ? EnableLLMQueryError.Create(message: message) : EnableLLMQueryError.Create(title, message),
                (int)ErrorCodeEnum.DocumentOutdatedError => title == null ? DocumentOutdatedError.Create(message: message) : DocumentOutdatedError.Create(title, message),
                (int)ErrorCodeEnum.InvalidPayTypeError => title == null ? InvalidPayTypeError.Create(message: message) : InvalidPayTypeError.Create(title, message),
                (int)ErrorCodeEnum.MultipleDocumentsError => title == null ? MultipleDocumentsError.Create(message: message) : MultipleDocumentsError.Create(title, message),
                (int)ErrorCodeEnum.ExpirationDateBeforeCloseError => title == null ? ExpirationDateBeforeCloseError.Create(message: message) : ExpirationDateBeforeCloseError.Create(title, message),
                (int)ErrorCodeEnum.ExpirationDateIssueDateMismatch => title == null ? ExpirationDateIssueDateMismatch.Create(message: message) : ExpirationDateIssueDateMismatch.Create(title, message),
                (int)ErrorCodeEnum.EmploymentStatusError => title == null ? EmploymentStatusError.Create(message: message) : EmploymentStatusError.Create(title, message),
                (int)ErrorCodeEnum.NoSuitableRentalProperties => title == null ? NoSuitableRentalProperties.Create(message: message) : NoSuitableRentalProperties.Create(title, message),
                (int)ErrorCodeEnum.DocumentTypeError => title == null ? DocumentTypeError.Create(message: message) : DocumentTypeError.Create(title, message),
                (int)ErrorCodeEnum.SalesContractError => title == null ? SalesContractDocTypeError.Create(message: message) : SalesContractDocTypeError.Create(title, message),
                (int)ErrorCodeEnum.InvalidTaxYearError => title == null ? InvalidTaxYearError.Create(message: message) : InvalidTaxYearError.Create(title, message),

                _ => new Error()
            };
        }

        public static string ToStringFormat(dynamic value)
        {
            decimal val = Convert.ToDecimal(value);
            CultureInfo us = new CultureInfo("en-US");
            return val.ToString("N", us);
        }

        #region OldCode
        //interpreter.SetFunction("NoOperation", () => { });

        //interpreter.SetFunction("Subtract", new Func<string, string, decimal>(Subtract));
        //interpreter.SetFunction("Add", new Func<string, string, decimal>(Add));
        //interpreter.SetFunction("Devide", new Func<string, string, decimal>(Devide));
        //interpreter.SetFunction("Multiply", new Func<string, string, decimal>(Multiply));

        //foreach (var rule in rules)
        //{
        //    bool result = interpreter.Eval<bool>(rule.Condition);

        //    if (result)
        //    {
        //        var actions = rule.Action.Split('#');
        //        foreach (var action in actions)
        //        {
        //            interpreter.Eval(action.Trim());
        //        }
        //    }
        //}

        //public static decimal Subtract(string a, string b)
        //{
        //    return (decimal.TryParse(a, out var valA) ? valA : 0)
        //         - (decimal.TryParse(b, out var valB) ? valB : 0);
        //}
        //public static decimal Add(string a, string b)
        //{
        //    return (decimal.TryParse(a, out var valA) ? valA : 0)
        //         + (decimal.TryParse(b, out var valB) ? valB : 0);
        //}
        //public static decimal Devide(string a, string b)
        //{
        //    return (decimal.TryParse(a, out var valA) ? valA : 0)
        //         /(decimal.TryParse(b, out var valB) ? valB : 0);
        //}
        //public static decimal Multiply(string a, string b)
        //{
        //    return (decimal.TryParse(a, out var valA) ? valA : 0)
        //         * (decimal.TryParse(b, out var valB) ? valB : 0);
        //}
        #endregion
    }
}
