using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MVCSocialMedia.Data;
using MVCSocialMedia.Models;
using MVCSocialMedia.Services;

namespace MVCSocialMedia.Controllers
{
    public class PostsController : Controller
    {
        private readonly IUploadedFileChecker _uploadedFileChecker;
        private readonly IPostRepository _postRepository;

        public PostsController(IUploadedFileChecker uploadedFileChecker, IPostRepository postRepository)
        {
            _uploadedFileChecker = uploadedFileChecker;
            _postRepository = postRepository;
        }


        // GET: Posts
        public async Task<IActionResult> Index()
        {

            //Attempt with postRepository
            return _postRepository != null ?
                        View( _postRepository.GetPosts()) :
                        Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        }

        // GET: Posts in admin view
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllPostsBaseView()
        {
            return _postRepository != null ?
                        View( _postRepository.GetPosts()) :
                        Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        }

        // GET: Posts/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _postRepository == null)
            {
                return NotFound();
            }

            //var post = _context.Posts.FindByIDAsync(id);
            //var post = _postRepository.GetByIdAsync(id);
            var post = _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return View("Details", post);
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //GET: Posts/ViewUserPosts
        [Authorize]
        public async Task<IActionResult> ViewUserPosts()
        {
            return _postRepository != null ?
                View("Index", _postRepository.ViewSpecificUserPosts(User.Identity.Name)) :
                Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            /*return _context.posts != null ?
                        View("Index", await _context.Posts.Where(x => x.Username.Equals(User.Identity.Name)).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            */
        }

        //Post: Posts/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            /*return _context.Posts != null ?
                          View("Index", await _context.Posts.Where(j => j.Title.Contains(SearchPhrase)).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Posts'  is null.")
            */

            return _postRepository != null ?
                          View("Index", _postRepository.GetSearchResults(SearchPhrase)) :
                          Problem("Entity set 'ApplicationDbContext.Posts'  is null.");


        }

        //GET: Posts/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostRequest request, IFormFile? file)
        {

            if (!ModelState.IsValid)
            {
                return View("");
            }
            byte[] PostImageAsByteArray = null;
            if (file != null)
            {
                var memoryStream = new MemoryStream();

                await file.CopyToAsync(memoryStream);

                if (_uploadedFileChecker.CheckFileSizeResult(file))
                {
                    PostImageAsByteArray = memoryStream.ToArray();
                }
                else
                {
                    //TODO Add warning on form that file is too large
                }
            }

            request.Username = User.Identity.Name;

            Post newPost = new Post(request.Title, request.Username, request.OpinionText, PostImageAsByteArray);



            _postRepository.InsertPost(newPost);
            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            /*if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
            */

            if (id == null || _postRepository == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreatePostRequest request, IFormFile? file)
        {
            request.Username = User.Identity.Name;

            //var post = await _context.Posts.FindAsync(request.Id);
            var post = await _postRepository.GetByIdAsync(request.Id);

            if (ModelState.IsValid)
            {
                post.Title = request.Title;
                post.OpinionText = request.OpinionText;

                if (file != null)
                {
                    var memoryStream = new MemoryStream();

                    await file.CopyToAsync(memoryStream);

                    if (_uploadedFileChecker.CheckFileSizeResult(file))
                    {
                        post.PostImageAsByteArray = memoryStream.ToArray();
                    }
                    else
                    {
                        //TODO Add warning on form that file is too large
                    }
                }

                try
                {
                    //_context.Update(post);
                    //await _context.SaveChangesAsync();
                    _postRepository.UpdatePost(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            /*if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
            */

            if (id == null || _postRepository == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */


            if (_postRepository == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var post = await _postRepository.GetByIdAsync(id);
            if (post != null)
            {
                _postRepository.DeletePost(post);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _postRepository.DoesPostExist(id);
        }
    }
}
