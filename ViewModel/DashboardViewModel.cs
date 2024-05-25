namespace DACS.ViewModel
{
    public class DashboardViewModel
    {
        public string TotalRevenue { get; set; }
        public int TotalOrder { get; set; }
        public int Year { get; set; }
        public List<MonthlyRevenue> MonthlyRevenues { get; set; }
    }
}
