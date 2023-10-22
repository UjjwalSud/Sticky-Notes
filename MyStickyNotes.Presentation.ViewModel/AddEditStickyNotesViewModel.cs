using System;
using System.Windows.Input;
using System.Collections.Generic;
using MyStickyNotes.Domain;
using MyStickyNotes.DomainFacade;
using System.Windows;
using Helper.Utilities;
namespace MyStickyNotes.Presentation.ViewModel
{
    public class AddEditStickyNotesViewModel : ViewModelBase
    {
        #region Command Fields
        ICommand _addNewNotesCommand;
        ICommand _deleteNoteCommand;
        ICommand _closeNoteCommand;
        #endregion

        #region Private Fields
        MainMenuViewModel _mainMenuViewModel;
        #endregion

        #region Public Fields
        public NoteEntity _noteEntity;
        #endregion

        #region Constructor
        /// <summary>
        /// Add Constructor
        /// </summary>
        /// <param name="mainMenuViewModel"></param>
        public AddEditStickyNotesViewModel(MainMenuViewModel mainMenuViewModel)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _noteEntity = new NoteEntity();
            BackgroundColour = getRandomColor();
            TextWrapping = System.Windows.TextWrapping.Wrap;
            InitialiseMenu();
            mainMenuViewModel = null;
        }
        /// <summary>
        /// Edit Constructor
        /// </summary>
        /// <param name="mainMenuViewModel"></param>
        /// <param name="noteEntity"></param>
        public AddEditStickyNotesViewModel(MainMenuViewModel mainMenuViewModel, NoteEntity noteEntity)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _noteEntity = noteEntity;
            InitialiseMenu();
            assignWrapNoWrapValue();
            mainMenuViewModel = null;
            noteEntity = null;
        }
        #endregion

        #region Commands
        public ICommand AddNewNotesCommand
        {
            get
            {
                if (_addNewNotesCommand == null)
                {
                    _addNewNotesCommand = new RelayCommand(param => OnRequestInsert(), param => true);
                }
                return _addNewNotesCommand;
            }
        }
        public ICommand DeleteNoteCommand
        {
            get
            {
                if (_deleteNoteCommand == null)
                {
                    _deleteNoteCommand = new RelayCommand(param => deleteNote(), param => true);
                }
                return _deleteNoteCommand;
            }
        }
        public ICommand CloseNoteCommand
        {
            get
            {
                if (_closeNoteCommand == null)
                {
                    _closeNoteCommand = new RelayCommand(param => closeNote(), param => true);
                }
                return _closeNoteCommand;
            }
        }
        #endregion

        #region Public Properties
        public string BackgroundColour
        {
            get { return _noteEntity.str_BackgroundColour; }
            set
            {
                if (_noteEntity.str_BackgroundColour != value)
                {
                    _noteEntity.str_BackgroundColour = value;
                    OnPropertyChanged("BackgroundColour");
                }
            }
        }
        public MainMenuViewModel MainMenuViewModel
        {
            get { return _mainMenuViewModel; }
            set { _mainMenuViewModel = value; }
        }
        public string XMLName
        {
            get
            {
                return _noteEntity.XMLName;
            }
            set
            {
                if (value != _noteEntity.XMLName)
                {
                    _noteEntity.XMLName = value;
                    OnPropertyChanged("XMLName");
                }
            }
        }
        public string NoteDescription
        {
            get
            {
                return _noteEntity.str_NoteDescription;
            }
            set
            {
                if (value != _noteEntity.str_NoteDescription)
                {
                    _noteEntity.str_NoteDescription = value;
                    OnPropertyChanged("NoteDescription");
                    saveData();
                }
            }
        }
        public TextWrapping TextWrapping
        {
            get
            {
                return _noteEntity.TextWrapping;
            }
            set
            {
                if (_noteEntity.TextWrapping != value)
                {
                    _noteEntity.TextWrapping = value;
                    OnPropertyChanged("TextWrapping");
                    if (_noteEntity.XMLName != null && _noteEntity.XMLName != "")
                    {
                        saveData();
                    }
                }
            }
        }
        #endregion

        #region Private Functions

        #region Menu Commands/Initialise Menu
        void InitialiseMenu()
        {
            _mainMenuViewModel.CloseNoteDelegate = closeNote;
            _mainMenuViewModel.DeleteNoteDelegate = deleteNote;
            _mainMenuViewModel.WrapCommandDelegate = wrapCommandDelegate;
        }
        void closeNote()
        {
            OnPropertyChanged("CloseNote");
        }
        void deleteNote()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this note?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                AddEditStickyNotesFacade.deleteNote(_noteEntity);
                OnPropertyChanged("CloseNote");
                OnRequestDelete();
                _noteEntity = null;
            }
        }
        void wrapCommandDelegate()
        {
            if (TextWrapping == System.Windows.TextWrapping.NoWrap)
            {
                TextWrapping = System.Windows.TextWrapping.Wrap;
                _mainMenuViewModel._wrapText.IsChecked = true;
            }
            else
            {
                TextWrapping = System.Windows.TextWrapping.NoWrap;
                _mainMenuViewModel._wrapText.IsChecked = false;
            }
        }
        #endregion

        public static string getRandomColor()
        {
            List<String> colors = new List<String> { "#FFFFCB", "#E5CBE4", "#CBE4DE", "#D5C69D","#D0E17D" };
            //List<String> colors = new List<String> { "#56C4E8" };
            Random rnd = new Random();
            System.Windows.Media.BrushConverter converter = new System.Windows.Media.BrushConverter();
            try { return colors[rnd.Next(0, colors.Count - 1)]; }
            finally { rnd = null; colors = null; }
        }
        void assignWrapNoWrapValue()
        {
            if (TextWrapping == System.Windows.TextWrapping.Wrap)
            {
                _mainMenuViewModel._wrapText.IsChecked = true;
            }
            else
            {
                _mainMenuViewModel._wrapText.IsChecked = false;
            }
        }

        #region Save Data
        void saveData()
        {
            bool isUpdated = false;
            if (_noteEntity.XMLPath == "" || _noteEntity.XMLPath == null)
            {
                //New Record created
                _noteEntity.XMLPath = FileSystemFacade.getNewXMLPath();
                XMLName = FileSystem.getFileNameWithExtension(_noteEntity.XMLPath);
                _noteEntity.dt_CreatedDateTime = DateTime.Now;
                isUpdated = false;
            }
            else
            {
                XMLName = FileSystem.getFileNameWithExtension(_noteEntity.XMLPath);
                isUpdated = false;
            }
            AddEditStickyNotesFacade.saveUpdateNote(_noteEntity);
            if (isUpdated)
            {
                OnRequestInsert();
            }
            else
            {
                OnRequestUpdate();
            }
        }
        #endregion

        #endregion

    }
}


