namespace Races.Db
{
    public partial class Car
    {
        #region Properties
        public int? CarsId { get; set; }
        public string? Name { get; set; }
        public int? MaxSpeed { get; set; }
        public int? Cc { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? DriverId { get; set; }
        #endregion

        #region Ctor
        public Car()
        {
        }
        #endregion
    }
}
