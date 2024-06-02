using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : Controller
    {
        private readonly IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Country>>> GetStatesAsync()
        {
            var states = await _stateService.GetStatesAsync();

            if (states == null || !states.Any()) return NotFound();

            return Ok(states);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/states/get
        public async Task<ActionResult<Country>> GetStateByIdAsync(Guid id)
        {
            var states = await _stateService.GetStateByIdAsync(id);

            if (states == null) return NotFound(); //NotFound = Status Code 404

            return Ok(states); //Ok = Status Code 200
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<State>> CreateStateAsync(State state)
        {
            try
            {
                var newState = await _stateService.CreateStateAsync(state);
                if (newState == null) return NotFound();
                return Ok(newState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", state.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<State>> EditStateAsync(State state)
        {
            try
            {
                var editedState = await _stateService.EditStateAsync(state);
                if (editedState == null) return NotFound();
                return Ok(editedState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", state.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<State>> DeleteStateAsync(Guid id)
        {
            if (id == null) return BadRequest();

            var deletedState = await _stateService.DeleteStateAsync(id);

            if (deletedState == null) return NotFound();

            return Ok("Borrado satisfactoriamente");

        }
    }
}
