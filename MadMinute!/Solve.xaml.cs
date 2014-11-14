using MadMinute_.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MadMinute_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Solve : Page
    {
        //Data passed through from difficulty selection. Contains range of numbers.
        Payload payload = new Payload();

        //Instantiate components
        const int COUNTDOWN = 3;
        const int MINUTE = 60;
        int topInt, bottomInt, product, value, mult, score, rangeLower, rangeUpper, precounter;
        string temp;
        Random rand = new Random();
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        DispatcherTimer oneSecondTimer;
        //

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public Solve()
        {
            //Change states (generated)
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            //Reset & Generate values
            score = 0;
            Top.Text = String.Empty;
            Bottom.Text = String.Empty;
            txtOutput.Text = String.Empty;
            timerBlock.Text = String.Empty;
            ScoreDialogue.Text = "0";
            precounter = COUNTDOWN;

            //Initialize timers with time spans
            oneSecondTimer = new DispatcherTimer();
            oneSecondTimer.Tick += oneSecond_Tick;
            oneSecondTimer.Interval = TimeSpan.FromSeconds(1);

            //Start the oneSecondTimer
            oneSecondTimer.Start();
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            payload = e.Parameter as Payload;
            rangeLower = payload.num1;
            rangeUpper = payload.num2;
            mult = payload.mult;
            //setNewNumbers();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        //Number click events
        private void btnDigit_Click(object sender, RoutedEventArgs e)
        {
            //Clear if clear is pressed. Add numbers otherwise
            if (((Button)sender).Content.Equals("CE"))
                txtOutput.Text = String.Empty;
            else
                txtOutput.Text += (string)((Button)sender).Content;

            //Check if field is blank or too large for possible answer (max possible answer is 10000 ==> 100*100 which is 5 digits long)
            temp = txtOutput.Text;
            if(temp.Length > 5)
                { txtOutput.Text = String.Empty; }
            else
            {
                //If there is something in the field
                if(temp.Length > 0)
                {
                    //Parse the string into int. compare to product.
                    value = int.Parse(txtOutput.Text);
                    if (value == product)
                    {
                        score+=mult;
                        ScoreDialogue.Text = Convert.ToString(score);
                        setNewNumbers();
                        txtOutput.Text = String.Empty;
                    }
                }
            }
        }

        public void setNewNumbers()
        {
            topInt = rand.Next(rangeLower, rangeUpper);
            bottomInt = rand.Next(rangeLower, rangeUpper);
            Top.Text = Convert.ToString(topInt);
            Bottom.Text = Convert.ToString(bottomInt);
            product = topInt * bottomInt;
        }

        //Count from COUNTDOWN to 0. Stop this timer, and start the minuteTimer
        void oneSecond_Tick(object sender, object e)
        {
            if (precounter > 0)
            {
                timerBlock.Text = "" + precounter;
                precounter--;   
            }
            else
            {
                timerBlock.Text = String.Empty;
                setNewNumbers();
                timerBlock.Text = String.Empty;
                oneSecondTimer.Stop();
            }
        }
    }
}
