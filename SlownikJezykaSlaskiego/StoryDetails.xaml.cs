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

            string fullText = story.content;
            List<Word> wordList = story.words;

            List<string> plainTexts = new List<string>();
            for (int i = 0; i < wordList.Count; i++)
            {
                string[] split = fullText.Split(new String[] { "{" + i + "}" }, StringSplitOptions.RemoveEmptyEntries);
                plainTexts.Add(split[0]);
                fullText = split[1];
            }
            plainTexts.Add(fullText);

            Paragraph myParagraph = new Paragraph();
            for (int i = 0; i < plainTexts.Count; i++) {
                Run myRun1 = new Run();
                myRun1.Text = plainTexts[i];
                myParagraph.Inlines.Add(myRun1);

                if (i < wordList.Count)
                {
                    Word aWord = wordList[i];
                    Hyperlink hl = new Hyperlink();
                    hl.Foreground = new SolidColorBrush(Colors.Black);
                    hl.Inlines.Add(aWord.silesian);
                    hl.Click += (sender, ex) =>
                    {
                        NavigationService.Navigate(new Uri("/DictionaryDetails.xaml?msg=" + JsonConvert.SerializeObject(aWord), UriKind.Relative));
                    };
                    myParagraph.Inlines.Add(hl);
                }
            }

            txtDescription.Blocks.Add(myParagraph);


        }

    }
}