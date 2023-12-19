using KrutangerHighSchoolDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace KrutangerHighSchoolDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = new();
            app.RunApp();        
        }
    }
}
