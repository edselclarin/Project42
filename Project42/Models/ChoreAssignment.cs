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
        public static string GetDateRange(this ChoreAssignment choreAssignment)
        {
            return choreAssignment.DurationDays == 1 ?
                $"{choreAssignment.StartDate:MM/dd/yyyy}" :
                String.Join(
                    " - ",
                    $"{choreAssignment.StartDate:MM/dd/yyyy}",
                    $"{choreAssignment.EndDate:MM/dd/yyyy}");
        }
    }
}
