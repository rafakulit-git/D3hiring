using d3hiring.Context;
using d3hiring.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace d3hiring.Services
{
    public class DBServices
    {
        #region public methods
        public static HiringContext _context;
        public DBServices(HiringContext context)
        {
            _context = context;
        }
        #endregion

        #region internal methods
        internal static bool register(string teacher, string students)
        {
            //return _register(teacher, students);
            List<int> teacherListing = teacherList(teacher);

            //Get the list of student(s) based on the parameter
            List<int> studentListing = studentList(students);

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

        internal static bool suspend(string students)
        {
            List<int> studentList = _context.Student
                        .Where(s => students.Contains(s.Email))
                        .Select(s => s.studentID).ToList();

            //add check student if more than 1 and if existing
            if ((!string.IsNullOrEmpty(students.Trim()) && studentList.Count == 1))
            {
                Student stud = (Student)_context.Student
                    .Where(s => students.Contains(s.Email)).FirstOrDefault();

                if (!string.IsNullOrEmpty(stud.Email.Trim()) && stud.Suspended != 1)
                {
                    stud.Suspended = 1;

                    _context.Student.Update(stud);
                    _context.SaveChanges();
                }
                return true;
            }
            else
                return false;
        }

        internal static object retrievefornotifications(string teacher, string notification)
        {
            List<string> results = new List<string>();
            List<string> mentionedList = new List<string>();
            List<string> notinList = new List<string>();

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
            List<int> teacherListing = teacherList(teacher);

            //Get the teacher and student relation based on the teacher list
            //Count is to confirm if the student is registered to the list of teachers
            List<int> relatedStudents = _context.Class
                .Where(c => teacherListing.Contains(c.TeacherID))
                .Select(rS => rS.StudentID).ToList();

            //Get the list of students registered to the teacher that is not suspended
            List<string> cstudents = _context.Student
                .Where(st => relatedStudents.Contains(st.studentID))
                .Where(st => st.Suspended == 0)
                .Select(s => s.Email).ToList();

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
                    Recipients = string.Join(",", unionList.ToArray())
                };

                _context.Notifications.Add(notif);
                _context.SaveChanges();

                return recipients;
            }
            else
                return new Recipients();
        }

        internal static object commonstudents(string[] teacher)
        {
            //Get the list of teacher(s) based on the parameter
            List<int> teacherListing = teacherList(teacher);

            //Get the teacher and student relation based on the teacher list
            //Count is to confirm if the student is registered to the list of teachers
            List<int> relatedStudents = _context.Class
                .Where(cs => teacherListing.Contains(cs.TeacherID))
                .GroupBy(rs => rs.StudentID)
                .Where(gcs => gcs.Count() == teacherListing.Count())
                .Select(rS => rS.Key).ToList();

            //Get student email based on the relatedStudents result
            var cstudents = _context.Student
                .Where(s => relatedStudents.Contains(s.studentID))
                .Select(s => s.Email).ToList();

            //To check if the teacher(s) requested exist
            //and if any common students between the requested teacher list
            if ((teacherListing.Count == 0) || (cstudents.Count == 0))
            {
                return new CommonStudents();
            }

            CommonStudents commonStudents = new CommonStudents
            {
                students = cstudents.ToArray()
            };

            return commonStudents;
        }

        internal static object getstudents()
        {
            var students = _context.Student.Select(s => s.Email).ToList();

            CommonStudents commonStudents = new CommonStudents
            {
                students = students.ToArray()
            };

            return commonStudents;
        }

        internal static object getteachers()
        {
            var teachers = _context.Teacher.Select(t => t.Email).ToList();

            CommonTeachers commonTeachers = new CommonTeachers
            {
                teachers = teachers.ToArray()
            };

            return commonTeachers;
        }
        #endregion

        #region private methods
        //Get the list of teacher(s) based on the parameter
        private static List<int> teacherList(string teacher)
        {
            return _context.Teacher
                .Where(t => teacher.Contains(t.Email))
                .Select(t => t.TeacherID).ToList();
        }

        private static List<int> teacherList(string[] teacher)
        {
            //Get the list of teacher(s) based on the parameter
            return _context.Teacher
                .Where(t => teacher.Contains(t.Email))
                .Select(t => t.TeacherID).ToList();
        }

        //Get the list of student(s) based on the parameter
        private static List<int> studentList(string students)
        {
            return _context.Student
                .Where(s => students.Contains(s.Email))
                .Select(s => s.studentID).ToList();
        }
        #endregion

        #region Response Models
        internal class CommonStudents
        {
            public String[] students { get; set; }
        }

        internal class CommonTeachers
        {
            public String[] teachers { get; set; }
        }

        internal class Recipients
        {
            public String[] recipients { get; set; }
            public String[] errorRecords { get; set; }
        }
        #endregion
    }
}
