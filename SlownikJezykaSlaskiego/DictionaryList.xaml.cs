using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using System.IO;

namespace SlownikJezykaSlaskiego
{
    public partial class DictionaryList : PhoneApplicationPage
    {
        private List<Word> mAllWords;
		
        public DictionaryList()
        {
            InitializeComponent();
			

            loadJson();
        }
		
		private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ 
            Word word = (sender as ListBox).SelectedItem as Word;
            NavigationService.Navigate(new Uri("/DictionaryDetails.xaml?msg=" + JsonConvert.SerializeObject(word), UriKind.Relative));
		}

        private async void loadJson()
        {

            var str = Application.GetResourceStream(new Uri("json.json", UriKind.Relative));
            StreamReader streamReader = new StreamReader(str.Stream);
            string myString = streamReader.ReadToEnd();

            var results = JsonConvert.DeserializeObject<Results>(myString);

            mAllWords = results.results;
            filterByConstraint("");
            
        }

        private void filterByConstraint(string constraint)
        {
            List<Word> words = new List<Word>();

            foreach (Word word in mAllWords) 
            {
                if (word.silesian.Contains(constraint)) 
                {
                    words.Add(word);
                }
            }

            listBox.ItemsSource = words;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterByConstraint(TBInputFilter.Text);
        }
      
    }
}