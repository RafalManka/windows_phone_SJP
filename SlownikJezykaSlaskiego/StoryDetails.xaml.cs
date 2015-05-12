using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System.Windows.Documents;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace SlownikJezykaSlaskiego
{
    public partial class StoryDetails : PhoneApplicationPage
    {
        public StoryDetails()
        {
            InitializeComponent();
        }

   
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("msg", out msg)) ;
            Story story = JsonConvert.DeserializeObject<Story>(msg);

            if (story == null)
            {
                NavigationService.GoBack();
                return;
            }
            TxtTitle.Text = story.title;
            txtAuthor.Text = story.author;

            if (story.image != null)
            {
                if (story.image.is_dark) {
                    TxtTitle.Foreground = new SolidColorBrush(Colors.White);
                    txtAuthor.Foreground = new SolidColorBrush(Colors.White);
                }
            
                Uri myUri = new Uri(story.image.url, UriKind.Absolute);
                BitmapImage bmi = new BitmapImage();
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = myUri;
                imgHeader.Source = bmi;
            }
   


            string fullText = story.content;
            List<Word> wordList = story.words;

            List<string> plainTexts = new List<string>();
            for (int i = 0; i < wordList.Count; i++)
            {
                string[] split = fullText.Split(new String[] { "{" + i + "}" }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 1) {
                    continue;
                }
                plainTexts.Add(split[0]);
                fullText = split[1];
            }
            plainTexts.Add(fullText);

            Paragraph paragraph = new Paragraph();
            int j = 0;
            for (int i = 0; i < plainTexts.Count; i++) {
                Run run = new Run();
                run.Text = plainTexts[i];
                paragraph.Inlines.Add(run);

                if (j < wordList.Count)
                {
                    Word word = wordList[j];
                    j++;
                    if (word.display == null) {
                        continue;
                    }
                    Hyperlink hyperlink = new Hyperlink();
                    hyperlink.Foreground = new SolidColorBrush(Colors.Black);
                    hyperlink.Inlines.Add(word.display);
                    hyperlink.Click += (sender, ex) =>
                    {
                        NavigationService.Navigate(new Uri("/DictionaryDetails.xaml?msg=" + JsonConvert.SerializeObject(word), UriKind.Relative));
                    };
                    paragraph.Inlines.Add(hyperlink);
                }
            }

            txtDescription.Blocks.Add(paragraph);


        }

    }
}