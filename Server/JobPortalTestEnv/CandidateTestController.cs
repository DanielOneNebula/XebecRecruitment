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
using XebecPortal.Server.JobPortalTestEnv.Helpers.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateTestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateTestRepo candidateRepo;

        public CandidateTestController(IUnitOfWork unitOfWork, ICandidateTestRepo candidateRepo)
        {
            _unitOfWork = unitOfWork;
            this.candidateRepo = candidateRepo;
        }

        // GET: api/<JobsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplications([FromQuery] string JobId)
        {
            try
            {
                int job = int.Parse(JobId);
                var Jobs = await candidateRepo.GetApplicantIds(job);
             
                return Ok(Jobs);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpGet("candidates/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchCandidates(string Job, [FromQuery] string SearchQuery, [FromQuery] string ethnicityFiler, [FromQuery] string GenderFilter, [FromQuery] string disabilityFilter)
        {
            try
            {
                int jobIdInt = int.Parse(Job);
                var Jobs = await candidateRepo.SearchCandidates(jobIdInt, SearchQuery, ethnicityFiler, GenderFilter, disabilityFilter);

                return Ok(Jobs);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        // POST api/<JobsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateJob([FromBody] Job Job)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {

                await _unitOfWork.Jobs.Insert(Job);
                await _unitOfWork.Save();

                return CreatedAtAction("GetJob", new {id = Job.Id}, Job);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }
        }
    }
}
