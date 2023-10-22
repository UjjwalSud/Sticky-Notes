using System.ComponentModel;
using System;
using System.Xml.Serialization;
namespace MyStickyNotes.Domain
{
    public abstract class DomainObject : IDataErrorInfo
    {
        #region Common properties
        public DateTime dt_CreatedDateTime { get; set; }
        public string str_CreatedDateTime { get; set; }
        #endregion

        [XmlIgnoreAttribute()]
        public string XMLPath { get; set; }
        [XmlIgnoreAttribute()]
        public string XMLName { get; set; }


        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
        }

        public string this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        #endregion

        #region Validation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public abstract string GetValidationError(string PropertyName);

        public abstract bool IsValid { get; }

        #endregion
    }
}
