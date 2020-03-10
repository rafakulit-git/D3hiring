using d3hiringNew.Context;
using d3hiringNew.Interface;
using d3hiringNew.Models;
using d3hiringNew.RequestResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace d3hiringNew.Repositories
{
    public class HiringRepository : IHiringRepository
    {
        private HiringContext _context;

        #region Public
        public HiringRepository(HiringContext dbContext)
        {
            _context = dbContext;
        }
        
        public bool Register(string teacher, string[] students)
        {
            //return _register(teacher, students);
            List<int> teacherListing = teacherIDList(teacher);

            //Get the list of student(s) based on the parameter
            List<int> studentListing = studentIDList(students);

            //add check teacher if more than 1 and if it exist.
            if (teacherListing.Count == 1 && studentListing.Count > 0)
            {
                foreach (int student in studentListing)
                {
                    Class ClassRec = new Class()
                    {
                        TeacherID = teacherListing[0],
                        StudentID = student
                    };

                    _context.Class.Add(ClassRec);
                    _context.SaveChanges();
                }
                return true;
            }
            else
                return false;
        }

        public object CommonStudents(string[] teacher)
        {
            //Get the list of teacher(s) based on the parameter
            List<int> teacherListing = teacherIDList(teacher);

            //Get the teacher and student relation based on the teacher list
            //Count is to confirm if the student is registered to the list of teachers
            List<int> relatedStudents = commonStudentClassIDList(teacherListing);

            //Get student email based on the relatedStudents result
            var cstudents = studentEmailList(relatedStudents);

            //To check if the teacher(s) requested exist
            //and if any common students between the requested teacher list
            if ((teacherListing.Count == 0) || (cstudents.Count == 0))
            {
                return null;
            }

            if (teacherListing.Count == 1)
            {
                cstudents.Add("student_only_under_" + teacherEmail(teacherListing));
            }

            StudentResponse commonStudents = new StudentResponse
            {
                students = cstudents.ToArray()
            };

            return commonStudents;
        }

        public object GetTeachers()
        {
            var teachers = _context.Teacher.Select(t => t.Email).ToList();

            TeacherResponse commonTeachers = new TeacherResponse
            {
                teachers = teachers.ToArray()
            };

            return commonTeachers;
        }

        public object GetStudents()
        {
            var students = _context.Student.Select(s => s.Email).ToList();

            StudentResponse commonStudents = new StudentResponse
            {
                students = students.ToArray()
            };

            return commonStudents;
        }

        public bool suspend(string students)
        {
            return suspendStudent(students);
        }

        public object retrievefornotifications(string teacher, string notification)
        {
            List<string> results = new List<string>();
            List<string> mentionedList = new List<string>();
            List<string> notinList = new List<string>();

            //This is to check if there is any @ mentioned in the notification
            if (notification.Contains('@'))
            {
                results = emailValidator(notification);

                //Get the list of students that is @ mentioned and not suspended
                mentionedList = commonStudentEmailList(results);

                if (results.Count != mentionedList.Count)
                {
                    foreach (string mentioned in results)
                    {
                        if (!mentionedList.Contains(mentioned))
                            notinList.Add(mentioned);
                    }
                }
            }

            //Get the list of teacher based on the parameter
            List<int> teacherListing = teacherIDList(teacher);

            //Get the teacher and student relation based on the teacher list
            //Count is to confirm if the student is registered to the list of teachers
            List<int> relatedStudents = studentClassIDList(teacherListing);

            //Get the list of students registered to the teacher that is not suspended
            List<string> cstudents = commonStudentEmailList(relatedStudents);

            List<string> unionList = cstudents.Union(mentionedList).Distinct().ToList();

            Recipients recipients = new Recipients
            {
                recipients = unionList.ToArray(),
                errorRecords = notinList.ToArray()
            };

            if (unionList.Count > 0 && teacherListing.Count == 1)
            {
                Notifications notif = new Notifications()
                {
                    TeacherID = teacherListing[0],
                    Notification = notification,
                    Recipients = string.Join(",", unionList.ToArray()),
                    ErrorRecords = string.Join(",", notinList.ToList())
                };

                _context.Notifications.Add(notif);
                _context.SaveChanges();

                return recipients;
            }
            else
                return null;
        }
        #endregion

        #region Private
        #region Teacher
        //Get the list of teacher(s) based on the string parameter
        private List<int> teacherIDList(string teacher)
        {
            return _context.Teacher
                .Where(t => teacher.Contains(t.Email))
                .Select(t => t.TeacherID).ToList();
        }

        private string teacherEmail(List<int> teacher)
        {
            return _context.Teacher
                .Where(t => teacher.Contains(t.TeacherID))
                .Select(t => t.Email).FirstOrDefault();
        }

        //Get the list of teacher(s) based on the array parameter
        private List<int> teacherIDList(string[] teacher)
        {
            //Get the list of teacher(s) based on the parameter
            return _context.Teacher
                .Where(t => teacher.Contains(t.Email))
                .Select(t => t.TeacherID).ToList();
        }
        #endregion

        #region Student
        //Get the list of student(s) based on the string parameter
        private List<int> studentIDList(string[] students)
        {
            return _context.Student
                .Where(s => students.Contains(s.Email))
                .Select(s => s.studentID).ToList();
        }

        private bool suspendStudent(string students)
        {
            Student stud = (Student)_context.Student
                    .Where(s => students.Contains(s.Email)).FirstOrDefault();

            if (!string.IsNullOrEmpty(stud.Email.Trim()) && stud.Suspended != 1)
            {
                stud.Suspended = 1;

                _context.Student.Update(stud);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        //Get the list of student(s) based on the List<int> parameter
        private List<string> commonStudentEmailList(List<int> students)
        {
            return _context.Student
                .Where(st => students.Contains(st.studentID))
                .Where(st => st.Suspended == 0)
                .Select(s => s.Email).ToList();
        }

        //Get the list of student(s) based on the List<string> parameter
        private List<string> commonStudentEmailList(List<string> students)
        {
            return _context.Student
                .Where(st => students.Contains(st.Email))
                .Where(st => st.Suspended == 0)
                .Select(s => s.Email).ToList();
        }

        //Get the list of student(s) based on the List<int> parameter
        private List<string> studentEmailList(List<int> students)
        {
            return _context.Student
                .Where(st => students.Contains(st.studentID))
                .Select(s => s.Email).ToList();
        }
        #endregion

        #region Class
        //Get the list of student(s) based on the List<int> parameter
        private List<int> studentClassIDList(List<int> teacher)
        {
            return _context.Class
                .Where(c => teacher.Contains(c.TeacherID))
                .Select(rS => rS.StudentID).ToList();
        }

        //Get the list of student(s) based on the List<int> parameter
        private List<int> commonStudentClassIDList(List<int> teacherList)
        {
            return _context.Class
                .Where(cs => teacherList.Contains(cs.TeacherID))
                .GroupBy(rs => rs.StudentID)
                .Where(gcs => gcs.Count() == teacherList.Count())
                .Select(rS => rS.Key).ToList();
        }
        #endregion

        private List<string> emailValidator(string notification)
        {
            List<string> results = new List<string>();
            Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
            Match match;

            for (match = reg.Match(notification); match.Success; match = match.NextMatch())
            {
                if (!(results.Contains(match.Value)))
                    results.Add(match.Value);
            }

            return results;
        }
        #endregion
    }
}
