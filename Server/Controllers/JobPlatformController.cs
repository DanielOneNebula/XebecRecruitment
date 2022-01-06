using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XebecPortal.Server.DTOs;
using XebecPortal.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XebecPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPlatformController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public JobPlatformController (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<JobPlatformController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobPlatform()
        {
            try
            {
                var jobPlatforms = await _unitOfWork.JobPlatforms.GetAll();
                return Ok(jobPlatforms);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<JobPlatformController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetJobPlatform(int id)
        {
            try
            {
                var jobPlatforms = await _unitOfWork.JobPlatforms.GetT(q => q.Id == id);
                return Ok(jobPlatforms);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<JobPlatformController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateJobPlatform(int[] listOfId)
        {
            List<JobPlatformHelper> jobPlatformHelper = new List<JobPlatformHelper>();

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {
                var jobs = _unitOfWork.Jobs.GetAll();
                var job = jobs.Result.LastOrDefault();
                foreach(var items in listOfId)
                {
                    if(items != 0)
                    {
                        jobPlatformHelper.Add(new JobPlatformHelper
                        {
                            JobID = job.Id,
                            JobPlatformId = items
                        });
                    }
                }
                await _unitOfWork.JobPlatformHelpers.InsertRange(jobPlatformHelper);

                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }


        }


        // PUT api/<JobPlatformController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPlatform(int id, [FromBody] JobPlatformDTO jobPlatform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalJobType = await _unitOfWork.JobTypes.GetT(q => q.Id == id);

                if (originalJobType == null)
                {
                    return BadRequest("Submitted data is invalid");
                }
                mapper.Map(jobPlatform, originalJobType);
                _unitOfWork.JobTypes.Update(originalJobType);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/<JobPlatformController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteJobPlatform(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var JobType = await _unitOfWork.JobTypes.GetT(q => q.Id == id);

                if (JobType == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.JobTypes.Delete(id);
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
