using System.Windows;
using MyStickyNotes.Presentation.ViewModel;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Controls;

namespace MyStickyNotes.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        MainWindowViewModel _viewModel;
        private FrameworkElement _title;
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        #endregion

        #region Window Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiseNotifyIcon();
            #region Move Window
            this._title = PART_Title;
            this._title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);
            #endregion
            _viewModel = (MyStickyNotes.Presentation.ViewModel.MainWindowViewModel)DataContext;
            _viewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_viewModel_PropertyChanged);
        }
        #endregion

        #region Move Window Event Handler
        private void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region Initialise Notify Icon
        public void InitialiseNotifyIcon()
        {
            notifyIcon.ShowBalloonTip(1000, "Welcome", "Welcome to Quick Notes", CodeLibrary.Controls.NotifyBalloonIcon.Info);
        }
        #endregion

        #region Main Window Property Changed Event
        void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "AddNewNote":
                    if (!findWindow(_viewModel.CurrentXMLName))
                    {
                        StickyNotesLauncher wStickyNotesLauncher = new StickyNotesLauncher();
                        wStickyNotesLauncher.DataContext = _viewModel.AddEditStickyNotes;
                        wStickyNotesLauncher.Show();
                        wStickyNotesLauncher = null;
                    }
                    _viewModel.CurrentXMLName = "";
                    break;
                case "CloseStickNoteWindow"://When user delete the record from Grid
                    foreach (Window w in System.Windows.Application.Current.Windows)
                    {
                        TextBlock txtBlockxmlName = (TextBlock)w.FindName("txt_XMLName");
                        if (txtBlockxmlName != null)
                        {
                            if (txtBlockxmlName.Text == _viewModel.CurrentXMLName)
                            {
                                w.Close();
                                w.DataContext = null;
                                return;
                            }
                        }
                        txtBlockxmlName = null;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        bool findWindow(string xmlName)
        {
            foreach (Window w in System.Windows.Application.Current.Windows)
            {
                TextBlock txtBlockxmlName = (TextBlock)w.FindName("txt_XMLName");
                if (txtBlockxmlName != null)
                {
                    if (txtBlockxmlName.Text == xmlName)
                    {
                        w.Focus();
                        return true;
                    }
                }
                txtBlockxmlName = null;
            }
            return false;
        }

        #region Menu Clicks
        private void AddNewNote_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MainMenuViewModel.AddNewNoteDelegate();
        }
        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            notifyIcon.Dispose();
            notifyIcon = null;
            System.Windows.Application.Current.Shutdown();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void OpenGrid_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Normal;
            this.ShowInTaskbar = true;
        }
        #endregion

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            if (WindowState != WindowState.Minimized) return;
            this.ShowInTaskbar = false;
            if (notifyIcon != null)
                notifyIcon.ShowBalloonTip(1000, "Still Running", "My Notes is still running", CodeLibrary.Controls.NotifyBalloonIcon.Info);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region Disposing Notification Icon
            notifyIcon.Dispose();
            notifyIcon = null;
            #endregion

            #region Disposing All Opn Windows
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                //foreach (Window window in System.Windows.Application.Current.Windows)
            }
            #endregion
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                _viewModel.openNote(((TextBlock)sender).DataContext);
            }
            sender = null;
            e = null;
        }
    }
}
