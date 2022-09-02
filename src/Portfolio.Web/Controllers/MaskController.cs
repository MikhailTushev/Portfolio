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

        /// <summary>
        /// Выводит возможные спец слова, для того, что бы построить маску. 
        /// </summary>
        [HttpGet("/mask_conditions")]
        public Dictionary<string, string> GetMaskConditions()
        {
            return MaskConst.Values;
        }

        /// <summary>
        /// Создает маску по спец словам. example "simple/{UserName}@{Year}-example1"
        /// </summary>
        [HttpPost]
        public Task<string> Create([FromQuery] GenerateMaskCommand request)
        {
            return _mediator.Send(request);
        }
    }
}