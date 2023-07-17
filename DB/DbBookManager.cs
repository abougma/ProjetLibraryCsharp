using Library.Models;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics.Contracts;


namespace Library.DB
{
    public class DbBookManager : DbManager
    {
        public DbBookManager(string dbPath) : base(dbPath)
        {
        }

        //InsertNewBook permet d'ajouter dans la DB un nouveau livre

        public Book InsertNewBook(Book book)
        {
            // cmd est une SQLiteCommand, elle permet d'écrire le query qui sera
            // interprété par SQLite

            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);


            //Insertion d'un nouveau livre dans le DB

            cmd.CommandText = "INSERT INTO Book (isbn, title, author, yearOf_publication, numberOf_Page, stock)"
                + "VALUES(@isbn, @title, @author, @yearOf_Publication, @numberOf_Page, @stock);";

            // Ajout des parameters. Ils remplaceront les éléments précédé d'un @ dans VALUES

            SQLiteParameter isbnparameter = new SQLiteParameter("@isbn", book.Isbn);
            SQLiteParameter titleparameter = new SQLiteParameter("@title", book.Title);
            SQLiteParameter authorparameter = new SQLiteParameter("@author", book.Author);
            SQLiteParameter yearOf_publicationparameter = new SQLiteParameter("@yearOf_Publication", book.YearOf_Publication);
            SQLiteParameter numberOf_Pageparameter = new SQLiteParameter("@numberOf_Page", book.NumberOf_Page);
            SQLiteParameter stockparameter = new SQLiteParameter("@stock", book.Stock);

            // Ajout des parameters à notre cmd

            cmd.Parameters.Add(isbnparameter);
            cmd.Parameters.Add(titleparameter);
            cmd.Parameters.Add(authorparameter);
            cmd.Parameters.Add(yearOf_publicationparameter);
            cmd.Parameters.Add(numberOf_Pageparameter);
            cmd.Parameters.Add(stockparameter);

            OpenConnection();

            cmd.ExecuteNonQuery();
            int lastId = (int)_dbConnection.LastInsertRowId;

            CloseConnection();


            Book bookAdded = new Book(book, lastId);
            Console.WriteLine("Le livre a été ajouté avec succès !");
            return bookAdded;

           
        }

        // Recuperation d'un livre grace a son titre

        public Book GetBook(string title)
        {
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);
            cmd.CommandText = "SELECT * FROM Book WHERE title = @title";
            SQLiteParameter titleParameter = new  SQLiteParameter("title", title);

            cmd.Parameters.Add(titleParameter);

            return SelectBook(cmd);
        }
       
        //Retourner tous les livres

        public List<Book> GetBooks()
        {
            SQLiteCommand cmd = new SQLiteCommand( _dbConnection);
            cmd.CommandText = "SELECT * FROM Book";

            return SelectBooks(cmd);
           
        }

        //Suppression d'un livre 

        public void DeleteBook(string isbn)
        {
            OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);
            cmd.CommandText = "DELETE FROM Book WHERE isbn = @isbn";
            SQLiteParameter isbnParameter = new SQLiteParameter("isbn", isbn);

            cmd.Parameters.Add(isbnParameter);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        // Fin suppression d'un livre 


        // Methode de Modification d'un livre

        public void UpdateBook(Book book)
        {
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);
            cmd.CommandText = "UPDATE Book SET title = @title, author = @author, " +
                              "yearOf_Production = @yearOf_Production, numberOf_Page = @numberOf_Page, " +
                              "stock = @stock WHERE isbn = @isbn";
            cmd.Parameters.AddWithValue("isbn", book.Isbn);
            cmd.Parameters.AddWithValue("title", book.Title);
            cmd.Parameters.AddWithValue("author", book.Author);
            cmd.Parameters.AddWithValue("yearOf_Production", book.YearOf_Publication);
            cmd.Parameters.AddWithValue("numberOf_Page", book.NumberOf_Page);
            cmd.Parameters.AddWithValue("stock", book.Stock);

            cmd.ExecuteNonQuery();
        }

        // Fin Methode de Modification d'un livre



        // Méthode exécutant la CMD et retournant un livre
        private Book SelectBook(SQLiteCommand cmd)
        {
            OpenConnection();
            // Execution de la cmd
            SQLiteDataReader reader = cmd.ExecuteReader();

            Book returnBook = null;

            // lecture de la DB ligne par ligne grâce à Read()
            while (reader.Read())
            {
                // Récupération du livre créé dans GetBookFromReader
                returnBook = GetBookFromReader(reader);
            }

            CloseConnection();

            return returnBook;
        }

        // Méthode exécutant la CMD et retournant une liste de livres
        private List<Book> SelectBooks(SQLiteCommand cmd)
        {
            OpenConnection();
            SQLiteDataReader reader = cmd.ExecuteReader();

            List<Book> returnBooks = new List<Book>();

            while (reader.Read())
            {
                returnBooks.Add(GetBookFromReader(reader));
            }

            CloseConnection();

            return returnBooks;
        }

        // Méthode permettant de centraliser la création d'un objet "Book"
        // suivant les données reçue par la DB
        private Book GetBookFromReader(SQLiteDataReader reader)
        {
            return new Book(Convert.ToInt32(reader["Book_id"]),
                                        Convert.ToString(reader["Isbn"]),
                                        Convert.ToString(reader["Title"]),
                                        Convert.ToString(reader["Author"]),
                                        Convert.ToString(reader["YearOf_Publication"]),
                                        Convert.ToInt32(reader["NumberOf_Page"]),
                                        Convert.ToInt32(reader["Stock"])
                                        );
        }

       
    }

}
