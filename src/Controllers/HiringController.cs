using System;
using System.IO;
using d3hiring.Context;
using d3hiring.Services;
using d3hiring.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace d3hiring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiringController : ControllerBase
    {
        private readonly HiringContext _context;
        private string badrequest = "Your client has issued a malformed or illegal request.";
        private string notfound = "One or more person(s) requested is not found.";
        private string processingError = "";

        public HiringController(HiringContext context)
        {
            _context = context;
            DBServices._context = context;
        }

        #region Post
        [HttpPost("register")]
        public IActionResult register()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    JObject body = JObject.Parse(reader.ReadToEnd());
                    //To check if the body has the correct request key paramater(s)
                    if (!body.ContainsKey("teacher") || !body.ContainsKey("students"))
                    {
                        processingError = badrequest;
                        throw new ProcessingException();
                    }

                    //Extract teacher and student(s) from request body
                    string teacher = body["teacher"].ToString();
                    string students = body["students"].ToString();

                    //add check teacher if more than 1 and if it exist.
                    if (!DBServices.register(teacher,students))                    
                    {
                        processingError = notfound;
                        throw new ProcessingException();
                    }

                    return NoContent();
                }
            }
            catch (ProcessingException)
            {
                // TO-DO: Handle processing exception
                error error = new error
                {
                    message = processingError.ToString()
                };
                return BadRequest(error);
            }
            catch (Exception)
            {
                // TO-DO: Handle the non processing related exception
                return BadRequest();
            }
        }

        [HttpPost("suspend")]
        public IActionResult suspend()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    JObject body = JObject.Parse(reader.ReadToEnd());
                    //To check if the body has the correct request key paramater(s)
                    if (!body.ContainsKey("students"))
                    {
                        processingError = badrequest;
                        throw new ProcessingException();
                    }

                    //Extract student from request body
                    string students = body["students"].ToString();
                    
                    //add check student if more than 1 and if existing
                    if (!DBServices.suspend(students))
                    {
                        processingError = notfound;
                        throw new ProcessingException();
                    }

                    return NoContent();
                }
            }
            catch (ProcessingException)
            {
                // TO-DO: Handle processing exception
                error error = new error
                {
                    message = processingError.ToString()
                };
                return BadRequest(error);
            }
            catch (Exception)
            {
                // TO-DO: Handle the non processing related exception
                return BadRequest();
            }
        }

        [HttpPost("retrievefornotifications")]
        public IActionResult retrievefornotifications()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    JObject body = JObject.Parse(reader.ReadToEnd());
                    //To check if the body has the correct request key paramater(s)
                    if (!body.ContainsKey("teacher") || !body.ContainsKey("notification"))
                    {
                        processingError = badrequest;
                        throw new ProcessingException();
                    }

                    //Extract teacher and notification from request body
                    string teacher = body["teacher"].ToString();
                    string notification = body["notification"].ToString();

                    var recipients = DBServices.retrievefornotifications(teacher, notification);

                    //revisit this check
                    if (recipients.ToString().Trim() == "")
                    {
                        processingError = notfound;
                        throw new ProcessingException();
                    }

                    return Ok(JsonConvert.SerializeObject(recipients, Formatting.Indented));
                }
            }
            catch (ProcessingException)
            {
                // TO-DO: Handle processing exception
                error error = new error
                {
                    message = processingError.ToString()
                };
                return BadRequest(error);
            }
            catch (Exception)
            {
                // TO-DO: Handle the non processing related exception
                return BadRequest();
            }
        }
        #endregion

        #region Get
        [HttpGet("commonstudents")]
        public IActionResult commonstudents([FromQuery]string[] teacher)
        {
            try
            {
                //To check if the request paramater(s)
                if (teacher.Length == 0)
                {
                    processingError = badrequest;
                    throw new ProcessingException(badrequest);
                }

                var commonStudents = DBServices.commonstudents(teacher);

                return Ok(JsonConvert.SerializeObject(commonStudents, Formatting.Indented));
            }
            catch (ProcessingException)
            {
                // TO-DO: Handle processing exception
                error error = new error
                {
                    message = processingError.ToString()
                };
                return BadRequest(error);
            }
            catch (Exception)
            {
                // TO-DO: Handle the non processing related exception
                return BadRequest();
            }            
        }

        [HttpGet("getstudents")]
        //public IActionResult getstudents()
        public IActionResult getstudents()
        {
            return Ok(JsonConvert.SerializeObject(DBServices.getstudents(), Formatting.Indented));
        }

        [HttpGet("getteachers")]
        public IActionResult getteachers()
        {
            return Ok(JsonConvert.SerializeObject(DBServices.getteachers(), Formatting.Indented));
        }
        #endregion

        #region Response Models
        internal class error
        {
            public string message { get; set; }
        }
        #endregion
    }
}