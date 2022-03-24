using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models; 

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        Transfer CreateTransfer(Transfer transfer);

        //public decimal ReceiveTransfer(decimal amount);

        List<Transfer> GetTransfersByUser(int id);

        Transfer GetTransferById(int id); 

        
    }
}
