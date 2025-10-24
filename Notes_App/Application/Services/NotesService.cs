using Microsoft.EntityFrameworkCore;
using Notes_App.Application.Interfaces;
using Notes_App.Domain.Entities;
using Notes_App.Infrastructure.Data;

namespace Notes_App.Application.Services
{
    public class NotesService : INotesService
    {
        private readonly INoteRepository _repository;

        public NotesService(INoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Note?> GetNoteByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            await _repository.AddAsync(note);
            return note;
        }

        public async Task<Note> UpdateNoteAsync(Note note)
        {
            await _repository.UpdateAsync(note);
            return note;
        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
            var note = await _repository.GetByIdAsync(id);
            if (note == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Note>> SearchNotesAsync(string searchTerm)
        {
            return await _repository.SearchAsync(searchTerm);
        }
    }
}