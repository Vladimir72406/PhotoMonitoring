using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PhotoMonitoring.Model
{
    public class Settings : INotifyPropertyChanged
    {
        private string workFolder;
        private int countRowOnMonitor;

        public string WorkFolder
        {
            get { return workFolder; }
            set
            {
                workFolder = value;
                OnPropertyChanged("WorkFolder");
            }
        }

        public int CountRowOnMonitor
        {
            get { return countRowOnMonitor; }
            set
            {
                countRowOnMonitor = value;
                OnPropertyChanged("CountRowOnMonitor");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
