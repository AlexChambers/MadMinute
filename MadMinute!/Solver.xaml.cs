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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace MadMinute_
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Solver : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        //Instantiate components
        int topInt, bottomInt, product, value, mult, score, rangeLower, rangeUpper;
        string temp;
        Payload payload = new Payload();
        Random rand = new Random();

        //Number click events
        private void btnDigit_Click(object sender, RoutedEventArgs e)
        {
            //Clear if clear is pressed. Add numbers otherwise
            if (((Button)sender).Content.Equals("CLEAR"))
                txtOutput.Text = String.Empty;
            else
                txtOutput.Text += (string)((Button)sender).Content;

            //Check if field is blank or too large for possible answer (max possible answer is 10000 ==> 100*100 which is 5 digits long)
            temp = txtOutput.Text;
            if (temp.Length > 5)
            { txtOutput.Text = String.Empty; }
            else
            {
                //If there is something in the field
                if (temp.Length > 0)
                {
                    //Parse the string into int. compare to product.
                    value = int.Parse(txtOutput.Text);
                    if (value == product)
                    {
                        score += mult;
                        ScoreDialogue.Text = "Score: " + score;
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
            Top.Text = "" + topInt;
            Bottom.Text = "" + bottomInt;
            product = topInt * bottomInt;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            payload = e.Parameter as Payload;
            rangeLower = payload.num1;
            rangeUpper = payload.num2;
            mult = payload.mult;
            setNewNumbers();
        }

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


        public Solver()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            setNewNumbers();
            score = 0;
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
