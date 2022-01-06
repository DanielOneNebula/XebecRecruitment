using AutoMapper;
using Server.Data;
using Server.IRepository;
using XebecPortal.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Server.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkHistoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public WorkHistoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<WorkHistoryController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWorkHistories()
        {
            try
            {
                var WorkHistory = await _unitOfWork.WorkHistory.GetAll();
             
                return Ok(WorkHistory);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<WorkHistoryController>/single/5
        [HttpGet("single/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkHistory(int id)
        {
            try
            {
                var WorkHistory = await _unitOfWork.WorkHistory.GetT(q => q.Id == id);
                return Ok(WorkHistory);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<WorkHistoryController>/all/1
        [HttpGet("all/{userId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkHistoryByUserId(int userId)
        {
            try
            {
                var workHistory = await _unitOfWork.WorkHistory.GetAll(q => q.AppUserId == userId);
                return Ok(workHistory);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleWorkHistoryByUserId(int id)
        {
            try
            {
                var ProfilePortfolioLink = await _unitOfWork.WorkHistory.GetT(q => q.AppUserId == id);
                return Ok(ProfilePortfolioLink);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<WorkHistoryController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWorkHistory([FromBody] WorkHistory WorkHistory)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {

                await _unitOfWork.WorkHistory.Insert(WorkHistory);
                await _unitOfWork.Save();

                return CreatedAtAction("GetWorkHistory", new { id = WorkHistory.Id }, WorkHistory);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }


        }


        // PUT api/<WorkHistoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkHistory(int id, [FromBody] WorkHistoryDTO WorkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalWorkHistory = await _unitOfWork.WorkHistory.GetT(q => q.Id == id);

                if (originalWorkHistory == null)
                {
                    return BadRequest("Submitted data is invalid");
                }
                mapper.Map(WorkHistory, originalWorkHistory);
                _unitOfWork.WorkHistory.Update(originalWorkHistory);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/<WorkHistoryController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWorkHistory(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var WorkHistory = await _unitOfWork.WorkHistory.GetT(q => q.Id == id);

                if (WorkHistory == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.WorkHistory.Delete(id);
                await _unitOfWork.Save();

                return NoContent();


            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}
