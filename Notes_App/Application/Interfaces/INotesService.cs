using Notes_App.Domain.Entities;

namespace Notes_App.Application.Interfaces
{
    public interface INotesService
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> CreateNoteAsync(Note note);
        Task<Note> UpdateNoteAsync(Note note);
        Task<bool> DeleteNoteAsync(int id);

        Task<Note?> GetNoteByIdAsync(int id);

        Task<IEnumerable<Note>> SearchNotesAsync(string searchTerm);
    }
}
