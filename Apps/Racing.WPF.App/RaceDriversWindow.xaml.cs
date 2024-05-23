using Microsoft.Extensions.Logging;
using Races.Db;
using System.Collections.Generic;
using System.Windows;

namespace Generic.Host.WPF.App
{
    /// <summary>
    /// Interaction logic for DriversWindow.xaml
    /// </summary>
    public partial class RaceDriversWindow : Window
    {
        #region Properties
        private readonly ILogger<RaceDriversWindow> _logger;
        /*
        private readonly IConfiguration _configuration; 
        */
        private readonly RacesDb _repository;
        private static int _loadCount = 0;
        internal int RaceId { get; set; }

        #endregion

        #region Ctor
        public RaceDriversWindow(ILogger<RaceDriversWindow> logger, RacesDb db/*, IConfiguration configuration*/)
        {
            InitializeComponent();


            _logger = logger;
            //_configuration = configuration;
            _repository = db;

        }
        #endregion

        #region Methods
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //_logger.LogDebug("Closing");
            //_logger.LogInformation("Connection string: " + _configuration.GetConnectionString("Db"));
            
            this.Visibility = Visibility.Collapsed; // om te verbergen
            e.Cancel = true; // om te voorkomen dat de WPF app het window toch nog vernietigt
            RaceDriversDg.ItemsSource = null;
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDrivers();
        }

        private void LoadDrivers()
        {
            List<Driver> drivers = _repository.Driver.Query.All();
            List<Race_Driver> race_Drivers = _repository.Race_Driver.Query.All();
            List<Driver> selectedDrivers = new List<Driver>();


            
            for ( int i = 0; i < race_Drivers.Count; i++)
            {
                for ( int j = 0; j < drivers.Count; j++ )
                {
                    if (race_Drivers[i].DriverId == drivers[j].DriverId && race_Drivers[i].RaceId == RaceId )
                    {
                        selectedDrivers.Add(drivers[j] );
                    }
                }
            }
            RaceDriversDg.ItemsSource = selectedDrivers;
            
        }

        #endregion
    }
}
