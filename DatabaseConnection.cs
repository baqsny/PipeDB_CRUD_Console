using System.Data.SQLite;

namespace PipeDB.Core
{
    public class DatabaseConnection
    {
        public SQLiteConnection sqlite_conn;
        public SQLiteCommand sqlite_cmd;
        public SQLiteDataReader sqlite_datareader;
        public void ConnectionToDatabase()
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Foreign Key Constraints=On;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
        }
    }
}
