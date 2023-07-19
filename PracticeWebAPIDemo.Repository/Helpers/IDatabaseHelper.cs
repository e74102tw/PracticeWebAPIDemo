using System.Data;

namespace PracticeWebAPIDemo.Repository.Helpers
{
    public interface IDatabaseHelper
    {
        IDbConnection GetConnection();
    }
}
