using Catstagram.Server.Features.Cats.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

using static Catstagram.Server.Infrastructure.WebConstants;

namespace Catstagram.Server.Features.Cats
{
    [Authorize]
    public class CatsController : ApiController
    {
        private readonly ICurrentUserService _user;
        private readonly ICatsService _catService;

        public CatsController(ICurrentUserService currentUser, ICatsService catService)
        {
            _user = currentUser;
            _catService = catService;
        }

        [HttpGet]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
        {
            var userId = _user.GetId();
            var cats = await this._catService.ByUser(userId);
            return cats;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CatDetailsServiceModel>> Details(int id)
                 => await _catService.Details(id);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(CreateCatRequestModel model)
        {
            var userId = _user.GetId();

            var id = await _catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCatRequestModel model) 
        {
            var userId = _user.GetId();

            var updated = await this._catService.Update(model.Id, model.Description, userId);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _user.GetId();

            var deleted = await _catService.Delete(id, userId);

            if(!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
