using MyStickyNotes.DomainLogic;
namespace MyStickyNotes.DomainFacade
{
    public static class AddEditStickyNotesFacade
    {
        public static void saveUpdateNote(Domain.NoteEntity _noteEntity)
        {
            AddEditStickyNotesDomainLogic obj = new AddEditStickyNotesDomainLogic();
            try { obj.saveUpdateNote(_noteEntity); }
            finally { obj = null; _noteEntity = null; }
        }
        public static void deleteNote(Domain.NoteEntity _noteEntity)
        {
            AddEditStickyNotesDomainLogic obj = new AddEditStickyNotesDomainLogic();
            try { obj.deleteNote(_noteEntity); }
            finally { obj = null; _noteEntity = null; }
        }
        public static System.Collections.Generic.IList<Domain.NoteEntity> getAllNotes()
        {
            AddEditStickyNotesDomainLogic obj = new AddEditStickyNotesDomainLogic();
            try { return obj.getAllNotes(); }
            finally { obj = null; }
        }
    }
}
