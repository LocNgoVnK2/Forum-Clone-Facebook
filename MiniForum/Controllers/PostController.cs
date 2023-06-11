using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MiniForum.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IFriendFollowService friendFollowService;
        private readonly ILike_DetailService like_detailService;
        private readonly IUserService userService;
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        public PostController(IUserService userService, ICommentService commentService, IPostService postService, ILike_DetailService like_detailService, IFriendFollowService friendFollowService, IMapper mapper)
        {
            this.postService = postService;
            this.mapper = mapper;
            this.like_detailService = like_detailService;
            this.friendFollowService = friendFollowService;
            this.userService = userService;
            this.commentService= commentService;
        }

        public IActionResult Index()
        {

            
            var posts = postService.GetPosts()
                        .Include(p => p.User)
                        .OrderByDescending(p => p.PostID)
                        .ToList();
            IQueryable<Like_Detail> likes = like_detailService.GetLike_Details();
            foreach (var postViewModel in posts)
            {
                var likeCount = like_detailService.CountLikeForEachPost(postViewModel.PostID);
                postViewModel.LikeCount = likeCount;

                var likedUsers = likes.Where(l => l.PostID == postViewModel.PostID)
                        .Select(l => l.UserID)
                        .ToList();
                postViewModel.LikedUserIDs = likedUsers;


            }
            IQueryable<FriendFollow> friends = friendFollowService.GetFriendFollows();
            List<FriendFollow> friendList = friends.ToList();

            string friendListJson = JsonConvert.SerializeObject(friendList);
            HttpContext.Session.SetString("Friends", friendListJson);
            return View(posts);
        }
        
        public IActionResult AddOrEdit(string typeCall,int id = 0)
        {
            PostViewModel postView;
            if (id != 0)
            {
                Post p = postService.GetPost(id);

                postView = mapper.Map<PostViewModel>(p);

                if (postView == null)
                {
                    return NotFound();
                }
                // Truy vấn đối tượng User tương ứng với UserID
         
            }
            else
            {
                Response.Cookies.Append("TypeCall", typeCall);
                postView = new PostViewModel();
            }
           
            return View(postView);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(PostViewModel post)
        {
            string typeCall = Request.Cookies["TypeCall"];
            if (ModelState.IsValid)
            {
                try
                {

                    Post pos= mapper.Map<Post>(post);
                    User user = userService.GetUser(pos.UserID);
                    pos.User = user;

                    if (post.Photo != null && post.Photo.Length > 0)
                        pos.Photo = ConvertToByteArray(post.Photo);
                    else
                        pos.Photo=null;

                    if (pos.PostID == 0)
                        
                        postService.InsertPost(pos);
                    else
                    {
                        Post oldPost = postService.GetPost(pos.PostID);
                        oldPost.Caption = pos.Caption;
                        if (post.Photo != null && post.Photo.Length > 0)
                            oldPost.Photo = ConvertToByteArray(post.Photo);
                        else if (Request.Form["RemovePhoto"] == "on")
                            oldPost.Photo = null;
                        postService.UpdatePost(oldPost);
                    }
                }


                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }



            if (typeCall == "Home")
            {
                return RedirectToAction("Index", "Post");
            }
            else if (typeCall == "Personal")
            {
               
                var userJson = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<User>(userJson);

                var userId = user.UserID;

                return RedirectToAction("PersonalPage", "User", new { UserId = userId });
            }
            else
            {
                return RedirectToAction("Index", "Post");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, string typeCall)
        {

            Post p = postService.GetPost(id);

            var likeDetails = like_detailService.GetLike_Details().Where(ld => ld.PostID == id).ToList();
            var comments = commentService.GetComments().Where(cmt => cmt.PostID == id).ToList();
            foreach (var likeDetail in likeDetails)
            {
                like_detailService.DeleteLike_Detail(likeDetail);
            }
            foreach(var comment in comments)
            {
                commentService.DeleteComment(comment);
            }
            postService.DeletePost(p);




            if (typeCall == "Home")
            {
                return RedirectToAction("Index", "Post");
            }
            else if (typeCall == "Personal")
            {

                var userJson = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<User>(userJson);

                var userId = user.UserID;

                return RedirectToAction("PersonalPage", "User", new { UserId = userId });
            }
            else
            {
                return RedirectToAction("Index", "Post");
            }
        }

        public byte[] ConvertToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public IActionResult InsertLike(int postId, string userId, string typeCall)
        {
            // Kiểm tra nếu người dùng đã like bài viết này thì không thêm like nữa
            if (like_detailService.GetLike_Detail(postId, userId) != null)
            {
                return RedirectToAction("Index", "Post");
            }
            
            // Tạo một đối tượng Like_Detail mới
            Like_Detail like = new Like_Detail
            {
                PostID = postId,
                UserID = userId
            };

            // Lưu thông tin like vào database
            like_detailService.InsertLike_Detail(like);



            if (typeCall == "Home")
            {
                return RedirectToAction("Index", "Post");
            }
            else if (typeCall == "Personal")
            {

                var userJson = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<User>(userJson);

                var Id = user.UserID;

                return RedirectToAction("PersonalPage", "User", new { UserId = Id });
            }
            else
            {
                return RedirectToAction("Index", "Post");
            }
        }
        public IActionResult DeleteLike(int postId, string userId, string typeCall)
        {
            // Kiểm tra nếu người dùng đã like bài viết này thì không thêm like nữa
            if (!like_detailService.GetLike_Details().Any(like => like.PostID == postId && like.UserID == userId))
            {
                return RedirectToAction("Index", "Post");
            }

            // Tạo một đối tượng Like_Detail mới
            Like_Detail like = new Like_Detail
            {
                PostID = postId,
                UserID = userId
            };

            // Lưu thông tin like vào database
            like_detailService.DeleteLike_Detail(like);

            if (typeCall == "Home")
            {
                return RedirectToAction("Index", "Post");
            }
            else if (typeCall == "Personal")
            {

                var userJson = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<User>(userJson);

                var Id = user.UserID;

                return RedirectToAction("PersonalPage", "User", new { UserId = Id });
            }
            else
            {
                return RedirectToAction("Index", "Post");
            }
        }




    }
}
