using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kalanchiyam.API.Security.Entity;
using Kalanchiyam.API.Security.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalanchiyam.API.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private ILoggerManager logger;
        public TableController(ILoggerManager _logger)
        {

            logger = _logger;
        }

        [HttpGet("GetRecords")]
        public async Task<IActionResult> GetRecords(string customerType,string customerId)
        {
            var customer = new CustomerEntity();
            try
            {
                customer= await TableClient.getRecords(customerType, customerId);
            }
            catch (Exception ex)
            {
                logger.LogException("GetRecords", ex, ex.ToString());
                throw;
            }
            return Ok(customer);

        }

        [HttpPost("InsetRow")]
        public async Task<IActionResult> InsetTable(CustomerEntity customerEntity)
        {
            try
            {
                TableClient.InsertRecordToTable(customerEntity);
            }
            catch (Exception ex)
            {
                logger.LogException("InsetTable", ex, ex.ToString());
                throw;
            }
            return Ok();
        }
        [HttpPost("UpdateRow")]
        public async Task<IActionResult> UpdateRow(CustomerEntity customerEntity)
        {
            try
            {
                 TableClient.UpdateRecordInTable(customerEntity);
            }
            catch (Exception ex)
            {
                logger.LogException("UpdateRow", ex, ex.ToString());
                throw;
            }
            return Ok();
        }

        [HttpPost("DeleteRow")]
        public async Task<IActionResult> DeleteRow(string customerType, string customerId)
        {
            try
            {
                 TableClient.DeleteRecordinTable(customerType, customerId);
            }
            catch (Exception ex)
            {
                logger.LogException("DeleteRow", ex, ex.ToString());
                throw;
            }
            return Ok();
        }

    }
}