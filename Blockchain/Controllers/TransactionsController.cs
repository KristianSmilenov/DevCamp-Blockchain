﻿using Blockchain.Models;
using Blockchain.Services;
using BlockchainCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockchain.Controllers
{
    [Route("api/transactions")]
    [Produces("application/json")]
    public class TransactionsController : BlockchainController
    {
        public TransactionsController(IBlockchainService blockchainService) : base(blockchainService) { }

        /// <summary>
        /// Create new transaction
        /// </summary>
        [HttpPost]
        public TransactionHashInfo Post([FromBody]TransactionDataSigned data)
        {
            return _blockchainService.CreateTransaction(data);
        }

        /// <summary>
        /// Get transaction info
        /// </summary>
        [HttpGet("{hash}")]
        public IActionResult Get(string hash)
        {
            Transaction result = _blockchainService.GetTransaction(hash);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Get list of transactions
        /// </summary>
        /// <param name="status">Fitler by status: pending and confirmed</param>
        [HttpGet()]
        public CollectionContext<Transaction> GetAll([FromQuery]string status, [FromQuery]int? pageNumber = 0, [FromQuery]int? pageSize = 20)
        {
            return _blockchainService.GetTransactions(status, pageNumber.Value, pageSize.Value);
        }
    }
}
