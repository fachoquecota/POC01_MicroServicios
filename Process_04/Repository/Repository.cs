namespace Process_04.Repository
{
    public class Repository : IRepository
    {
        private readonly string _connectionString;
        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetData()
        {
            return $"Repository Data from {_connectionString}";
        }
    }
}
