using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using d3hiring.Context;
using d3hiring.Helper;
using d3hiring.Model;
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

                    //Get the list of teacher(s) based on the parameter
                    List<int> teacherList = _context.Teacher
                        .Where(t => teacher.Contains(t.Email))
                        .Select(t => t.TeacherID).ToList();

                    //Get the list of student(s) based on the parameter
                    List<int> studentList = _context.Student
                        .Where(s => students.Contains(s.Email))
                        .Select(s => s.studentID).ToList();

                    //add check teacher if more than 1 and if it exist.
                    if (teacherList.Count == 1 && studentList.Count > 0)
                    {
                        foreach (int stud in studentList)
                        {
                            Class ClassRec = new Class()
                            {
                                TeacherID = teacherList[0],
                                StudentID = stud
                            };

                            _context.Class.Add(ClassRec);
                            _context.SaveChanges();
                        }
                    }
                    else
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

                    List<int> studentList = _context.Student
                        .Where(s => students.Contains(s.Email))
                        .Select(s => s.studentID).ToList();

                    //add check student if more than 1 and if existing
                    if ((!string.IsNullOrEmpty(students.Trim()) && studentList.Count == 1))
                    {
                        Student stud = (Student)_context.Student
                            .Where(s => students.Contains(s.Email)).FirstOrDefault();

                        stud.Suspended = 1;

                        _context.Student.Update(stud);
                        _context.SaveChanges();
                    }
                    else
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

                    List<string> results = new List<string>();
                    List<string> mentionedList = new List<string>();

                    //This is to check if there is any @ mentioned in the notification
                    if (notification.Contains('@'))
                    {
                        Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
                        Match match;
                                                
                        for (match = reg.Match(notification); match.Success; match = match.NextMatch())
                        {
                            if (!(results.Contains(match.Value)))
                                results.Add(match.Value);
                        }

                        //Get the list of students that is @ mentioned and not suspended
                        mentionedList = _context.Student
                            .Where(stm => results.Contains(stm.Email))
                            .Where(stm => stm.Suspended == 0)
                            .Select(sm => sm.Email).ToList();
                    }

                    //Get the list of teacher based on the parameter
                    List<int> teacherList = _context.Teacher
                        .Where(t => teacher.Contains(t.Email))
                        .Select(t => t.TeacherID).ToList();

                    //Get the teacher and student relation based on the teacher list
                    //Count is to confirm if the student is registered to the list of teachers
                    List<int> relatedStudents = _context.Class
                        .Where(c => teacherList.Contains(c.TeacherID))
                        .Select(rS => rS.StudentID).ToList();

                    //Get the list of students registered to the teacher that is not suspended
                    List<string> cstudents = _context.Student
                        .Where(st => relatedStudents.Contains(st.studentID))
                        .Where(st => st.Suspended == 0)
                        .Select(s => s.Email).ToList();

                    List<string> unionList = cstudents.Union(mentionedList).Distinct().ToList();

                    Recipients recipients = new Recipients
                    {
                        recipients = unionList.ToArray()
                    };

                    if (unionList.Count > 0 && teacherList.Count == 1)
                    {
                        Notifications notif = new Notifications()
                        {
                            TeacherID = teacherList[0],
                            Notification = notification,
                            Recipients = string.Join(",", unionList.ToArray())
                    };

                        _context.Notifications.Add(notif);
                        _context.SaveChanges();
                    }
                    else
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

                //Get the list of teacher(s) based on the parameter
                List<int> teacherList = _context.Teacher
                    .Where(t => teacher.Contains(t.Email))
                    .Select(t => t.TeacherID).ToList();

                //Get the teacher and student relation based on the teacher list
                //Count is to confirm if the student is registered to the list of teachers
                List<int> relatedStudents = _context.Class
                    .Where(cs => teacherList.Contains(cs.TeacherID))
                    .GroupBy(rs => rs.StudentID)
                    .Where(gcs => gcs.Count() == teacherList.Count())
                    .Select(rS => rS.Key).ToList();

                //Get student email based on the relatedStudents result
                var cstudents = _context.Student
                    .Where(s => relatedStudents.Contains(s.studentID))
                    .Select(s => s.Email).ToList();

                //To check if the teacher(s) requested exist
                //and if any common students between the requested teacher list
                if ((teacherList.Count == 0) || (cstudents.Count == 0))
                {
                    processingError = notfound;
                    throw new ProcessingException();
                }

                CommonStudents commonStudents = new CommonStudents
                {
                    students = cstudents.ToArray()
                };

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

        [HttpGet("students")]
        public string students()
        {
            var students = _context.Student.Select(s => s.Email).ToList();
            return JsonConvert.SerializeObject(students);
        }

        [HttpGet("teachers")]
        public string teachers()
        {
            var teachers = _context.Teacher.Select(t => t.Email).ToList();
            return JsonConvert.SerializeObject(teachers);
        }
        #endregion

        #region Response Models
        internal class CommonStudents
        {
            public String[] students { get; set; }
        }

        internal class Recipients
        {
            public String[] recipients { get; set; }
        }

        internal class error
        {
            public string message { get; set; }
        }
        #endregion
    }
}