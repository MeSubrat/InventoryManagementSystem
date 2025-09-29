using InventoryManagementStudio.Authentication;
using InventoryManagementStudio.Controller;
using InventoryManagementStudio.DB;
using InventoryManagementStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementStudio
{
    public class Client
    {
        private void AdminMenu()
        {
            Console.WriteLine("1: Add a Product\n" +
                "2: View all products\n" +
                "3: View products of an user\n" +
                "4: Delete a product \n" +
                "5: Update a product\n"+
                "6: Exit");
        }
        private (string username,string password) LoginMenu()
        {
            Console.WriteLine("PLEASE LOGIN: ");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            return (username, password);
        }
        private (string username,string password, bool isAdmin) SignupMenu()
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
            //Client instnce 
            Client client = new Client();
            
            
            Console.WriteLine("WELCOME TO INVENTORY MANAGEMENT SYSTEM");
            //Sign up Menu
            Console.WriteLine("1:Login\n2:Signup");
            Console.WriteLine("Enter choice: ");
            int authChoice = Convert.ToInt32(Console.ReadLine());

            string username="",password="";
            bool isAdmin;

            IUserDB userDb = new UserDb();
            IProductDB productDb = new ProductDb();
            UserAuthentication auth = new UserAuthentication(userDb);
            UserModel loggedInUser;
            InventoryController controller;

            switch (authChoice)
            {
                case 1:
                    (username, password) = client.LoginMenu();
                    loggedInUser = auth.Login(username, password);
                    controller = new InventoryController(productDb, userDb, loggedInUser);
                    break;
                case 2:
                    //Getting signup/user credentials
                    (username, password, isAdmin) = client.SignupMenu();
                    //Sign up
                    auth.Signup(username, password, isAdmin);
                    //Login
                    loggedInUser = auth.Login(username, password);
                    controller = new InventoryController(productDb, userDb, loggedInUser);
                    //Displaying Login Menu
                    //(username, password) = client.LoginMenu();
                    if(loggedInUser.isAdmin == true)
                    {
                        while (true)
                        {
                            //Displaying Admin Menu
                            client.AdminMenu();
                            Console.Write("Enter choice: ");
                            int adminChoice = Convert.ToInt32(Console.ReadLine());
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
                                    controller.GetAllProducts();
                                    break;
                                case 3:
                                    //View products of an user
                                    Console.WriteLine("Enter UserId: ");
                                    string userId = Console.ReadLine();
                                    controller.GetProductsByUserId(userId);
                                    break;
                                case 4:
                                    //Delete a product
                                    Console.WriteLine("Enter productId: ");
                                    string productIdToDelete = Console.ReadLine();
                                    controller.DeleteProduct(productIdToDelete);
                                    break;
                                case 5: //Update a product
                                    break;
                                case 6:
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("Enter a valid choice...");
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Enter a valid authentication choice");
                    break;
            }
            
        }
    }
}
