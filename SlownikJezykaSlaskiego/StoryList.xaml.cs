using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Phone.Tasks;

namespace SlownikJezykaSlaskiego
{
    public partial class RecipesList : PhoneApplicationPage
    {
        public RecipesList()
        {
            InitializeComponent();
            loadJson();
        }

        private async void loadJson()
        {

            var str = Application.GetResourceStream(new Uri("stories.json", UriKind.Relative));
            StreamReader streamReader = new StreamReader(str.Stream);
            string myString = streamReader.ReadToEnd();

            StoryList stories = JsonConvert.DeserializeObject<StoryList>(myString);

            listBox.ItemsSource = stories.stories;

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Story story = (sender as ListBox).SelectedItem as Story;
            NavigationService.Navigate(new Uri("/StoryDetails.xaml?msg=" + JsonConvert.SerializeObject(story), UriKind.Relative));
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri("http://kopruch.blazejczak.eu/", UriKind.Absolute);
            webBrowserTask.Show();
        }

       
    }
}