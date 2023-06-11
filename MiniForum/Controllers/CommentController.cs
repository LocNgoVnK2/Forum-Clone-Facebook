using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using MiniForum.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniForum.Controllers
{
    public class CommentController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly ICommentService _commentService;
       

        private readonly IMapper mapper;
        public CommentController(ICommentService _commentService, IUserService userService, IPostService postService,   IMapper mapper)
        {
            this._commentService = _commentService;
            this.mapper = mapper;
            this.postService = postService;
            this.userService = userService;
        
        }

        public IActionResult Index(int postId)
        {
            
            Post p = postService.GetPost(postId);

            PostViewModel postView = mapper.Map<PostViewModel>(p);
            postView.User = mapper.Map<UserViewModel>(userService.GetUser(p.UserID));

            IQueryable<Post> posts = postService.GetPosts();
            IQueryable<Comment> commentsContent = _commentService.GetComments();
            IQueryable<User> users = userService.GetUsers();

            var query = from comment in commentsContent
                        join post in posts on comment.PostID equals p.PostID
                        where post.PostID == p.PostID
                        join user in users on comment.UserID equals user.UserID
                        select new CommentViewModel
                        {
                            PostID = comment.PostID,
                            CmtID = comment.CmtID,
                            UserID = comment.UserID,
                            User = mapper.Map<UserViewModel>(user),
                            Content = comment.Content,
                        };
         
            postView.Comments = query.ToList().OrderByDescending(x => x.CmtID).ToList();

            

            Post_CommentViewModel postComment = new Post_CommentViewModel();
            postComment.postViewModel = postView;
            postComment.commentViewModel = new CommentViewModel();

            //bỏ post id qua view để thê
            ViewBag.PostId = postId;

            return View(postComment);
        }

        // POST: Comment_Detail/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post_CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Comment comment = mapper.Map<Comment>(model.commentViewModel);
                _commentService.InsertComment(comment);

  

                    
                    //return RedirectToAction("Index", "Post");
                    return RedirectToAction("Index", "Comment", new {/* routeValues, for example: */ postId = comment.PostID });

                    //return RedirectToAction("Index", "Comment_Detail");

                }
                else
                {
                    ViewBag.ErrorMessage = "Bình luận đã tồn tại";
                }
            return RedirectToAction("Index", "Comment", new { postId = model.postViewModel.PostID });
            //return RedirectToAction("Index", new { postId = model.PostID });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Post_CommentViewModel model)
        {

            if (ModelState.IsValid)
            {
                Comment comment = mapper.Map<Comment>(model.commentViewModel);
                _commentService.DeleteComment(comment);
                 
            }
            return RedirectToAction("Index", "Comment", new { postId = model.commentViewModel.PostID });
         
        }

    }
}
