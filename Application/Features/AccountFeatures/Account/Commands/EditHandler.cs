using Application.IRepository;
using MediatR;
using FluentValidation;

namespace Application.Features.AccountFeatures.Account.Commands
{
    public class EditCommand : IRequest<object>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LocalName { get; set; }
        public int RankId { get; set; }
        //public string RoleId { get; set; }
        public string DepartmentId { get; set; }
        //public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class TaskItemValidator : AbstractValidator<EditCommand>
    {
        public TaskItemValidator()
        {
            RuleFor(v => v.Id).NotEmpty().WithMessage("id is not found");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(v => v.LocalName).NotEmpty().WithMessage("Local Name is required");
            RuleFor(v => v.PhoneNumber).NotEmpty().WithMessage("Phone Number is required");
            RuleFor(v => v.RankId).NotEmpty().WithMessage("Rank is required");
            RuleFor(v => v.DepartmentId).NotEmpty().WithMessage("Department is required");
        }
    }
    public class EditHandler : IRequestHandler<EditCommand, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
     

        public Task<object> Handle(EditCommand request, CancellationToken cancellationToken)
        {
            var validator = new TaskItemValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(" ",
                    validationResult.Errors.Select(e => e.ErrorMessage));

                //    TempData["ErrorMessage"] = errors;
                return Task.FromResult<object>(new
                {
                    success = false,
                    errorList = errors
                });

            }
                var Result = _unitOfWork.Account.EditAppUser(request);

                return Task.FromResult<object>(new { success = true,data =Result });
        }
    }
}
