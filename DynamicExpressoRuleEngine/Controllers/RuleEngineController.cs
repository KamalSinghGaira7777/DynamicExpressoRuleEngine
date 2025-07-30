using DynamicExpresso;
using DynamicExpressoRuleEngine.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DynamicExpressoRuleEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleEngineController : ControllerBase
    {
        [HttpPost("RunValidation")]
        public async Task<IActionResult> RunValidation()
        {
            string requestBody = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            MortgageValidatedDocument? response = JsonConvert.DeserializeObject<MortgageValidatedDocument>(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();

            Loan? loanDetails = config.GetSection("LoanDetails").Get<Loan>();

            CustomValidationRules clientsRule = new CustomValidationRules();
            
            clientsRule.HoiReplacementCost = 70;
            clientsRule.HoiCondoReplacementCost = 50;
            clientsRule.HoiDeductiblePercentage = 75;
            clientsRule.HoiEffectiveDateNumDays = 450;
            clientsRule.HoiEffectiveDateSameMonth = true;

            var interpreter = new Interpreter().Reference(typeof(List<>));

            interpreter.SetVariable("loanDetails", loanDetails);
            interpreter.SetVariable("clientsRule", clientsRule);

            RuleEngineService.RuleEngineValidateDocuments(response, interpreter);

            return Ok(new
            {
                Status =200,
                Result = response
            });
        }
    }
}
