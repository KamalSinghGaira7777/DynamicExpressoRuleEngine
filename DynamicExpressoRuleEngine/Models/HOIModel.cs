using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DynamicExpressoRuleEngine.Models
{
    public class ValidationRule
    {
        public string RuleName { get; set; }
        public string Condition { get; set; }
        public string Action { get; set; }
    }

    #region ValidatedDocuemnt
    public class ProcessedDocument
    {
        [JsonProperty("version")]
        public Version? Version { get; set; }

        [JsonProperty("results")]
        public List<Result>? Results { get; set; }

        [JsonProperty("errors")]
        public List<Error>? Errors { get; set; }

        public void AddError(Error error)
        {
            Error error2 = error;
            if (Errors == null)
            {
                Errors = new List<Error>();
            }

            if (Errors.Where((Error e) => e.Code == error2.Code).Count() == 0)
            {
                Errors.Add(error2);
            }
        }
    }

    public class Result
    {
        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("label")]
        public string? Label { get; set; }

        [JsonProperty("sortOrder")]
        public int? SortOrder { get; set; }

        [JsonProperty("value")]
        public dynamic? Value { get; set; }

        [JsonProperty("originalValue")]
        public dynamic? OriginalValue { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("isOverride")]
        public bool? IsOverride { get; set; }

        [JsonProperty("regEx")]
        public string? RegEx { get; set; }

        [JsonProperty("source")]
        public string? Source { get; set; }

        [JsonProperty("processingResult")]
        public ProcessingResult? ProcessingResult { get; set; }
    }

    public class ProcessingResult
    {
        [JsonProperty("tags")]
        public List<Tag>? OcrTags { get; set; }

        [JsonProperty("notes")]
        public List<string>? Notes { get; set; }

        [JsonProperty("errors")]
        public List<Error>? Errors { get; set; }

        [JsonProperty("potentialValues")]
        public List<dynamic>? PotentialValues { get; set; }

        public void AddNote(string note)
        {
            if (Notes == null)
            {
                Notes = new List<string>();
            }

            if (!Notes.Contains(note))
            {
                Notes.Add(note);
            }
        }

        public void AddError(Error error)
        {
            Error error2 = error;
            if (Errors == null)
            {
                Errors = new List<Error>();
            }

            if (Errors.Where((Error e) => e.Code == error2.Code).Count() == 0)
            {
                Errors.Add(error2);
            }
        }

        public void AddPotentialValues(List<dynamic> values)
        {
            if (PotentialValues == null)
            {
                PotentialValues = new List<object>();
            }

            PotentialValues.AddRange(values);
        }

        public void AddPotentialValues(dynamic value)
        {
            if (PotentialValues == null)
            {
                PotentialValues = new List<object>();
            }

            ((List<object>)PotentialValues).Add(value);
        }
    }

    public class Tag
    {
        [JsonProperty("tag")]
        public string? Name { get; set; }

        [JsonProperty("rawValue")]
        public string? RawValue { get; set; }

        [JsonProperty("mlConfidence")]
        public double MlConfidence { get; set; }

        [JsonProperty("errors")]
        public List<Error>? Errors { get; set; }
    }

    public class Error
    {
        [JsonProperty("level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevel Level { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("accepted")]
        public bool? Accepted { get; set; }
    }

    public class MortgageValidatedDocument
    {
        [JsonProperty("loanNumber", Order = 10)]
        public string? LoanNumber { get; set; }

        [JsonProperty("losId", Order = 11)]
        public string? LosId { get; set; }

        [JsonProperty("universalLoanId", Order = 12)]
        public string UniversalLoanId { get; set; } = string.Empty;

        [JsonProperty("borrowerId", Order = 13)]
        public string BorrowerId { get; set; } = string.Empty;

        [JsonProperty("employmentLosId", Order = 14)]
        public string EmploymentLosId { get; set; } = string.Empty;

        [JsonProperty("coborrowerId", Order = 15)]
        public string CoBorrowerId { get; set; } = string.Empty;

        [JsonProperty("id", Order = 1)]
        public string? Id { get; set; }

        [JsonProperty("version", Order = 2)]
        public string Version { get; set; } = "1.0";

        [JsonProperty("type", Order = 3)]
        public string? Type { get; set; }

        [JsonProperty("mlDocumentRefId", Order = 4)]
        public string? MlDocumentRefId { get; set; }

        [JsonProperty("processedDocument", Order = 50)]
        public ProcessedDocument? ProcessedDocument { get; set; }
    }
    #endregion

    #region Loan
    public class Loan
    {
        //loan Identifiers
        public string LoanGuid { get; set; } = null!;
        public string LoanNumber { get; set; } = null!;
        public string OrgId { get; set; } = null!;
        public string UniversalLoanId { get; set; } = null!;
        public string LoanType { get; set; } = null!;

        //Key Dates and Statuses
        public string LoanStatus { get; set; } = null!;
        public DateTime? FileStartedDate { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime? FundedDate { get; set; }
        public DateTime? LoanSoldDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? CanceledDate { get; set; }

        //Basic Information
        public string LoanPurpose { get; set; }
        public string PropertyType { get; set; }
        public LoanAddress Property { get; set; } = null!;
        public decimal LoanAmountOriginal { get; set; }
        public string Impounds { get; set; }

        public string InvestorName { get; set; }
        public string InvestorLoanNumber { get; set; }
        public string MortgageType { get; set; }
        public string LienType { get; set; }

        public string LoanOfficerName { get; set; }
        public string LoanOfficerEmail { get; set; }

        //Borrowers
        public LoanParty Borrower { get; set; } = null!;
        public LoanParty Coborrower { get; set; } = null!;

        
        public List<LoanParty> AdditionalBorrowers { get; set; } = new List<LoanParty>();

        
        public List<LoanParty> AllBorrowers
        {
            get
            {
                var allBorrowers = new List<LoanParty>();
                allBorrowers.Add(Borrower);
                allBorrowers.Add(Coborrower);
                allBorrowers.AddRange(AdditionalBorrowers);
                return allBorrowers;
            }
        }

        
        public List<string> VestingNonBorrowers { get; set; } = new List<string>();
    }

    public class LoanParty
    {
        public string LosId { get; set; }
        public string FullName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Suffix { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public LoanAddress CurrentAddress { get; set; } = null!;
        public LoanAddress PriorAddress { get; set; } = null!;
        public LoanAddress MailingAddress { get; set; } = null!;
        public string YearsAtCurrentAddress { get; set; }
        public string ApplicationId { get; set; }
        public string TaxpayerIdentificationNumber { get; set; }
    }

    public class LoanAddress
    {

        public string AddressLineOne { get; set; } = null!;
        public string AddressLineTwo { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string County { get; set; } = null!;
        public string Country { get; set; } = null!;

        public string FullAddress
        {
            get
            {
                var array = new[] { AddressLineOne, AddressLineTwo, City, State, PostalCode };
                return string.Join(", ", array.Where(s => !string.IsNullOrEmpty(s)));
            }
        }
    }
    #endregion
}
