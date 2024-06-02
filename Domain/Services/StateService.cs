using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Entities;
using WebAPI.DAL;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Domain.Services
{
    public class StateService : IStateService
    {
        private readonly DataBaseContext _context;

        public StateService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<State> CreateStateAsync(State state)
        {
            try
            {
                state.Id = Guid.NewGuid();
                state.CreatedDate = DateTime.Now;
                _context.States.Add(state); 

                await _context.SaveChangesAsync(); 

                return state;

            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> DeleteStateAsync(Guid id)
        {
            try
            {
                var state = await GetStateByIdAsync(id);

                if (state == null)
                {
                    return null;
                }

                _context.States.Remove(state); 
                await _context.SaveChangesAsync(); 

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> EditStateAsync(State state)
        {
            try
            {
                state.ModifiedDate = DateTime.Now;

                _context.States.Update(state); 
                await _context.SaveChangesAsync();

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
        public async Task<State> GetStateByIdAsync(Guid id)
        {
            try
            {
                var state = await _context.States.FirstOrDefaultAsync(c => c.Id == id);
                //Otras dos formas de traerme un objeto desde la BD
                var state1 = await _context.States.FindAsync(id);
                var state2 = await _context.States.FirstAsync(c => c.Id == id);

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<IEnumerable<State>> GetStatesAsync()
        {
            try
            {
                var states = await _context.States.ToListAsync();
                return states;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
    }
}
