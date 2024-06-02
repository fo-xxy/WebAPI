using WebAPI.DAL.Entities;

namespace WebAPI.Domain.Interfaces
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetStatesAsync();
        Task<State> GetStateByIdAsync(Guid id);
        Task<State> CreateStateAsync(State state);
        Task<State> EditStateAsync(State state);
        Task<State> DeleteStateAsync(Guid id);
    }
}
