using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        //decimal GetBalance(int accountId);

        Account GetAccountByUserId(int id);
        Account GetAccountByAccountId(int id);

        //void IncreaseBalance(decimal amount, int id);

        //void DecreaseBalance(decimal amount, int id);

        Account UpdateBalance(Account account);
        List<Account> GetAllAccounts();
    }
}
