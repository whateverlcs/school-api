using Microsoft.AspNetCore.Mvc;
using School.API.Attributes;
using School.API.Binders;
using School.Application.UseCases.Student.Delete;
using School.Application.UseCases.Student.GetAllStudents;
using School.Application.UseCases.Student.GetByAcademyId;
using School.Application.UseCases.Student.GetById;
using School.Application.UseCases.Student.Register;
using School.Application.UseCases.Student.Update;
using School.Communication.Requests;
using School.Communication.Responses;

namespace School.API.Controllers
{
    [AuthenticatedUser]
    public class StudentController : SchoolBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseStudentJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
        [FromServices] IRegisterStudentUseCase useCase,
        [FromBody] RequestRegisterStudentJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
        [FromServices] IDeleteStudentUseCase useCase,
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
        [FromServices] IUpdateStudentUseCase useCase,
        [FromRoute][ModelBinder(typeof(SchoolIdBinder))] long id,
        [FromBody] RequestRegisterStudentJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseStudentJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
        [FromServices] IGetStudentByIdUseCase useCase,
        [FromRoute][ModelBinder(typeof(SchoolIdBinder))] long id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("academy/{id}")]
        [ProducesResponseType(typeof(ResponseStudentJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAcademyId(
        [FromServices] IGetStudentByAcademyIdUseCase useCase,
        [FromRoute][ModelBinder(typeof(SchoolIdBinder))] long id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseStudentJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllStudents(
        [FromServices] IGetAllStudentsUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }
    }
}