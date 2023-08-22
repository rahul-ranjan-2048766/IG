using Instagram.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IQueryService, QueryService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IProfilePicService, ProfilePicService>();

builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IAdminService, AdminService>();

builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddTransient<IChatService, ChatService>();

builder.Services.AddSession(option => 
    option.IdleTimeout = TimeSpan.FromMinutes(15));
builder.Services.AddCors(option => option.AddDefaultPolicy(sam => 
    sam.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();