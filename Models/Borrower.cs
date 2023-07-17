using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Borrower
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }
        

        private string _first_name;
    
        public string First_Name
        {
            get { return _first_name; }
            private set { _first_name = value; }
        }

        private string _last_name;

        public string Last_Name
        {
            get { return _last_name; }
            private set { _last_name = value;}
        }

        private string _adress;

        public string Adress
        {
            get { return _adress; }
            private set { _adress = value; }
        }


        private string  _phone_number;

        public string Phone_Number
        {
            get { return _phone_number; }
            private set { _phone_number = value;}

        }

        private string _email;

        public string Email
        {
            get { return _email; }
            private set { _email = value;}
        }



        public Borrower(int id, string first_Name, string last_Name, string adress, string phone_Number, string email)
        {
            Id = id;
            First_Name = first_Name;
            Last_Name = last_Name;
            Adress = adress;
            Phone_Number = phone_Number;
            Email = email;
        }

        public Borrower(Borrower borrowerToCopy, int newId)
        {
            Id = newId;
            First_Name=borrowerToCopy.First_Name;
            Last_Name=borrowerToCopy.Last_Name;
            Adress = borrowerToCopy.Adress;
            Phone_Number= borrowerToCopy.Phone_Number;
            Email = borrowerToCopy.Email;
        }
    }
}
