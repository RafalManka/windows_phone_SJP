using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Phone.Shell;

namespace SJSScheduledTaskAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            //Debugger.
                //WriteLine("Hello world");
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {

            if (task is PeriodicTask) 
            {

                var str = Application.GetResourceStream(new Uri("json.json", UriKind.Relative));
                StreamReader streamReader = new StreamReader(str.Stream);
                string myString = streamReader.ReadToEnd();

                var results = JsonConvert.DeserializeObject<Results>(myString);

                List<Word> allWords = results.results;

                Random random = new Random();
                Word word = allWords[random.Next(0, (allWords.Count - 1))];
                //Word word = allWords[(allWords.Count - 1)];

                IEnumerable<ShellTile> PinnedTile = ShellTile.ActiveTiles;
                foreach (var item in PinnedTile)
                {
                    FlipTileData UpdatedTileData = new FlipTileData
                    {
                        Title = "Słownik Śląski",
                        BackTitle = "Słówko dnia",
                        BackContent = word.silesian + "-\n" + word.polish,
                        WideBackContent = word.silesian + "-\n" + word.polish
                    };
                    item.Update(UpdatedTileData);

                }
            
            }
            
            NotifyComplete();
            
        }

    }
}