using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TenmoServer.DAO;
using TenmoServer.Models;


namespace TenmoServer.Controllers
{
    [Route("transfers")]
    [ApiController]
    [Authorize]
    public class TransfersController : ControllerBase
    {
        private ITransferDAO transferDAO;

        public TransfersController(ITransferDAO transferDAO)
        {
            this.transferDAO = transferDAO;
        }

        [HttpGet("{id}")]
        public Transfer GetTransfer(int id)
        {
            return transferDAO.GetTransferById(id);
        }


        
        [HttpGet("/users/transfers")]
        public ActionResult<List<Transfer>> GetTransferByUserId()
        {
            string userId = User.FindFirst("sub")?.Value;

            int userIdNumber = Convert.ToInt32(userId); 

            return transferDAO.GetTransfersByUser(userIdNumber); 
            
        }

        [HttpPost()]

        public ActionResult<Transfer> CreateTransfer(Transfer transfer)
        {
            Transfer added = transferDAO.CreateTransfer(transfer);
            return Created($"/transfers/{added.TransferId}", added); 
        }
        
        

    }
}
