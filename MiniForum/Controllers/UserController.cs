using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniForum.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MiniForum.Controllers
{
    public class UserController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly ILike_DetailService like_DetailService;
        private readonly IFriendFollowService friendFollowService;
        private readonly IMapper mapper;
        private readonly EXDbContext dbContext;
        public UserController(IFriendFollowService friendFollowService,ILike_DetailService like_DetailService,IPostService postService,IUserService userService, IMapper mapper, EXDbContext dbContext)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.postService = postService;
            this.like_DetailService = like_DetailService;
            this.friendFollowService = friendFollowService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserViewModel userViewModel)
        {
            var usera = userViewModel.Name;
            if (ModelState.IsValid)
            {

                
                var actualUser = userService.GetUserByName(userViewModel.Name);

                if (actualUser != null && actualUser.PassWord == userViewModel.PassWord)
                {
                    User user = new User();
                    user.UserID = actualUser.UserID; // gán giá trị cho thuộc tính UserID
                    user.Name = actualUser.Name;
                    user.PassWord = actualUser.PassWord;
                    user.Avatar = actualUser.Avatar;

               
           
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
             
                    return RedirectToAction("Index", "Post");
                }
                else
                {

                    TempData["ErrorMessage"] = "Đăng nhập thất bại. Vui lòng kiểm tra lại tài khoản và mật khẩu.";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserViewModel userViewModel)
        {
            
            var actualUser = userService.GetUserByName(userViewModel.Name);
            User UserExist = userService.GetUser(userViewModel.UserID);
            
            if (actualUser == null && UserExist==null)
            {
                User user = new User();
                user.UserID = userViewModel.UserID; // gán giá trị cho thuộc tính UserID
                user.Name = userViewModel.Name;
                user.PassWord = userViewModel.PassWord;
                user.Avatar =null;
                userService.InsertUser(user);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản đã tồn tại";
            }
            return View();
        }

        public IActionResult PersonalPage(string UserId)
        {

            var posts = postService.GetPosts()
                        .Include(p => p.User)
                        .OrderByDescending(p => p.PostID)
                        .Where(post => post.UserID == UserId)
                        .ToList();
            IQueryable<Like_Detail> likes = like_DetailService.GetLike_Details();
            foreach (var postViewModel in posts)
            {
                
                var likeCount = like_DetailService.CountLikeForEachPost(postViewModel.PostID);
                postViewModel.LikeCount = likeCount;

                var likedUsers = likes.Where(l => l.PostID == postViewModel.PostID)
                        .Select(l => l.UserID)
                        .ToList();
                postViewModel.LikedUserIDs = likedUsers;
            }
            IQueryable<FriendFollow> friends = friendFollowService.GetFriendFollows();
            IQueryable<Post> allPost = postService.GetPosts();
            int follower = friends.Count(id=>id.UserID2 == UserId);
            int postCount = allPost.Count(id => id.UserID == UserId);
            int following = friends.Count(id => id.UserID1 == UserId);
            HttpContext.Session.SetInt32("following", following);
            HttpContext.Session.SetInt32("follower", follower); 
            HttpContext.Session.SetInt32("postCount", postCount);
            return View(posts);
           
        }


        public IActionResult UpdateInformation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateInformation(UserViewModel userViewModel)
        {
         
            User user = userService.GetUser(userViewModel.UserID);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = userViewModel.Name;

            if (string.IsNullOrEmpty(userViewModel.PassWord))
            {
                // Lấy lại mật khẩu cũ từ cơ sở dữ liệu
                var oldUser = userService.GetUser(user.UserID);
                if (oldUser != null)
                {
                    user.PassWord = oldUser.PassWord;
                }
            }
            else
            {
                user.PassWord = userViewModel.PassWord;
            }

            if (!string.IsNullOrEmpty(userViewModel.Avatar))
                user.Avatar = userViewModel.Avatar;
            
            
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();

            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));


            return RedirectToAction("PersonalPage", "User", new { UserId = userViewModel.UserID });
        }

    }
}