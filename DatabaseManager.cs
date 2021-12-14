using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PipeDB.Core
{
    public class DatabaseManager
    {
        public void CreateTableInDatabase()
        {
            string query = @"CREATE TABLE IF NOT EXISTS Pipe(
                            Pipe_Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            Pipe_Name TEXT,
                            Pipe_OuterDiameter DOUBLE,
                            Pipe_WallThickness DOUBLE,
                            Pipe_kgM DOUBLE)";
            DatabaseConnection dbConn = new DatabaseConnection();
            dbConn.ConnectionToDatabase();

            SQLiteCommand cmd = new SQLiteCommand(query, dbConn.sqlite_conn);
            cmd = dbConn.sqlite_conn.CreateCommand();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            dbConn.sqlite_conn.Close();
        }
        public void CreatePipeInDatabase(string pipeName, double pipeOutDiameter, double pipeWallThickness, double pipeKgM)
        {
            string query = "INSERT INTO Pipe (Pipe_Name, Pipe_OuterDiameter, Pipe_WallThickness, Pipe_kgM) VALUES (@Pipe_Name, @Pipe_OuterDiameter, @Pipe_WallThickness, @Pipe_kgM)";

            DatabaseConnection dbConn = new DatabaseConnection();
            dbConn.ConnectionToDatabase();

            SQLiteCommand cmd = new SQLiteCommand(query, dbConn.sqlite_conn);

            cmd.Parameters.AddWithValue("@Pipe_Name", pipeName);
            cmd.Parameters.AddWithValue("@Pipe_OuterDiameter", pipeOutDiameter);
            cmd.Parameters.AddWithValue("@Pipe_WallThickness", pipeWallThickness);
            cmd.Parameters.AddWithValue("@Pipe_kgM", pipeKgM);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            dbConn.sqlite_conn.Close();
        }

        public void DeletePipeInDatabase(int pipeId)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            dbConn.ConnectionToDatabase();

            string query = "DELETE FROM Pipe WHERE Pipe_Id = @Pipe_Id";

            SQLiteCommand cmd = new SQLiteCommand(query, dbConn.sqlite_conn);

            cmd.Parameters.AddWithValue("@Pipe_Id", pipeId);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            dbConn.sqlite_conn.Close();
        }

        public void DeleteAllDataInDatabase()
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            dbConn.ConnectionToDatabase();

            string query = "DELETE FROM Pipe";

            SQLiteCommand cmd = new SQLiteCommand(query, dbConn.sqlite_conn);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = "DELETE FROM sqlite_sequence WHERE name='Pipe'";

            cmd = new SQLiteCommand(query, dbConn.sqlite_conn);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            dbConn.sqlite_conn.Close();
        }

        public void ListAllPipesFromDatabase()
        {

            DatabaseConnection dbConn = new DatabaseConnection();
            dbConn.ConnectionToDatabase();

            string query = "SELECT * FROM Pipe";

            SQLiteCommand cmd = new SQLiteCommand(query, dbConn.sqlite_conn);

            cmd = dbConn.sqlite_conn.CreateCommand();
            cmd.CommandText = query;

            dbConn.sqlite_datareader = cmd.ExecuteReader();
            string tableRowSeparator = "+-------------------------------------------------------------------------+";
            Console.WriteLine("List of pipes: ");
            Console.WriteLine(tableRowSeparator);
            Console.WriteLine(string.Format("| {0,-4} | {1,-4} | {2,-20} | {3,-8} | {4,-8} | {5,-12} |", "No.", "ID", "Name", "D", "e", "kg/m factor"));
            Console.WriteLine(tableRowSeparator);

            int i = 1;

            while (dbConn.sqlite_datareader.Read())
            {
                Console.WriteLine(string.Format("| {0,-4} | {1,-4} | {2,-20} | {3,-8} | {4,-8} | {5,-12} |", i, dbConn.sqlite_datareader["Pipe_Id"].ToString(), dbConn.sqlite_datareader["Pipe_Name"].ToString(), dbConn.sqlite_datareader["Pipe_OuterDiameter"].ToString(), dbConn.sqlite_datareader["Pipe_WallThickness"].ToString(), dbConn.sqlite_datareader["Pipe_kgM"].ToString()));
                i++;
            }
            Console.WriteLine(tableRowSeparator);

            dbConn.sqlite_conn.Close();
        }
        public void UpdatePipeInDatabase(int pipeId, string pipeName, double pipeOutDiameter, double pipeWallThickness, double pipeKgM)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            dbConn.ConnectionToDatabase();

            string query = "UPDATE Pipe SET Pipe_Name = @Pipe_Name, Pipe_OuterDiameter = @Pipe_OuterDiameter, Pipe_WallThickness = @Pipe_WallThickness, Pipe_kgM = @Pipe_kgM WHERE Pipe_Id = @Pipe_Id";

            SQLiteCommand cmd = new SQLiteCommand(query, dbConn.sqlite_conn);

            cmd.Parameters.AddWithValue("@Pipe_Id", pipeId);
            cmd.Parameters.AddWithValue("@Pipe_Name", pipeName);
            cmd.Parameters.AddWithValue("@Pipe_OuterDiameter", pipeOutDiameter);
            cmd.Parameters.AddWithValue("@Pipe_WallThickness", pipeWallThickness);
            cmd.Parameters.AddWithValue("@Pipe_kgM", pipeKgM);


            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            dbConn.sqlite_conn.Close();

        }
        public List<string> ReadPipesIdsFromDatabase()
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            dbConn.ConnectionToDatabase();

            string query = "SELECT * FROM Pipe";

            SQLiteCommand cmd = new SQLiteCommand(query, dbConn.sqlite_conn);

            cmd = dbConn.sqlite_conn.CreateCommand();
            cmd.CommandText = query;

            dbConn.sqlite_datareader = cmd.ExecuteReader();

            var pipeIds = new List<string>();

            while (dbConn.sqlite_datareader.Read())
            {
                var pipeIdString = dbConn.sqlite_datareader["Pipe_Id"].ToString();
                pipeIds.Add(pipeIdString);
            }

            dbConn.sqlite_conn.Close();

            return pipeIds;
        }

        public Pipe ReadPipeFromDatabase(int pipeId)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            dbConn.ConnectionToDatabase();

            string query = "SELECT * FROM Pipe WHERE Pipe_Id = @Pipe_Id";

            SQLiteCommand cmd = new SQLiteCommand(query, dbConn.sqlite_conn);

            cmd = dbConn.sqlite_conn.CreateCommand();
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@Pipe_Id", pipeId);

            dbConn.sqlite_datareader = cmd.ExecuteReader();

            var pipeFromDatabase = new Pipe();

            while (dbConn.sqlite_datareader.Read())
            {
                var pipeOuterDiameterString = dbConn.sqlite_datareader["Pipe_OuterDiameter"].ToString();
                var pipeWallThicknessString = dbConn.sqlite_datareader["Pipe_WallThickness"].ToString();
                var pipeKgMString = dbConn.sqlite_datareader["Pipe_kgM"].ToString();

                pipeFromDatabase = new Pipe
                {
                    PipeOutDiameter = double.Parse(pipeOuterDiameterString),
                    PipeWallThickness = double.Parse(pipeWallThicknessString),
                    PipeKgM = double.Parse(pipeKgMString)
                };
            }

            dbConn.sqlite_conn.Close();

            return pipeFromDatabase;
        }

    }
}
