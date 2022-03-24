using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;
using Microsoft.AspNetCore.Authorization;


namespace TenmoServer.Controllers
{
    [Route("accounts")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private IAccountDAO accountDao;

        public AccountsController(IAccountDAO accountDao)
        {
            this.accountDao = accountDao;
        }

        [HttpGet]
        public List<Account> ListAllAccounts()
        {
            return accountDao.GetAllAccounts();
        }
        
        [HttpGet("/accounts/myaccount")]
        public Account GetMyAccount()
        {
            string userId = User.FindFirst("sub")?.Value;

            int userIdNumber = Convert.ToInt32(userId);
            Account account = accountDao.GetAccountByUserId(userIdNumber);
            return account;
        }

        [HttpGet("/accounts/users/{id}")]
        public Account GetAccountByUserId(int id)
        {
            return accountDao.GetAccountByUserId(id);
        }

        [HttpGet("{id}")]
        public Account GetAccountByAccountID(int id)
        {
            return accountDao.GetAccountByAccountId(id);
        }

        [HttpPut("{id}")]
        public ActionResult<Account> UpdateBalance(int id, Account account)
        {
            Account existingAccount = accountDao.GetAccountByAccountId(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            accountDao.UpdateBalance(account);
            Account updatedAccount = accountDao.GetAccountByAccountId(id); 
            return Ok(updatedAccount);
        }

        
    }
}
