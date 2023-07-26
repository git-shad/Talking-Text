using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Talking_Text
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private Brush HexColorBrush(string hexcolor) {
            var color = (Color)ColorConverter.ConvertFromString(hexcolor);
            return new SolidColorBrush(color);
        }

        private void ActiveButtonColor(Rectangle b,TextBlock text) {
            b.Fill = HexColorBrush("#FF334D33");
            b.Stroke = HexColorBrush("#FFCBFFC7");
            text.Foreground = HexColorBrush("#FFCBFFC7");
        }

        private void NotActiveButtonColor(Rectangle b,TextBlock text) {
            b.Fill = HexColorBrush("#FFCBFFC7");
            b.Stroke = HexColorBrush("#FF334D33");
            text.Foreground = HexColorBrush("#FF334D33");
        }

        private void FocusActiveButtonColor(Rectangle b, TextBlock text)
        {
            b.Fill = HexColorBrush("#FF74A874");
            b.Stroke = HexColorBrush("#FFDAFFD8");
            text.Foreground = HexColorBrush("#FFDAFFD8");
        }

        bool boyswitchactive = false;
        private void BoyButton(object sender, MouseButtonEventArgs e)
        {
            if (!boyswitchactive)
            {
                ActiveButtonColor(boy_b,boy_text);
                boyswitchactive = true; 

                //
                NotActiveButtonColor(girl_b, girl_text);
                girlswitchactive = false;
            }
            
            using(var synthesizer = new System.Speech.Synthesis.SpeechSynthesizer()){
                synthesizer.SelectVoiceByHints(System.Speech.Synthesis.VoiceGender.Male);
                synthesizer.Speak(texttospeak.Text);
                synthesizer.Dispose();
            }
        }

        private void FocusBoyEnableButton(object sender, MouseEventArgs e)
        {
            if(!boyswitchactive){
                FocusActiveButtonColor(boy_b, boy_text);
            }
        }

        private void FocusBoyDisableButton(object sender, MouseEventArgs e)
        {
            if(!boyswitchactive){
                NotActiveButtonColor(boy_b, boy_text);
            }
        }

        //
        bool girlswitchactive = true;//true, because this is defualt
        private void GirlButton(object sender, MouseButtonEventArgs e)
        {
            if (!girlswitchactive)
            {
                ActiveButtonColor(girl_b, girl_text);
                girlswitchactive = true;

                //
                NotActiveButtonColor(boy_b, boy_text);
                boyswitchactive = false;
            }
            
            using (var synthesizer = new System.Speech.Synthesis.SpeechSynthesizer())
            {
                synthesizer.SelectVoiceByHints(System.Speech.Synthesis.VoiceGender.Female);
                synthesizer.Speak(texttospeak.Text);
                synthesizer.Dispose();
            }
        }

        private void FocusGirlEnableButton(object sender, MouseEventArgs e)
        {
            if(!girlswitchactive){
                FocusActiveButtonColor(girl_b,girl_text);
            }
        }

        private void FocusGirlDisableButton(object sender, MouseEventArgs e)
        {
            if(!girlswitchactive){
                NotActiveButtonColor(girl_b,girl_text);
            }
        }
        
        private void SaveButton(object sender, MouseButtonEventArgs e)
        {
            ActiveButtonColor(save_b, save_text);
            //
            var savefile = new System.Windows.Forms.SaveFileDialog();
            savefile.FileName = "tacking text.mp3";
            savefile.DefaultExt = ".mp3";
            savefile.Filter = "MP3 File (*.mp3)|*.mp3|All Files (*.*)|*.*";
            var result = savefile.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK){
                using(var synthesizer = new System.Speech.Synthesis.SpeechSynthesizer()){
                    synthesizer.SetOutputToWaveFile(savefile.FileName);
                    if(boyswitchactive){
                        synthesizer.SelectVoiceByHints(System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Child);
                    }
                    else if (girlswitchactive)
                    {
                        synthesizer.SelectVoiceByHints(System.Speech.Synthesis.VoiceGender.Female);
                    }
                    synthesizer.Speak(texttospeak.Text);
                }
            }
            //
            NotActiveButtonColor(save_b, save_text);
        }

        private void FocusSaveEnableButton(object sender, MouseEventArgs e)
        {
            FocusActiveButtonColor(save_b,save_text);
        }

        private void FocusSaveDisableButton(object sender, MouseEventArgs e)
        {
            NotActiveButtonColor(save_b,save_text);
        }

        private void ExitButton(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Environment.Exit(Environment.ExitCode);
        }

        private void DragAni(object sender, MouseButtonEventArgs e)
        {
            this.OnMouseLeftButtonDown(e);
            this.DragMove();


        }

        





    }
}
