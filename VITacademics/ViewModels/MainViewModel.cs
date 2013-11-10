using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows.Media;
using System.Windows;
using VITacademics.Resources;

namespace VITacademics.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        DataHandler dat = new DataHandler();
        WebClient wClient;
        int status = 0;

        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool isCache;
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        private void loadPage(String url) {
            wClient = new WebClient();
            wClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(loadHTMLCallback);
            wClient.DownloadStringAsync(new Uri(url));
        }

        public void LoadData()
        {
            
            this.Items.Clear();

            if (!isCache)
            {
                //USER WANTS REFRESH LETS DO THIS
                String url = "http://www.vitacademicsrel.appspot.com/captchasub/" + dat.getReg() + "/" + dat.getDob() + "/" + dat.getCap();
                System.Diagnostics.Debug.WriteLine(url);
                loadPage(url);
                this.Items.Add(new ItemViewModel() { prgColor = new SolidColorBrush(Colors.Green), LineOne = "Loading...", LineTwo = "", LineThree = "" });

            }

            else {
                //OFFLINE SHOW SAVED DATA!
                loadSaved();
            }
            
            // Sample data; replace with real data
           
            
            
            
        }
        
        private Color getClr (double per){
            if (per < 80 && per >= 75)
                return Colors.Orange;
            else if (per < 75)
                return Colors.Red;
            else
                return Colors.Green;
        }

        private void loadSaved(){
            this.Items.Clear();
            try
            {
                double per;
                List<Subject> subs = new List<Subject>();
                subs = dat.loadSubjects();
                for (int i = 0; i < subs.Count; i++) {
                    Subject t = subs[i];
                    per = Math.Round(((double)t.attended / (double) t.conducted) * 100, 1);
                    this.Items.Add(new ItemViewModel() { Percentage = t.percentage, prgVal = (int) per, prgColor = new SolidColorBrush(getClr(per)), LineOne = t.title, LineTwo = t.slot, LineThree = t.type});
                }
                this.IsDataLoaded = true;
           }
            catch (Exception e) {
                MessageBox.Show("Error occured while loading data.\nTry refreshing!", "Oops!", MessageBoxButton.OK);
            }
        }

        public void loadHTMLCallback(Object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {

                String res = (string)e.Result;
                
                switch (status)
                {
                        //SUBMIT CAPTCHA CALLBACK
                    case 0:
                        if (res!= null && res.Contains("success"))
                        {
                            //captchasub ok
                            wClient = new WebClient();
                            status++;
                            loadPage("http://www.vitacademicsrel.appspot.com/attj/" + dat.getReg() + "/" + dat.getDob());
                        }
                        else
                            MessageBox.Show("Captcha error ocuured.\nIf error persists check your credentials.", "Error!", MessageBoxButton.OK);
                        break;

                        //DOWNLOAD ATTENDANCE CALLBACK
                    case 1:
                        if (res != null && res.Contains("valid%"))
                        {
                            //SAVE ATTENDANCE
                            dat.saveAttendance(res);
                            //CALL MARKS
                            status++;
                            loadPage("http://www.google.com");
                            loadSaved();
                        }
                        else
                            MessageBox.Show("Could not load marks.\nIf error persists check your network.", "Error!", MessageBoxButton.OK);
                        break;
                    
                    case 2:
                        //SAVE MARKS
                        break;



                }
            }
            catch (Exception ex) { Console.Write(ex.ToString()); MessageBox.Show("Error occured while loading attendance"); }
           
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}