
using System.ComponentModel;
using System;
namespace MyStickyNotes.Presentation.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Event Handlers
        public event EventHandler RequestUpdate;
        public event EventHandler RequestDelete;
        public event EventHandler RequestInsert;

        protected void OnRequestUpdate()
        {
            EventHandler handler = this.RequestUpdate;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        protected void OnRequestDelete()
        {
            EventHandler handler = this.RequestDelete;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        protected void OnRequestInsert()
        {
            EventHandler handler = this.RequestInsert;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion // INotifyPropertyChanged Members

    }
}
