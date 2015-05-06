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
            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("msg", out msg)) ;
            Story story = JsonConvert.DeserializeObject<Story>(msg);

            if (story == null)
            {
                NavigationService.GoBack();
                return;
            }
            TxtTitle.Text = story.title;
            TxtDescription.Text = story.content;

        }

    }
}