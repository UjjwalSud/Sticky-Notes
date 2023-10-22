
using Helper.Utilities;
using System;
namespace MyStickyNotes.DomainLogic
{
    public class FileSystemDomainLogic : DomainLogicBase
    {
        public string getNewXMLName()
        {
            string path = FileSystem.ApplicationDirectory + FolderName.MyNotes + "\\" + Guid.NewGuid().ToString() + ".xml";
            FileSystem.createDirectory(path);
            return path;
        }
        public string getXMLFolderPath()
        {
            return FileSystem.ApplicationDirectory + FolderName.MyNotes;
        }
    }
}
