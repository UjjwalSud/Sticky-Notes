using System.Xml.Serialization;
using System.Windows;
namespace MyStickyNotes.Domain
{
    public class NoteEntity : DomainObject
    {
        public string str_NoteDescription { get; set; }
        [XmlIgnoreAttribute()]
        public string str_TempNoteDescription { get; set; }
        public string str_BackgroundColour;
        public TextWrapping TextWrapping { get; set; }
        #region Validation
        public override string GetValidationError(string PropertyName)
        {
            return "";
        }
        public override bool IsValid
        {
            get { return true; }
        }
        #endregion
    }
}
