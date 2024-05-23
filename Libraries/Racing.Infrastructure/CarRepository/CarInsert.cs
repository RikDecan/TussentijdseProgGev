using Ado.Data.SqlServer;
using System.Data.SqlClient;

namespace Races.Db.DriverRepository
{
    public  partial class CarInsert
    {
        #region Properties
        private readonly SqlServerTable _table;
        #endregion

        #region Ctor
        public CarInsert(SqlServerTable table)
        {
            this._table = table;
        }
        #endregion

        #region Methods
        public void NewRecord(Car car)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "INSERT INTO [Cars] (CarsId, Name, MaxSpeed, Cc, RegistrationDate, DriverId) VALUES (@CarsId, @Name, @MaxSpeed, @Cc, @RegistrationDate, @DriverId); SELECT SCOPE_IDENTITY() AS INT;";
                sqlCommand.Parameters.AddWithValue("@CarsId", car.CarsId);
                sqlCommand.Parameters.AddWithValue("@Name", car.Name);
                sqlCommand.Parameters.AddWithValue("@MaxSpeed", car.MaxSpeed);
                sqlCommand.Parameters.AddWithValue("@Cc", car.Cc);
                sqlCommand.Parameters.AddWithValue("@RegistrationDate", car.RegistrationDate);
                sqlCommand.Parameters.AddWithValue("@DriverId", car.DriverId);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }
        #endregion
    }
}
