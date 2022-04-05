using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinWageController : ControllerBase
    {
        private IMinWageRepository _minWageRepository;
        public MinWageController(IMinWageRepository minWageRepo)
        {
            _minWageRepository = minWageRepo;
        }
        // GET: api/<MinWageController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _minWageRepository.GetMinWagesAsync());
        }

        // POST api/<MinWageController>
        [HttpPost]
        public void Post([FromBody] MinWage minWage)
        {
            _minWageRepository.AddMinWage(minWage);
        }

    }
}
