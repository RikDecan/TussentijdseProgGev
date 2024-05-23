using Microsoft.Extensions.Logging;
using Races.Db;
using System;
using System.Linq;
using System.Windows;

namespace Generic.Host.WPF.App
{
    /// <summary>
    /// Interaction logic for DriversWindow.xaml
    /// </summary>
    public partial class CarsWindow : Window
    {
        #region Properties
        private readonly ILogger<CarsWindow> _logger;
        /*
        private readonly IConfiguration _configuration; 
        */
        private readonly RacesDb _repository;
        private static int _loadCount = 0;
        public int? CarsId { get; set; }

        #endregion

        #region Ctor
        public CarsWindow(ILogger<CarsWindow> logger, RacesDb db/*, IConfiguration configuration*/)
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

            // om het leeg te maken bij volgende keer
            CarsDg.ItemsSource = null;
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDrivers();
        }

       
        private void LoadDrivers()
        {
            var cars = _repository.Car.Query.All();
            CarsDg.ItemsSource = cars;
            _loadCount++;
            LoadButton.Content = $"Load all ({_loadCount})";
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var carRow = CarsDg.SelectedItem as Car;
                var car = _repository.Driver.Query.ByCarsId(carRow.CarsId).Single();
                car.MaxSpeed = carRow.MaxSpeed;
                car.Cc = carRow.Cc;
                car.RegistrationDate = carRow.RegistrationDate;
                
                _repository.Car.Update.ByCarId(car);
                _repository.SaveChanges();
                MessageBox.Show("Row Updated Successfully.");
                LoadDrivers();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
        }

        #endregion
    }
}
