using Microsoft.AspNetCore.Mvc;
using ToDo.Domain;
using ToDo.Domain.Services;
using ToDo.WebApi.Commands;
using ToDo.WebApi.Validations;

namespace ToDo.WebApi.Controllers
{
    [ApiController]
    [Route("todos")]
    public class ToDosController : ControllerBase
    {

        private readonly ILogger<ToDosController> _logger;
        private readonly IToDoService _service;

        public ToDosController(IToDoService service, ILogger<ToDosController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetToDos()
        {
            var motos = _service.List();
            return Ok(motos.Select(m => m.ToDTO()));
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<IActionResult> GetToDoAsync(string id)
        {
            if (!Validations.Validations.IsValidGuid(id))
            {
                return NotFound();
            }

            var toDo = await _service.GetAsync(id);
            return toDo == null ? NotFound() : Ok(toDo.ToDTO());
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddToDo command)
        {
            var validation = new AddToDoValidation().Validate(command);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors.Select(m => m.ErrorMessage));
            }

            var toDo = Domain.Entities.ToDo.Create(command.Desc);

            var result = await _service.AddAsync(toDo);

            return result.Match<IActionResult>(
                onSuccess: () => CreatedAtAction("GetTodo", new { id = toDo.Id.ToString() }, command),
                onFailure: errors => BadRequest(errors)
            );

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateToDoDesc command)
        {
            if (!Validations.Validations.IsValidGuid(id))
            {
                return NotFound();
            }

            var result = await _service.UpdateAsync(id, command.Desc);

            return result.Match<IActionResult>(
                onSuccess: () => NoContent(),
                onFailure: errors => BadRequest(errors)
            );

        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteAsync(string id)
        {
            if (!Validations.Validations.IsValidGuid(id))
            {
                return NotFound();
            }

            var result = await _service.CompleteAsync(id);

            return result.Match<IActionResult>(
                onSuccess: () => NoContent(),
                onFailure: errors => BadRequest(errors)
            );

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Validations.Validations.IsValidGuid(id))
            {
                return NotFound();
            }

            var result = await _service.DeleteAsync(id);

            return result.Match<IActionResult>(
                onSuccess: () => NoContent(),
                onFailure: errors => BadRequest(errors)
            );
        }
    }
}
