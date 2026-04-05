namespace HMS.API.DTOs
{
    public class DashboardAnalyticsDTO
    {
        public List<string> Months { get; set; }

        public List<int> AppointmentCounts { get; set; }

        public List<decimal> Revenue { get; set; }
    }
}