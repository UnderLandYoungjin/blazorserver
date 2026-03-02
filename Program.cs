using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tradesystem.Data;
using tradesystem.Components;

var builder = WebApplication.CreateBuilder(args);

// ── 연결문자열 설정 ──────────────────────────────
builder.Configuration["ConnectionStrings:AppDbContextConnection"] = "Data Source=trade.db";

// ── Blazor Server 등록 ───────────────────────────
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ── Razor Pages (Identity UI용) ──────────────────
builder.Services.AddRazorPages();

// ── SQLite DB 연결 ───────────────────────────────
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=trade.db"));

// ── Identity 등록 (AddDefaultIdentity 하나만 사용) ─
// 스캐폴딩된 코드와 중복되지 않도록 하나만 등록
builder.Services.AddDefaultIdentity<IdentityUser>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 4;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ── DB 자동 생성 ─────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// ── 라우팅 ───────────────────────────────────────
app.MapRazorPages();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();