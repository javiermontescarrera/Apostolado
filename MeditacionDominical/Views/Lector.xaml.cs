using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;
using MeditacionDominical.Resources;
using MeditacionDominical.Models;

namespace MeditacionDominical.Views
{
    public partial class Lector : PhoneApplicationPage
    {
        SpeechSynthesizer ss;

        private Common common = new Common();

        public Lector()
        {
            InitializeComponent();
            try
            {
                BuildLocalizedApplicationBar();
                tblContenido.Html = common.LeerClave("feedItem");
                // PronunciarTexto();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarIconButton appBarIconButton = new ApplicationBarIconButton(new Uri("/Assets/microphone.png", UriKind.Relative));
            appBarIconButton.Text = AppResources.AppBarMenuItemLeerText;
            appBarIconButton.Click += appPronunciarButton_Click;
            ApplicationBar.Buttons.Add(appBarIconButton);

            ApplicationBarIconButton appBarIconButtonStop = new ApplicationBarIconButton(new Uri("/Assets/cancel.png", UriKind.Relative));
            appBarIconButtonStop.Text = AppResources.AppBarMenuItemDetenerText;
            appBarIconButtonStop.Click += appDetenetButton_Click;
            ApplicationBar.Buttons.Add(appBarIconButtonStop);
        }

        private async void PronunciarTexto()
        {
            string texto = tblContenido.Html;
            RssTextTrimmer Trimmer = new RssTextTrimmer();
            texto = Trimmer.Convert(texto, null, null, null).ToString();
            if (texto.Trim() != String.Empty)
            {
                ss = new SpeechSynthesizer();

                //Find the voice of your choice. In this case french woman
                var voice = (from x in InstalledVoices.All
                             where x.Language == "es-ES" &&
                             x.Gender == VoiceGender.Female
                             select x).FirstOrDefault();

                ss.SetVoice(voice);
                ss.SpeakTextAsync(texto);
            }
        }

        private void appPronunciarButton_Click(object sender, EventArgs e)
        {
            try
            {
                PronunciarTexto();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void appDetenetButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ss != null)
                {
                    ss.CancelAll();
                    ss = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}