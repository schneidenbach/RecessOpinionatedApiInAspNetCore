using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWorkshop.Api.Jobs.DeleteJob
{
    public class DeleteJobRequest : IRequest<IActionResult>
    {
        public DeleteJobRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
