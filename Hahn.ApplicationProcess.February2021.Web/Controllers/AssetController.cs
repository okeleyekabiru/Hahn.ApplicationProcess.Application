using Hahn.ApplicationProcess.February2021.Domain.Application.Command;
using Hahn.ApplicationProcess.February2021.Domain.Application.Query;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Hahn.ApplicationProcess.February2021.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Create an asset.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Asset successfully created</response>
        /// <response code="400">Invalid asset input </response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<Unit>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<>), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddAssetCommand request)
        {
            var response = await _mediator.Send(request);
            return CreatedAtAction(nameof(Post), response);
        }
        /// <summary>
        /// Get an asset.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Asset query successfully</response>
        /// <response code="400">Invalid asset id </response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<Asset>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<>), (int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAssetQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Update an asset.
        /// </summary>s
        /// <param name="request"></param>
        /// <response code="200">Asset Update successfully</response>
        /// <response code="400">Invalid asset id </response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<Unit>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<>), (int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAssetCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        /// <summary>
        /// Delete an asset.
        /// </summary>s
        /// <param name="request"></param>
        /// <response code="200">Asset Deleted successfully</response>
        /// <response code="400">Invalid asset id </response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<Unit>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<>), (int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteAssetCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
