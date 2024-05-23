using Ado.Data.SqlServer;
using Races.Db.DriverRepository;

namespace Races.Db.RepositoryPacks
{
    public partial class CarRepositoryPack : SqlServerTable
    {
        #region Properties
        public CarQuery Query { get; set; }
        public CarInsert Insert { get; set; }
        public CarUpdate Update { get; set; }
        
        #endregion

        #region Ctor
        public CarRepositoryPack(SqlServerDbAccess dbAccess) : base(dbAccess)
        {
            Query = new CarQuery(this);
            Insert = new CarInsert(this);
            Update = new CarUpdate(this);
            
        }
        #endregion
    }
}
