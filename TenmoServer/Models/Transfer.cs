using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }

        public int TransferTypeId { get; set; }

        public int TransferStatusId {get; set; }

        public int AccountFrom { get; set; }

        public int AccountTo { get; set; }
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "Transfer Amount Cannot be negative")] 
        public decimal Amount { get; set; }
        //don't forget to mirror the changes to object here


        public Transfer(int transferId, int transferTypeId, int transferStatusId, int accountFrom, int accountTo, decimal amount)
        {
            TransferId = transferId;
            TransferTypeId = transferTypeId;
            TransferStatusId = transferStatusId;
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            Amount = amount;
        }
        
        public Transfer() 
        { 
        }

    }

}
