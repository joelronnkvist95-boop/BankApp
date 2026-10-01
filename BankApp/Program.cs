namespace BankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till banken");

            while (true)
            {
                bool loggedIn = false;
                
                Console.Write("Ange användarnamn: ");
                string usernameInput = Console.ReadLine();

                if(usernameInput == username)
                {
                    Console.Write("Ange Lösenord: ");
                    string passwordInput = Console.ReadLine();
                    
                    if(passwordInput == password)
                    {
                        loggedIn = true;

                        while (loggedIn)
                        {
                            Console.WriteLine($"Välkommen {username}!");
                            Console.WriteLine("1. Se dina konton och saldon");
                            Console.WriteLine("2. Överföring mellan konton");
                            Console.WriteLine("3. Ta ut pengar");
                            Console.WriteLine("4. Logga ut");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Fel lösenord. Försök igen");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Fel användarnamn. Försök igen");
                    Console.ReadKey();
                }
                   


        }
    }
}
