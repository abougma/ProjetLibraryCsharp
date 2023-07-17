using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Loan
    {
        private int _id;

        public int Id { get { return _id; }
        set { _id = value; } }

        private string _borrowed_date;

        public string Borrowed_Date
        {
            get { return _borrowed_date;}
            private set { _borrowed_date = value; }
        }


        private string _return_date;

        public string Return_Date
        {
            get { return _return_date;}
            private set { _return_date = value;}
        }

        private int _book_id;

        public int Book_Id
        {
            get { return _book_id; }
            private set { _book_id = value; }
        }

        private int _borrower_id;
       
        public int Borrower_Id
        {
            get { return _borrower_id; }
            private set { _borrower_id = value; }
        }

        public object Book { get; internal set; }
        public object Borrower { get; internal set; }

        public Loan (int id, string borrowed_date, string return_date)
        {
            Id = id;
            Borrowed_Date = borrowed_date;
            Return_Date = return_date;
        }

        public Loan(Loan loanToCopy, int newId)
        {
            Id = newId;
            Borrowed_Date=loanToCopy.Borrowed_Date;
            Return_Date=loanToCopy.Return_Date;
        }

        public Loan(string borrowed_Date, string return_Date, object book, object borrower, int lastId)
        {
            Borrowed_Date = borrowed_Date;
            Return_Date = return_Date;
            Book = book;
            Borrower = borrower;
        }

       
    }
}
