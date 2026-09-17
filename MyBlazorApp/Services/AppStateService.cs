using MyBlazorApp.Models;

namespace MyBlazorApp.Services;

public class AppStateService
{
    public User? CurrentUser { get; private set; }
    public event Action? OnChange;

    public List<Expense> Expenses { get; } = new()
    {
        new Expense("Flight to Tokyo", new DateOnly(2026, 9, 12), 842.00m),
        new Expense("Hotel — 4 nights", new DateOnly(2026, 9, 13), 560.00m),
        new Expense("Conference registration", new DateOnly(2026, 9, 14), 299.00m),
        new Expense("Airport transfer", new DateOnly(2026, 9, 12), 48.50m),
        new Expense("Team dinner", new DateOnly(2026, 9, 15), 214.00m),
        new Expense("Museum tickets", new DateOnly(2026, 9, 16), 32.00m),
    };

    public decimal Total => Expenses.Sum(e => e.Cost);

    public void Login(string email, string username = "User")
    {
        CurrentUser = new User(username, email, true);
        NotifyStateChanged();
    }

    public void Logout()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    public void AddExpense(string item, decimal cost)
    {
        if (string.IsNullOrWhiteSpace(item) || cost <= 0)
        {
            return;
        }

        Expenses.Add(new Expense(item.Trim(), DateOnly.FromDateTime(DateTime.Now), cost));
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
