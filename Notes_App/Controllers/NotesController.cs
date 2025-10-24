using Microsoft.AspNetCore.Mvc;
using Notes_App.Application.Interfaces;
using Notes_App.Domain.Entities;

namespace Notes_App.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesService _notesService;
        private readonly ILogger<NotesController> _logger;

        public NotesController(INotesService notesService, ILogger<NotesController> logger)
        {
            _notesService = notesService;
            _logger = logger;
        }

        // GET: Notes
        public async Task<IActionResult> Index(string searchTerm)
        {
            try
            {
                IEnumerable<Note> notes;

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    notes = await _notesService.SearchNotesAsync(searchTerm);
                    ViewData["SearchTerm"] = searchTerm;
                }
                else
                {
                    notes = await _notesService.GetAllNotesAsync();
                }

                return View(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notes");
                TempData["ErrorMessage"] = "An error occurred while retrieving notes.";
                return View(new List<Note>());
            }
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var note = await _notesService.GetNoteByIdAsync(id);
                if (note == null)
                {
                    return NotFound();
                }

                return View(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note with ID {Id}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving the note.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] Note note)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _notesService.CreateNoteAsync(note);
                    TempData["SuccessMessage"] = "Note created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating note");
                ModelState.AddModelError("", "An error occurred while creating the note.");
            }

            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var note = await _notesService.GetNoteByIdAsync(id);
                if (note == null)
                {
                    return NotFound();
                }

                return View(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note for editing with ID {Id}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving the note for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Notes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CreatedAt")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _notesService.UpdateNoteAsync(note);
                    TempData["SuccessMessage"] = "Note updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating note with ID {Id}", id);
                ModelState.AddModelError("", "An error occurred while updating the note.");
            }

            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var note = await _notesService.GetNoteByIdAsync(id);
                if (note == null)
                {
                    return NotFound();
                }

                return View(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note for deletion with ID {Id}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving the note for deletion.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _notesService.DeleteNoteAsync(id);
                if (success)
                {
                    TempData["SuccessMessage"] = "Note deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Note not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting note with ID {Id}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the note.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}