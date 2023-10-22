using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyStickyNotes.Presentation.ViewModel;
namespace MyStickyNotes.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        MainWindowViewModel _viewModel;
        bool isMinimizeMessageDisplayed = false;
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
            _viewModel = (MyStickyNotes.Presentation.ViewModel.MainWindowViewModel)DataContext;
            _viewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_viewModel_PropertyChanged);
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
                    else
                    {
                        _viewModel.AddEditStickyNotes = null;
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
            minimizeEvent();
        }

        private void minimizeEvent()
        {
            if (WindowState != WindowState.Minimized) return;
            if (!isMinimizeMessageDisplayed)
            {
                notifyIcon.ShowBalloonTip(1000, "Still Running", "My Notes is still running", CodeLibrary.Controls.NotifyBalloonIcon.Info);
                isMinimizeMessageDisplayed = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = WindowState.Minimized;
            minimizeEvent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void lstMyNotes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.openNote(lstMyNotes.SelectedItem);
        }

        #region General Window Behavior

        #region Methods

        private void MaximizeOrNormalizeWindow()
        {
            if (this.WindowState != WindowState.Maximized)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void AdaptWindowHeight(double heightChange)
        {
            if (Height + heightChange > MinHeight)
                Height += heightChange;
            else
                Height = MinHeight;
        }

        private void AdaptWindowWidth(double widthChange)
        {
            if (Width + widthChange > MinWidth)
                Width += widthChange;
            else
                Width = MinWidth;
        }

        private void AdaptWindowTop(double topChange)
        {
            Top += topChange;
        }

        private void AdaptWindowLeft(double leftChange)
        {
            Left += leftChange;
        }

        #endregion

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            MaximizeOrNormalizeWindow();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BorderWindowTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                MaximizeOrNormalizeWindow();
            else
                DragMove();
        }

        private void ThumbTopLeft_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            AdaptWindowTop(e.VerticalChange);
            AdaptWindowLeft(e.HorizontalChange);
            AdaptWindowHeight(-e.VerticalChange);
            AdaptWindowWidth(-e.HorizontalChange);
        }

        private void ThumbTop_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            AdaptWindowTop(e.VerticalChange);
            AdaptWindowHeight(-e.VerticalChange);
        }

        private void ThumbTopRight_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            AdaptWindowTop(e.VerticalChange);
            AdaptWindowHeight(-e.VerticalChange);
            AdaptWindowWidth(e.HorizontalChange);
        }

        private void ThumbRight_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            AdaptWindowWidth(e.HorizontalChange);
        }

        private void ThumbBottomRight_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            AdaptWindowHeight(e.VerticalChange);
            AdaptWindowWidth(e.HorizontalChange);
        }

        private void ThumbBottom_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            AdaptWindowHeight(e.VerticalChange);
        }

        private void ThumbBottomLeft_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            AdaptWindowLeft(e.HorizontalChange);
            AdaptWindowHeight(e.VerticalChange);
            AdaptWindowWidth(-e.HorizontalChange);
        }

        private void ThumbLeft_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            AdaptWindowLeft(e.HorizontalChange);
            AdaptWindowWidth(-e.HorizontalChange);
        }

        #endregion
    }
}
//http://www.codeproject.com/Articles/36788/WPF-XAML-NotifyIcon-and-Taskbar-System-Tray-Popup
//http://channel9.msdn.com/coding4fun/blog/Adding-some-spark-to-your-next-WPF-project-with-WPFSpark
//http://www.codeproject.com/Articles/30727/XPlorerBar-A-WPF-Windows-XP-Style-Explorer-Bar-Con


//C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\VS2010ImageLibrary\1033


//http://www.wpf-tutorial.com/common-interface-controls/contextmenu/