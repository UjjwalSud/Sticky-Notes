using MyStickyNotes.DomainLogic;
namespace MyStickyNotes.DomainFacade
{
    public static class FileSystemFacade
    {
        public static string getNewXMLPath()
        {
            FileSystemDomainLogic obj = new FileSystemDomainLogic();
            try { return obj.getNewXMLName(); }
            finally { obj = null; }
        }
    }
}
