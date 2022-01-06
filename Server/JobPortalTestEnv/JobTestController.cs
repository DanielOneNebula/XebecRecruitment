using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Server.JobPortalTestEnv.Helpers.Repositories;
using XebecPortal.Shared;

namespace XebecPortal.Server.JobPortalTestEnv
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJobTestRepo jobTest;

        public JobTestController(IUnitOfWork unitOfWork, IJobTestRepo jobTest)
        {
            _unitOfWork = unitOfWork;
            this.jobTest = jobTest;
        }

        // GET: api/<JobsTestController>
        [HttpGet("jobs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobsTests()
        {
            try
            {
                var Jobs = await _unitOfWork.Jobs.GetAll();

                return Ok(Jobs);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobsbySeacrch([FromQuery]string searchQuery, [FromQuery] string searchLocation, [FromQuery]string jobtypeQuery)
        {
            try
            {
                var Jobs = await jobTest.SearchJobs(searchQuery, searchLocation, jobtypeQuery);

                return Ok(Jobs);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }





    }
}