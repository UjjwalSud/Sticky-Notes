using System.Collections.ObjectModel;
using System.Windows.Input;
namespace MyStickyNotes.Presentation.ViewModel
{
    public class MenuCommandViewModel : ViewModelBase
    {
        #region Private Variables
        private bool _isSelected;
        private string _iconName;
        private string _menuHeader;
        private bool _isVisible = true;
        private bool _isChecked;
        string _menuImagePath = "";
        private ObservableCollection<MenuCommandViewModel> _items;
        #endregion

        #region Properties
        public ICommand Command { get; set; }
        public string MenuHeader
        {
            get { return _menuHeader; }
            set { _menuHeader = value; OnPropertyChanged("MenuHeader"); }
        }
        public string IconName
        {
            get { return _iconName; }
            set { _iconName = value; OnPropertyChanged("IconName"); }
        }
        public bool IsVisible
        {
            get { return _isVisible; }
            set { if (value != _isVisible) { _isVisible = value; OnPropertyChanged("IsVisible"); } }
        }
        public bool IsSelected
        {
            get { return _isSelected; }
            set { if (value != _isSelected) { _isSelected = value; OnPropertyChanged("IsSelected"); } }
        }
        public bool IsChecked
        {
            get { return _isChecked; }
            set { if (value != _isChecked) { _isChecked = value; OnPropertyChanged("IsChecked"); } }
        }
        public bool IsSeparator { get; set; }
        public ObservableCollection<MenuCommandViewModel> Items
        {
            get
            {
                return (_items = _items ??
                                 new ObservableCollection<MenuCommandViewModel>());
            }
        }
        public bool HasChildren
        {
            get { return Items.Count > 0; }
        }
        #endregion
    }
}
