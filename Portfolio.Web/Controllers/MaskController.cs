using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.ApplicationServices.Example;
using Portfolio.Common.Consts;

namespace Portfolio.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/mask_conditions")]
        public Dictionary<string, string> GetMaskConditions()
        {
            return MaskConst.Values;
        }

        [HttpPost]
        public Task<string> CreateNotification([FromQuery] GenerateMaskCommand request)
        {
            return _mediator.Send(request);
        }
    }
}