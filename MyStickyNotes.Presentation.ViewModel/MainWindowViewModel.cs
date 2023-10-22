using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MyStickyNotes.Domain;
using MyStickyNotes.DomainFacade;
namespace MyStickyNotes.Presentation.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields
        IList<Domain.NoteEntity> _noteEntity;
        ICollectionView _noteEntityView;
        string _searchText = "";
        string _currentXMLName = "";
        Visibility _resetButtonVisibility = Visibility.Collapsed;
        #endregion

        #region Command Fields
        ICommand _addNewNoteCommand;
        ICommand _resetFilterTextCommand;
        ICommand _viewNoteCommand;
        ICommand _deleteNoteCommand;
        #endregion

        #region View Model Fields
        MainMenuViewModel _mainMenuViewModel;
        AddEditStickyNotesViewModel _addEditStickyNotes;
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            #region Initialise Menu Commands/Viewmodel
            InitialiseMainMenuViewModel();
            #endregion

            LoadMyNotes();
        }
        public static MainWindowViewModel CreateInstance()
        {
            return new MainWindowViewModel();
        }
        #endregion

        #region Initialise MainMenu ViewModel Delegate
        private void InitialiseMainMenuViewModel()
        {
            _mainMenuViewModel = MainMenuViewModel.CreateInstance();
            _mainMenuViewModel.AddNewNoteDelegate = AddNewNoteDelegate;
        }
        #endregion

        #region Menu Functions
        void AddNewNoteDelegate()
        {
            AddNote();
        }
        #endregion

        #region View Model Properties
        public MainMenuViewModel MainMenuViewModel
        {
            get { return _mainMenuViewModel; }
        }
        public AddEditStickyNotesViewModel AddEditStickyNotes
        {
            get { return _addEditStickyNotes; }
            set { _addEditStickyNotes = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Property Used to find the Current Window
        /// </summary>
        public string CurrentXMLName
        {
            get { return _currentXMLName; }
            set { _currentXMLName = value; }
        }
        public ICollectionView NoteEntityView
        {
            get { return _noteEntityView; }
        }
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value != _searchText)
                {
                    _searchText = value;
                    refereshNoteEntity();
                    OnPropertyChanged("SearchText");
                }
            }
        }
        public Visibility ResetButtonVisibility
        {
            get { return _resetButtonVisibility; }
            set
            {
                if (value != _resetButtonVisibility)
                {
                    _resetButtonVisibility = value;
                    OnPropertyChanged("ResetButtonVisibility");
                }
            }
        }
        #endregion

        #region Load All Notes
        void LoadMyNotes()
        {
            _noteEntity = DomainFacade.AddEditStickyNotesFacade.getAllNotes();
            foreach (NoteEntity n in _noteEntity)
            {
                n.str_TempNoteDescription = formateDescription(n.str_NoteDescription);
            }
            refereshNoteEntity();
        }
        void refereshNoteEntity()
        {
            if (_noteEntity != null && _noteEntity.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(_searchText))
                {
                    _noteEntityView = CollectionViewSource.GetDefaultView(_noteEntity.OrderBy(c => c.str_NoteDescription));
                    ResetButtonVisibility = Visibility.Collapsed;
                }
                else
                {
                    _noteEntityView = CollectionViewSource.GetDefaultView(_noteEntity.Where(x => x.str_NoteDescription.ToLower().Contains(_searchText.ToLower())).OrderBy(c => c.str_NoteDescription));
                    ResetButtonVisibility = Visibility.Visible;
                }
            }
            if (NoteEntityView != null)
            {
                NoteEntityView.Refresh();
            }
            OnPropertyChanged("NoteEntityView");
        }

        #endregion

        #region Delete Note
        void DeleteNote()
        {
            OnPropertyChanged("DeleteNewNote");
        }
        #endregion

        #region Commands
        public ICommand AddNewNoteCommand
        {
            get
            {
                if (_addNewNoteCommand == null)
                {
                    _addNewNoteCommand = new RelayCommand(param => AddNote(), param => true);
                }
                return _addNewNoteCommand;
            }
        }
        public ICommand ResetFilterTextCommand
        {
            get
            {
                if (_resetFilterTextCommand == null)
                {
                    _resetFilterTextCommand = new RelayCommand(param => clearSearch(), param => true);
                }
                return _resetFilterTextCommand;
            }
        }
        public ICommand ViewNoteCommand
        {
            get
            {
                if (_viewNoteCommand == null)
                {
                    _viewNoteCommand = new RelayCommand<object>(OpenNoteCommand, CanExecuteOpenNoteCommand);
                }
                return _viewNoteCommand;
            }
        }
        public ICommand DeleteNoteCommand
        {
            get
            {
                if (_deleteNoteCommand == null)
                {
                    _deleteNoteCommand = new RelayCommand<object>(DeleteNoteCommandFunction, CanExecuteDeleteNoteCommand);
                }
                return _deleteNoteCommand;
            }
        }
        #endregion

        #region Command Functions
        void OpenNoteCommand(object XMlName)
        {
            NoteEntity n = _noteEntity.Where(x => x.XMLName == XMlName.ToString()).Single();
            openNote(n);
        }
        bool CanExecuteOpenNoteCommand(object temp)
        {
            return true;
        }
        void DeleteNoteCommandFunction(object XMLName)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this note?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NoteEntity temp = _noteEntity.Where(x => x.XMLName == XMLName.ToString()).Single();
                AddEditStickyNotesFacade.deleteNote(temp);
                _noteEntity.Remove(temp);
                refereshNoteEntity();
                CurrentXMLName = XMLName.ToString();
                OnPropertyChanged("CloseStickNoteWindow");
            }
        }
        bool CanExecuteDeleteNoteCommand(object temp)
        {
            return true;
        }
        #endregion

        #region Add/Edit/Open Note New Note
        void AddNote()
        {
            MainMenuViewModel p = MainMenuViewModel.CreateInstance();
            p.AddNewNoteDelegate = AddNewNoteDelegate;
            this.AddEditStickyNotes = new ViewModel.AddEditStickyNotesViewModel(p);
            p = null;
            this.AddEditStickyNotes.RequestInsert += new System.EventHandler(_stickyNotesViewModel_RequestInsert);
            this.AddEditStickyNotes.RequestUpdate += new System.EventHandler(AddEditStickyNotes_RequestUpdate);
            this.AddEditStickyNotes.RequestDelete += new System.EventHandler(AddEditStickyNotes_RequestDelete);
            OnPropertyChanged("AddNewNote");
        }
        public void openNote(object obj)
        {
            NoteEntity noteEntity = (NoteEntity)(obj);
            openNote(noteEntity);
            noteEntity = null;
        }
        void openNote(NoteEntity noteEntity)
        {
            if (noteEntity == null) return;
            MainMenuViewModel p = MainMenuViewModel.CreateInstance();
            p.AddNewNoteDelegate = AddNewNoteDelegate;
            CurrentXMLName = noteEntity.XMLName;
            this.AddEditStickyNotes = new ViewModel.AddEditStickyNotesViewModel(p, noteEntity);
            this.AddEditStickyNotes.RequestInsert += new System.EventHandler(_stickyNotesViewModel_RequestInsert);
            this.AddEditStickyNotes.RequestUpdate += new System.EventHandler(AddEditStickyNotes_RequestUpdate);
            this.AddEditStickyNotes.RequestDelete += new System.EventHandler(AddEditStickyNotes_RequestDelete);
            p = null;
            noteEntity = null;
            OnPropertyChanged("AddNewNote");
        }

        #region Event Handlers
        void _stickyNotesViewModel_RequestInsert(object sender, System.EventArgs e)
        {
            if (_noteEntity == null)
            {
                _noteEntity = new List<NoteEntity>();
            }
            AddEditStickyNotesViewModel temp = (AddEditStickyNotesViewModel)(sender);
            temp._noteEntity.str_TempNoteDescription = formateDescription(temp._noteEntity.str_NoteDescription);
            if (temp != null)
            {
                _noteEntity.Add(temp._noteEntity);
                refereshNoteEntity();
            }
            temp = null;
            sender = null;
        }
        void AddEditStickyNotes_RequestUpdate(object sender, System.EventArgs e)
        {
            if (_noteEntity == null)
            {
                _noteEntity = new List<NoteEntity>();
            }
            AddEditStickyNotesViewModel temp = (AddEditStickyNotesViewModel)(sender);
            if (temp != null)
            {
                _noteEntity.Remove(temp._noteEntity);
                temp._noteEntity.str_TempNoteDescription = formateDescription(temp._noteEntity.str_NoteDescription);
                _noteEntity.Add(temp._noteEntity);
                refereshNoteEntity();
            }
            temp = null;
            sender = null;
        }
        void AddEditStickyNotes_RequestDelete(object sender, System.EventArgs e)
        {
            AddEditStickyNotesViewModel temp = (AddEditStickyNotesViewModel)(sender);
            if (temp != null && temp.XMLName != null && temp.XMLName != "")
            {
                _noteEntity.Remove(_noteEntity.Where(x => x.XMLName == temp.XMLName).Single());
                refereshNoteEntity();
            }
            temp = null;
            sender = null;
        }
        #endregion
        #endregion

        #region Clear Search Text
        private void clearSearch()
        {
            SearchText = "";
            refereshNoteEntity();
        }
        #endregion

        string formateDescription(string description)
        {
            if (description == null) return "";
            if (description.Length <= 0) return "";
            description = description.Replace("\n", "");
            description = description.Replace("\r", "");
            if (description.Length > 100)
            {
                return description.Substring(0, 90) + "...";
            }
            return description;
        }
    }
}
