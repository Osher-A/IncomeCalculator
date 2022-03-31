using IncomeCalculator.DAL;
using IncomeCalculator.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaxCreditsController : ControllerBase
    {
        private ITaxCreditsRepository _taxCreditsRepo;
        public TaxCreditsController(ITaxCreditsRepository taxCreditsRepo)
        {
            _taxCreditsRepo = taxCreditsRepo;
        }
        [HttpGet]
        [ActionName("WtcData")]
        public IEnumerable<WorkingTaxCredit> GetAllWTCData()
        {
            return _taxCreditsRepo.GetAllWTCData();
        }

        [HttpGet]
        [ActionName("CtcData")]
        public IEnumerable<ChildTaxCredit> GetAllCTCData()
        {
            return _taxCreditsRepo.GetAllCTCData();
        }

        [HttpPost]
        [ActionName("AddWtc")]
        public void AddWTC([FromBody] WorkingTaxCredit wtc)
        {
            _taxCreditsRepo.AddWTCData(wtc);
        }

        [HttpPost]
        [ActionName("AddCtc")]
        public void AddCTC([FromBody] ChildTaxCredit ctc)
        {
            _taxCreditsRepo.AddCTCData(ctc);
        }

    }
}
