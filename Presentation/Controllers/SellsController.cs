using Application.Features.Sells.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class SellsController : Controller
    {
        private readonly IMediator _mediat;
        public SellsController(IMediator mediat)
        {
            _mediat = mediat;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoadSellsData()
        {
            var draw = Convert.ToInt16(Request.Form["draw"].FirstOrDefault());
            var skip = Convert.ToInt16(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = Convert.ToInt16(Request.Form["length"].FirstOrDefault() ?? "0");
            var query = new sellsQeury
            {
                length = length,
                start = skip
            };
            var result =await _mediat.Send(query);
            result.draw = draw;
            return Json(result);
        }
    }
}
