﻿using _30JoursDeBD.Common;
using _30JoursDeBD.Common.testmodel;
using _30JoursDeBD.testmodel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace _30JoursDeBD
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class listeAuteur : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private List<Auteur> _listeAuteur = new List<Auteur>();
        public List<Auteur> ListeAuteur { get { return _listeAuteur; } }

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


        public listeAuteur()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        //Gestion des Visuals States en fonction de la taille de l'écran, lors de l'appel de l'event SizeChanged
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 600)
            {
                VisualStateManager.GoToState(this, "NarrowLayout", true);
            }
            else if (e.NewSize.Height > e.NewSize.Width)
            {
                VisualStateManager.GoToState(this, "PortraitLayout", true);
            }
            else if (e.NewSize.Width > 2000)
            {
                VisualStateManager.GoToState(this, "BigDefaultLayout", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "DefaultLayout", true);
            }
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
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            HttpClient client = new HttpClient();
            var jsonString = await client.GetStringAsync(new Uri("http://30joursdebd.com/?json=get_author_index"));
            var httpresponse = JsonConvert.DeserializeObject<AuthorIndex>(jsonString.ToString());
            Auteur  auteur;
            foreach (Author a in httpresponse.authors)
            {
                auteur = new Auteur();
                auteur.Nom = a.name;
                auteur.URL = a.url;
                auteur.Image = "http://30joursdebd.com/30jdbdv3/wp-content/themes/30jdbd/scripts/timthumb.php?src=/30jdbdv3/wp-content/themes/30jdbd/images/auteurs/"+a.name+".jpg&w=130&h=130&zc=1&q=90";
                auteur.Description = a.description;
                _listeAuteur.Add(auteur);
            }
            this.DataContext = this;
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

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
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
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        //Gestion AppBar
        private void AppBar_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void AppBar_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).BorderBrush = new SolidColorBrush(Color.FromArgb(255, 139, 4, 87));
        }

        private void AppBar_Tapped(object sender, TappedRoutedEventArgs e) // Navigation
        {
            string leNom = (sender as Border).Name;
            string[] tabNom = {"Accueil","BD","Albums","BestOf","Auteurs","Participer"};
            int i;
            for ( i=0; i < tabNom.Length; i++)
            {
                if ( leNom.Contains(tabNom[i]))
                    break;
            }
            switch(i)
            {
                case 0:
                    Frame.Navigate(typeof(MainPage));
                    break;
                case 1:
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
                case 5:
                   
                    break;
            }
        }
    }
}