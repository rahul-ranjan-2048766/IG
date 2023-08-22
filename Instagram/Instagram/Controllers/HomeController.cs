using Instagram.Models;
using Instagram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Instagram.Controllers
{
    public class HomeController : Controller
    {
        private readonly Constants _constants;
        private readonly ILogger<HomeController> _logger;
        
        private readonly IUserService _userService;
        private readonly IQueryService _queryService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IProfilePicService _profilePicService;

        private readonly IMessageService _messageService;
        private readonly IAdminService _adminService;

        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        private readonly IChatService _chatService;

        public HomeController(ILogger<HomeController> logger,
            IUserService userService,
            IQueryService queryService,
            IAuthenticationService authenticationService,
            IProfilePicService profilePicService,
            IConfiguration config,
            IMessageService messageService,
            IAdminService adminService,
            IPostService postService,
            ICommentService commentService,
            IChatService chatService)
        {
            _logger = logger;
            _userService = userService;
            _queryService = queryService;
            _authenticationService = authenticationService;
            _profilePicService = profilePicService;
            _constants = new Constants(config);

            _messageService = messageService;
            _adminService = adminService;

            _postService = postService;
            _commentService = commentService;

            _chatService = chatService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(string name, string username, string email, string password)
        {
            User data = new User 
            { 
                Name = name,
                Username = username,
                Email = email,
                Password = password,
                DateTimeOfRegistration = DateTime.UtcNow,
                Address = null,
                Chronicle = null,
                Firm = null,
                Job = null,
                Phone = null,
                Id = 0
            };
            var result = await _userService.Add(data);
            if (result is not null)
            {
                var message = JObject.Parse(result).GetValue("message");

                if (message is not null)
                    ViewData["message"] = message.ToString();
            }

            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(string email, string query)
        {
            Query data = new Query 
            { 
                Email = email,
                Data = query
            };
            var result = await _queryService.Add(data);
            if (result is not null)
            {
                var message = JObject.Parse(result).GetValue("message");

                if (message is not null)
                    ViewData["message"] = message.ToString();
            }
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            ViewData["Title"] = "Dashboard";

            List<User> Users = await _userService.GetAll();
            User? user = Users.FirstOrDefault(option => option.Username == username);

            if (user == null)
            {
                ViewData["message"] = "User is not registered";
                return View();
            }

            if (user.BANNED == true)
            {
                ViewData["message"] = "You are banned by the Admin....";
                return View();
            }
                 

            (Response?, string?) result = await _authenticationService.AuthenticateUser(new UserCred
                { 
                    Username = username,
                    Password = password
                });

            if (result.Item1 == null)
            {
                if (result.Item2 is not null)
                {
                    var message = JObject.Parse(result.Item2).GetValue("message")?.ToString();
                    ViewData["message"] = message;
                }
                
                return View("SignIn");
            }

            HttpContext.Session.SetString(_constants.USER_NAME, result.Item1.User.Username);
            HttpContext.Session.SetString(_constants.USER_TOKEN, result.Item1.Token);

            return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> Dashboard()
        {
            User? user = await GetUser();
            var userToken = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (user == null || userToken == null) return RedirectToAction("SignIn");

            UserAndProfilePic data = new UserAndProfilePic 
            { 
                User = user,

                ProfilePic = (await _profilePicService.GetAll(userToken))
                                .LastOrDefault(option => option.Username == user.Username)
            };
            List<User> users = await _userService.GetAll();
            List<Post> posts = (await _postService.GetAll(userToken))
                                .OrderByDescending(option => option.Id).ToList();

            List<FileUser> fileUsers = new List<FileUser>();
            
            foreach (var post in posts)
            {
                var user__ = users.FirstOrDefault(option => option.Username == post.Sender);

                List<Comment> Comments = (await _commentService.GetAll(userToken))
                                        .Where(option => option.PostId == post.Id)
                                        .OrderByDescending(option => option.Id)
                                        .ToList();


                if (user__ != null)
                {
                    List<UserComment> userComments = new List<UserComment>();

                    foreach (var comment in Comments)
                    {
                        var user_6 = users.FirstOrDefault(option => option.Username == comment.Sender);

                        if (user_6 != null)
                            userComments.Add(new UserComment 
                            { 
                                Comment = comment,
                                UserAndProfilePic = new UserAndProfilePic 
                                { 
                                    User = user_6,
                                    ProfilePic = (await _profilePicService.GetAll(userToken))
                                                    .LastOrDefault(option => option.Username == user_6.Username)
                                }
                            });
                    }

                    fileUsers.Add(new FileUser
                    {
                        Post = post,
                        UserAndProfilePic = new UserAndProfilePic
                        {
                            User = user__,
                            ProfilePic = (await _profilePicService.GetAll(userToken))
                                            .LastOrDefault(option => option.Username == user__.Username)
                        },
                        UserComments = userComments
                    });
                }
                    
            }

            Tuple<UserAndProfilePic, List<FileUser>> tuple = new 
                Tuple<UserAndProfilePic, List<FileUser>>(data, fileUsers);

            return View(tuple);
        }

        public new IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        private async Task<User?> GetUser()
        {
            var username = HttpContext.Session.GetString(_constants.USER_NAME);
            var userToken = HttpContext.Session.GetString(_constants.USER_TOKEN);
            if (userToken != null)
            {
                List<User> users = await _userService.GetAll();
                User? user = users.FirstOrDefault(option => option.Username.Equals(username));
                return user;
            }
            return null;
        }

        public async Task<IActionResult> Profile()
        {
            User? user = await GetUser();
            var userToken = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (userToken == null) return RedirectToAction("SignIn");
            
            if (user != null)
            {
                UserAndProfilePic data = new UserAndProfilePic 
                { 
                    User = user,

                    ProfilePic = (await _profilePicService.GetAll(userToken))
                                    .LastOrDefault(option => option.Username == user.Username)
                };
                
                return View(data);
            }
            
            return RedirectToAction("SignIn");
        }

        public async Task<IActionResult> Edit(string? message = null)
        {
            ViewData["message"] = message;

            User? user = await GetUser();
            var userToken = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (userToken == null || user == null) return RedirectToAction("SignIn");

            UserAndProfilePic data = new UserAndProfilePic 
            { 
                User = user,

                ProfilePic = (await _profilePicService.GetAll(userToken))
                                .LastOrDefault(option => option.Username == user.Username)
            };
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, string username, 
            string email, string phone, string job, string firm,
            string address, string chronicle,
            string password
            )
        {
            User? user = await GetUser();
            var userToken = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (user == null || userToken == null) 
                return RedirectToAction("SignIn");

            List<User> users = await _userService.GetAll();

            User? data = users.FirstOrDefault(option => option.Username == user.Username);
            if (data == null) return RedirectToAction("SignIn");

            data.Name = name;
            data.Username = username;
            data.Email = email;
            data.Phone = phone;
            data.Job = job;
            data.Firm = firm;
            data.Address = address;
            data.Chronicle = chronicle;
            data.Password = password;

            var result = await _userService.Update(data, userToken);
            string? message = null;

            if (result != null)
            {
                message = JObject.Parse(result).GetValue("message")?.ToString();
            }

            return RedirectToAction("Edit", "Home", new { message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateImage(IFormFile file)
        {
            User? user = await GetUser();
            if (user == null) return RedirectToAction("SignIn");

            var userToken = HttpContext.Session.GetString(_constants.USER_TOKEN);
            if (userToken == null) return RedirectToAction("SignIn");

            var result = await _profilePicService.Add(new ProfilePic 
            { 
                Id = 0,
                Username = user.Username,
                ImageFile = Path.Combine("media", file.FileName),
                DateTime = DateTime.Now
            }, userToken);

            if (result != null)
            {
                var image = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media", file.FileName);

                using (FileStream writer = new FileStream(image, FileMode.Create))
                    file.CopyTo(writer);
            }

            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Friends()
        {
            User? user = await GetUser();
            var userToken = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (user == null || userToken == null)
                return RedirectToAction("SignIn");

            UserAndProfilePic data = new UserAndProfilePic
            {
                User = user,

                ProfilePic = (await _profilePicService.GetAll(userToken)).LastOrDefault(option => 
                                option.Username == user.Username)
            };

            List<UserAndProfilePic> friends = new List<UserAndProfilePic>();

            List<User> users = await _userService.GetAll();
            foreach (User f in users)
            {
                if (f.Username != user.Username)
                {
                    friends.Add(new UserAndProfilePic
                    {
                        User = f,

                        ProfilePic = (await _profilePicService.GetAll(userToken)).LastOrDefault(option =>
                                        option.Username == f.Username)
                    });
                }                
            }

            Tuple<UserAndProfilePic, List<UserAndProfilePic>> tuple = 
                new Tuple<UserAndProfilePic, List<UserAndProfilePic>>(data, friends);

            return View(tuple);
        }

        public async Task<IActionResult> ViewProfile(int id)
        {
            User? user = await GetUser();
            var userToken = HttpContext.Session.GetString(_constants.USER_TOKEN);


            if (user == null || userToken == null)
                return RedirectToAction("SignIn");

            User? friend = await _userService.Get(id, userToken);
            if (friend == null) return Content("Friend is not enrolled.");

            List<ProfilePic> profilePics = await _profilePicService.GetAll(userToken);

            Tuple<UserAndProfilePic, UserAndProfilePic> tuple = 
                new Tuple<UserAndProfilePic, UserAndProfilePic>(
                        new UserAndProfilePic 
                        { 
                            User = user,

                            ProfilePic = profilePics.LastOrDefault(option => 
                                            option.Username == user.Username)
                        }, 
                        
                        new UserAndProfilePic 
                        { 
                            User = friend,

                            ProfilePic = profilePics.LastOrDefault(option => 
                                            option.Username == friend.Username)
                        }
                    );

            return View(tuple);
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(string email, string password)
        {
            ViewData["Title"] = "AdminLogin";

            (AdminResponse?, string?) result = await _authenticationService.AuthenticateAdmin(new AdminCred
            {
                Email = email,
                Password = password
            });

            if (result.Item1 == null)
            {
                if (result.Item2 is not null)
                {
                    var message = JObject.Parse(result.Item2).GetValue("message")?.ToString();
                    ViewData["message"] = message;
                }

                return View("AdminLogin");
            }

            HttpContext.Session.SetString(_constants.ADMIN_NAME, result.Item1.Admin.Email);
            HttpContext.Session.SetString(_constants.ADMIN_TOKEN, result.Item1.Token);

            return RedirectToAction("AdminDash");
        }


        public IActionResult AdminDash()
        {
            return View();
        }

        public async Task<IActionResult> People()
        {
            string? token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (token == null)
                return RedirectToAction("AdminLogin");

            List<User> Users = await _userService.GetAll();

            List<UserAndProfilePic> data = new List<UserAndProfilePic>();

            foreach (User user in Users)
            {
                data.Add(new UserAndProfilePic 
                { 
                    User = user,

                    ProfilePic = (await _profilePicService.GetAll(token))
                                    .LastOrDefault(option => option.Username == user.Username)
                });
            }

            return View(data);
        }

        public async Task<IActionResult> ViewPeople(int id, string? message = null)
        {
            ViewData["message"] = message;

            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (token == null)
                return RedirectToAction("AdminLogin");

            User? user = await _userService.Get(id, token);
            if (user == null)
                return Content("User is not registered.");

            UserAndProfilePic data = new UserAndProfilePic 
            { 
                User = user,
                ProfilePic = (await _profilePicService.GetAll(token))
                                .LastOrDefault(option => option.Username == user.Username)
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanUser(int userId)
        {
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (token == null)
                return RedirectToAction("AdminLogin");

            User? user = await _userService.Get(userId, token);
            if (user == null)
                return Content("User is not registered.");

            user.BANNED = true;

            var result = await _userService.Update(user, token);
            string? message = null;
            
            if (result is not null)
            {
                message = JObject.Parse(result).GetValue("message")?.ToString();
                if (message == "User is updated successfully.")
                    message = $"{user.Username} is banned successfully.";
            }
                

            return RedirectToAction("ViewPeople", new { id = userId, message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllowUser(int userId)
        {
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (token == null)
                return RedirectToAction("AdminLogin");

            User? user = await _userService.Get(userId, token);
            if (user == null)
                return Content("User is not registered.");

            user.BANNED = false;

            var result = await _userService.Update(user, token);
            string? message = null;

            if (result is not null)
            {
                message = JObject.Parse(result).GetValue("message")?.ToString();
                if (message == "User is updated successfully.")
                    message = $"{user.Username} is allowed successfully.";
            }

            return RedirectToAction("ViewPeople", new { id = userId, message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchUser (string searchWord)
        {
            if (searchWord == null)
                return RedirectToAction("People");

            if (string.IsNullOrEmpty(searchWord.Trim()))
                return RedirectToAction("People");

            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);
            if (token == null)
                return RedirectToAction("AdminLogin");

            List<User> Users = (await _userService.GetAll());

            List<User> data = new List<User>();

            bool flag = false;

            foreach (User user in Users)
            {
                flag = false;

                if (user.Username.ToLower().Contains(searchWord.ToLower())
                    || user.Name.ToLower().Contains(searchWord.ToLower())
                    || user.Email.ToLower().Contains(searchWord.ToLower()))
                {
                    data.Add(user);
                    flag = true;
                }

                if (flag == false)
                    if (user.Job != null)
                        if (user.Job.ToLower().Contains(searchWord.ToLower()))
                        {
                            data.Add(user);
                            flag = true;
                        }
                            

                if (flag == false)
                    if (user.Address != null)
                        if (user.Address.ToLower().Contains(searchWord.ToLower()))
                        {
                            data.Add(user);
                            flag = true;
                        }

                if (flag == false)
                    if (user.Firm != null)
                        if (user.Firm.ToLower().Contains(searchWord.ToLower()))
                        {
                            data.Add(user);
                            flag = true;
                        }
                
            }

            List<UserAndProfilePic> userAndProfilePics = new List<UserAndProfilePic>();
            foreach (User f in data)
            {
                userAndProfilePics.Add(new UserAndProfilePic 
                { 
                    User = f,
                    ProfilePic = (await _profilePicService.GetAll(token))
                                    .LastOrDefault(option => option.Username == f.Username)
                });
            }

            return View("People", userAndProfilePics);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            string? message = null;
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);
            if (token == null)
                return RedirectToAction("AdminLogin");

            var result = await _userService.Delete(userId, token);
            if (result is not null)
            {
                message = JObject.Parse(result).GetValue("message")?.ToString();
                return RedirectToAction("People");
            }
            return RedirectToAction("ViewPeople", new { message = message });
        }

        public async Task<IActionResult> Queries(string? message = null)
        {
            ViewData["message"] = message;

            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);
            if (token == null)
                return RedirectToAction("AdminLogin");

            List<Query> Queries = await _queryService.GetAll(token);

            return View(Queries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveQuery(int id)
        {
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);
            if (token == null)
                return RedirectToAction("AdminLogin");

            var result = await _queryService.Delete(id, token);

            string? message = null;
            if (result is not null)
                message = JObject.Parse(result).GetValue("message")?.ToString();

            return RedirectToAction("Queries", new { message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAllQueries()
        {
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);
            if (token == null)
                return RedirectToAction("AdminLogin");

            var result = await _queryService.DeleteAll(token);
            string? message = null;
            if (result is not null)
                message = JObject.Parse(result).GetValue("message")?.ToString();

            return RedirectToAction("Queries", new { message = message });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactDeveloper()
        {
            return View();
        }

        public async Task<IActionResult> Community()
        {
            var username = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);
            if (token == null || username == null)
                return RedirectToAction("Index");

            List<User> Users = await _userService.GetAll();
            User? user = Users.FirstOrDefault(option => option.Username == username);

            List<Admin> Admins = await _adminService.GetAll();

            if (user == null)
                return RedirectToAction("Index");

            UserAndProfilePic data = new UserAndProfilePic 
            { 
                User = user,
                ProfilePic = (await _profilePicService.GetAll(token))
                                .LastOrDefault(option => option.Username == username)
            };

            List<Community> communities = await _messageService.GetAll(token);

            List<object> messages = new List<object>();

            foreach (var f in communities)
            {
                if (Admins.Any(option => option.Email == f.Sender))
                {
                    messages.Add(f);
                }
                else
                {
                    User? user__ = Users.FirstOrDefault(option => option.Username == f.Sender);

                    if (user__ != null)

                        messages.Add(new Message
                        {
                            userAndProfilePic = new UserAndProfilePic
                            {
                                User = user__,
                                ProfilePic = (await _profilePicService.GetAll(token)).LastOrDefault(option =>
                                                option.Username == user__.Username)
                            },
                            community = f
                        });
                }       
            }

            Tuple<UserAndProfilePic, List<object>> tuple = new 
                Tuple<UserAndProfilePic, List<object>> (data, messages);

            return View(tuple);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveMessage(string message)
        {
            var user = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (user == null || token == null)
                return RedirectToAction("Index");

            Community data = new Community 
            { 
                Id = 0,
                Sender = user,
                Message = message,
                DateTime = DateTime.UtcNow
            };

            _ = await _messageService.Add(data, token);

            return RedirectToAction("Community");
        }

        public async Task<IActionResult> AdminCommunity()
        {
            var admin = HttpContext.Session.GetString(_constants.ADMIN_NAME);
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (admin == null || token == null)
                return RedirectToAction("Index");

            List<Community> communities = await _messageService.GetAll(token);
            List<User> Users = await _userService.GetAll();

            List<object> messages = new List<object>();

            foreach (var f in communities)
            {
                if (f.Sender == admin)
                {
                    messages.Add(f);
                }
                else
                {
                    var user = Users.FirstOrDefault(option => option.Username == f.Sender);
                    if (user != null)
                    {
                        messages.Add(new Message 
                        { 
                            userAndProfilePic = new UserAndProfilePic 
                            { 
                                User = user,
                                ProfilePic = (await _profilePicService.GetAll(token))
                                                .LastOrDefault(option => option.Username == user.Username)
                            },

                            community = f 
                        });
                    }
                }
                
                
            }

            return View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAdminCommunityMessage(string message)
        {
            var admin = HttpContext.Session.GetString(_constants.ADMIN_NAME);
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (admin == null || token == null)
                return RedirectToAction("Index");

            Community community = new Community 
            { 
                Id = 0,
                Message = message,
                Sender = admin,
                DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
                                TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata"))
            };

            var result = await _messageService.Add(community, token);

            return RedirectToAction("AdminCommunity");
        }

        public async Task<IActionResult> MyPosts(string? message = null)
        {
            ViewData["message"] = message;

            var username = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (username == null || token == null)
                return RedirectToAction("Index");

            List<Post> posts = await _postService.GetAll(token);

            List<Post> myPosts = posts.Where(option => option.Sender == username)
                .OrderByDescending(option => option.Id).ToList();

            List<User> users = await _userService.GetAll();

            User? user = users.FirstOrDefault(option => option.Username == username);

            if (user == null)
                return RedirectToAction("Index");

            UserAndProfilePic userAndProfilePic = new UserAndProfilePic 
            { 
                User = user,
                ProfilePic = (await _profilePicService.GetAll(token))
                                .LastOrDefault(option => option.Username == user.Username)
            };

            Tuple<UserAndProfilePic, List<Post>> tuple = new 
                Tuple<UserAndProfilePic, List<Post>>(userAndProfilePic, myPosts);

            return View(tuple);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublishFile(IFormFile file)
        {
            var user = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (user == null || token == null)
                return RedirectToAction("Index");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media", "images", file.FileName);
            
            using (FileStream writer = new FileStream(filePath, FileMode.Create))
                file.CopyTo(writer);

            Post data = new Post();

            data.Sender = user;
            data.File = Path.Combine("media", "images", file.FileName);

            var result = await _postService.Add(data, token);

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveComment(int postId, string comment)
        {
            var user = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (user == null || token == null)
                return RedirectToAction("Index");

            Comment data = new Comment 
            { 
                Id = 0,
                Message = comment,
                PostId = postId,
                Sender = user,
                DateTime = TimeZoneInfo.ConvertTimeFromUtc(
                        DateTime.UtcNow,
                        TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")
                    )
            };

            var result = await _commentService.Add(data, token);

            return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> DisplayComment(int id)
        {
            var username = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (username == null || token == null)
                return RedirectToAction("Index");

            List<User> users = await _userService.GetAll();

            List<ProfilePic> profilePics = await _profilePicService.GetAll(token);

            var post = await _postService.Get(id, token);

            List<Comment> comments = (await _commentService.GetAll(token))
                                        .Where(option => option.PostId == id).ToList();

            List<UserComment> userComments = new List<UserComment>();


            foreach (var comment in comments)
            {
                var user = users.FirstOrDefault(option => option.Username == comment.Sender);

                if (user != null)
                    userComments.Add(new UserComment 
                    { 
                        Comment = comment,
                        UserAndProfilePic = new UserAndProfilePic 
                        { 
                            User = user,
                            ProfilePic = profilePics.LastOrDefault(option => option.Username == user.Username)
                        }
                    });
            }

            User? user__ = users.FirstOrDefault(option => option.Username == username);
            if (user__ == null)
                return RedirectToAction("Index");

            UserAndProfilePic userAndProfilePic = new UserAndProfilePic 
            { 
                User = user__,
                ProfilePic = profilePics.LastOrDefault(option => option.Username == user__.Username)
            };

            Tuple<UserAndProfilePic, List<UserComment>> tuple = new
                Tuple<UserAndProfilePic, List<UserComment>>(userAndProfilePic, userComments);

            return View(tuple);
        }

        public async Task<IActionResult> RemovePost(int id)
        {
            var username = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (username == null || token == null)
                return RedirectToAction("Index");

            var result = await _postService.Delete(id, token);
            string? message = null;
            
            if (result != null)
                message = JObject.Parse(result).GetValue("message")?.ToString();

            return RedirectToAction("MyPosts", new { message = message });
        }

        public async Task<IActionResult> Posts(string? message = null)
        {
            ViewData["message"] = message;

            var admin = HttpContext.Session.GetString(_constants.ADMIN_NAME);
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (admin == null || token == null)
                return RedirectToAction("AdminLogin");

            List<Post> posts = (await _postService.GetAll(token))
                                    .OrderByDescending(option => option.Id).ToList();

            List<FileUser> fileUsers = new List<FileUser>();
            List<User> users = await _userService.GetAll();

            foreach (var data in posts)
            {
                var user = users.FirstOrDefault(option => option.Username == data.Sender);

                if (user != null)
                    fileUsers.Add(new FileUser 
                    { 
                        Post = data,
                        UserAndProfilePic = new UserAndProfilePic 
                        { 
                            User = user,
                            ProfilePic = (await _profilePicService.GetAll(token))
                                            .LastOrDefault(option => option.Username == user.Username)
                        }
                    });
            }

            return View(fileUsers);
        }

        public async Task<IActionResult> DisplayCommentsToAdmin(int id)
        {
            var admin = HttpContext.Session.GetString(_constants.ADMIN_NAME);
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (admin == null || token == null)
                return RedirectToAction("AdminLogin");

            List<ProfilePic> profilePics = await _profilePicService.GetAll(token);
            List<User> users = await _userService.GetAll();
            List<UserComment> userComments = new List<UserComment>();
            List<Comment> comments = (await _commentService.GetAll(token))
                                        .Where(option => option.PostId == id)
                                        .OrderByDescending(option => option.Id)
                                        .ToList();

            foreach (var comment in comments)
            {
                var user = users.FirstOrDefault(option => option.Username == comment.Sender);
                if (user != null)
                    userComments.Add(new UserComment 
                    { 
                        Comment = comment,
                        UserAndProfilePic = new UserAndProfilePic 
                        { 
                            User = user,
                            ProfilePic = profilePics.LastOrDefault(option => option.Username == user.Username)
                        }
                    });
            }

            return View(userComments);
        }

        public async Task<IActionResult> RemovePostByAdmin(int id)
        {
            var admin = HttpContext.Session.GetString(_constants.ADMIN_NAME);
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (admin == null || token == null)
                return RedirectToAction("AdminLogin");

            var result = await _postService.Delete(id, token);

            string? message = null;
            if (result is not null)
                message = JObject.Parse(result).GetValue("message")?.ToString();

            return RedirectToAction("Posts", new { message = message });
        }

        public async Task<IActionResult> ChatPage(string receiver)
        {
            var username = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (username == null || token == null)
                return RedirectToAction("Index");

            List<User> users = await _userService.GetAll();

            var user_1 = users.FirstOrDefault(option => option.Username == username);
            var user_2 = users.FirstOrDefault(option => option.Username == receiver);

            if (user_1 == null)
                return RedirectToAction("Index");

            if (user_2 == null)
                return Content("User is not registered.");

            List<ProfilePic> profilePics = await _profilePicService.GetAll(token);

            UserAndProfilePic _1 = new UserAndProfilePic
            {
                User = user_1,
                ProfilePic = profilePics.LastOrDefault(option => option.Username == user_1.Username)
            };

            UserAndProfilePic _2 = new UserAndProfilePic
            {
                User = user_2,
                ProfilePic = profilePics.LastOrDefault(option => option.Username == user_2.Username)
            };

            List<Chat> chats = (await _chatService.GetAll(token))
                                    .Where(option => 
                                    (option.Sender == username && option.Receiver == receiver)
                                    || (option.Sender == receiver && option.Receiver == username))
                                    .ToList();

            List<ChatDetails> chatDetails = new List<ChatDetails>();

            foreach (var chat in chats)
            {
                if (chat.Sender == _1.User.Username)
                    chatDetails.Add(new ChatDetails
                    {
                        Chat = chat,
                        Sender = _1,
                        Receiver = _2
                    });

                else if (chat.Sender == _2.User.Username)
                    chatDetails.Add(new ChatDetails 
                    { 
                        Chat = chat,
                        Sender = _2,
                        Receiver = _1
                    });
            }

            Tuple<UserAndProfilePic, UserAndProfilePic, List<ChatDetails>> tuple = new
                Tuple<UserAndProfilePic, UserAndProfilePic, List<ChatDetails>>(_1, _2, chatDetails);

            return View(tuple);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Text(string receiver)
        {
            return RedirectToAction("ChatPage", new { receiver = receiver });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveChat(string receiver, string message)
        {
            var username = HttpContext.Session.GetString(_constants.USER_NAME);
            var token = HttpContext.Session.GetString(_constants.USER_TOKEN);

            if (username == null || token == null)
                return RedirectToAction("Index");

            Chat data = new Chat 
            { 
                Id = 0,
                DateTime = TimeZoneInfo.ConvertTimeFromUtc(
                        DateTime.UtcNow,
                        TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")
                    ),
                Message = message,
                Receiver = receiver,
                Sender = username
            };

            _ = await _chatService.Add(data, token);

            return RedirectToAction("ChatPage", new { receiver = receiver });
        }

        public IActionResult Chats(List<ChatDetails>? chatDetails = null)
        {
            if (chatDetails == null)
                chatDetails = new List<ChatDetails>();
            
            return View(chatDetails);
        }

        public async Task<IActionResult> SearchChats(string user_1, string user_2)
        {
            if (user_1 == null || user_2 == null)
                return RedirectToAction("Chats");

            var admin = HttpContext.Session.GetString(_constants.ADMIN_NAME);
            var token = HttpContext.Session.GetString(_constants.ADMIN_TOKEN);

            if (admin == null || token == null)
                return RedirectToAction("AdminLogin");

            List<Chat> chats = (await _chatService.GetAll(token)).Where(option =>
                (option.Sender == user_1 && option.Receiver == user_2)
                || (option.Sender == user_2 && option.Receiver == user_1))
                .OrderByDescending(option => option.Id)
                .ToList();

            List<ChatDetails> chatDetails = new List<ChatDetails>();
            List<User> users = await _userService.GetAll();

            List<ProfilePic> profilePics = await _profilePicService.GetAll(token);

            var _1 = users.FirstOrDefault(option => option.Username == user_1);
            var _2 = users.FirstOrDefault(option => option.Username == user_2);

            

            var __1_ = new UserAndProfilePic 
            { 
                User = _1,
                ProfilePic = profilePics.LastOrDefault(option => option.Username == _1.Username)
            };

            var __2_ = new UserAndProfilePic 
            { 
                User = _2,
                ProfilePic = profilePics.LastOrDefault(option => option.Username == _2.Username)
            };

            foreach (var chat in chats)
            {
                if (__1_.User.Username == chat.Sender)
                    chatDetails.Add(new ChatDetails
                    {
                        Chat = chat,
                        Sender = __1_,
                        Receiver = __2_
                    });
                else if (__2_.User.Username == chat.Sender)
                    chatDetails.Add(new ChatDetails 
                    { 
                        Chat =chat,
                        Sender = __2_,
                        Receiver = __1_
                    });
            }

            return View("Chats", chatDetails);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}