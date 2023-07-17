using Library.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DB
{
    public class DbBorrowerManager : DbManager
    {
        public DbBorrowerManager(string dbPath) : base(dbPath) 
        {   
        }

        //InsertNewBorrower permet d'ajouter un nouveau emprunteur

        public Borrower InsertNewBorrower(Borrower borrower)
        {
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);


            cmd.CommandText = " INSERT INTO Borrower (first_Name, last_Name, adress, phone_Number, email)"
                    + "VALUES(@first_Name,@last_Name, @adress, @phone_Number, @email);";

            SQLiteParameter First_Name = new SQLiteParameter("@first_Name", borrower.First_Name);
            SQLiteParameter Last_Name = new SQLiteParameter("@last_Name", borrower.Last_Name);
            SQLiteParameter Adress = new SQLiteParameter("@adress", borrower.Adress);
            SQLiteParameter Phone_Number = new SQLiteParameter("@phone_Number", borrower.Phone_Number);
            SQLiteParameter Email = new SQLiteParameter("@email", borrower.Email);


            cmd.Parameters.Add(First_Name);
            cmd.Parameters.Add(Last_Name);
            cmd.Parameters.Add(Adress);
            cmd.Parameters.Add(Phone_Number);
            cmd.Parameters.Add(Email);



            OpenConnection();

            cmd.ExecuteNonQuery();
            int lastId = (int)_dbConnection.LastInsertRowId;

            CloseConnection();


            Borrower borrowerAdded = new Borrower(borrower, lastId);
            Console.WriteLine("Emprunteur enregistrer avec succes !");
            return borrowerAdded;


        }

        //Retourner un emprunteur par son Nom
        public Borrower GetBorrower(string first_Name)
        {
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);
            cmd.CommandText = "SELECT * FROM Borrower WHERE first_Name = @first_Name";
            SQLiteParameter first_NameParameter = new SQLiteParameter("@first_Name", first_Name);

            cmd.Parameters.Add(first_NameParameter);

            return SelectBorrower(cmd);
        }


        //Retourner la liste de tout les emprunteurs

        public List<Borrower> GetBorrowers()
        {
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);
            cmd.CommandText = "SELECT * FROM Borrower";

            return SelectBorrowers(cmd);
        }

        //Suppression d'un emprunteur

        public void DeleteBorrower (string first_Name)
        {
            OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);
            cmd.CommandText = "DELETE FROM Borrower WHERE first_Name = @first_Name";
            SQLiteParameter first_NameParameter =  new SQLiteParameter("first_Name", first_Name);

            cmd.Parameters.Add(first_NameParameter);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        //Fin de la methode la suppression d'un emprunteur

        // Méthode exécutant la CMD et retournant un emprunteur

        private Borrower SelectBorrower(SQLiteCommand cmd)
        {
            OpenConnection();
            // Execution de la cmd
            SQLiteDataReader reader = cmd.ExecuteReader();

            Borrower returnBorrower = null;

            // lecture de la DB ligne par ligne grâce à Read()
            while (reader.Read())
            {
                // Récupération de l'emprunteur créé dans GetBorrowerFromReader
                returnBorrower = GetBorrowerFromReader(reader);
            }

            CloseConnection();

            return returnBorrower;
        }


        // Méthode exécutant la CMD et retournant une liste des emprunteurs
        private List<Borrower> SelectBorrowers(SQLiteCommand cmd)
        {
            OpenConnection();
            SQLiteDataReader reader = cmd.ExecuteReader();

            List<Borrower> returnBorrowers = new List<Borrower>();

            while (reader.Read())
            {
                returnBorrowers.Add(GetBorrowerFromReader(reader));
            }

            CloseConnection();

            return returnBorrowers;
        }


        // Méthode permettant de centraliser la création d'un objet "Borrower"
        // suivant les données reçue par la DB
      
        private Borrower GetBorrowerFromReader(SQLiteDataReader reader)
        {
            return new Borrower(Convert.ToInt32(reader["Borrower_id"]),
                                Convert.ToString(reader["First_Name"]),
                                Convert.ToString(reader["Last_Name"]),
                                Convert.ToString(reader["Adress"]),
                                Convert.ToString(reader["Phone_Number"]),
                                Convert.ToString(reader["Email"])
                                
                                );
        }
    }
}
