using MyStickyNotes.Domain;
using Helper.Utilities;
using System.Collections.Generic;
namespace MyStickyNotes.DomainLogic
{
    public class AddEditStickyNotesDomainLogic : DomainLogicBase
    {
        public void saveUpdateNote(NoteEntity _noteEntity)
        {
            SerializeHelper.Serialize<Domain.NoteEntity>(_noteEntity.XMLPath, _noteEntity);
            _noteEntity = null;
        }
        public void deleteNote(NoteEntity _noteEntity)
        {
            try
            {
                if (FileSystem.fileExists(_noteEntity.XMLPath))
                {
                    FileSystem.DeleteFile(_noteEntity.XMLPath);
                }
            }
            finally { _noteEntity = null; }
        }

        public IList<NoteEntity> getAllNotes()
        {
            FileSystemDomainLogic fileSystem = new FileSystemDomainLogic();
            IList<NoteEntity> lst = new List<NoteEntity>();
            try
            {
                string path = fileSystem.getXMLFolderPath();
                string[] files = FileSystem.getFiles(path, "xml");
                if (files == null) return lst;
                foreach (string f in files)
                {
                    NoteEntity n = new NoteEntity();
                    n = DeserializeHelper.DeserializeXML<Domain.NoteEntity>(f);
                    n.XMLName = FileSystem.getFileNameWithExtension(f);
                    n.XMLPath = path + "\\" + n.XMLName;
                    lst.Add(n);
                }
                return lst;
            }
            finally { fileSystem = null; lst = null; }
        }
    }
}
