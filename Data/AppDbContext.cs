using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace tradesystem.Data;

public class Order
{
    public int Id { get; set; }
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Status { get; set; } = "대기중";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string UserId { get; set; } = "";
}

public class ChatMessage
{
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string Message { get; set; } = "";
    public DateTime SentAt { get; set; } = DateTime.Now;
}

public class UploadedFile
{
    public int Id { get; set; }
    public string FileName { get; set; } = "";
    public string FilePath { get; set; } = "";
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.Now;
    public string UserId { get; set; } = "";
}

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<UploadedFile> UploadedFiles => Set<UploadedFile>();
}