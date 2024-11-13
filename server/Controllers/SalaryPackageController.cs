using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryPackageController : ControllerBase
    {
        private readonly SalaryPackgingService _salaryPackingService;

        public SalaryPackageController()
        {
            _salaryPackingService = new SalaryPackgingService();
        }

        [HttpPost]
        public ActionResult<SalaryPackagingFinalResult> GetSalaryPackaging([FromBody] UserInfo userInfo)
        {
            if (userInfo == null || userInfo.Salary <= 0) 
            {
                return BadRequest(new SalaryPackagingFinalResult
                {
                    TotalSalaryPackagin = 0
                });
            }
            var result = _salaryPackingService.CalculateSalaryPackaging(userInfo);
            return Ok(result);
        }

    }
}
