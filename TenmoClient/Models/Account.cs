using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        public int UserId { get; set; }

        public decimal Balance { get; set; }

        public string Username { get; set; }
    }
}
