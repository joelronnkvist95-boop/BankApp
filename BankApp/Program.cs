namespace BankApp
{
    internal class Program
    {

        // Användararrayer för att spara inloggningsuppgifter
        static string[] usernames = { "Kenta", "Johnny", "Glenn", "Leif", "Bosse" };

        static string[] pincodes = { "1234", "2345", "3456", "4567", "5678" };
        static string[,] userAccounts =
        {
            {"lönekonto", "sparkonto", ""},
            {"allkonto", "sparkonto", ""},
            {"allkonto", "sparkonto", "matkonto"},
            {"lönekonto", "övrigtkonto", ""},
            {"allkonto", "", ""}
        };
        static decimal[,] userBalances =
        {
            {35000.89m, 10000.28m, 0.00m},
            {37000.69m, 5000.99m, 0.00m},
            {41000.53m, 0.00m, 3000.17m},
            {10000.32m, 462500.00m, 0.00m},
            {2300.11m, 0.00m, 0.00m}
        };

        static void Main(string[] args)
        {

            bool running = false;

            Console.WriteLine("VÄLKOMMEN TILL BANK ABC");
            Console.WriteLine();
            Console.WriteLine("Tryck ENTER för att fortsätta till inloggning");
            Console.ReadKey();

            running = true;

            // while-loop som tar användaren tillbaka till början om inloggningsuppgifterna inte stämmer.
            while (running)
            {
                Console.Clear();

                // Här skickar TryLogin() metoden tillbaka indexet för den avnändare som lyckas logga in eller annars -1 om inloggningen skulle misslyckas.
                int loggedInUser = TryLogin();

                // Ifall TryLogin()-metoden inte returnerar -1 utan istället användarens index, kör if-blocket innehållande MainMenu()-metoden, för den användares index som sparats i variabeln loggedInUser. 
                if (loggedInUser != -1)
                {
                    bool continueRunning = MainMenu(loggedInUser);
                    if (!continueRunning)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

            }

        }

        // Inloggningsmetod som testar om inloggningsuppgfiterna stämmer och vilken användare som loggar in. Detta genom att returnera tillbaka arrayindexen för den användaren.
        static int TryLogin()
        {
 
            while (true)
            {
                Console.WriteLine("Logga in");
                Console.WriteLine();
                Console.Write("Ange användarnamn: ");
                
                string inputUsername = Console.ReadLine();

                // For-loopen startar på i = 0 och loopas så länge i är mindre än längden på usernames[] som är 5.
                // Eftersom värdet för i ökar med 1 för varje varv kan i användas direkt som index för arrayen i if-satsen, där användarinput jämförs med arrayen och dess index.

                for (int i = 0; i < usernames.Length; i++)
                {
                    // Nu kan värdet för i stoppas in som index[i] så att varje index i arrayerna kan jämföras med användarinputen.
                    if (inputUsername == usernames[i])
                    {
                        Console.Clear();
                        Console.WriteLine("Rätt användarnamn");
                        Console.WriteLine();
                        
                        bool pincodeCheck = CheckPincode(i);


                        if (!pincodeCheck)
                        {
                            return -1;
                        }
                        else
                        {
                            return i;
                        }

                    }

                }
                Console.WriteLine();
                Console.WriteLine("Fel användarnamn. Försök igen");
                Console.WriteLine();
                Console.WriteLine("Tryck ENTER för att fortsätta");
                Console.ReadKey();
                Console.Clear();
            }

        }
        // Metod som visar användaren huvudmenun efter inloggning.
        static bool MainMenu(int loggedInUser)
        {
            bool running = true;

            // while-loopen ser till att programmet inte avslutas efter att användaren har exempelvis gjort en överföring eller visat konton.
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Välj menyval:");
                Console.WriteLine();
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Logga ut");
                Console.WriteLine();
                Console.Write("Val: ");

                string menuChoice = Console.ReadLine();

                // Switch- satsen tolkar användaren input "menu Choice" och bestämmer vilket case som ska köras, där olika metoder anropas.
                switch (menuChoice)
                {
                    case "1":
                        {
                            Console.Clear();
                            Console.WriteLine("Kontosaldon:");
                            
                            ViewAccounts(loggedInUser);
                            
                            Console.WriteLine("Tryck på ENTER för att återgå till huvudmenyn...");
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
                            bool success = Withdraw(loggedInUser);
                            
                            if (!success)
                            {
                                return false;
                            }
                            break;


                        }
                    case "4":
                        {
                            // Metoden avslutas och återgår till while-loopen där den startar om Meny-metoden.
                            Console.WriteLine("Tack för besöket");
                            return true;


                        }
                    default:
                        {
                            // Om användarinputen inte matchar något av casens nummer så talar default om det och ta en tillbaka till menyn.
                            Console.Clear();
                            Console.WriteLine("Ogiltigt val, försök igen!");
                            Console.WriteLine();
                            Console.WriteLine("Tryck ENTER för att återgå till meny...");
                            Console.ReadKey();
                            break;
                        }

                }
            }
            return true;
        }
        // ViewAccounts metoden är till för att visa användarens konton och saldon.
        static void ViewAccounts(int loggedInUser)
        {
            Console.WriteLine();
            // For-loopen loopas 3 varv för att inget konto ska missas.
            for (int i = 0; i < 3; i++)
            {
                // If-satsen körs 3 gånger (i=0, i=1, i=2) där varje [i] representerar ett konto och kolumn. loggedInUser här representerar vilken användare och rad som ska bläddras igenom
                if (userAccounts[loggedInUser, i] != "")
                {
                    Console.WriteLine($"{i + 1}:{userAccounts[loggedInUser, i]}: {userBalances[loggedInUser, i]}:-");
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

            int fromAccount = 0;
            int destinationAccount = 0;

            while (true)
            {
                ViewAccounts(loggedInUser);

                Console.WriteLine();
                Console.Write("Ange numret på kontot du vill överföra från:  ");

                // Numret på kontot sparas från användarens input i variabeln fromAccount och det värdet subtraheras med 1 för att matcha indexet för kontot. 
                fromAccount = int.Parse(Console.ReadLine()) - 1;

                if(fromAccount < 0 || fromAccount > 2 || userAccounts[loggedInUser, fromAccount] == "")
                {
                    Console.WriteLine("Ogiltigt konto. Försök igen");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    Console.Write("Ange numret på kontot du vill överföra till: ");

                    // Samma gäller här.
                    destinationAccount = int.Parse(Console.ReadLine()) - 1;
                    Console.WriteLine();

                    if(destinationAccount < 0 || destinationAccount > 2 || userAccounts[loggedInUser, destinationAccount] == "")
                    {
                        Console.WriteLine("Ogiltigt konto. Försök igen");
                        Console.ReadKey();
                        continue;
                    }
                    else if(fromAccount == destinationAccount)
                    {
                        Console.WriteLine("Du kan inte överföra till samma konto. Försök igen");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

             
            }
           


            // Här sparas beloppet som användaren vill överföra.
            Console.Write("Välj belopp att överföra: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            // Som första kontroll kontrolleras beloppet så det inte är 0 eller är negativt.
            if (amount <= 0)
            {
                Console.WriteLine("För lågt belopp");
                Console.WriteLine();
                Console.WriteLine("Tryck ENTER för återgå till huvudmenyn");
                Console.ReadKey();
                return;
            }

            // Annars om beloppet är lika stort eller lägre än saldot så körs detta blocket
            else if (userBalances[loggedInUser, fromAccount] >= amount)
            {
                // Här dras det valda beloppet bort från överföringskontots saldo.
                userBalances[loggedInUser, fromAccount] -= amount;
                // Sen ökar saldot för mottagarkontot här med det valda beloppet.
                userBalances[loggedInUser, destinationAccount] += amount;

            }
            // Här kontrolleras beloppet så att det inte överstiger saldot det valda överföringskontot.
            else
            {
                Console.WriteLine("För stort belopp");
                Console.WriteLine();
                Console.WriteLine("Tryck ENTER för att återgå till huvudmenyn");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            // ViewAccounts() anropas efter överföringen för att visa de nya saldona.
            Console.WriteLine("Nytt saldo:");
            Console.WriteLine();
            
            ViewAccounts(loggedInUser);
            
            Console.WriteLine();
            Console.WriteLine("Tryck på ENTER för att återgå till huvudmenyn");

            Console.ReadKey();

            Console.Clear();
        }
        //Metod för att ta ut pengar
        static bool Withdraw(int loggedInUser)
        {
            Console.Clear();

           
            Console.WriteLine("Menyval 3: ");

            int withdrawAccount = 0;

            while (true)
            {
                // For loopen snurrar 3 gånger för att inte missa något konto i arrayen. 
                ViewAccounts(loggedInUser);

                Console.WriteLine();
                Console.Write("Vilket konto vill du ta ut från?: ");

                // Numret på kontot sparas från användarens input i variabeln withdrawAccount och det värdet subtraheras med 1 för att matcha indexet för kontot. 
                withdrawAccount = int.Parse(Console.ReadLine()) - 1;

                if(withdrawAccount < 0 || withdrawAccount > 2 || userAccounts[loggedInUser, withdrawAccount] == "")
                {
                    Console.WriteLine("Ogiltigt konto. Försök igen");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    break;
                }
            }
           

            while (true)
            {

                // Här sparas beloppet som användaren vill ta ut i variabeln amount
                Console.WriteLine();
                Console.Write("Hur mycket vill du ta ut?: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                Console.Clear();

                // Som första kontroll kontrolleras beloppet så det inte är 0 eller är negativt.
                if (amount <= 0)
                {
                    Console.WriteLine("För litet belopp");
                    Console.WriteLine();
                    Console.WriteLine("Tryck ENTER för att försöka igen");
                    Console.ReadKey();
                    continue;

                }
                // Annars om beloppet är lika stort eller lägre än saldot så körs detta blocket
                else if (userBalances[loggedInUser, withdrawAccount] >= amount)
                {
                    Console.WriteLine("Skriv in pinkoden för att bekräfta uttaget");
                    Console.WriteLine("\n");

                    bool pincodeCheck = CheckPincode(loggedInUser);

                    // Pinkoden kontrolleras
                    if (!pincodeCheck)
                    {
                        Console.Clear();
                        Console.WriteLine("Programmet avslutas...");
                        return false;
                    }

                    // Här dras det valda beloppet bort från uttagsskontots saldo via saldorrayens index subtraherat med amount.
                    userBalances[loggedInUser, withdrawAccount] -= amount;
                    
                    Console.Clear();
                    Console.WriteLine("Nytt saldo:");
                    Console.WriteLine();
                    
                    ViewAccounts(loggedInUser);
                    
                    Console.WriteLine("Tryck på ENTER för att återgå till huvudmenyn");
                    Console.ReadKey();
                    return true;
                }
                // Här kontrolleras beloppet så att det inte överstiger saldot det valda överföringskontot.
                else
                {
                    Console.WriteLine("För stort belopp");
                    Console.WriteLine();
                    Console.WriteLine("Tryck ENTER för att försöka igen");
                    Console.ReadKey();
                    continue;
                }
            }

        }
        // Metod för pinkodskontroll som returnerar bool tar in int parameter.
        static bool CheckPincode(int loggedInUser)
        {

            // Räknare för att hålla reda på hur många gånger användaren skriver fel pinkod.
            int pincodeAttempts = 0;


            while (true)
            {
                Console.Write("Ange pinkod: ");
                string pincodeInput = Console.ReadLine();

                // Är det fel pinkod så körs if-blocket
                if (pincodeInput != pincodes[loggedInUser])
                {
                    Console.Clear();

                    // Antal felaktiga försök räknas upp och sparas i variabeln pincodeAttempts.
                    pincodeAttempts++;

                    // Försök kvar beräknas genom att subtrahera 3 med antalet felaktiga försök.
                    int attemptsLeft = 3 - pincodeAttempts;

                    Console.WriteLine($"Fel pinkod (Antal försök kvar: {attemptsLeft})");

                    // Om pinkoden blir fel 3 gånger körs detta if block som skriver ut det i konsollen och returnerar false till där metoden anropas.
                    if (pincodeAttempts == 3)
                    {
                        Console.Clear();
                        Console.WriteLine("För många felaktiga försök! Försök igen senare");
                        Console.WriteLine();
                        Console.WriteLine("Tryck ENTER för att avsluta programmet");
                        Console.ReadKey();
                        return false;
                    }
                }
                // Blev pinkoden stämmer, returnera true.
                else
                {
                    Console.Clear();
                    return true;
                }
            }
        }
    }
}


























































