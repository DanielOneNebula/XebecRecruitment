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
    public class JobTypeHelperController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public JobTypeHelperController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<JobTypeHelpersController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobTypeHelpers()
        {
            try
            {
                var JobTypeHelpers = await _unitOfWork.JobTypeHelpers.GetAll();
             
                return Ok(JobTypeHelpers);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<JobTypeHelpersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetJobTypeHelper(int id)
        {
            try
            {
                var JobTypeHelper = await _unitOfWork.JobTypeHelpers.GetT(q => q.Id == id);
                return Ok(JobTypeHelper);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<JobTypeHelpersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateJobTypeHelper(int[] listOfId)
        {
            List<JobTypeHelper> jobTypeHelper = new List<JobTypeHelper>();

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {
                var jobs = _unitOfWork.Jobs.GetAll();
                var job = jobs.Result.LastOrDefault();
                foreach (var items in listOfId)
                {
                    jobTypeHelper.Add(new JobTypeHelper
                    {
                        JobID = job.Id,
                        JobTypeID = items
                    });
                }

                await _unitOfWork.JobTypeHelpers.InsertRange(jobTypeHelper);

                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }


        }


        // PUT api/<JobTypeHelpersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobTypeHelper(int id, [FromBody] JobTypeHelperDTO JobTypeHelper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalJobTypeHelper = await _unitOfWork.JobTypeHelpers.GetT(q => q.Id == id);

                if (originalJobTypeHelper == null)
                {
                    return BadRequest("Submitted data is invalid");
                }
                mapper.Map(JobTypeHelper, originalJobTypeHelper);
                _unitOfWork.JobTypeHelpers.Update(originalJobTypeHelper);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/<JobTypeHelpersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteJobTypeHelper(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var JobTypeHelper = await _unitOfWork.JobTypeHelpers.GetT(q => q.Id == id);

                if (JobTypeHelper == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.JobTypeHelpers.Delete(id);
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
