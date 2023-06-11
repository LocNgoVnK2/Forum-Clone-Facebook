using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using MiniForum.Models;
using System.Linq;

namespace MiniForum.Controllers
{
    public class FriendFollowController : Controller
    {
        private readonly IMapper mapper;
        private readonly IFriendFollowService friendFollowService;
        private readonly IUserService userService;
        public FriendFollowController(IUserService userService, IMapper mapper, IFriendFollowService friendFollowService)
        {
           
            this.mapper = mapper;
            this.friendFollowService = friendFollowService;
            this.userService = userService;

        }
        public IActionResult Index(string typeCall, string userId)
        {
          
                IQueryable<User> users = userService.GetUsers();
                User_FollowViewModel User_Follow = new User_FollowViewModel();

                IQueryable<FriendFollow> Friends = friendFollowService.GetFriendFollows();
            if (typeCall == "allUser")
            {


                User_Follow.users = users;
                User_Follow.friendFollows = Friends;
                return View(User_Follow);
            }else if(typeCall == "follower")
            {
                IQueryable<User> filteredUsers = users.Where(u => Friends.Any(f => f.UserID2 == userId && f.UserID1 == u.UserID));

                User_FollowViewModel userFollow = new User_FollowViewModel();
                userFollow.users = filteredUsers;
                userFollow.friendFollows = Friends;

                return View(userFollow);
            }
            else // folowing
            {
                IQueryable<User> filteredUsers = users.Where(u => Friends.Any(f => f.UserID1 == userId && f.UserID2 == u.UserID));

                User_FollowViewModel userFollow = new User_FollowViewModel();
                userFollow.users = filteredUsers;
                userFollow.friendFollows = Friends;

                return View(userFollow);
            }
           
        }
        public IActionResult Follower(string userId)
        {
   
            IQueryable<User> users = userService.GetUsers();
            IQueryable<FriendFollow> friends = friendFollowService.GetFriendFollows();

           

            IQueryable<User> filteredUsers = users.Where(u => friends.Any(f => f.UserID2 == userId && f.UserID1 == u.UserID));

            User_FollowViewModel userFollow = new User_FollowViewModel();
            userFollow.users = filteredUsers;
            userFollow.friendFollows = friends;

            return View(userFollow);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFollow(User_FollowViewModel model)
        {
            FriendFollow friendFollow = mapper.Map<FriendFollow>(model);
            friendFollowService.InsertFriendFollow(friendFollow);
            string typeCall = "allUser"; // Thay đổi type thành typeCall
            return RedirectToAction("Index", "FriendFollow", new { typeCall });


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFollow(User_FollowViewModel model)
        {
           
            FriendFollow friendFollow = mapper.Map<FriendFollow>(model);
            friendFollowService.DeleteFriendFollow(friendFollow);
            string typeCall = "allUser"; // Thay đổi type thành typeCall
            return RedirectToAction("Index", "FriendFollow", new { typeCall });
        }

    }
}
