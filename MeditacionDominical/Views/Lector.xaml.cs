using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
//using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;
using MeditacionDominical.Resources;
using MeditacionDominical.Models;
//using Microsoft.Phone.BackgroundAudio;

namespace MeditacionDominical.Views
{
    public partial class Lector : PhoneApplicationPage
    {
        //SpeechSynthesizer ss;

        //private DisplayRequest dispRequest = null;

        private Common common = new Common();
        String strTextoALeer;

        public Lector()
        {
            InitializeComponent();
            try
            {
                BuildLocalizedApplicationBar();
                setearTamanioTexto();
                tblTitulo.Text = common.LeerClave("Titulo");
                strTextoALeer = tblNombreAplicacion.Text + "\n" + tblTitulo.Text + "\n" + common.LeerClave("ContenidoPlano");
                wbContenido.NavigateToString(common.LeerClave("Contenido"));
                AppResources.ss.BookmarkReached += new Windows.Foundation.TypedEventHandler<SpeechSynthesizer,SpeechBookmarkReachedEventArgs>(synth_BookmarkReached);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static void synth_BookmarkReached(object sender, SpeechBookmarkReachedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
        } 


        private void setearTamanioTexto()
        {
            double ScreenWidth = Application.Current.Host.Content.ActualWidth;
            double ScreenHeight = Application.Current.Host.Content.ActualHeight;
            scrContenido.Height = ScreenHeight - 193;
            wbContenido.Width = ScreenWidth - 50;
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
            if (strTextoALeer.Trim() != String.Empty)
            {
                AppResources.ss.CancelAll();

                //Find the voice of your choice. In this case french woman
                var voice = (from x in InstalledVoices.All
                             where x.Language == "es-ES" &&
                             x.Gender == VoiceGender.Female
                             select x).FirstOrDefault();

                try
                { 
                    AppResources.ss.SetVoice(voice);
                    //AppResources.ss.SpeakSsmlAsync(strTextoALeer);

                    string ssmlText = "<speak version=\"1.0\" ";
                    ssmlText += "xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"es-ES\">";
                    ssmlText += strTextoALeer;
                    ssmlText += "<mark name=\"fin\"/></speak>";

                    AppResources.ss.SpeakSsmlAsync(ssmlText);

                    // Con el siguiente comando prevenimos que la pantalla del telefono se apague y se deje de leer el texto...
                    PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
                AppResources.ss.CancelAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;

        }
    }
}