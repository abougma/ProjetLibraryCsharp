using Library.DB;
using Library.Models;

namespace Library
{
    internal class Program
    {

        // Un dictionnaire pour stocker les menus de l'application
        private static Dictionary<string, List<string>> menu = new Dictionary<string, List<string>>();

        // Le gestionnaire de base de données pour la library
        private static DbManager dbManager;

        // Les index actuels du menu et du sous-menu
        private static int currentHeaderMenu = 0;
        private static int currentSubMenu = 0;

        static void Main(string[] args)
        {
            AddItemsMenu();

            Console.WriteLine("---------Bienvenue dans le système de gestion de library numerique 2.0 de l'ECOLE-IT !----------");

            // Afficher le menu principal
            while (true)
            {
                Console.WriteLine("--------------------------------------MENU PRINCIPAL-------------------------------------------");
                foreach (string headerMenuText in menu.Keys)
                {
                    Console.WriteLine(headerMenuText);
                }

                // Lire la saisie de l'utilisateur
                Console.Write("Entrez le numéro de l'action de votre choix : ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0)
                    {
                        // Quitter l'application si l'utilisateur entre 0
                        break;
                    }
                    else if (menu.ContainsKey($"{choice}. Livre"))
                    {
                        // Afficher le sous-menu pour les livres si l'utilisateur entre 1
                        currentHeaderMenu = choice;
                        currentSubMenu = 0;
                        while (true)
                        {
                            ShowSubMenu(currentHeaderMenu);

                            // Lire la saisie de l'utilisateur
                            Console.Write("Entrez le numéro de votre choix : ");
                            input = Console.ReadLine();
                            if (int.TryParse(input, out int subChoice))
                            {
                                if (subChoice == 0)
                                {
                                    // Revenir au menu principal si l'utilisateur entre 0
                                    break;
                                }
                                else if (subChoice > 0 && subChoice <= menu[$"{currentHeaderMenu}. Livre"].Count)
                                {
                                    // Lancer l'action correspondant au choix de l'utilisateur
                                    currentSubMenu = subChoice;
                                    LaunchAction();
                                }
                            }
                        }
                    }
                    else if (menu.ContainsKey($"{choice}. Emprunteur"))
                    {
                        // Afficher le sous-menu pour les Emprunteurs si l'utilisateur entre 2
                        currentHeaderMenu = choice;
                        currentSubMenu = 0;
                        while (true)
                        {
                            ShowSubMenu(currentHeaderMenu);

                            // Lire la saisie de l'utilisateur
                            Console.Write("Entrez le numéro de votre choix : ");
                            input = Console.ReadLine();
                            if (int.TryParse(input, out int subChoice))
                            {
                                if (subChoice == 0)
                                {
                                    // Revenir au menu principal si l'utilisateur entre 0
                                    break;
                                }
                                else if (subChoice > 0 && subChoice <= menu[$"{currentHeaderMenu}. Emprunteur"].Count)
                                {
                                    // Lancer l'action correspondant au choix de l'utilisateur
                                    currentSubMenu = subChoice;
                                    LaunchAction();
                                }
                            }
                        }
                    }
                    else if (menu.ContainsKey($"{choice}. Prêt"))
                    {
                        // Afficher le sous-menu pour les Prêteurs si l'utilisateur entre 3
                        currentHeaderMenu = choice;
                        currentSubMenu = 0;
                        while (true)
                        {
                            ShowSubMenu(currentHeaderMenu);

                            // Lire la saisie de l'utilisateur
                            Console.Write("Entrez le numéro de votre choix : ");
                            input = Console.ReadLine();
                            if (int.TryParse(input, out int subChoice))
                            {
                                if (subChoice == 0)
                                {
                                    // Revenir au menu principal si l'utilisateur entre 0
                                    break;
                                }
                                else if (subChoice > 0 && subChoice <= menu[$"{currentHeaderMenu}. Prêt"].Count)
                                {
                                    // Lancer l'action correspondant au choix de l'utilisateur
                                    currentSubMenu = subChoice;
                                    LaunchAction();
                                }
                            }
                        }
                    }
                }

            }

        }


        private static void AddItemsMenu()
        {
            // Ajouter les options de menu pour les livres

            menu.Add("1. Livre", new List<string>()
            {
            "1. Ajouter un nouveau livre",
            "2. Voir la liste des livres",
            "3. Voir un livre spécifique",
            "4. Supprimer un livre"
            });

            // Ajouter les options de menu pour les emprunteurs
            menu.Add("2. Emprunteur", new List<string>()
            {
            "1. Ajouter un nouveau emprunteur",
            "2. Voir la des emprunteur",
            "3. Voir un emprunteur spécifique",
            "4. Supprimer un emprunteur"
            });

            // Ajouter les options de menu pour les prêts

            menu.Add("3. Prêt", new List<string>()
            {
            "1. Ajouter un nouveau prêt",
            "2. Voir la des prêteur",
            "3. Voir un prêteur spécifique",
            "4. Supprimer un prêteur"
            });
        }


        private static void ShowSubMenu(int headerMenuId)
        {
            foreach (string subMenuText in menu.ElementAt(headerMenuId - 1).Value)
            {
                Console.WriteLine(subMenuText);
            }
            Console.WriteLine("0. Revenir au menu précédent");
        }


        private static void LaunchAction()
        {
            switch (currentHeaderMenu)
            {
                case 1:
                    LaunchActionBook();
                    break;
                case 2:
                    LaunchActionBorrower();
                    break;
                case 3:
                    LaunchActionLoan();
                    break;

                default: throw new Exception($"Current Header Menu not exist. Id : {currentHeaderMenu}");
            }
        }


       #region Loan
        private static void LaunchActionLoan()
        {
            dbManager = new DbLoanManager("Library.db");

            switch (currentHeaderMenu)
            {
                case 1:
                    InsertLoan();
                    break;
            }
        }

        private static void InsertLoan()
        {
            //Ajout d'un nouveau pret

            Console.WriteLine("Entrez la date de pret svp ! : ");
            string Borrowed_Date = Console.ReadLine();

            Console.WriteLine("Entrez la date de retour du pret svp ! :");
            string Return_Date = Console.ReadLine();

            Console.WriteLine("Le livre emprunter est : ");

            Console.WriteLine("L'emprunteur est : ");



            Loan loan = new Loan(-1, Borrowed_Date, Return_Date);

            ((DbLoanManager)dbManager).InsertNewLoan(loan);

            Console.WriteLine("Emprunter ajouter avec succes !");


        }

        #endregion


       #region Borrower

        private static void LaunchActionBorrower()
        {

            dbManager = new DbBorrowerManager("Library.db");


            switch (currentSubMenu)
            {
                case 1:
                    InsertBorrower();
                    break;
                case 2:
                    ShowAllBorrowers();
                    break;
                case 3:
                    Console.WriteLine("Veuillez entrer le nom de l'emprunteur souhaité : ");
                    ShowBorrowerByFirst_Name(Console.ReadLine());
                    break;

                case 4:
                    DeleteBorrower();
                    break;
                default: throw new Exception($"Current Sub Menu not exist. Id : {currentSubMenu}");
            }

        }

        //Récupération et affichages des Emprunteurs présents en DB
        private static void ShowAllBorrowers()
        {

            ShowHeaderBorrower();
            List<Borrower> borrowers = new List<Borrower>(((DbBorrowerManager)dbManager).GetBorrowers());

            foreach (Borrower borrower in borrowers)
            {
                ShowBorrower(borrower);
            }
        }

        private static void InsertBorrower()
        {
            //Ajout d'un Emprunteur par utilisateur dans la DB

            Console.WriteLine("Veuillez entrer le Nom de l'emprunteur : ");
            string first_Name = Console.ReadLine();

            Console.WriteLine("Veuillez entrer le prenom de l'emprunteur : ");
            string last_Name = Console.ReadLine();

            Console.WriteLine("Veuillez entrer l'adresse de l'emprunteur : ");
            string adress = Console.ReadLine();

            Console.WriteLine("Veuillez entrer le numero de telephone  : ");
            string phone_Number = Console.ReadLine();

            Console.WriteLine("Veuillez entrer l'email : ");
            string email = Console.ReadLine();

            Borrower borrower = new Borrower(-1, first_Name, last_Name, adress, phone_Number, email);

            ((DbBorrowerManager)dbManager).InsertNewBorrower(borrower);

            Console.WriteLine("Emprunter ajouter avec succes !");

        }

        // Methode de Suppression d'un emprunteur 

        private static void DeleteBorrower()
        {
            Console.WriteLine("Veillez entrer le nom de l'emprunteur que vous voulez supprimer : ");
            string first_Name = Console.ReadLine();

            ((DbBorrowerManager)dbManager).DeleteBorrower(first_Name);

            Console.WriteLine("L'emprunteur de nom " + first_Name + " a été supprimé de la base de données.");
        }
        //Fin de Methode de suppression

        private static void ShowBorrowerByFirst_Name(string first_Name)
        {
            Borrower borrower = ((DbBorrowerManager)dbManager).GetBorrower(first_Name);
            ShowHeaderBorrower();
            ShowBorrower(borrower);
        }

        private static void ShowHeaderBorrower()
        {
            Console.WriteLine("Id - First Name - Last Name - Adress - Phone Number - Email");
        }

        private static void ShowBorrower(Borrower borrower)
        {
            Console.WriteLine($"{borrower.Id} - {borrower.First_Name} - {borrower.Last_Name} - {borrower.Adress} - {borrower.Phone_Number} - {borrower.Email}");
        }

        #endregion


       #region Book
        private static void LaunchActionBook()
        {
            dbManager = new DbBookManager("Library.db");

            switch (currentSubMenu)
            {
                case 1:
                    InsertBook();
                    break;
                case 2:
                    ShowAllBooks();
                    break;
                case 3:
                    Console.WriteLine("Veillez entrer le titre du livre souhaite : ");
                    ShowBookByTitle(Console.ReadLine());
                    break;

                // case 4:
                //   UpdateBook();
                // break;
                case 4:
                    DeleteBook();
                    break;

                default: throw new Exception($"Current sub Menu not exist. Id : {currentSubMenu}");
            }
        }

        //Récupération et affichages des livres présents en DB
        private static void ShowAllBooks()
        {

            ShowHeaderBook();
            List<Book> books = new List<Book>(((DbBookManager)dbManager).GetBooks());

            foreach (Book book in books)
            {
                ShowBook(book);
            }
        }

        private static void InsertBook()
        {
            //Ajout d'un livre par utilisateur dans la DB

            Console.WriteLine("Veuillez entrer le Isbn du livre : ");
            string isbn = Console.ReadLine();

            Console.WriteLine("Veuillez entrer le titre du livre : ");
            string title = Console.ReadLine();

            Console.WriteLine("Veuillez entrer le nom de l'auteur : ");
            string author = Console.ReadLine();

            Console.WriteLine("Veuillez entrer l'année de production du livre : ");
            string yearOf_Publication = Console.ReadLine();

            Console.WriteLine("Veuillez entrer le nombre de pages du livre : ");
            int numberOf_Page;
            int.TryParse(Console.ReadLine(), out numberOf_Page);

            Console.WriteLine("Veuillez entrer le nombre de livre en stock : ");
            int stock;
            int.TryParse(Console.ReadLine(), out stock);

            Book book = new Book(-1, isbn, title, author, yearOf_Publication, numberOf_Page, stock);

            ((DbBookManager)dbManager).InsertNewBook(book);



        }

        // Methode de Suppression d'un livre 

        private static void DeleteBook()
        {
            Console.WriteLine("Veillez entrer le Isnb du livre que vous voulez supprimer : ");
            string isbn = Console.ReadLine();

            ((DbBookManager)dbManager).DeleteBook(isbn);

            Console.WriteLine("Le livre avec l'ISBN " + isbn + " a été supprimé de la base de données.");
        }
        //Fin de Methode de suppression



        // Methode de Modification d'un livre


        /* public void UpdateBook(string isbn)
         {
             Console.WriteLine("Entrez les informations à modifier :");


             Console.Write("Titre : ");
             string title = Console.ReadLine();

             Console.Write("Auteur : ");
             string author = Console.ReadLine();

             Console.Write("Année de production : ");
             string yearOf_Publication = Console.ReadLine();

             Console.Write("Nombre de pages : ");
             int numberOf_Page;
             int.TryParse(Console.ReadLine(), out numberOf_Page);

             Console.Write("Nombre de livres en stock : ");
             int stock;
             int.TryParse(Console.ReadLine(), out stock);

             Book book = new Book(-1, isbn, title, author, yearOf_Publication, numberOf_Page, stock);

             ((DbBookManager)dbManager).UpdateBook(book);
         }
        */


        // Fin Methode de Modification d'un livre
        private static void ShowBookByTitle(string title)
        {
            Book book = ((DbBookManager)dbManager).GetBook(title);
            ShowHeaderBook();
            ShowBook(book);
        }

        private static void ShowHeaderBook()
        {
            Console.WriteLine("Id - Isbn - Title - Author - Year Of Production - Number Of Page - Stock");
        }

        private static void ShowBook(Book book)
        {
            Console.WriteLine($"{book.Id} - {book.Isbn} - {book.Title} - {book.Author} - {book.YearOf_Publication} - {book.NumberOf_Page} - {book.Stock}");
        }
        #endregion
    }
}

