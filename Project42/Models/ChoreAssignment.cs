using System.ComponentModel.DataAnnotations;

namespace Project42.Models
{
    public class ChoreAssignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ChoreId { get; set; }
        public string ChoreName { get; set; }
        public string ChoreDescription { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationDays { get; set; }
        public string Frequency { get; set; }
        public DateTime EndDate { get; set; }
    }

    public static class ChoreAssignmentExtension
    {
        public static string GetDateRange(this ChoreAssignment choreAssignment)
        {
            return String.Join(
                " - ",
                $"{choreAssignment.StartDate.ToString("MM/dd/yyyy")}",
                $"{choreAssignment.EndDate.ToString("MM/dd/yyyy")}");
        }
    }
}
