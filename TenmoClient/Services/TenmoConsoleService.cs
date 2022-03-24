using System;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoClient.Services;

namespace TenmoClient.Services
{
    public class TenmoConsoleService : ConsoleService
    {
        /************************************************************
            Print methods
        ************************************************************/
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

        public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hello, {username}!");
            Console.WriteLine("1: View your current balance");
            Console.WriteLine("2: View your past transfers");
            Console.WriteLine("3: View your pending requests");
            Console.WriteLine("4: Send TE bucks");
            Console.WriteLine("5: Request TE bucks");
            Console.WriteLine("6: Log out");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }
        public LoginUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        // Add application-specific UI methods here...

        public void DisplayBalance(Account account)
        {
            Console.WriteLine($"Hello {account.Username}! Your current account balance is {account.Balance}");
        }

        public void DisplayAllTransfers(List<Transfer> transfers, int requestingAccountId)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Transfers");
            Console.WriteLine("ID          From/To                 Amount");
            Console.WriteLine("-------------------------------------------");
            foreach (Transfer transfer in transfers)
            {
             

                if (transfer.AccountFrom == requestingAccountId)
                {
                    Console.WriteLine($"{transfer.TransferId}    To Account #{transfer.AccountTo}     {transfer.Amount}");
                }
                if (transfer.AccountTo == requestingAccountId)
                {
                    Console.WriteLine($"{transfer.TransferId}     From Account #{transfer.AccountFrom}     {transfer.Amount}");
                }
            
            }
            Console.WriteLine("-----------");

            
        }

        public void DisplayListOfUsers(List<User> users)
        {
            Console.WriteLine("|-------------- Users --------------|");
            Console.WriteLine("     Id | Username                  ");
            Console.WriteLine("|-------+---------------------------|");
            foreach (User user in users)
            {
                Console.WriteLine($"   {user.UserId} | {user.Username}              ");
            }
            Console.WriteLine("|-----------------------------------|");

        }
        
        public void DisplayTransferDetails(Transfer selectedTransfer)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Transfer Details");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Id: {selectedTransfer.TransferId}");
            Console.WriteLine($"From: {selectedTransfer.AccountFrom}");
            Console.WriteLine($"To: {selectedTransfer.AccountTo}");
            if (selectedTransfer.TransferTypeId == 1)
            {
                Console.WriteLine($"Type: Request");

            }
            if (selectedTransfer.TransferTypeId == 2)
            {
                Console.WriteLine("Type: Send");
            }
            if (selectedTransfer.TransferStatusId == 1)
            {
                Console.WriteLine("Status: Pending");
            }
            if (selectedTransfer.TransferStatusId == 2)
            {
                Console.WriteLine("Status: Approved");

            }
            if (selectedTransfer.TransferStatusId == 3)
            {
                Console.WriteLine("Status: Rejected");
            }
            Console.WriteLine($"Amount: {selectedTransfer.Amount}");
            
            

        }
       

    }
}
