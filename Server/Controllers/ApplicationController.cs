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
    public class ApplicationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public ApplicationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<ApplicationsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplications()
        {
            try
            {
                var Applications = await _unitOfWork.Applications.GetAll();

                return Ok(Applications);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<ApplicationsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplication(int id)
        {
            try
            {
                var Application = await _unitOfWork.Applications.GetT(q => q.Id == id);
                return Ok(Application);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<ApplicationsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateApplication([FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               
                await _unitOfWork.Applications.Insert(application);
                await _unitOfWork.Save();
                ApplicationPhaseHelper applicationPhaseHelper = new ApplicationPhaseHelper
                {
                    ApplicationId = application.Id,
                    StatusId = 1,
                    ApplicationPhaseId = 1,
                    TimeMoved = application.TimeApplied
                };
                await _unitOfWork.ApplicationPhaseHelpers.Insert(applicationPhaseHelper);
                await _unitOfWork.Save();

                return CreatedAtAction("GetApplication", new { id = application.Id }, application);
                //return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }
        }

        // PUT api/<ApplicationsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody] ApplicationDTO Application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalApplication = await _unitOfWork.Applications.GetT(q => q.Id == id);

                if (originalApplication == null)
                {
                    return BadRequest("Submitted data is invalid");
                }
                mapper.Map(Application, originalApplication);
                _unitOfWork.Applications.Update(originalApplication);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<ApplicationsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var Application = await _unitOfWork.Applications.GetT(q => q.Id == id);

                if (Application == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Applications.Delete(id);
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