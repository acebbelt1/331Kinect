using System;
using System.Collections.Generic;
using System.IO;
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
using System.Speech.Recognition;
using Microsoft.Kinect;


namespace sound_pad
{
    /// <summary>
    /// Interaction logic for KinectApp.xaml
    /// </summary>
    public partial class KinectApp : Window
    {
        public KinectApp()
        {
            InitializeComponent();
            //What should happen when the applicatioj is loaded
            Loaded += MainWindow_Loaded;
            //what should happen when the application is unloaded
            Unloaded += MainWindow_Unloaded;
            //What should happen when the application is closing
            Closing += MainWindow_Closing;      
        }



        /// <summary>
        /// Name of speech grammar corresponding to file. Note that the name must be the same, it is case sensative
        /// </summary>
        //For the Play Functionality 
        private const string playrule = "playrule";
        //For the Stop Functionality 
        private const string Stoprule = "Stoprule";
        //For the Pause funtionality
        private const string Pauserule = "Pauserule";
        //for Volume down funtionality
        private const string Volumedownrule = "Volumedown";
        //for Volume up functionality
        private const string Volumeuprule = "Volumeup";
        
        /// <summary>
        /// Speech recognizer used to detect voice commands issued by application users.
        /// </summary>
        private SpeechRecognizer speechRecognizer;
 
        /// <summary>
        /// Speech grammar used during Application.
        /// </summary> 
        private Grammar PlayGrammar; 
        private Grammar StopGrammar;
        private Grammar PauseGrammar;
        private Grammar VolumeupGrammar;
        private Grammar VolumedownGrammar;



        //Stop the Sensor when the application is being closed
        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sensor.Stop(); 
        }
        //Stop the Sensor when the application is being closed
        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            sensor.Stop();         
        }

        //get the First Sensor
        KinectSensor sensor = KinectSensor.KinectSensors[0];

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


            //Check if the Sensor is Connected
            if (sensor.Status == KinectStatus.Connected)
            {
                //Start the Sensor
                sensor.Start();

                // Create and configure speech grammars and recognizer  
                this.PlayGrammar = CreateGrammar(playrule);
                this.StopGrammar = CreateGrammar(Stoprule);
                this.PauseGrammar = CreateGrammar(Pauserule);
                this.VolumedownGrammar = CreateGrammar(Volumedownrule);
                this.VolumeupGrammar = CreateGrammar(Volumeuprule);

                //recognize the speech
                this.speechRecognizer = SpeechRecognizer.Create(new[] { PlayGrammar, StopGrammar, PauseGrammar, VolumeupGrammar, VolumedownGrammar });

                if (null != speechRecognizer)
                {
                    this.speechRecognizer.SpeechRecognized += SpeechRecognized;

                    this.speechRecognizer.Start(sensor.AudioSource);
                }
            }
        }

        private void SpeechRecognized(object sender, VoiceCommandsInKinect.SpeechRecognizerEventArgs e)
        { 
            //Play the Video
            const string Play = "PLAY";
            //Stop the Video 
            const string StopCommand  = "STOP";
            //Pause
            const string PauseCommand = "PAUSE";
            //Volume Down
            const string VolumedownCommand = "DOWN";
            //Volume Up
            const string VolumeupCommand = "UP";


            if (null == e.SemanticValue)
            {
                return;
            }

            // Handle game mode control commands
            switch (e.SemanticValue)
            {
                 
                case Play:
                    PlayVideo();
                    return;

                case StopCommand:
                    SongPlayer.Stop();
                    return;

                case PauseCommand:
                    SongPlayer.Pause();
                    return;

                case VolumedownCommand:

                    SongPlayer.Volume = 0;
                    return;


                case VolumeupCommand:
                    SongPlayer.Volume = 1;
                    return;
            } 

            // We only handle speech commands with an associated sound source angle, so we can find the
            // associated player
            if (!e.SourceAngle.HasValue)
            {
                return;
            } 
        }
          
 
        /// <summary>
        /// Create a grammar from grammar definition XML file.
        /// </summary>
        /// <param name="ruleName">
        /// Rule corresponding to grammar we want to use.
        /// </param>Tha
        /// <returns>
        /// New grammar object corresponding to specified rule.
        /// </returns>
        private Grammar CreateGrammar(string ruleName)
        {
            Grammar grammar;

            using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.SpeechGrammar)))  //Access a Gramar File
            {
                grammar = new Grammar(memoryStream, ruleName);
            }

            return grammar;
        }
        //Function to Play a Video
        private void  PlayVideo()
        {
            SongPlayer.Source = new Uri(@"C:\Users\Brandon\Downloads\jumparound.mp3", UriKind.Absolute);
            SongPlayer.LoadedBehavior = MediaState.Manual;
            SongPlayer.Play();
        }







        static WaveGesture _gesture = new WaveGesture();

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        } //Closing Window Loaded


        void Sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    Skeleton[] skeletons = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(skeletons);

                    if (skeletons.Length > 0)
                    {
                        var user = skeletons.Where(
                                   u => u.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();

                        if (user != null)
                        {
                            Console.WriteLine("User Found!"); //Debugging information 
                            JointCollection jointCollection = user.Joints; //Debugging information 
                            Console.WriteLine(jointCollection[JointType.ElbowRight].TrackingState.ToString()); //Debugging
                            Console.WriteLine(jointCollection[JointType.HandRight].TrackingState.ToString()); //Debugging
                            Canvas.SetLeft(ellipiseElbowRight, jointCollection[JointType.ElbowRight].Position.X * 300); //Debugging
                            Canvas.SetTop(ellipiseElbowRight, jointCollection[JointType.ElbowRight].Position.Y * -300); //Debugging
                            Canvas.SetLeft(ellipiseHandRight, jointCollection[JointType.HandRight].Position.X * 300); //Debugging
                            Canvas.SetTop(ellipiseHandRight, jointCollection[JointType.HandRight].Position.Y * -300); //Debugging
                            _gesture.Update(user);
                        }
                    }
                }
            }
        } //Closing Sensor_SkeletonFrameReady method

        void Gesture_GestureRecognized(object sender, EventArgs e)
        {
            var textBoxNew = new TextBox();
            textBoxNew.Text = "You just waved!";
            myCanvas.Children.Add(textBoxNew);
            Console.WriteLine("You just waved!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sensor = KinectSensor.KinectSensors.Where(
                       s => s.Status == KinectStatus.Connected).FirstOrDefault();

            if (sensor != null)
            {
                LBLStatus.Content = "Kinect Status: Found";
                sensor.SkeletonStream.Enable();
                sensor.SkeletonFrameReady += Sensor_SkeletonFrameReady;
                _gesture.GestureRecognized += Gesture_GestureRecognized;
                sensor.Start();
            }

            else
                LBLStatus.Content = "Kinect Status: Not Found";
        }
    }
}
