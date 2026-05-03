using Application.DTO.Accountdto;
using Application.Features.AccountFeatures.Account.Query;
using Application.Features.Department.Command;
using Application.Features.Department.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
         _mediator = mediator;
        }
        [Authorize(Policy ="Add User")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetDepartments()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            int start =Convert.ToInt16(Request.Form["start"].FirstOrDefault() ?? "0") ;
            int length = Convert.ToInt16(Request.Form["length"].FirstOrDefault() ?? "10");

            var Query =new DepartmentListQuery
            {
                start = start,
                length = length
            };
            var data = await _mediator.Send(Query);
            data.draw = Convert.ToInt16(draw);
            return Json(data);
        }
        public JsonResult AddDepartment(Departmentdto dto)
        {
            var Data = new DepartmenCommand
            { 
            DepartmentName = dto.DepartmentName
            };
            var Result  = _mediator.Send(Data); 
            return Json(new { success=true});
        }
    }
}
