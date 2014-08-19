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
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;
using MeditacionDominical.Resources;
using MeditacionDominical.Models;
using Microsoft.Phone.BackgroundAudio;

namespace MeditacionDominical.Views
{
    public partial class Lector : PhoneApplicationPage
    {
        SpeechSynthesizer ss;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                ss = new SpeechSynthesizer();

                //grabarAudio();

                //Find the voice of your choice. In this case french woman
                var voice = (from x in InstalledVoices.All
                             where x.Language == "es-ES" &&
                             x.Gender == VoiceGender.Female
                             select x).FirstOrDefault();

                ss.SetVoice(voice);
                ss.SpeakTextAsync(strTextoALeer);
            }
        }

        //private void grabarAudio()
        //{ 
        //    sfd.Filter = ""wav files (*.wav)|*.wav";
        //    sfd.Title = "Save to a wave file";
        //    sfd.FilterIndex = 2;
        //    sfd.RestoreDirectory = true;

        //    if (ss.ShowDialog() == DialogResult.OK)
        //    {
        //         FileStream fs = new FileStream(sfd.FileName,FileMode.Create,FileAccess.Write);
        //         ss.SetOutputToWaveStream(fs);
        //         ss.Speak(richTextBox1.Text);
        //         fs.Close();
        //    }
        //}

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