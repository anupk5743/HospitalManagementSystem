namespace HMS.Web.Models
{
    public class DashboardAnalyticsVM
    {
        public List<string> Months { get; set; }

        public List<int> AppointmentCounts { get; set; }

        public List<decimal> Revenue { get; set; }
    }
}