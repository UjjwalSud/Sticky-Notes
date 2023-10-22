using System.Windows;
using MyStickyNotes.Presentation.ViewModel;

namespace MyStickyNotes.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Start Up Function
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow wMainWindow = new UI.MainWindow();
            MainWindowViewModel obj = MainWindowViewModel.CreateInstance();
            App.Current.MainWindow = wMainWindow;
            wMainWindow.DataContext = obj;
            wMainWindow.Show();
        }
        #endregion
    }
}
