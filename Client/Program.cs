using Authentication.Lib;
using Common.Lib;
using DBLayer;
using BusinessLayer;
namespace Client
{
    public class Program
    {
        private void AdminMenu()
        {
            Console.WriteLine("1: Add a Product\n" +
                "2: View all products\n" +
                "3: View products of an user\n" +
                "4: Delete a product \n" +
                "5: Update a product\n" +
                "6: View product by productId\n" +
                "7: Update an user\n" +
                "8: View all users\n" +
                "9: View an User\n" +
                "10: Delete an User\n" +
                "11: Logout\n");
        }
        private void UserMenu()
        {
            Console.WriteLine("1: Add a Product\n" +
                "2: Delete a product \n" +
                "3: Update a product\n" +
                "4: View product by productId\n" +
                "5: View all products\n" +
                "6: Delete your account\n" +
                "7: Logout\n");
        }
        private (string username, string password) LoginMenu()
        {
            Console.WriteLine("PLEASE LOGIN: ");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            return (username, password);
        }
        private (string username, string password, bool isAdmin) SignupMenu()
        {
            Console.WriteLine("PLEASE SIGNUP");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.Write("Admin : Y or N");
            var adminResponse = Console.ReadLine().ToLower();
            bool isAdmin;
            while (true)
            {
                if (adminResponse == "y")
                {
                    isAdmin = true;
                    break;
                }
                else if (adminResponse == "n")
                {
                    isAdmin = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Please Enter valid choice: ");
                }
            }
            return (username, password, isAdmin);
        }
        static void Main(string[] args)
        {
            //Client instance 
            Program client = new Program();

            Console.WriteLine("WELCOME TO INVENTORY MANAGEMENT SYSTEM");

            string username = "", password = "";
            bool isAdmin;

            IUserDB userDb = new UserDb();
            IProductDB productDb = new ProductDb();
            UserAuthentication auth = new UserAuthentication(userDb);
            UserModel loggedInUser;
            InventoryController controller;
            while (true)
            {
                //Sign up Menu
                Console.WriteLine("1:Login\n2:Signup\n3:Exit");
                Console.WriteLine("Enter choice: ");
                int authChoice = Convert.ToInt32(Console.ReadLine());
                switch (authChoice)
                {
                    case 1:
                        (username, password) = client.LoginMenu();
                        loggedInUser = auth.Login(username, password);
                        if (loggedInUser == null) break;
                        controller = new BusinessLayer.InventoryController(productDb, userDb, loggedInUser);
                        if (loggedInUser != null && loggedInUser.isAdmin == true)
                        {
                            //Operations if the user is an admin..
                            while (true)
                            {
                                //Displaying Admin Menu
                                client.AdminMenu();
                                Console.Write("Enter choice: ");
                                int adminChoice = Convert.ToInt32(Console.ReadLine());
                                if (adminChoice == 11) break;
                                switch (adminChoice)
                                {
                                    case 1:
                                        //Input Product details
                                        Console.WriteLine("Enter product Name: ");
                                        string productName = Console.ReadLine();
                                        Console.WriteLine("Enter product Price: ");
                                        double productPrice = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter product Quantity: ");
                                        int productQuantity = Convert.ToInt32(Console.ReadLine());
                                        //Creating and passing new product
                                        controller.AddProduct(new ProductModel(productName, productPrice, productQuantity));
                                        break;
                                    case 2:
                                        //Get all products
                                        var products = controller.GetAllProducts();
                                        foreach (var product in products)
                                        {
                                            Console.WriteLine(product.ToString());
                                        }
                                        break;
                                    case 3:
                                        //View products of an user
                                        Console.WriteLine("Enter UserId: ");
                                        string userId = Console.ReadLine();
                                        var requiredProducts = controller.GetProductsByUserId(userId);
                                        foreach (var product in requiredProducts)
                                        {
                                            Console.WriteLine(product.ToString());
                                        }
                                        break;
                                    case 4:
                                        //Delete a product
                                        Console.WriteLine("Enter productId: ");
                                        string productIdToDelete = Console.ReadLine();
                                        controller.DeleteProduct(productIdToDelete);
                                        break;
                                    case 5: //Update a product
                                        Console.WriteLine("Enter productId: ");
                                        string productIdToUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter product Name: ");
                                        string productNameUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter product Price: ");
                                        double productPriceUpdate = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter product Quantity: ");
                                        int productQuantityUpdate = Convert.ToInt32(Console.ReadLine());
                                        var updateProduct = new ProductModel(productNameUpdate, productPriceUpdate, productQuantityUpdate);
                                        controller.UpdateProduct(productIdToUpdate, updateProduct);
                                        break;
                                    case 6:
                                        //view product by product id
                                        Console.WriteLine("Enter product Id: ");
                                        string productId = Console.ReadLine();
                                        var searchedProduct = controller.GetProduct(productId);
                                        Console.WriteLine(searchedProduct.ToString());
                                        break;
                                    case 7:
                                        //UPDATE USER
                                        Console.WriteLine("Enter user Id: ");
                                        string userIdToUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter username: ");
                                        string usernameToUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter password: ");
                                        string passwordToUpdate = Console.ReadLine();
                                        Console.WriteLine("Is Admin: Y or N");
                                        string adminUpdate = Console.ReadLine().ToLower();
                                        bool isAdminUpdated;
                                        while (true)
                                        {
                                            if (adminUpdate == "y")
                                            {
                                                isAdminUpdated = true;
                                                break;
                                            }
                                            else if (adminUpdate == "n")
                                            {
                                                isAdminUpdated = false;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please Enter valid choice: ");
                                            }
                                        }
                                        controller.UpdateUser(userIdToUpdate, new UserModel(userIdToUpdate, username, password, isAdminUpdated));
                                        break;
                                    case 8: //VIEW ALL USERS
                                        var users = controller.GetAllUsers();
                                        foreach (var user in users)
                                        {
                                            Console.WriteLine(user.ToString());
                                        }
                                        break;
                                    case 9: //VIEW AN USER
                                        Console.WriteLine("Enter userid: ");
                                        string userIdToSearch = Console.ReadLine();
                                        var requiredUser = controller.GetUser(userIdToSearch);
                                        Console.WriteLine(requiredUser.ToString());
                                        break;
                                    case 10: //DELETE AN USER
                                        Console.WriteLine("Enter userid: ");
                                        string userIdToDelete = Console.ReadLine();
                                        controller.DeleteUser(userIdToDelete);
                                        break;
                                    default:
                                        Console.WriteLine("Enter a valid choice...");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            //Operations if the user is not an admin
                            while (true)
                            {
                                client.UserMenu();
                                Console.Write("Enter choice: ");
                                int UserChoice = Convert.ToInt32(Console.ReadLine());
                                if (UserChoice == 7) break;
                                switch (UserChoice)
                                {
                                    //1: Add a Product
                                    case 1: //Input Product details
                                        Console.WriteLine("Enter product Name: ");
                                        string productName = Console.ReadLine();
                                        Console.WriteLine("Enter product Price: ");
                                        double productPrice = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter product Quantity: ");
                                        int productQuantity = Convert.ToInt32(Console.ReadLine());
                                        //Creating and passing new product
                                        controller.AddProduct(new ProductModel(productName, productPrice, productQuantity));
                                        break;
                                    //2: Delete a product 
                                    case 2:
                                        Console.WriteLine("Enter productId: ");
                                        string productIdToDelete = Console.ReadLine();
                                        controller.DeleteProduct(productIdToDelete);
                                        break;
                                    //3: Update a product
                                    case 3:
                                        Console.WriteLine("Enter productId: ");
                                        string productIdToUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter product Name: ");
                                        string productNameUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter product Price: ");
                                        double productPriceUpdate = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter product Quantity: ");
                                        int productQuantityUpdate = Convert.ToInt32(Console.ReadLine());
                                        var updateProduct = new ProductModel(productNameUpdate, productPriceUpdate, productQuantityUpdate);
                                        controller.UpdateProduct(productIdToUpdate, updateProduct);
                                        break;
                                    //4: View product by productId
                                    case 4:
                                        Console.WriteLine("Enter product Id: ");
                                        string productId = Console.ReadLine();
                                        var searchedProduct = controller.GetProduct(productId);
                                        Console.WriteLine(searchedProduct.ToString());
                                        break;
                                    //5: View all products
                                    case 5:
                                        var products = controller.GetAllProducts();
                                        foreach (var product in products)
                                        {
                                            Console.WriteLine(product.ToString());
                                        }
                                        break;
                                    //6: Delete an User
                                    case 6:
                                        controller.DeleteUser(loggedInUser.Id);
                                        break;
                                    default:
                                        Console.WriteLine("Please enter a valid choice!!");
                                        break;
                                }
                            }
                        }
                        break;
                    case 2:
                        //Getting signup/user credentials
                        (username, password, isAdmin) = client.SignupMenu();
                        //Sign up
                        auth.Signup(username, password, isAdmin);
                        //Login
                        loggedInUser = auth.Login(username, password);
                        controller = new InventoryController(productDb, userDb, loggedInUser);
                        if (loggedInUser.isAdmin == true)
                        {
                            //Operations if the user is an admin..
                            while (true)
                            {
                                //Displaying Admin Menu
                                client.AdminMenu();
                                Console.Write("Enter choice: ");
                                int adminChoice = Convert.ToInt32(Console.ReadLine());
                                if (adminChoice == 11) break;
                                switch (adminChoice)
                                {
                                    case 1:
                                        //Input Product details
                                        Console.WriteLine("Enter product Name: ");
                                        string productName = Console.ReadLine();
                                        Console.WriteLine("Enter product Price: ");
                                        double productPrice = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter product Quantity: ");
                                        int productQuantity = Convert.ToInt32(Console.ReadLine());
                                        //Creating and passing new product
                                        controller.AddProduct(new ProductModel(productName, productPrice, productQuantity));
                                        break;
                                    case 2:
                                        //Get all products
                                        var products = controller.GetAllProducts();
                                        foreach (var product in products)
                                        {
                                            Console.WriteLine(product.ToString());
                                        }
                                        break;
                                    case 3:
                                        //View products of an user
                                        Console.WriteLine("Enter UserId: ");
                                        string userId = Console.ReadLine();
                                        var requiredProducts = controller.GetProductsByUserId(userId);
                                        foreach (var product in requiredProducts)
                                        {
                                            Console.WriteLine(product.ToString());
                                        }
                                        break;
                                    case 4:
                                        //Delete a product
                                        Console.WriteLine("Enter productId: ");
                                        string productIdToDelete = Console.ReadLine();
                                        controller.DeleteProduct(productIdToDelete);
                                        break;
                                    case 5: //Update a product
                                        Console.WriteLine("Enter productId: ");
                                        string productIdToUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter product Name: ");
                                        string productNameUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter product Price: ");
                                        double productPriceUpdate = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter product Quantity: ");
                                        int productQuantityUpdate = Convert.ToInt32(Console.ReadLine());
                                        var updateProduct = new ProductModel(productNameUpdate, productPriceUpdate, productQuantityUpdate);
                                        controller.UpdateProduct(productIdToUpdate, updateProduct);
                                        break;
                                    case 6:
                                        //view product by product id
                                        Console.WriteLine("Enter product Id: ");
                                        string productId = Console.ReadLine();
                                        var searchedProduct = controller.GetProduct(productId);
                                        Console.WriteLine(searchedProduct.ToString());
                                        break;
                                    case 7:
                                        //UPDATE USER
                                        Console.WriteLine("Enter user Id: ");
                                        string userIdToUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter username: ");
                                        string usernameToUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter password: ");
                                        string passwordToUpdate = Console.ReadLine();
                                        Console.WriteLine("Is Admin: Y or N");
                                        string adminUpdate = Console.ReadLine().ToLower();
                                        bool isAdminUpdated;
                                        while (true)
                                        {
                                            if (adminUpdate == "y")
                                            {
                                                isAdminUpdated = true;
                                                break;
                                            }
                                            else if (adminUpdate == "n")
                                            {
                                                isAdminUpdated = false;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please Enter valid choice: ");
                                            }
                                        }
                                        controller.UpdateUser(userIdToUpdate, new UserModel(userIdToUpdate, username, password, isAdminUpdated));
                                        break;
                                    case 8: //VIEW ALL USERS
                                        var users = controller.GetAllUsers();
                                        foreach (var user in users)
                                        {
                                            Console.WriteLine(user.ToString());
                                        }
                                        break;
                                    case 9: //VIEW AN USER
                                        Console.WriteLine("Enter userid: ");
                                        string userIdToSearch = Console.ReadLine();
                                        var requiredUser = controller.GetUser(userIdToSearch);
                                        Console.WriteLine(requiredUser.ToString());
                                        break;
                                    case 10: //DELETE AN USER
                                        Console.WriteLine("Enter userid: ");
                                        string userIdToDelete = Console.ReadLine();
                                        controller.DeleteUser(userIdToDelete);
                                        break;
                                    default:
                                        Console.WriteLine("Enter a valid choice...");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            //Operations if the user is not an admin
                            while (true)
                            {
                                client.UserMenu();
                                Console.Write("Enter choice: ");
                                int UserChoice = Convert.ToInt32(Console.ReadLine());
                                if (UserChoice == 7) break;
                                switch (UserChoice)
                                {
                                    //1: Add a Product
                                    case 1: //Input Product details
                                        Console.WriteLine("Enter product Name: ");
                                        string productName = Console.ReadLine();
                                        Console.WriteLine("Enter product Price: ");
                                        double productPrice = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter product Quantity: ");
                                        int productQuantity = Convert.ToInt32(Console.ReadLine());
                                        //Creating and passing new product
                                        controller.AddProduct(new ProductModel(productName, productPrice, productQuantity));
                                        break;
                                    //2: Delete a product 
                                    case 2:
                                        Console.WriteLine("Enter productId: ");
                                        string productIdToDelete = Console.ReadLine();
                                        controller.DeleteProduct(productIdToDelete);
                                        break;
                                    //3: Update a product
                                    case 3:
                                        Console.WriteLine("Enter productId: ");
                                        string productIdToUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter product Name: ");
                                        string productNameUpdate = Console.ReadLine();
                                        Console.WriteLine("Enter product Price: ");
                                        double productPriceUpdate = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter product Quantity: ");
                                        int productQuantityUpdate = Convert.ToInt32(Console.ReadLine());
                                        var updateProduct = new ProductModel(productNameUpdate, productPriceUpdate, productQuantityUpdate);
                                        controller.UpdateProduct(productIdToUpdate, updateProduct);
                                        break;
                                    //4: View product by productId
                                    case 4:
                                        Console.WriteLine("Enter product Id: ");
                                        string productId = Console.ReadLine();
                                        var searchedProduct = controller.GetProduct(productId);
                                        Console.WriteLine(searchedProduct.ToString());
                                        break;
                                    //5: View all products
                                    case 5:
                                        var products = controller.GetProductsByUserId(loggedInUser.Id);
                                        foreach (var product in products)
                                        {
                                            Console.WriteLine(product.ToString());
                                        }
                                        break;
                                    //6: Delete an User
                                    case 6:
                                        controller.DeleteUser(loggedInUser.Id);
                                        break;
                                    default:
                                        Console.WriteLine("Please enter a valid choice!!");
                                        break;
                                }
                            }
                        }
                        break;
                    case 3:
                        //Exit
                        Console.WriteLine("Thank You!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Enter a valid authentication choice");
                        break;
                }
            }

        }
    }
}
