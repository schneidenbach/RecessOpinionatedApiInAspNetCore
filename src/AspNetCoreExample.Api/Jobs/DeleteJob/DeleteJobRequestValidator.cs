using FluentValidation;

namespace AspNetCoreWorkshop.Api.Jobs.DeleteJob
{
    public class DeleteJobRequestValidator : AbstractValidator<DeleteJobRequest>
    {
        public DeleteJobRequestValidator()
        {
        }
    }
}