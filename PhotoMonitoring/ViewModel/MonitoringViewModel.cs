using PhotoMonitoring.AppLogic;
using PhotoMonitoring.Model;
using PhotoMonitoring.Model.Results;
using PhotoMonitoring.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using WForms = System.Windows.Forms;

namespace PhotoMonitoring.ViewModel
{
    public class MonitoringViewModel
    {
        public string teest { get; set; }
        public FileSystemWatcher watcher;
        public Dispatcher myDisp;
        public ActionCommand actionCmd { get; set; }
        public Settings sets { get; set; }
        public ObservableCollection<int> Numbers { get; set; }

        public ObservableCollection<Photo> lstFiles { get; set; }
        public MonitoringViewModel()
        {
            Numbers = new ObservableCollection<int>();
            Numbers.Add(2);
            Numbers.Add(5);
            Numbers.Add(10);
                        
            sets = AppSetting.loadSettings();

            //
            myDisp = Dispatcher.CurrentDispatcher;
            lstFiles = new ObservableCollection<Photo>();

            //
            this.startWatch();

            //
            actionCmd = new ActionCommand(changePath);

            this.teest = sets.WorkFolder;
        }

        public void watcher_created(object sender, FileSystemEventArgs e)
        {
            string fileName = e.Name;//Иванова_Татьяна_Ивановна_35.66.jpg

            /*блок вынести в проверку*/
            string fullPath = e.FullPath;
            fileName = fileName.Substring(0, fileName.Length - 4);
            string[] parts = fileName.Split('_');
            string surname = parts[0];
            string name = parts[1];
            string middleName = parts[2];
            parts[3] = parts[3].Replace('.', ',');
            double temperature = 0;
            /*-в проверку-*/

            if (double.TryParse(parts[3], out temperature))
            {
                temperature = Convert.ToDouble(parts[3]);
            }

            //добавление через поток
            myDisp.Invoke(new Action(() =>
            {
                Photo m = new Photo();
                m.file = fullPath;
                m.surname = surname;
                m.name = name;
                m.middleName = middleName;
                m.temperature = temperature;
                m.datetimeFix = DateTime.Now;
                                
                while (this.lstFiles.Count >= sets.CountRowOnMonitor)
                {
                    lstFiles.RemoveAt(lstFiles.Count - 1);
                }

                this.lstFiles.Insert(0, m);
            }));
        }

        public void changePath()
        {
            string newFolder = string.Empty;
            Settings newSt = new Settings();
            newSt.CountRowOnMonitor = this.sets.CountRowOnMonitor;
            newSt.WorkFolder = this.sets.WorkFolder;

            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == WForms.DialogResult.OK)
            {
                newFolder = FBD.SelectedPath.ToString();
                WForms.MessageBox.Show("Вы выбрали : " + newFolder);

                newSt.WorkFolder = newFolder;
                ResultSettings resultSaveWorkFolder = AppSetting.saveSetting(newSt);//AppSettings.setWorkFolder(newFolder);

                if (resultSaveWorkFolder.code == 0)
                {
                    WForms.MessageBox.Show("Сохранено");
                    //this.sets = resultSaveWorkFolder.settings;
                    this.sets.WorkFolder = resultSaveWorkFolder.settings.WorkFolder;
                    this.sets.CountRowOnMonitor = resultSaveWorkFolder.settings.CountRowOnMonitor;
                    this.startWatch();

                    this.teest = newFolder;
                }
                else
                {
                    WForms.MessageBox.Show("Ошибка сохранения.\n" + resultSaveWorkFolder.info);
                }
            }
        }

        public void startWatch()
        {
            watcher = new FileSystemWatcher(sets.WorkFolder);//(@"C:\monitor");
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
            watcher.Created += watcher_created;
            watcher.Filter = "*.jpg";
        }
    }
}
