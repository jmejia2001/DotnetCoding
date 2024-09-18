using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Interfaces;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        public readonly IQueueRepository _queueRepository;

        public QueueController(IQueueRepository approvalQueueRepository)
        {
            _queueRepository = approvalQueueRepository;
        }

        [HttpGet("approvalQueue")]
        public async Task<IActionResult> GetApprovalQueue()
        {
            var approvalQueue = await _queueRepository.GetApprovalQueue();
            return Ok(approvalQueue);
        }
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveProduct(int id)
        {
            try
            {
                await _queueRepository.ApproveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectProduct(int id)
        {
            try
            {
                await _queueRepository.RejectAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
