using SQLite;
using MathGameMauiApp.Models;

namespace MathGameMauiApp.Data
{
    public class GameRepo
    {
        string _dbPath;
        private SQLiteConnection conn;

        public GameRepo (string dbPath)
        {
            _dbPath = dbPath;
            Init();
        }

        public void Init()
        {
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Game>();
        }

        public List<Game>GetAllGAmes() 
        {
            return conn.Table<Game>().ToList();
        }

        public void Add(Game game)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Insert(game);
        }

        public void Delete(int id)
        { 
            conn = new SQLiteConnection(_dbPath);
            conn.Delete(new Game{ Id = id });
        }
    }
}
