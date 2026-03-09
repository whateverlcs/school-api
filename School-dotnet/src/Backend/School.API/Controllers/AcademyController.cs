using Microsoft.AspNetCore.Mvc;
using School.API.Attributes;
using School.API.Binders;
using School.Application.UseCases.Academy.Delete;
using School.Application.UseCases.Academy.GetAllAcademys;
using School.Application.UseCases.Academy.GetByCity;
using School.Application.UseCases.Academy.GetById;
using School.Application.UseCases.Academy.GetByName;
using School.Application.UseCases.Academy.GetByState;
using School.Application.UseCases.Academy.Register;
using School.Application.UseCases.Academy.Update;
using School.Communication.Requests;
using School.Communication.Responses;

namespace School.API.Controllers
{
    [AuthenticatedUser]
    public class AcademyController : SchoolBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseAcademyJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
        [FromServices] IRegisterAcademyUseCase useCase,
        [FromBody] RequestRegisterAcademyJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
        [FromServices] IDeleteAcademyUseCase useCase,
        [FromRoute][ModelBinder(typeof(SchoolIdBinder))] long id)
        {
            await useCase.Execute(id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
        [FromServices] IUpdateAcademyUseCase useCase,
        [FromRoute][ModelBinder(typeof(SchoolIdBinder))] long id,
        [FromBody] RequestRegisterAcademyJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseAcademyJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
        [FromServices] IGetAcademyByIdUseCase useCase,
        [FromRoute][ModelBinder(typeof(SchoolIdBinder))] long id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseAcademyJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAcademys(
        [FromServices] IGetAllAcademysUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }

        [HttpPost]
        [Route("name")]
        [ProducesResponseType(typeof(IList<ResponseAcademyJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName(
        [FromServices] IGetAcademyByNameUseCase useCase,
        [FromBody] RequestGetAcademyByName request)
        {
            var response = await useCase.Execute(request.Name);

            return Ok(response);
        }

        [HttpPost]
        [Route("state")]
        [ProducesResponseType(typeof(IList<ResponseAcademyJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByState(
        [FromServices] IGetAcademyByStateUseCase useCase,
        [FromBody] RequestGetAcademyByState request)
        {
            var response = await useCase.Execute(request.State);

            return Ok(response);
        }

        [HttpPost]
        [Route("city")]
        [ProducesResponseType(typeof(IList<ResponseAcademyJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCity(
        [FromServices] IGetAcademyByCityUseCase useCase,
        [FromBody] RequestGetAcademyByCity request)
        {
            var response = await useCase.Execute(request.City);

            return Ok(response);
        }
    }
}