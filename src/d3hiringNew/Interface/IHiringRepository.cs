using d3hiringNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Interface
{
    public interface IHiringRepository
    {
        object GetTeachers();

        object GetStudents();

        bool Register(string teacher, string[] students);

        object CommonStudents(string[] teacher);

        bool suspend(string students);

        object retrievefornotifications(string teacher, string notification);
    }
}
