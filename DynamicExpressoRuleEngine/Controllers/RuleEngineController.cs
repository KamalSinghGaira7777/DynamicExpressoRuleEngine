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

            RuleEngineService.RuleEngineValidateDocuments(response, null);

            return Ok(new
            {
                Status =200,
                Result = response
            });
        }
    }
}
