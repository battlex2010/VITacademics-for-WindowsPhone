using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows.Media;
using System.Windows;
using VITacademics.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VITacademics.TimeTable;

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
            this.tt_Items = new ObservableCollection<TTSignals>();

        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        public ObservableCollection<TTSignals> tt_Items { get; private set; }

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

        DateTime startTime;
        public void LoadData()
        {
            startTime = DateTime.Now;
            this.Items.Clear();
            this.tt_Items.Clear();

            if (!isCache)
            {
                //USER WANTS REFRESH LETS DO THIS
                string url;
                if (dat.isVellore())
                    url = "http://www.vitacademicsrel.appspot.com/captchasub/" + dat.getReg() + "/" + dat.getDob() + "/" + dat.getCap();
                else
                    url = "http://www.vitacademicsrelc.appspot.com/captchasub/" + dat.getReg() + "/" + dat.getDob() + "/" + dat.getCap();
                loadPage(url);
                this.Items.Add(new ItemViewModel() { prgColor = new SolidColorBrush(Colors.Green), Title = "Loading...", Slot = "", Type = "" });
                this.tt_Items.Add(new TTSignals() { TT_Title = "Loading..."});
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
        private Color getClr2(double per)
        {
            if (per < 80 && per >= 75)
                return Colors.Orange;
            else if (per < 75)
                return Colors.Red;
            else
                return Colors.White;
        }


        private void loadStatus(Object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                String res = (string)e.Result;
                if (res != null)
                {
                    JsonTextReader reader = new JsonTextReader(new System.IO.StringReader(res));
                    JObject j = JObject.Load(reader);
                    int cur = ((int)j["msg_no"]);
                    if (cur != dat.getStatusNum())
                    {
                        MessageBox.Show((string)j["msg_content"], (string)j["msg_title"], MessageBoxButton.OK);
                        dat.saveStatus(res);
                    }
                }
            }
            catch (Exception) { }
        }

        private void loadSaved(){
            this.Items.Clear();
            
            try
            {
                WebClient cClient = new WebClient();
                cClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(loadStatus);
                cClient.DownloadStringAsync(new Uri("http://www.vitacademicsrel.appspot.com/status"));

                double per;
                List<Subject> subs = new List<Subject>();
                subs = dat.loadSubjects();

                //Load saved attendance
                for (int i = 0; i < subs.Count; i++) {
                    Subject t = subs[i];
                    per = Math.Round(((double)t.attended / (double) t.conducted) * 100, 1);
                    this.Items.Add(new ItemViewModel() {UID = t.classnbr, Percentage = t.percentage, prgVal = (int) per, prgColor = new SolidColorBrush(getClr(per)), Title = t.title, Slot = t.slot, Type = t.type});
                    
                }

                //Load today's time table
                loadTT();
                
                this.IsDataLoaded = true;
                GoogleAnalytics.EasyTracker.GetTracker().SendTiming(DateTime.Now.Subtract(startTime), "Refresh", "Load_UI", "Displayed_UI");
           }
            catch (Exception) {
                this.IsDataLoaded = true;
                MessageBox.Show("Error occured while loading data.\nTry refreshing!", "Oops!", MessageBoxButton.OK);
            }
        }

        public void loadTT()
        {
            this.tt_Items.Clear();

            TimeTable.TimeTable t = new TimeTable.TimeTable();

            //No Classes today!
            if (t.today().Count == 0)
            {

                this.tt_Items.Add(new TTSignals() { TT_Title = "No classes!" });
            }

            //Have classes :)
            else
            {
                
                foreach (Slot s in t.today())
                {
                    Subject sub = dat.getSubject(s.clsnbr);
                    double per = Math.Round ( (   (double)sub.attended / (double)sub.conducted) * 100, 1);
                    double pre = Math.Round ( ( ( (double)sub.attended + 1.0) / ((double)sub.conducted + 1.0)) * 100, 0);
                    double abs = Math.Round ( ( ( (double)sub.attended) / ((double)sub.conducted + 1.0)) * 100, 0);
                    
                    if(DateTime.Now > s.frm_time && DateTime.Now < s.to_time)
                        this.tt_Items.Add(new TTSignals() {Title_Color = new SolidColorBrush(Colors.Red), Att_Color = new SolidColorBrush(getClr2(per)), Pre_Color = new SolidColorBrush(getClr2(pre)), Abs_Color = new SolidColorBrush(getClr2(abs)), TT_Pre = "Go: " + pre.ToString() + "%", TT_Abs = "Miss: " + abs.ToString() + "%", TT_Title = sub.title + " (Now)", TT_Slot = s.slt.ToUpper(), TT_Att = sub.percentage, TT_Time = s.frm_time.ToString("t") + " - " + s.to_time.ToString("t") });
                    else
                        this.tt_Items.Add(new TTSignals() {Title_Color = new SolidColorBrush(Colors.White), Att_Color = new SolidColorBrush(getClr2(per)),  Pre_Color = new SolidColorBrush(getClr2(pre)), Abs_Color = new SolidColorBrush(getClr2(abs)), TT_Pre = "Go: " + pre.ToString() + "%", TT_Abs = "Miss: " + abs.ToString() + "%",  TT_Title = sub.title, TT_Slot = s.slt.ToUpper(), TT_Att = sub.percentage, TT_Time = s.frm_time.ToString("t") + " - " + s.to_time.ToString("t")});
                }
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
                        if (res != null && res.Contains("success"))
                        {
                            //captchasub ok
                            wClient = new WebClient();
                            GoogleAnalytics.EasyTracker.GetTracker().SendTiming(DateTime.Now.Subtract(startTime), "Refresh", "CaptchaSub", "Captcha_Loaded");
                            startTime = DateTime.Now;
                            status++;
                            if (dat.isVellore()) 
                                loadPage("http://www.vitacademicsrel.appspot.com/attj/" + dat.getReg() + "/" + dat.getDob());
                            else
                                loadPage("http://www.vitacademicsrelc.appspot.com/attj/" + dat.getReg() + "/" + dat.getDob());
                        }
                        else
                        {
                            MessageBox.Show("Captcha error ocuured.\nIf error persists check your credentials.", "Error!", MessageBoxButton.OK);
                            loadSaved();
                        }

                        break;

                        //DOWNLOAD ATTENDANCE CALLBACK
                    case 1:
                        if (res != null && res.Contains("valid%"))
                        {
                            //SAVE ATTENDANCE
                            dat.saveAttendance(res);
                            GoogleAnalytics.EasyTracker.GetTracker().SendTiming(DateTime.Now.Subtract(startTime), "Refresh", "Attendance", "Att_Loaded");
                            startTime = DateTime.Now;
                            //CALL MARKS
                            status++;
                            if(dat.isVellore())
                                loadPage("http://www.vitacademicsrel.appspot.com/marks/" + dat.getReg() + "/" + dat.getDob());
                            else
                                loadPage("http://www.vitacademicsrelc.appspot.com/marks/" + dat.getReg() + "/" + dat.getDob());
                        }
                        else
                            MessageBox.Show("Could not load marks.\nIf error persists check your network.", "Error!", MessageBoxButton.OK);
                        break;
                    
                    case 2:
                        if (res != null)
                        {
                            //SAVE MARKS
                            dat.saveMarks(res);
                            GoogleAnalytics.EasyTracker.GetTracker().SendTiming(DateTime.Now.Subtract(startTime), "Refresh", "Marks", "Marks_Loaded");
                            startTime = DateTime.Now;
                            //LOAD DATA
                            status++;
                            loadSaved();   
                        }
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