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
    public class RegisterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public RegisterController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<RegisterHelpersController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRegisterHelpers()
        {
            try
            {
                var RegisterHelpers = await _unitOfWork.RegisterHelpers.GetAll();
             
                return Ok(RegisterHelpers);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<RegisterHelpersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRegisterHelper(int id)
        {
            try
            {
                var RegisterHelper = await _unitOfWork.RegisterHelpers.GetT(q => q.Id == id);
                return Ok(RegisterHelper);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<RegisterHelpersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRegisterHelper([FromBody] RegisterHelper RegisterHelper)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {

                await _unitOfWork.RegisterHelpers.Insert(RegisterHelper);
                await _unitOfWork.Save();

                return CreatedAtAction("GetRegisterHelper", new { id = RegisterHelper.Id }, RegisterHelper);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }


        }


        // PUT api/<RegisterHelpersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegisterHelper(int id, [FromBody] RegisterHelperDTO RegisterHelper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalRegisterHelper = await _unitOfWork.RegisterHelpers.GetT(q => q.Id == id);

                if (originalRegisterHelper == null)
                {
                    return BadRequest("Submitted data is invalid");
                }
                mapper.Map(RegisterHelper, originalRegisterHelper);
                _unitOfWork.RegisterHelpers.Update(originalRegisterHelper);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/<RegisterHelpersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRegisterHelper(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var RegisterHelper = await _unitOfWork.RegisterHelpers.GetT(q => q.Id == id);

                if (RegisterHelper == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.RegisterHelpers.Delete(id);
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
