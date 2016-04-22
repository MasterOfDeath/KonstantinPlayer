using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KonstantinPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["ArbitraryArgName"] != null)
            {
                string fname = Application.Current.Properties["ArbitraryArgName"].ToString();
                mediaElement.Source = new Uri(fname);
            }
        }

        private void mediaElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TogglePlay();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                TogglePlay();
            }
        }

        private void TogglePlay()
        {
            if (this.GetMediaState(mediaElement) == MediaState.Pause)
            {
                mediaElement.LoadedBehavior = MediaState.Manual;
                mediaElement.Play();
            }
            else if (this.GetMediaState(mediaElement) == MediaState.Play)
            {
                mediaElement.LoadedBehavior = MediaState.Manual;
                mediaElement.Pause();
            }
        }

        private MediaState GetMediaState(MediaElement myMedia)
        {
            FieldInfo hlp = typeof(MediaElement).GetField("_helper", BindingFlags.NonPublic | BindingFlags.Instance);
            object helperObject = hlp.GetValue(myMedia);
            FieldInfo stateField = helperObject.GetType().GetField("_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            MediaState state = (MediaState)stateField.GetValue(helperObject);

            return state;
        }

    }
}
