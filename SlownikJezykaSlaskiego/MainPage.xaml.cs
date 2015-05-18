#define DEBUG_AGENT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SlownikJezykaSlaskiego.Resources;
using Microsoft.Phone.Notification;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Phone.Scheduler;

namespace SlownikJezykaSlaskiego
{
    public partial class MainPage : PhoneApplicationPage
    {         
        // Constructor
        public MainPage()
        {  
            InitializeComponent();
            //loadJson();
            
        }

        PeriodicTask periodicTask;

        string periodicTaskName = "ScheduledTaskAgent";

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            StartPeriodicAgent();

        }

        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
            }
        }

        private void StartPeriodicAgent()
        {


            // Obtain a reference to the period task, if one exists
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule
            if (periodicTask != null)
            {
                RemoveAgent(periodicTaskName);
            }

            periodicTask = new PeriodicTask(periodicTaskName);

            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the device.
            periodicTask.Description = "Słownik Języka Śląskiego";

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(periodicTask);                 
            }
            catch (InvalidOperationException exception)
            {
            }
            catch (SchedulerServiceException)
            {
            }
        }

        private async void loadJson()
        {
            var str = Application.GetResourceStream(new Uri("json.json", UriKind.Relative));
            StreamReader streamReader = new StreamReader(str.Stream);
            string myString = streamReader.ReadToEnd();

            var results = JsonConvert.DeserializeObject<Results>(myString);

            List<Word> AllWords = results.results;

            Word word = AllWords[5];

            ShellTile PinnedTile = ShellTile.ActiveTiles.First();
            FlipTileData UpdatedTileData = new FlipTileData
            {
                Title = "Słownik Śląski",
                BackTitle = "Słówko dnia",
                BackContent = word.silesian+"-\n"+word.polish,
                WideBackContent = word.silesian + "-\n" + word.polish
            };
            PinnedTile.Update(UpdatedTileData);

        }

         
        private void BtnDictionary_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/DictionaryList.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/StoryList.xaml", UriKind.Relative));
        }

    }
}