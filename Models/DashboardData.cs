using LoanManagementSystem.Models;

public class DashboardData
{
    public int UserCount { get; set; }
    public int CategoryCount { get; set; }
    public int ProductCount { get; set; }
    public int OrderCount { get; set; }

    public decimal Revenue { get; set; }
    public decimal Profit { get; set; }
    public decimal InventoryValue { get; set; }

    public List<LoanApplication> LatestOrders { get; set; } = new();
    public List<LoanScheme> LowStockProducts { get; set; } = new();
    public List<LoanScheme> RecentProducts { get; set; } = new();

    public List<TopSellingProduct> TopSellingProducts { get; set; } = new();
}