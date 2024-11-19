using System;

namespace HelloWorld
{
    internal class Program
    {

        #region VARIABLES
        static int option = -1;
        static int internalOption = -1;
        static int counterClient = 0;
        static int counterProducts = 0;
        static int ID = 0;
        static int saveID = 0;
        static int types = 1;
        static int selectModify = 0;
        static int indexPurchase = 0;
        static int initialClientID = 100000;
        static int initialProductID = 100000;
        static int index = -1;
        static string password = "";
        static string name = "";
        static bool returnMenu = true;
        static Employee[] allEmployees = new Employee[EMPLOYEE_CAPACITY];
        static Client[] allClients = new Client[CLIENT_CAPACITY];
        static Products[] allProducts = new Products[STORE_CAPACITY];
        static Purchase[] allPurchases = new Purchase[STORE_CAPACITY];


        #endregion

        #region CONSTANTS
        const int EMPLOYEE_CAPACITY = 1;
        const int STORE_CAPACITY = 100;
        const int CLIENT_CAPACITY = 15;
        const int MAX_ID = 999999;
        const int MIN_ID = 100000;
        const int MAX_PASS = 10;
        const int MIN_PASS = 6;
        const double QST = 0.09975;
        const double GST = 0.05;
        const int EMPLOY_ID = 111111;
        const string EMPLOY_PASS = "tester";
        #endregion

        #region MENUS
        static string[] mainMenu = new string[] { "\n--- PRODUCT MANAGEMENT AND SALES SYSTEM ---\n\nMAIN MENU", "\tOption 1: Employee access", "\tOption 2: Client access", "\tOption 3: Exit\nEnter your choice: " };
        static string[] employeeMenu = new string[] { "\n--- PRODUCT MANAGEMENT AND SALES SYSTEM ---\n\nEMPLOYEE PORTAL", "\tOption 1: Create Products", "\tOption 2: Modify one product", "\tOption 3: Display all the products stored by ID", "\tOption 4: Create a Client", "\tOption 5: Modify a Client", "\tOption 6: Display all the Clients sorted by ID", "\tOption 7: Sell", "\tOption 8: Display all the Sell", "\tOption 9: Display total of sales", "\tOption 10: Sign Out: Back to Main Menu\nEnter your choice: " };
        static string[] clientMenu = new string[] { "\n--- PRODUCT MANAGEMENT AND SALES SYSTEM ---\n\nCLIENT PORTAL", "\tOption 1: Display all the Products", "\tOption 2: Display all the Purchase", "\tOption 3: Sign Out: Back to Main Menu\nEnter your choice: " };
        #endregion

        #region STRUCT
        struct Employee
        {
            public int employeeID;
            public string employeePass;
            public string employeeFirstName;
            public string employeeLastName;
            //Parametrized constructor Employee
            public Employee(int e_ID, string e_Pass, string e_f_name, string e_l_name)
            {
                employeeID = e_ID;
                employeePass = e_Pass;
                employeeFirstName = e_f_name;
                employeeLastName = e_l_name;
            }
        }

        struct Client
        {
            public int clientID;
            public string clientPass;
            public string clientFirstName;
            public string clientLastName;
            //Parametrized constructor Client
            public Client(int c_ID, string c_Pass, string c_Firstname, string c_Lastname)
            {
                clientID = c_ID;
                clientPass = c_Pass;
                clientFirstName = c_Firstname;
                clientLastName = c_Lastname;
            }
        }

        struct Products
        {
            public int productID;
            public string productName;
            public double productPrice;
            public int productInStorage;
            //Parametrized constructor Products
            public Products(int p_ID, string p_Name, double p_Price, int p_inStorage)
            {
                productID = p_ID;
                productName = p_Name;
                productPrice = p_Price;
                productInStorage = p_inStorage;
            }
        }

        struct Purchase
        {
            public Client client;
            public Products[] soldProducts;
            public int[] quantities;
            public double subtotal;
            public double taxes;
            public double totalPrice;
            //Parametrized constructor
            public Purchase(Client p_client, Products[] p_allProducts, int[] p_quantities, double p_subtotal, double p_taxes, double p_totalPrice)
            {
                client = p_client;
                soldProducts = p_allProducts;
                quantities = p_quantities;
                subtotal = p_subtotal;
                taxes = p_taxes;
                totalPrice = p_totalPrice;
            }
        }

        #endregion

        static void Main(string[] args)
        {
            CreateEmployee();
            IncludeClients();
            IncludeProducts();

            while (true)
            {
                DateToday();
                option = GetChoice(mainMenu);
                returnMenu = true;
                Console.Clear();

                switch (option)
                {
                    case -1:
                        Console.WriteLine("Please create some clients before.");
                        break;
                    case 1:
                        DateToday();
                        if (SignInEmployee() == false)
                        {
                            break;
                        }
                        while (returnMenu)
                        {
                            DateToday();
                            Console.Write("Employee ({0} {1})\n", allEmployees[0].employeeFirstName, allEmployees[0].employeeLastName);
                            internalOption = GetChoice(employeeMenu);
                            Console.Clear();
                            switch (internalOption)
                            {
                                case 1:
                                    Console.WriteLine("{0}--> Create Products:", employeeMenu[0]);
                                    CreateProducts();
                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.WriteLine("{0}--> Modify one Product:", employeeMenu[0]);
                                    ModifyProduct();
                                    Console.Clear();
                                    break;
                                case 3:
                                    Console.WriteLine("{0}--> Display all the products stored by ID:", employeeMenu[0]);
                                    ShowProducts();
                                    Console.Clear();
                                    break;
                                case 4:
                                    Console.WriteLine("{0}--> Create a Client:", employeeMenu[0]);
                                    CreateClient();
                                    Console.Clear();
                                    break;
                                case 5:
                                    Console.WriteLine("{0}--> Modify a Client:", employeeMenu[0]);
                                    ModifyClient();
                                    Console.Clear();
                                    break;
                                case 6:
                                    Console.WriteLine("{0}--> Display all the Clients sorted by ID:", employeeMenu[0]);
                                    ShowClients();
                                    Console.Clear();
                                    break;
                                case 7:
                                    Console.WriteLine("{0}--> Sell:", employeeMenu[0]);
                                    Sell();
                                    Console.Clear();
                                    break;
                                case 8:
                                    Console.WriteLine("{0}--> Display All the Sales:", employeeMenu[0]);
                                    DisplayAllSales();
                                    Console.Clear();
                                    break;
                                case 9:
                                    Console.WriteLine("{0}--> Display Total of Sales:", employeeMenu[0]);
                                    DisplayTotalSales();
                                    Console.Clear();
                                    break;
                                case 10:
                                    returnMenu = false;
                                    Console.Clear();
                                    break;
                            }
                        }
                        break;

                    case 2:
                        DateToday();
                        if (SignInClient() == false)
                        {
                            break;
                        }
                        while (returnMenu)
                        {
                            DateToday();
                            internalOption = GetChoice(clientMenu);
                            Console.Clear();
                            switch (internalOption)
                            {
                                case 1:
                                    Console.WriteLine("{0}--> Display All the Products:", clientMenu[0]);
                                    //X: for clients, it should display only the products withna quantity > 0
                                    ShowProducts();
                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.WriteLine("{0}--> Dsiplay all My Purchases:", clientMenu[0]);
                                    ShowPurchases();
                                    Console.Clear();
                                    break;
                                case 3:
                                    returnMenu = false;
                                    Console.Clear();
                                    break;
                            }
                        }
                        break;
                    case 3:
                        Exit();
                        break;
                }
            }
        }
        #region FUNCTION
        //FUNCTION MODIFY PRODUCTS
        /// <summary>
        /// Modify products' information by product ID or product name
        /// </summary>
        static void ModifyProduct()
        {
            Console.Write("\n\tSelect the product to modify by ID (Press 1) or by Name (Press 2): ");
            selectModify = ReadIntegerInRange(1, 2);
            switch (selectModify)
            {
                case 1:
                    Console.Write("\nSearch by ID\n\tEnter ID of the product to modify: ");
                    ID = ReadIntegerInRange(MIN_ID, MAX_ID);
                    index = GetIndexByProductId(ID);
                    if (index == -1) { Console.WriteLine("ID not found"); }
                    else
                    {
                        Console.Write("\nProduct ID {0} to Modify:", allProducts[index].productID);
                        Console.WriteLine("\nEnter the New Product Name: ");
                        allProducts[index].productName = ReadString();
                        Console.Write("\nEnter the New Product Price: ");
                        allProducts[index].productPrice = ReadDoubleInRange(0, double.MaxValue);
                        Console.Write("\nEnter the New Product Quantity: ");
                        allProducts[index].productInStorage = ReadIntegerInRange(0, int.MaxValue);
                        Console.Clear();
                    }
                    break;
                case 2:
                    Console.Write("Enter Name of the product to modify: ");
                    name = ReadString();
                    index = GetIndexByProductName(name);
                    if (index == -1) { Console.WriteLine("Name not found."); }
                    else
                    {
                        Console.Write("\nThe product to Modify is {0} with ID {2}: ", allProducts[index].productName, allProducts[index].productID);
                        Console.Write("\nEnter the New Product Name: ");
                        allProducts[index].productName = ReadString();
                        Console.Write("\nEnter the New Product Price: ");
                        allProducts[index].productPrice = ReadDoubleInRange(0, double.MaxValue);
                        Console.Write("\nEnter the New Product Quantity: ");
                        allProducts[index].productInStorage = ReadInteger();
                        Console.Clear();
                    }
                    break;
            }
        }

        //FUNCTION MODIFY CLIENT
        /// <summary>
        /// Modify a client's information
        /// </summary>
        static void ModifyClient()
        {
            Console.Write("\nEnter ID of the Client to modify: ");
            ID = ReadIntegerInRange(MIN_ID, MAX_ID);
            index = GetIndexByClientId(ID);
            if (index == -1) { Console.WriteLine("ID not found"); }
            else
            {
                Console.WriteLine("\tThe Client to modify is:\n\tClient ID: " + allClients[index].clientID + "\n\tFirst Name: " +
                             allClients[index].clientFirstName + "\n\tLast Name: " + allClients[index].clientLastName + "\n\tClient Password: " +
                             allClients[index].clientPass);
                Console.Write("\nEnter the New Client First Name: ");
                allClients[index].clientFirstName = ReadString();
                Console.Write("\nEnter the New Client Last Name: ");
                allClients[index].clientLastName = ReadString();
                Console.Write("\nEnter the New Client Password: ");
                allClients[index].clientPass = ValidateLongString(MIN_PASS, MAX_PASS);
            }
        }

        //FUNCTION SEE ALL ITEMS OF INVENTARY
        /// <summary>
        /// Function that show al the data of the Inventary
        /// </summary>
        static void ShowProducts()
        {
            Console.WriteLine("\tPRODUCT ID\t|\t" +
                "\tPRODUCT NAME\t\t|\t" +
                "PRODUCT PRICE\t\t|\t" +
                "\tQUANTITY IN STORAGE");
            for (int i = 0; i < counterProducts; i++)
            {
                if (allProducts[i].productName != "")
                {
                    Console.WriteLine("\t" + allProducts[i].productID + "\t\t|\t" +
                     "\t" + allProducts[i].productName + "\t\t|\t" +
                     "\t" + allProducts[i].productPrice + "\t\t|\t" +
                     "\t" + allProducts[i].productInStorage);
                }
            }
            Console.ReadLine();
        }

        //SHOW ALL CLIENTS
        /// <summary>
        /// Show all clients' information
        /// </summary>
        static void ShowClients()
        {
            Console.WriteLine("\tCLIENT ID\t|\t" +
                "\tFIRST NAME\t\t|\t" +
                "\tLAST NAME\t\t|\t" +
                "\tPASSWORD");
            for (int i = 0; i < counterClient; i++)
            {
                if (allClients[i].clientFirstName != "")
                {
                    Console.WriteLine("\t" + allClients[i].clientID + "\t\t|\t" +
                    "\t" + allClients[i].clientFirstName + "\t\t|\t" +
                     "\t" + allClients[i].clientLastName + "\t\t|\t" +
                     "\t" + allClients[i].clientPass);
                }
            }
            Console.ReadLine();
        }
        //FUNCTION SEE ALL PURCHASES
        /// <summary>
        /// Function that show al the data of the Purchases
        /// </summary>
        static void ShowPurchases()
        {
            if (indexPurchase != 0)
            {
                for (int i = 0; i <= indexPurchase; i++)
                {
                    if (allPurchases[i].client.clientID == saveID)
                    {
                        double total = 0;
                        for (int z = 0; z <= counterProducts; z++)
                        {
                            if (allPurchases[i].soldProducts[z].productName != null)
                            {
                                double sum = (allPurchases[i].quantities[z] * allPurchases[i].soldProducts[z].productPrice) + (allPurchases[i].quantities[z] * allPurchases[i].soldProducts[z].productPrice) * (GST + QST);
                                Console.WriteLine("\n\tPRODUCT NAME\t" + allPurchases[i].soldProducts[z].productName +
                             "\n\tPRODUCT PRICE\t$" + allPurchases[i].soldProducts[z].productPrice +
                             "\n\tQUANTITIES\t" + allPurchases[i].quantities[z] +
                             "\n\tSUBTOTAL W/TAX\t$" + sum);
                                total += sum;
                            }
                        }
                        Console.WriteLine("\n\tTOTAL AMOUNT:\t$" + total + "\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("No sales recorded.");
                Console.ReadLine();
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }


        //VALIDATION OF INVENTARY IS IN STOCK
        /// <summary>
        /// Validation of iventory is in stock
        /// </summary>
        /// <param name="productId">Product ID</param>
        static bool IsInStock(int productId)
        {
            if (allProducts[GetIndexByProductId(productId)].productInStorage > 0)
            {
                return true;
            }
            return false;
        }

        //FUNCTION CREATE PRODUCTS
        /// <summary>
        /// Function to create products
        /// </summary>
        static void CreateProducts()
        {
            if (counterProducts <= STORE_CAPACITY)
            {
                while (counterProducts <= STORE_CAPACITY)
                {
                    Console.Write("\nThe Product ID is: {0}", initialProductID);
                    allProducts[counterProducts].productID = initialProductID;
                    Console.Write("\nEnter the Product Name: ");
                    allProducts[counterProducts].productName = ReadString();
                    Console.Write("Enter the Product Price: ");
                    allProducts[counterProducts].productPrice = ReadDoubleInRange(0, double.MaxValue);
                    Console.Write("Enter the Product Quantity: ");
                    allProducts[counterProducts].productInStorage = ReadInteger();//X: could be the quantity less than 0?
                    initialProductID++;
                    counterProducts++;
                    Console.WriteLine("\nReturn to Main Menu?");
                    if (ReadBoolean())
                    {
                        Console.Clear();
                        break;
                    }
                    Console.Clear();
                }
            }
            else { Console.WriteLine("Full storage capacity."); }
        }

        //FUNCTION CREATE CLIENT
        /// <summary>
        /// Function to create clients
        /// </summary>
        static void CreateClient()
        {
            if (counterClient <= CLIENT_CAPACITY)
            {
                while (counterClient <= CLIENT_CAPACITY)
                {
                    Console.Write("\nThe Client ID is: {0}", initialClientID);
                    allClients[counterClient].clientID = initialClientID;
                    Console.Write("\nEnter the First Name of Client: ");
                    allClients[counterClient].clientFirstName = ReadString();
                    Console.Write("Enter the Last Name of Client: ");
                    allClients[counterClient].clientLastName = ReadString();
                    Console.Write("Enter the Password for the Client: ");
                    allClients[counterClient].clientPass = ValidateLongString(MIN_PASS, MAX_PASS);
                    initialClientID++;
                    counterClient++;
                    Console.WriteLine("\nReturn to Main Menu?");
                    if (ReadBoolean())
                    {
                        Console.Clear();
                        break;
                    }
                    Console.Clear();
                }
            }
            else { Console.WriteLine("Full storage capacity."); }
        }

        //FUNCTION SIGN IN EMPLOYEE
        ///<sumary>
        ///Function SignIn for Employee with a static ID and Pasword
        ///</sumary>         
        static bool SignInEmployee()
        {
            while (ID != allEmployees[0].employeeID || password != allEmployees[0].employeePass)
            {
                Console.WriteLine("\n--- PRODUCT MANAGEMENT AND SALES SYSTEM ---\n");
                Console.Write("EMPLOYEE LOGING PAGE:\n\tEnter ID:\t");
                ID = ReadIntegerInRange(MIN_ID, MAX_ID);
                Console.Write("\tEnter Password:\t");
                password = ValidateLongString(MIN_PASS, MAX_PASS);
                if (ID != allEmployees[0].employeeID || password != allEmployees[0].employeePass)
                {
                    Console.WriteLine("\n\tWrong ID or Password.");
                    Console.WriteLine("\nReturn to Main Menu?");
                    if (ReadBoolean())
                    {
                        Console.Clear();
                        ID = 0;
                        password = "";
                        return false;
                    }
                    else { Console.Clear(); }
                }
                Console.Clear();
            }
            ID = 0;
            password = "";
            return true;
        }

        //FUNCTION SIGN IN CLIENT
        /// <summary>
        /// Function that Sign in a Client
        /// </summary>
        static bool SignInClient()
        {
            while (SearchIdClient(ID) == false || password != EMPLOY_PASS)
            {
                Console.WriteLine("\n--- PRODUCT MANAGEMENT AND SALES SYSTEM ---\n");
                Console.Write("CLIENT LOGING PAGE:\n\tEnter ID:\t");
                ID = ReadIntegerInRange(MIN_ID, MAX_ID);
                Console.Write("\tEnter Password:\t");
                password = ValidateLongString(MIN_PASS, MAX_PASS);
                if (SearchIdClient(ID) == false || SearchPassClient(password) == false)
                {
                    Console.WriteLine("\n\tWrong ID or Password.");
                    Console.WriteLine("\nReturn to Main Menu?");
                    if (ReadBoolean())
                    {
                        Console.Clear();
                        ID = 0;// Restart the variable ID
                        password = "";//Restart the password variable
                        return false;
                    }
                    else { Console.Clear(); }
                }
                Console.Clear();
            }
            saveID = ID; //Saved ID for create the query of the purchases of the specific client
            ID = 0; //Restart the variable ID
            password = ""; //Restart the password variable
            return true;
        }

        //FUNCTION SEARCH CLIENT ID IN THE ARRAY CLIENT
        /// <summary>
        /// Function to search client ID in the client array
        /// </summary>
        /// <param name="IDtoSearch">Client ID</param>
        static bool SearchIdClient(int IDtoSearch)
        {
            counterClient = 0;//x: it should be updated only when the client is created
            for (int i = 0; i < allClients.GetLength(0); i++)
            {
                if (IDtoSearch == allClients[i].clientID)
                {
                    return true;
                }
                else { counterClient++; }
            }
            if (counterClient == allClients.GetLength(0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //FUNCTION SEARCH CLIENT PASSWORD IN THE ARRAY CLIENT
        /// <summary>
        /// Function to search client password in the client array
        /// </summary>
        /// <param name="PassToSearch">Client Password</param>
        static bool SearchPassClient(string PassToSearch)
        {
            counterClient = 0;
            for (int i = 0; i < allClients.GetLength(0); i++)
            {
                if (PassToSearch == allClients[i].clientPass)
                {
                    return true;
                }
                else { counterClient++; }
            }
            if (counterClient == allClients.GetLength(0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //VALIDATION THE LONG OF THE STRING FOR PASSWORD
        /// <summary>
        /// Validation of the length of the string for password
        /// </summary>
        /// <param name="minimun">Minimum length of password</param>
        /// <param name="maximun">Maximum length of password</param>
        static string ValidateLongString(int minimun, int maximun)
        {
            string toReturn = ReadString();
            while (toReturn.Length > minimun && toReturn.Length < maximun)
            {
                Console.Write("Invalid Password, try again:");
                toReturn = ReadString();
            }
            return toReturn;
        }

        //FUNCTION MENU and GET CHIOCE
        ///// <summary>
        /// Get choice from user
        /// </summary>
        /// <param name="menu">Menu</param>
        static int GetChoice(string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(menu[i]);
            }
            return ReadIntegerInRange(1, menu.Length - 1);
        }

        //FUNCTION EXIT
        /// <summary>
        /// Exit the programm.
        /// </summary>
        private static void Exit()
        {
            Console.WriteLine("Are you sure to exit? ");
            if (ReadBoolean())
            {
                Console.WriteLine("Thank You!");
                Environment.Exit(0);
            }
        }

        //FUNCTION DATE TODAY
        /// <summary>
        /// Show the date of today
        /// </summary>
        static void DateToday()
        {
            DateTime thisDay = DateTime.Today;
            Console.WriteLine(thisDay.ToString("D"));
        }

        //GET PRODUCT INDEX BY ID
        /// <summary>
        /// Get product index by using product ID.
        /// </summary>
        /// <param name="id">Product ID</param>
        static int GetIndexByProductId(int id)
        {
            for (int i = 0; i < STORE_CAPACITY; i++)
            {
                if (id == allProducts[i].productID)
                {
                    return i;
                }
            }
            return -1;
        }

        //GET PRODUCT INDEX BY NAME
        /// <summary>
        /// Get product index by using product name.
        /// </summary>
        /// <param name="name">Product Name</param>
        static int GetIndexByProductName(string name)
        {
            for (int i = 0; i < counterProducts; i++)
            {
                if (name == allProducts[i].productName)
                {
                    return i;
                }
            }
            return -1;
        }

        //GET ClIENT INDEX BY ID
        /// <summary>
        /// Get client index by using client ID.
        /// </summary>
        /// <param name="id">Client ID</param> 
        static int GetIndexByClientId(int id)
        {
            for (int i = 0; i < CLIENT_CAPACITY; i++)
            {
                if (id == allClients[i].clientID)
                {
                    return i;
                }
            }
            return -1;
        }

        //SELL PRODUCTS
        /// <summary>
        /// Sell products
        /// </summary>
        static void Sell()
        {
            //record the client who bought products in STRUCT Purchase.client
            Console.Write("\nEnter Client ID: ");
            int clientId = ReadIntegerInRange(MIN_ID, MAX_ID);
            int indexClient = GetIndexByClientId(clientId);
            if (indexClient != -1)
            {
                allPurchases[indexPurchase].client = allClients[indexClient];
                //record the sold products 
                double subtotal = 0;
                double taxes = 0;
                double totalPrice = 0;
                bool next = true;
                int i = 0;
                int quantity = 0;
                Products[] soldItems = new Products[STORE_CAPACITY];
                int[] soldItemsQuantities = new int[STORE_CAPACITY];

                while (next == true)
                {
                    Console.Write("\nEnter the Product ID: ");
                    int productId = ReadIntegerInRange(MIN_ID, MAX_ID);
                    int indexProduct = GetIndexByProductId(productId);
                    if (indexProduct != -1)
                    {
                        if (IsInStock(productId) == true)
                        {
                            Console.Write("\tEnter the Product Quantity to Sell: ");
                            quantity = ReadIntegerInRange(1, allProducts[indexProduct].productInStorage);
                            allProducts[indexProduct].productInStorage -= quantity;
                            subtotal += allProducts[indexProduct].productPrice * quantity;
                            taxes = subtotal * (QST + GST);
                            totalPrice = subtotal + taxes;
                            soldItems[i] = allProducts[indexProduct];
                            soldItemsQuantities[i] = quantity;
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("\nThe product is out of stock.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThe Product doesn't exist.");// and then return to the menu(code needed)
                    }
                    Console.WriteLine("\nReturn to Main Menu?");
                    if (ReadBoolean())
                    {
                        next = false;
                        Console.Clear();
                    }
                    allPurchases[indexPurchase] = new Purchase(allClients[indexClient], soldItems, soldItemsQuantities, subtotal, taxes, totalPrice);
                }
                if (totalPrice != 0)
                {
                    indexPurchase++;
                }
            }
            else
            {
                Console.WriteLine("\nThe client doesn't exist.");// and then return to the menu
                Console.ReadLine();
                Console.Clear();
            }
        }

        //DISPLAY ALL THE SALES (CASE8)
        /// <summary>
        /// Display all sales by selling order
        /// </summary>
        static void DisplayAllSales()
        {
            //double verification = 0;
            for (int i = 0; i < indexPurchase; i++)
            {
                if (SearchIdClient(allPurchases[i].client.clientID) == true)
                {
                    Console.Write("\nClient ID: " + allPurchases[i].client.clientID);
                    Console.Write("\tName: " + allPurchases[i].client.clientFirstName);
                    Console.Write("\tLast Name: " + allPurchases[i].client.clientLastName);
                    for (int j = 0; j < counterProducts; j++)
                    {
                        if (allPurchases[i].soldProducts[j].productName != null)
                        {
                            Console.Write("\n\tProduct ID: " + allPurchases[i].soldProducts[j].productID);
                            Console.Write("\n\tProduct Name: " + allPurchases[i].soldProducts[j].productName);
                            Console.Write("\n\tPrice : " + allPurchases[i].soldProducts[j].productPrice);
                            Console.Write("\n\tIn Stock: " + allPurchases[i].soldProducts[j].productInStorage);
                            Console.Write("\n\tSolds: " + allPurchases[i].quantities[j] + "\n");
                        }
                    }
                    Console.Write("\nSubTotal: $ " + allPurchases[i].subtotal);
                    Console.Write("\nTaxes (QST+GST): $ " + allPurchases[i].taxes);
                    Console.Write("\nTotal Amount: $ " + allPurchases[i].totalPrice + "\n");
                }
            }
            Console.WriteLine("Press enter to return...");
            Console.ReadLine();
        }

        //DISPLAY TOTAL OF SALES (CASE9)
        /// <summary>
        /// Display total amount of sales.
        /// </summary>
        static void DisplayTotalSales()
        {
            double t_subtotal = 0;
            double t_taxes = 0;
            double t_totalPrice = 0;
            for (int i = 0; i <= indexPurchase; i++)
            {
                t_subtotal += allPurchases[i].subtotal;
                t_taxes += allPurchases[i].taxes;
                t_totalPrice += allPurchases[i].totalPrice;
            }
            Console.Write("\nSubtotal of all sales: $ " + t_subtotal);
            Console.Write("\nTaxes of all sales: $ " + t_taxes);
            Console.Write("\nTotal of all sales: $ " + t_totalPrice);
            Console.ReadLine();
        }

        #endregion

        #region VALIDATIONS
        //----------- VALIDATION DATA -----------------------
        //VALIDACION STRING
        /// <summary>
        /// Validation of string
        /// </summary>
        static string ReadString()
        {
            string toReturn = Console.ReadLine();
            bool canConvert = int.TryParse(toReturn, out int number);
            while (canConvert == true || System.String.IsNullOrEmpty(toReturn) || toReturn == " ")
            {
                Console.Write("Invalid Name, try again:");
                toReturn = Console.ReadLine();
                canConvert = int.TryParse(toReturn, out number);
            }
            return toReturn;
        }

        //VALIDATION INTERGER
        /// <summary>
        /// Validation of input integer
        /// </summary>
        static int ReadInteger()
        {
            int toReturn = 0;

            while (!Int32.TryParse(Console.ReadLine(), out toReturn))
            {
                Console.Write("Invalid Data, please try again: ");
            }
            return toReturn;
        }

        //VALIDATION DOUBLE
        /// <summary>
        /// Validation of input double
        /// </summary>
        static double ReadDouble()
        {
            double toReturn = 0;
            while (!Double.TryParse(Console.ReadLine(), out toReturn))
            {
                Console.Write("Invalid Data, please try again: ");
            }
            return toReturn;
        }
        //VALIDATION DOUBLE IN RANGE
        /// <summary>
        /// Validation of input double in range
        /// </summary>
        /// <param name="min">Minimal Double</param>  
        /// <param name="max">Maximum Double</param> 
        static double ReadDoubleInRange(double min, double max)
        {
            double toReturn = 0;
            while (!Double.TryParse(Console.ReadLine(), out toReturn))
            {
                Console.Write("Invalid value, please try again: ");
            }
            while (!IsWithinRangeDouble(toReturn, min, max))
            {
                Console.Write("Invalid value, please try again: ");
            }
            return toReturn;
        }

        //VALIDATION INTERGER IN RANGE
        /// <summary>
        /// Validation of integer in range
        /// </summary>
        /// <param name="min">Minimal Integer</param>  
        /// <param name="max">Maximum Integer</param> 
        static int ReadIntegerInRange(int min, int max)
        {
            int toReturn = ReadInteger();
            while (!IsWithinRange(toReturn, min, max))
            {
                Console.Write($"The value must be between {min} and {max}. Please try again: ");
                toReturn = ReadInteger();
            }
            return toReturn;
        }

        //VALIDATION BOOLEAN IN RANGE INTERGER
        /// <summary>
        /// Validation of integer in range (boolean)
        /// </summary>
        /// <param name="num">Number needed to validate</param> 
        /// <param name="min">Minimal Integer</param>  
        /// <param name="max">Maximum Integer</param>  
        static bool IsWithinRange(int num, int min, int max)
        {
            return num >= min && num <= max;
        }

        //VALIDATION BOOLEAN IN RANGE DOUBLE
        /// <summary>
        /// Validation of double in range (boolean)
        /// </summary>
        /// <param name="num">Number needed to validate</param> 
        /// <param name="min">Minimal double</param>  
        /// <param name="max">Maximum double</param>
        static bool IsWithinRangeDouble(double num, double min, double max)
        {
            return num >= min && num <= max;
        }

        //VALIDATION FOR TWO CONDITION, 1 (YES) AND 2 (NO)
        /// <summary>
        /// Validation of two options: 1 for yes, 2 for no
        /// </summary>
        static bool ReadBoolean()
        {
            Console.Write("Enter 1 for YES, or 2 for NO: ");
            return ReadIntegerInRange(1, 2) == 1;
        }
        #endregion

        #region DATA TO STRUCT
        //FUNCTION TO CREATE A EMPLOYEE
        /// <summary>
        /// Initialization of one employee
        /// </summary>
        static void CreateEmployee()
        {
            allEmployees[0].employeeID = EMPLOY_ID;
            allEmployees[0].employeePass = EMPLOY_PASS;
            allEmployees[0].employeeFirstName = "Liming";
            allEmployees[0].employeeLastName = "Eguia";
        }
        /// <summary>
        /// Initialization of 10 clients 
        /// </summary>
        static void IncludeClients()
        {
            for (int i = 0; i <= 10; i++)
            {
                allClients[i].clientID = initialClientID;
                allClients[i].clientPass = EMPLOY_PASS;
                allClients[i].clientFirstName = "TestName" + i;
                allClients[i].clientLastName = "TestLast" + i;
                initialClientID++;
                counterClient++;
            }
        }
        /// <summary>
        /// Initialization of 10 products
        /// </summary>
        static void IncludeProducts()
        {
            for (int i = 0; i < 10; i++)
            {
                allProducts[i].productID = initialProductID;
                allProducts[i].productName = "ProductTest" + i;
                allProducts[i].productInStorage = i * 10;
                allProducts[i].productPrice = i * 100;
                initialProductID++;
                counterProducts++;
            }
        }

        #endregion
    }
}