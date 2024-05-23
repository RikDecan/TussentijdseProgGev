using Ado.Data.SqlServer;
using System.Data;
using System.Data.SqlClient;

namespace Races.Db.DriverRepository
{ 
    public partial class CarQuery
    {
        #region Properties
        private readonly SqlServerTable _table;
        #endregion

        #region Ctor
        public CarQuery(SqlServerTable table)
        {
            this._table = table;
        }
        #endregion

        #region Methods
        private List<Car> ToList(SqlCommand sqlCommand)
        {
            var dt = _table.DbAccess.GetDataTable(sqlCommand);

            List<Car> list = [];
            foreach (DataRow dataRow in dt.Rows)
            {
                Car car = new()
                {
                   
                    CarsId = (int)dataRow["CarsId"],
                    Name = (string)dataRow["Name"],
                    MaxSpeed = (int)dataRow["MaxSpeed"],
                    Cc = (int)dataRow["Cc"],
                    RegistrationDate = (DateTime)dataRow["RegistrationDate"],
                    DriverId = (int)dataRow["DriverId"]
                    
                };
                list.Add(car);
            }
            return list;
        }

        public List<Car> ToList(string sql)
        {
            return ToList(new SqlCommand(sql));
        }

        public int Count()
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT COUNT(*) FROM [Cars];";
                return Convert.ToInt32(_table.DbAccess.ExecuteScalar(sqlCommand));
            }
        }

        public int CountByKeyword(string keyword)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT COUNT(CarsId) FROM [Cars] WHERE (Name LIKE '%' + @Keyword + '%' OR MaxSpeed LIKE '%' + @Keyword + '%' OR Cc LIKE '%' + @Keyword + '%' OR RegistrationDate LIKE '%' + @Keyword + '%' OR DriverId LIKE '%' + @Keyword + '%');";
                sqlCommand.Parameters.AddWithValue("@Keyword", keyword);
                return Convert.ToInt32(_table.DbAccess.ExecuteScalar(sqlCommand));
            }
        }

        public List<Car> All()
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [CarsId], [Name], [MaxSpeed], [Cc], [RegistrationDate], [DriverId] FROM [Cars];";
                return ToList(sqlCommand);
            }
        }

        public List<Car> ByKeyword(string keyword, int start, int end, string orderByColumnName, string orderDirection = "ASC")
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = $"SELECT [CarsId], [Name], [MaxSpeed], [Cc], [RegistrationDate], [DriverId] FROM (SELECT ROW_NUMBER() OVER (ORDER BY {orderByColumnName} {orderDirection}) AS RowSequence, [CarsId], [Name], [MaxSpeed], [Cc], [RegistrationDate], [DriverId] FROM [Cars] WHERE  (Name LIKE '%' + @Keyword + '%' OR MaxSpeed LIKE '%' + @Keyword + '%' OR Cc LIKE '%' + @Keyword + '%' OR RegistrationDate LIKE '%' + @Keyword + '%' OR DriverId LIKE '%' + @Keyword + '%')) AS [Cars] WHERE RowSequence BETWEEN @Start AND @End;";
                sqlCommand.Parameters.AddWithValue("@Keyword", keyword);
                sqlCommand.Parameters.AddWithValue("@Start", start);
                sqlCommand.Parameters.AddWithValue("@End", end);
                return ToList(sqlCommand);
            }
        }

        public Car? ByPrimaryKey(int carsId)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT TOP 1 [CarsId], [Name], [MaxSpeed], [Cc], [RegistrationDate], [DriverId] FROM [Cars] WHERE CarsId=@CarsId;";
                sqlCommand.Parameters.AddWithValue("@CarsId", carsId);
                var list = ToList(sqlCommand);
                if (list.Count > 0)
                {
                    return list[0];
                }
                return null;
            }
        }

        public List<Car> ByCarsId(int carsId)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [Name], [MaxSpeed], [Cc], [RegistrationDate], [DriverId] FROM [Cars] WHERE CarsId=@CarsId;";
                sqlCommand.Parameters.AddWithValue("@CarsId", carsId);
                return ToList(sqlCommand);
            }
        }

        public List<Car> ByName(string Name)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [CarsId], [MaxSpeed], [Cc], [RegistrationDate], [DriverId] FROM [Cars] WHERE Name=@Name;";
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                return ToList(sqlCommand);
            }
        }

        public List<Car> ByMaxSpeed(int MaxSpeed)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [CarsId], [Name], [Cc], [RegistrationDate], [DriverId] FROM [Cars] WHERE MaxSpeed=@MaxSpeed;";
                sqlCommand.Parameters.AddWithValue("@MaxSpeed", MaxSpeed);
                return ToList(sqlCommand);
            }
        }
        public List<Car> ByCc(int Cc)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [CarsId], [Name], [MaxSpeed], [RegistrationDate], [DriverId] FROM [Cars] WHERE Cc=@Cc;";
                sqlCommand.Parameters.AddWithValue("@Cc", Cc);
                return ToList(sqlCommand);
            }
        }

        public List<Car> ByRegistrationDate(DateTime RegistrationDate)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [CarsId], [Name], [MaxSpeed], [Cc] , [DriverId] FROM [Cars] WHERE RegistrationDate=@RegistrationDate;";
                sqlCommand.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                return ToList(sqlCommand);
            }
        }
        public List<Car> ByDriverId(int DriverId)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [CarsId], [Name], [MaxSpeed], [Cc], [RegistrationDate], FROM [Cars] WHERE DriverId=@DriverId;";
                sqlCommand.Parameters.AddWithValue("@DriverId", DriverId);
                return ToList(sqlCommand);
            }
        }
        #endregion
    }
}
