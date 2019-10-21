using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AspNetCoreWorkshop.Api.Jobs.UpdateJob
{
    public class UpdateJobRequest : IRequest<IActionResult>
    {
        public UpdateJobRequest(int id, JObject data)
        {
            Id = id;
            Data = data;
        }
        
        public int Id { get; }
        public JObject Data { get; }
    }
}
