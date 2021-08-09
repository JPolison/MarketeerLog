using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using MarketeerLog.View;
using MarketeerLog.ViewModel;
namespace MarketeerLog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static App _instance;

        public static App Instance
        {
            get => _instance;
        }

        private static string _appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string AppDataPath
        {
            get => _appDataPath;
        }

        private static string _appName = "MarketeerLog";

        public static string AppName
        {
            get => _appName;
        }

        private static string _appdataBase = Path.Combine(_appDataPath, AppName);

        public static string AppDataBase
        {
            get => _appdataBase;
        }

        private static string _appdataConfig = Path.Combine(_appdataBase, "Config");

        public static string AppDataConfig
        {
            get => _appdataConfig;
        }

        private ConfigData _appConfigData;

        public ConfigData AppConfigData
        {
            get => _appConfigData;
            set => _appConfigData = value;
        }



        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
            _instance = this;
            SetupFolders();
            AppConfigData.ConfigDataChanged += OnDataChanged;
            new ViewModelController();
            new WindowProvider();
            
            WindowProvider.Instance.ShowWindow<MainWindow>(new MainViewModel());


        }

        private void SetupFolders()
        {

            Directory.CreateDirectory(AppDataBase);

            Directory.CreateDirectory(AppDataConfig);



            //create config data 
            if(!File.Exists(AppDataConfig + @"\Config.xml"))
            {
                File.Create(AppDataConfig + @"\Config.xml");
                _appConfigData = new ConfigData();
            }
            else
            {
                LoadConfig(ref _appConfigData);
            }
        }

        private void LoadConfig(ref ConfigData configdata)
        {
            string file = AppDataConfig + @"\Config.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                configdata = (ConfigData)serializer.Deserialize(fs);
            } catch (Exception e)
            {
                Debug.WriteLine("Execption!" + e.ToString());
                configdata = new ConfigData();
            }
        }

        private void SaveConfig()
        {
            string file = AppDataConfig + @"\Config.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
            try
            {
                FileStream fs = new FileStream(file, FileMode.OpenOrCreate);

                serializer.Serialize(fs, AppConfigData);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Execption!" + e.ToString());
            }
        }

        private void OnDataChanged(object o, EventArgs e)
        {
            SaveConfig();
        }

       

        
    }
}
