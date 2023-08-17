using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STLReader.Domain.Models;
using STLReader.Domain.Services;
using System;
using Swashbuckle.AspNetCore.Annotations;

namespace STLReader.API.Controllers
{
    [Route("api/[controller]")]
    public class STLController : Controller
    {
        private readonly ISTLProcessingService _stlProcessingService;

        public STLController(ISTLProcessingService stlProcessingService)
        {
            _stlProcessingService = stlProcessingService;
        }

        /// <summary>
        /// Uploads an STL file and returns processing results.
        /// </summary>
        /// <param name="file">The STL file to upload.</param>
        /// <returns>The processing results for the uploaded STL file.</returns>
        [HttpPost("upload")]
        [SwaggerOperation(Summary = "Uploads an STL file and returns processing results.")]
        [ProducesResponseType(typeof(STLFileResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Upload(
            [FromForm, SwaggerParameter("STL file to be uploaded", Required = true)] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File empty or not sent.");

            try
            {
                STLFileResult result = _stlProcessingService.Process(file.OpenReadStream());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
