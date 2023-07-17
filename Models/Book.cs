using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        private string _isbn;

        public string Isbn
        { 
            get { return _isbn; }
            private set { _isbn = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            private set { _title = value; }
        }

        private string _author;

        public string Author
        {
            get { return _author; }
            private set { _author = value; }
        }

        private string _yearof_publication;

        public string YearOf_Publication
        {
            get { return _yearof_publication;}
            private set
            {
                _yearof_publication = value;
            }

        }

        private int _numberofpage;

        public int NumberOf_Page
        {
            get { return _numberofpage; }
            private set
            {
                _numberofpage = value;
            }
        }

        private int _stock;
        public int Stock
        {
            get { return _stock; }
            private set
            {
                _stock = value;
            }
        }


        
       // private Book book;
       // private int lastId;

        public Book (int id, string isbn,string title,string author, string yearOf_Publication,int numberOf_Page, int stock)
        {
            Id = id;
            Isbn = isbn;
            Title = title;
            Author = author;
            YearOf_Publication = yearOf_Publication;
            NumberOf_Page = numberOf_Page;
            Stock = stock;
        }

        public Book(Book bookToCopy, int newId)
        {
            Id = newId;
            Isbn = bookToCopy.Isbn;
            Title = bookToCopy.Title;
            Author = bookToCopy.Author;
            YearOf_Publication = bookToCopy.YearOf_Publication;
            NumberOf_Page = bookToCopy.NumberOf_Page;
            Stock = bookToCopy.Stock;
        }
    }
}
