using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSQLDao: IAccountDAO
    {
        private readonly string connectionString;
       
        
        public AccountSQLDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetAccountByUserId(int id)
        {
            Account account = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT a.account_id, a.user_id, a.balance, u.username " +
                    "FROM account a JOIN tenmo_user u ON a.user_id = u.user_id " +
                    "WHERE u.user_id = @user_id;", conn);
                cmd.Parameters.AddWithValue("@user_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    account = CreateAccountFromReader(reader);
                }

            }
            return account;
        }
        public Account GetAccountByAccountId(int id)
        {
            Account account = null; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); 
                SqlCommand cmd = new SqlCommand("SELECT a.account_id, a.user_id, a.balance, u.username " +
                    "FROM account a JOIN tenmo_user u ON a.user_id = u.user_id " + 
                    "WHERE a.account_id = @account_id;", conn);
                cmd.Parameters.AddWithValue("@account_id", id);

                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    account = CreateAccountFromReader(reader); 
                }

            }
            return account; 
            //Actually return error code
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT a.account_id, a.user_id, a.balance, u.username " +
                    "FROM account a JOIN tenmo_user u ON a.user_id = u.user_id", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Account account = CreateAccountFromReader(reader);
                    accounts.Add(account);
                }

            }
            return accounts;
        }

        //public Account GetAccountByUserId(int id)
        //{
        //    Account account = new Account();            
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM account WHERE user_id = @user_id", conn);
        //        cmd.Parameters.AddWithValue("@user_id", id);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            account = CreateAccountFromReader(reader);
        //        }

        //    }
        //    return account;
                
                
        //        //actually return error value
        //}

        public void IncreaseBalance(decimal amount, int id)
        {
          // Account account = null; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); 
                SqlCommand cmd = new SqlCommand("UPDATE account SET balance = balance + @amount where account_id = @account_id", conn);
                cmd.Parameters.AddWithValue("@account_id", id);
                cmd.Parameters.AddWithValue("@amount", amount); 

                cmd.ExecuteNonQuery(); 
            }
            //return GetAccountById(id); 
            //return account; 
            //this will be an error handling
        }

        
        
        public Account UpdateBalance(Account account)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); 
                SqlCommand cmd = new SqlCommand("UPDATE account SET balance = @balance where account_id = @account_id", conn);
                cmd.Parameters.AddWithValue("@account_id", account.AccountId);
                cmd.Parameters.AddWithValue("@balance", account.Balance); 

                cmd.ExecuteNonQuery(); 
            }
            return GetAccountByAccountId(account.AccountId);
            
        }

        private Account CreateAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account();
            account.AccountId = Convert.ToInt32(reader["account_id"]);
            account.UserId = Convert.ToInt32(reader["user_id"]);
            account.Balance = Convert.ToDecimal(reader["balance"]);
            account.Username = Convert.ToString(reader["username"]);

            return account; 
        }
    }
}
