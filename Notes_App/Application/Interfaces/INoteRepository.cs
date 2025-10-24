using Notes_App.Domain.Entities;

namespace Notes_App.Application.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllAsync();
        Task<Note?> GetByIdAsync(int id);
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(int id);
        Task<IEnumerable<Note>> SearchAsync(string searchTerm);
    }
}
