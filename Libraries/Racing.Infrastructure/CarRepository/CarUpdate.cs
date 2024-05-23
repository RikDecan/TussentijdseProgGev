using Ado.Data.SqlServer;
using System.Data.SqlClient;

namespace Races.Db.DriverRepository
{
    public partial class CarUpdate
    {
        #region Properties
        private readonly SqlServerTable _table;
        #endregion

        #region Ctor
        public CarUpdate(SqlServerTable table)
        {
            this._table = table;
        }
        #endregion

        #region Methods
        private void SetSqlCommandParameter(SqlCommand sqlCommand, Car car)
        {
            sqlCommand.Parameters.AddWithValue("@CarsId", car.CarsId);
            sqlCommand.Parameters.AddWithValue("@Name", car.Name);
            sqlCommand.Parameters.AddWithValue("@MaxSpeed", car.MaxSpeed);
            sqlCommand.Parameters.AddWithValue("@Cc", car.Cc);
            sqlCommand.Parameters.AddWithValue("@RegistrationDate", car.RegistrationDate);
            sqlCommand.Parameters.AddWithValue("@DriverId", car.DriverId);
        }

        

        public virtual void ByMaxSpeed(Car car)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Cars] SET Cc=@Cc, RegistrationDate=@RegistrationDate, WHERE  MaxSpeed=@MaxSpeed;";
                SetSqlCommandParameter(sqlCommand, car);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByCc(Car car)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Cars] SET MaxSpeed=@MaxSpeed, RegistrationDate=@RegistrationDate, WHERE  Cc=@Cc;";
                SetSqlCommandParameter(sqlCommand, car);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }
        public virtual void ByRegistrationDate(Car car)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Cars] SET MaxSpeed=@MaxSpeed, Cc=@Cc, WHERE  RegistrationDate=@RegistrationDate;";
                SetSqlCommandParameter(sqlCommand, car);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public void ByCarId(Car car)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Cars] SET MaxSpeed=@MaxSpeed, Cc=@Cc, RegistrationDate=@RegistrationDate WHERE  CarsId=@CarsId;";
                SetSqlCommandParameter(sqlCommand, car);
                _table.DbAccess.Commands.Add(sqlCommand);
            }

        }


        #endregion
    }
}
