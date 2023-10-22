using System.Windows;
using MyStickyNotes.Presentation.ViewModel;
using System.Windows.Input;
namespace MyStickyNotes.UI
{
    /// <summary>
    /// Interaction logic for StickyNotesLauncher.xaml
    /// </summary>
    public partial class StickyNotesLauncher : Window
    {
        #region Fields
        AddEditStickyNotesViewModel _addEditStickyNotes;
        private FrameworkElement _title;
        #endregion

        #region Constructors
        public StickyNotesLauncher()
        {
            InitializeComponent();
        }
        #endregion

        #region Data Context Changed Event
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null) return;
            _addEditStickyNotes = (AddEditStickyNotesViewModel)this.DataContext;
            _addEditStickyNotes.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_viewModel_PropertyChanged);
        }
        #endregion

        #region Property Changed Event
        void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CloseNote":
                    this.Close();
                    this.DataContext = null;
                    break;
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = false;
            this._title = PART_Title;// (FrameworkElement)this.Template.FindName("PART_Title", this);
            if (null != this._title)
            {
                this._title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);
            }
        }
        private void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
