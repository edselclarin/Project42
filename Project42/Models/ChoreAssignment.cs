using System.ComponentModel.DataAnnotations;

namespace Project42.Models
{
    public class ChoreAssignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public int DayOfTheWeek { get; set; }
        public int DurationDays { get; set; }
        public int WeekParity { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ChoreId { get; set; }
        public string ChoreName { get; set; }
        public string ChoreDescription { get; set; }
        public DateTime FirstDayOfTheWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public static class ChoreAssignmentExtension
    {
        public static string GetDays(this ChoreAssignment choreAssignment)
        {
            string str = string.Empty;
            if (choreAssignment.DurationDays == 7)
            {
                str = "All Week";
            }
            else
            {
                for (var date = choreAssignment.StartDate; date <= choreAssignment.EndDate; date = date.AddDays(1))
                {
                    str += date.DayOfWeek.ToString().Substring(0, 2) + " ";
                }
            }
            return str;
        }
    }
}
