using System.Linq.Expressions;

namespace DataAccessLogic
{
    public class Database : IDatabase
    {
        private SqlConnection conn;
        private const string db;
        private SqlCommand cmd;
        public void GetMeasurement()
        {
            throw new System.NotImplementedException();
        }

        public void SaveMeasurement()
        {
            throw new System.NotImplementedException();
        }
    }
}