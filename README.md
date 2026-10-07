### 

### **1. Introduktion**



##### **1.1 Programmets struktur**



Välkommen till **BankApp**!



Projektet är uppbyggt för att efterlikna en bankomat där 5 användare har sina bankuppgifter sparade och kan utföra diverse bankärenden. Projektet är uppdelat i 6 metoder.



**1. static int TryLogin():** Kontrollerar inloggningsuppgifter och returnerar en *int*. Antingen *i* som är den inloggades *index* eller *-1* som ger att inloggningen misslyckades.



**2. static bool MainMenu(int loggedInUser**): Tar emot inparameter, användarens index (loggedInUser). Visar sen en *menyöversikt* och styr användarens menyval med en switch-sats. Returnerar också en *bool*, *(false)* ifall användaren skriver fel pin tre gånger i *Withdraw()*, eller *(true)* när användaren väljer *val 4*: "*Logga ut*".



**3. static void ViewAccounts(int loggedInUser):** Tar emot inparameter, användarens index (loggedInUser). Skriver sen ut konton och kontosaldon.



**4. static void Transfer(int loggedInUser):** Tar emot inparameter, användarens index (loggedInUser). Hjälper sedan användaren att välja konton att överföra mellan och summa.



**5. static bool Withdraw(int loggedInUser):** Tar emot inparameter, användarens index (loggedInUser). Hjälper sedan användaren att välja konto att göra uttag från och summa. Returnerar också bool false; om bekräftningspinkoden blir fel 3 gånger eller true om exempelvis uttaget lyckades.



**6. static bool CheckPincode(int loggedInUser):** Tar emot inparameter, användarens index (loggedInUser). Kontrollerar om användarens pinkodsinmatning matchar tillhörande användarindex.



TryLogIn() anropas i början av Main() där den returnerar den inloggades index (loggedInUser), och som sedan förs vidare in i Main() som argument i metodanropet för MainMenu(loggedInUser). När MainMenu() nås får användaren välja mellan 4 bankalternativ som styrs av en switch-sats som styrs av användarens input. Här anropas de andra metoderna genom switch-satsen för att utföra dem olika bankomat funktionerna.

##### 

##### 1.2 Körning av programmet



Vid körning av programmet finns dem 5 användaruppgifterna sparade i arrayerna som syns i början av programmet. Exempelvis kan du testa att logga in med användarnamn: Kenta med pinkoden: 1234 som kommer från index 0 i arrayerna. Det tar dig till huvudmenyn där du kan navigera genom de fyra menyalternativen för att utföra saker som överföring mellan konton eller uttag. Efter att du utfört dina plikter kan alternativ 4 väljas i menyn för att logga ut och återgå till inloggningen.

### 

### **2. Felsökning och reflektion**



**1.** Jag missade först att det skulle läggas till en kort kommentar för varje commit/push och klickade därför bara på push utan meddelanden dem första gångerna. De senare commitsen inkluderar dock detta och jag försökte så tydligt som möjligt sammanfatta de senaste ändringarna för att beskriva hur långt jag hade kommit vid tidpunkten jag commita.



**2.** Jag skulle kunna lägga till logik i koden som kontrollerar direkt om användarnamnet inte finns med i arrayen och direkt flagga som fel inloggningsuppgifter. Det jag har nu istället är att en kontroll först görs efter det att användaren har skrivit in både pin och användarnamn.



**3.** Jag inser att jag hade kunnat anropa ViewAccounts() i både Transfer() och Withdraw() istället för att knappa in två identiska for-loopar i dem metoderna. Problemet var bara det att om ViewAccounts() anropas inkluderar anropet utskrifter som "konton: " som jag bedömde vara ej relevant i den delen av koden. Jag har nu ändrat så att viewaccount anropas i dessa metoder och tagit bort for looparna.



**4.** När jag kommenterade delar i Withdraw() insåg jag att ännu ett stycke upprepas där beloppen och valda konton hade kunnat kontrolleras med en metod exempelvis ValidAmount() eller ValidAccount(). det hade kunnat korta ned Transfer() och Withdraw() rejält men jag sparar den iden till nästa projekt.



**5.** Utöver att pinkoden används först när användaren loggar in så kontrollerar min kod även pinkoden igen när menyval Withdraw() och Transfer() väljs. Jag vet inte om det var nödvändigt att ha denna för Transfer() då instruktionerna nämner att detta specifik behövs i Withdraw() för att bekräfta att användaren vill göra ett uttag. Jag kan i så fall lägga till det i efterhand.



**6.** jag försökte hitta ett sätt för programmet att stängas ned efter att man knappat fel pin 3 gånger i Withdraw() och slutade med att jag ändrade Withdraw() till att returnera en bool för att returnera en false som en staffetpinne tillbaka till Mainmenu() som sen i sin tur returnera en bool tillbaka till Main där den träffar return; för att avsluta programmet.



**7.** jag gjorde TrylogIn() kortare genom att anropa CheckPincode() direkt i metoden, istället för att behöva upprepa samma typ av logik som redan är skriven i CheckPincode().



### 3\. Resonemang för val av programstruktur



Eftersom att uppgiften är att skapa en bankomat som utför uppgifter för den inloggade användaren så kändes det smart att dela upp koden i olika metoder för dem olika funktionerna. Exempelvis att huvudmenyn byggs av en metod som visar en meny med olika alternativ för banktjänster som att ta ut pengar. När ett alternativ sedan väljs ska en annan metod anropas från meny-metoden och innehålla kod som hjälper användaren att utföra den valda banktjänsten. Sedan tyckte jag också att det var värt att lägga till enskilda metoder som styr inloggning och pinkodskontroll. Allt detta för att strukturera koden tydligare och göra det enklare att felsöka. Det jag har lärt mig från detta arbete är främst val av metoder, grundläggande logik och att dela in arbetet i mindre delar.

