using Library.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DB
{
    public class DbLoanManager : DbManager
    {
        public DbLoanManager(string dbPath) : base(dbPath)
        {
        }

        //InsertNewLoan permet d'ajouter dans la DB un nouveau pret

        public Loan InsertNewLoan(Loan loan)
        {
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);

            cmd.CommandText = "INSERT INTO Loan (borrowed_Date, return_Date, Book_id, Borrower_id)"
                + "VALUES (@borrowed_Date, @return_Date, @Book_id, @Borrower_id);";

            SQLiteParameter borrowed_Dateparameter = new SQLiteParameter("@borrowed_Date", loan.Borrowed_Date);
            SQLiteParameter return_Dateparameter = new SQLiteParameter("@return_Date", loan.Return_Date);
            SQLiteParameter book_idParameter = new SQLiteParameter("@Book_id", loan.Book_Id);
            SQLiteParameter borrower_idParameter = new SQLiteParameter("@Borrower_id", loan.Borrower_Id);

            cmd.Parameters.Add(borrowed_Dateparameter);
            cmd.Parameters.Add(return_Dateparameter);
            cmd.Parameters.Add(book_idParameter);
            cmd.Parameters.Add(borrower_idParameter);

            OpenConnection();

            cmd.ExecuteNonQuery();
            int lastId = (int)_dbConnection.LastInsertRowId;

            CloseConnection();

            Loan loanAdded = new Loan(loan.Borrowed_Date, loan.Return_Date, loan.Book, loan.Borrower, lastId);
            Console.WriteLine("Le pret a été ajouté avec succès !");
            return loanAdded;
        }

        //Suppression d'un emprunteur

        public void DeleteLoan(int loanId)
        {
            OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(_dbConnection);
            cmd.CommandText = "DELETE FROM Loan WHERE Loan_id = @loan_Id";
            SQLiteParameter loanIdParameter = new SQLiteParameter("@loanId", loanId);

            cmd.Parameters.Add(loanIdParameter);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        //Fin de la methode la suppression d'un pret
       
    }
}
