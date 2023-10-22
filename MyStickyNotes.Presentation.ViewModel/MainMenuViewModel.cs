using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace MyStickyNotes.Presentation.ViewModel
{
    public class MainMenuViewModel : ViewModelBase
    {
        #region Private Fields
        MenuCommandViewModel _fileMenu;
        public MenuCommandViewModel _wrapText;
        private ObservableCollection<MenuCommandViewModel> _menuItems;
        #endregion

        #region Constructors
        private MainMenuViewModel()
        {
            BuildMenu();
        }
        public static MainMenuViewModel CreateInstance()
        {
            return new MainMenuViewModel();
        }
        #endregion

        #region Action delegates
        public Action AddNewNoteDelegate { get; set; }
        public Action CloseNoteDelegate { get; set; }
        public Action DeleteNoteDelegate { get; set; }
        public Action WrapCommandDelegate { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<MenuCommandViewModel> MenuItems
        {
            get
            {
                return (_menuItems = _menuItems ??
                                     new ObservableCollection<MenuCommandViewModel>());
            }
        }
        #endregion

        #region Private Methods
        private void BuildMenu()
        {
            //File Menu
            _fileMenu = new MenuCommandViewModel { MenuHeader = "File" };
            MenuItems.Add(_fileMenu);
            _fileMenu.Items.Add(new MenuCommandViewModel { MenuHeader = "New", Command = new RelayCommand(param => AddNewNoteAction(), param => true), IsSeparator = false });
            _fileMenu.Items.Add(new MenuCommandViewModel { MenuHeader = "Close", Command = new RelayCommand(param => CloseNoteAction(), param => true), IsSeparator = false });
            _fileMenu.Items.Add(new MenuCommandViewModel { MenuHeader = "Delete", Command = new RelayCommand(param => DeleteNoteAction(), param => true), IsSeparator = false });

            // wrap Command
            _wrapText = new MenuCommandViewModel { MenuHeader = "Wrod Wrap", Command = new RelayCommand(param => WrapCommandAction(), param => true), IsSeparator = false, IsChecked = true };
            _fileMenu.Items.Add(_wrapText);
            // _fileMenu.Items.Add(new MenuCommandViewModel { MenuHeader = "", IsSeparator = true });
        }
        void AddNewNoteAction()
        {
            AddNewNoteDelegate();
        }
        void CloseNoteAction()
        {
            CloseNoteDelegate();
        }
        void DeleteNoteAction()
        {
            DeleteNoteDelegate();
        }
        void WrapCommandAction()
        {
            WrapCommandDelegate();
        }
        #endregion
    }
}
