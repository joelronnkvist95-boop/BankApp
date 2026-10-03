namespace BankApp
{
    internal class Program
    {

        static string[] usernames = { "Kenta", "Johnny", "Glenn", "Leif", "Bosse" };
        static string[] passwords = { "abc123", "bcd234", "cde345", "def456", "efg567" };

        static string[,] userAccounts =
        {
            {"allkonto", "sparkonto", ""},
            {"allkonto", "sparkonto", ""},
            {"allkonto", "sparkonto", "matkonto"},
            {"allkonto", "sparkonto", ""},
            {"allkonto", "sparkonto", ""}
        };
        static decimal[,] userBalances =
        {
            {35000.00m, 10000m, 0m},
            {37000m, 5000m, 0m},
            {41000.53m, 0m, 3000m},
            {10000m, 462500m, 0m},
            {2300m, 0m, 0m}
        };
        
        static void Main(string[] args)
        {
            
            
            Console.WriteLine("Välkommen till banken");
            Console.WriteLine();

            int loggedInUser = TryLogin();

            if(loggedInUser != -1)
            {
                MainMenu(loggedInUser);
            }



        }
        
        static int TryLogin()
        {
            int logInAttempts = 0;
            
            while (true)
            {
                Console.Write("Ange användarnamn: ");
                string inputUsername = Console.ReadLine();

                Console.Write("Ange lösenord: ");
                string inputPassword = Console.ReadLine();

                for (int i = 0;  i < usernames.Length; i++)
                {
                   
                    if (inputUsername == usernames[i] && inputPassword == passwords[i])
                    {
                        Console.Clear();
                        Console.WriteLine("Du är nu inloggad");
                        Console.WriteLine();
                        Console.WriteLine("Tryck ENTER för att fortsätta...");
                        Console.ReadKey();
                        return i;
                    }

                }
                
                logInAttempts++;
                Console.WriteLine("Tyvärr du angav fel inloggningsuppgifter");
                
                if (logInAttempts >= 3)
                {
                    Console.WriteLine("Tyvärr du angav fel inloggningsuppgifter 3 gånger. Försök igen senare");
                    break;
                }
                
            }
            return -1;
        }
        
        static void MainMenu(int loggedInUser)
        {

            Console.Clear();

            bool running = true;

            while (running)
            {
                
                Console.WriteLine("Välj menyval:");
                Console.WriteLine();
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Logga ut");
                Console.WriteLine();
                Console.Write("Val: ");
                

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        {
                            ViewAccounts(loggedInUser);
                            Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case "2":
                        {
                            Transfer(loggedInUser);
                            break;
                        }
                    case "3":
                        {
                            Withdraw(loggedInUser);
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Tack för besöket");
                            running = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Ogiltigt val, försök igen.");
                            break;
                        }
                }
            }
        }

        static void ViewAccounts(int loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("Konton:");
            Console.WriteLine();

            for (int i = 0; i < 3; i++)
            {
                if (userAccounts[loggedInUser, i] != "")
                {
                    Console.WriteLine($"{userAccounts[loggedInUser, i]}: {userBalances[loggedInUser, i]}:-");
                }
            }
            Console.WriteLine();
            
            
            
        }
        static void Transfer(int loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("Menyval 2: ");
            Console.WriteLine("Vilka konton vill du överföra mellan?");
            Console.WriteLine("\n");
            
            for(int i = 0; i < 3; i++)
            {
                if (userAccounts[loggedInUser, i] != "")
                {
                    Console.WriteLine($"{i + 1}: {userAccounts[loggedInUser, i]}| Saldo: {userBalances[loggedInUser, i]}:-");
                }
            }

            Console.WriteLine();
            Console.Write("Ange numret på kontot du vill överföra från:  ");
            int fromAccount = int.Parse(Console.ReadLine()) - 1;


            Console.Write("Ange numret på kontot du vill överföra till: ");

            int destinationAccount = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine();

            
            Console.WriteLine("Välj belopp att överföra: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (amount <= 0)
            {
                Console.WriteLine("För lågt belopp");
            }

            else if (userBalances[loggedInUser, fromAccount] >= amount)
            {
                userBalances[loggedInUser, fromAccount] -= amount;
                userBalances[loggedInUser, destinationAccount] += amount;

            }

            else
            {
                Console.WriteLine("För stort belopp");
            }

            ViewAccounts(loggedInUser);
            Console.WriteLine();
            Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn...");

            Console.ReadKey();
            
            Console.Clear();
        }

        static void Withdraw(int loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("Menyval 3: ");
            
            Console.WriteLine("\n");

            for (int i = 0; i < 3; i++)
            {
                if (userAccounts[loggedInUser, i] != "")
                {
                    Console.WriteLine($"{i + 1}: {userAccounts[loggedInUser, i]}| Saldo: {userBalances[loggedInUser, i]}:-");
                }
            }
            Console.WriteLine();
            Console.Write("Vilket konto vill du ta ut från?: ");

            int withdrawAccount = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine();
            Console.Write("Hur mycket vill du ta ut?: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (amount <= 0)
            {
                Console.WriteLine("För litet belopp");
            }

            else if (userBalances[loggedInUser, withdrawAccount] >= amount)
            {
                userBalances[loggedInUser, withdrawAccount] -= amount;
                ViewAccounts(loggedInUser);
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn...");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Beloppet överstiger ditt kontosaldo");
            }
        }
        
    }
}
                
                
                
                
       




                
        

        
        
