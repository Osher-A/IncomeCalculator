using IncomeCalculator.Shared.DTO;
using IncomeCalculator.Shared.Interfaces;
using MyWebApi.DAL;
using IncomeCalculator.WASM.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinWageController : ControllerBase
    {
        private MinWageProxyRepo _minWageProxyRepo;

        public MinWageController(IMinWageRepository minWageRepository)
        {
            _minWageProxyRepo = new MinWageProxyRepo(minWageRepository);
        }
       
        // GET: api/<MinWageController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _minWageProxyRepo.GetAllMinWagesAsync());
        }

        // POST api/<MinWageController>
        [HttpPost]
        public async void Post([FromBody] MinWage minWage)
        {
            await Task.Run(() => _minWageProxyRepo.AddMinWage(minWage));
        }

    }
}
