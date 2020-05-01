using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kalanchiyam.API.Security.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalanchiyam.API.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private ILoggerManager logger;
        public QueueController(ILoggerManager _logger)
        {

            logger = _logger;
        }

        [HttpPost("AddMessage")]
        public async Task<IActionResult> AddMessage(string Message)
        {

            try
            {
                await QueueClient.AddQueueMessagesAsync(Message);
            }
            catch (Exception ex)
            {
                logger.LogException("AddMessage",ex,ex.ToString());
                throw;
            }
            return Ok();
        }

        [HttpPost("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(string queueid)
        {

            try
            {
                await QueueClient.DeleteQueueAsync(queueid);
            }
            catch (Exception ex)
            {
                logger.LogException("DeleteMessage", ex, ex.ToString());
                throw;
            }
            return Ok();
        }
    }
}