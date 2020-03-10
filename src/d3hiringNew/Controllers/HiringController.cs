using d3hiringNew.Helpers;
using d3hiringNew.Interface;
using d3hiringNew.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace d3hiringNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiringController : ControllerBase
    {
        private IHiringRepository _ihiringRepositorty;
        private string badrequest = "Your client has issued a malformed or illegal request.";
        private string notfound = "One or more person(s) requested is not found.";
        private string processingError = "";

        public HiringController(IHiringRepository hiringRepository)
        {
            _ihiringRepositorty = hiringRepository;
        }

        #region Post
        [HttpPost("register")]
        public IActionResult register(Register register)
        {
            try
            {
                //To check if the body has the correct request key paramater(s)
                if (String.IsNullOrEmpty(register.Teacher) || String.IsNullOrEmpty(register.Students[0].ToString().Trim()) 
                    || register.Students.Length == 0)
                {
                    processingError = badrequest;
                    throw new ProcessingException();
                }

                //Extract teacher and student(s) from request body
                string teacher = register.Teacher.ToString();
                string[] students = register.Students;

                //add check teacher if more than 1 and if it exist.
                if (!_ihiringRepositorty.Register(teacher, students))
                {
                    processingError = notfound;
                    throw new ProcessingException();
                }

                return NoContent();

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
        public IActionResult suspend(Suspend suspend)
        {
            try
            {
                if (String.IsNullOrEmpty(suspend.Student))
                {
                    processingError = badrequest;
                    throw new ProcessingException();
                }

                //Extract student from request body
                string students = suspend.Student.ToString();

                //add check student if more than 1 and if existing
                if (!_ihiringRepositorty.suspend(students))
                {
                    processingError = notfound;
                    throw new ProcessingException();
                }

                return NoContent();
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
        public IActionResult retrievefornotifications(RetrieveNotification retrieveNotification)
        {
            try
            {
                if (String.IsNullOrEmpty(retrieveNotification.Teacher) || String.IsNullOrEmpty(retrieveNotification.Notification))
                {
                    processingError = badrequest;
                    throw new ProcessingException();
                }

                //Extract teacher and notification from request body
                string teacher = retrieveNotification.Teacher.ToString();
                string notification = retrieveNotification.Notification.ToString();

                var recipients = _ihiringRepositorty.retrievefornotifications(teacher, notification);

                if (recipients == null)
                {
                    processingError = notfound;
                    throw new ProcessingException();
                }

                return Ok(JsonConvert.SerializeObject(recipients, Formatting.Indented));
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

                var commonStudents = _ihiringRepositorty.CommonStudents(teacher);

                if (commonStudents != null)
                    return Ok(JsonConvert.SerializeObject(commonStudents, Formatting.Indented));
                else
                    throw new ProcessingException(badrequest);

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
            return Ok(JsonConvert.SerializeObject(_ihiringRepositorty.GetStudents(), Formatting.Indented));
        }

        [HttpGet("getteachers")]
        public IActionResult getteachers()
        {
            return Ok(JsonConvert.SerializeObject(_ihiringRepositorty.GetTeachers(), Formatting.Indented));
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
