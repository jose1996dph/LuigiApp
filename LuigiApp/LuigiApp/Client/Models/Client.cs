using SQLite;

namespace LuigiApp.Client.Models
{
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Unique]
        public string Dni { get; set; }
        public Client()
        {

        }
        public Client(string name, string dni)
        {
            Name = name;
            Dni = dni;
        }

        public void Set(string name, string dni)
        {
            Name = name;
            Dni = dni;
        }
    }
}
