using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProjekt.Models;
using BlogProjekt.KlasyPomocnicze;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlogProjekt.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("PostyFollowedBlogs");
            }
            else
            {
                return RedirectToAction("ListaBlogow");
            }
        }

        [Authorize]
        public ActionResult ListaUzytkownikow()
        {
            if (ObsługaBazyDanych.sprawdzCzyUzytkownikJestAdminem(User.Identity.Name))
            {
                List<Users> listauzytkownikow = ObsługaBazyDanych.zwrocListeUzytkownikow();
                return View(listauzytkownikow);
            }
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult StworzBlog()
        {
            var kinds = ObsługaBazyDanych.bazadanych.KindsSet.Select(kind => new {KindId = kind.Kind_ID, KindName = kind.kindName}).ToList();
            ViewBag.Kinds = new MultiSelectList(kinds, "KindId", "KindName");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult StworzBlog(Blogs blog, FormCollection form)
        {
            if(ModelState.IsValid)
            {
                //foreach (int i in kindID) Debug.WriteLine(i.ToString());
                var test = form["KindsIds"];

                blog.dataAktualizacji = DateTime.Now;
                blog.dataZalozenia = DateTime.Now;
                blog.followCount = 0;
                ObsługaBazyDanych.dodajNowyBlog(User.Identity.Name, blog, test);
                return RedirectToAction("ListaBlogow");
            }
            return View();
        }

        public ActionResult ListaBlogow()
        {
            var listablogow = ObsługaBazyDanych.zwrocListeBlogow();
            
            return View(listablogow);
        }

        public ActionResult ListaBlogowPoKind(int id)
        {
            var listablogow = ObsługaBazyDanych.zwrocBlogiDanegoKind(id);
            return View("ListaBlogow", listablogow);
        }

        public ActionResult ListaPostowPoTagu(int id)
        {
            var listapostow = ObsługaBazyDanych.zwrocPostyDanegoTagu(id);
            return View(listapostow);
        }
        [Authorize]
        public ActionResult ListaBlogowUzytkownika(string username)
        {
            var listablogow = ObsługaBazyDanych.zwrocListeBlogowUzytkownika(username);
            return View(listablogow);
        }

        //na razie authorize
        
        public ActionResult WyswietlBlog(int id)
        {
            var blog = ObsługaBazyDanych.zwrocBlogPoId(id);
            return View(blog);
        }

        [Authorize]
        public ActionResult UsunBlog(int id)
        {
            ObsługaBazyDanych.usunBlog(id);
            return RedirectToAction("ListaBlogow");
        }

        [Authorize]
        public ActionResult StworzPost(int id)
        {
            TempData["BlogPostId"] = id;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult StworzPost(Post post, FormCollection form)
        {
            var idBlogu = (int)TempData["BlogPostId"];
            if(ModelState.IsValid)
            {
                var test = form["TagsIds"];
                ObsługaBazyDanych.dodajPost(idBlogu, post, test);
                return RedirectToAction("WyswietlBLog", new { id = idBlogu });
            }
            return View();
        }

        public ActionResult WyswietlPost(int id)
        {
            var post = ObsługaBazyDanych.zwrocPostPoId(id);
            
            return View(post);
        }
        [Authorize]
        public ActionResult UsunPost(int id, int idBlog)
        {
            ObsługaBazyDanych.usunPost(id);
            var blog = ObsługaBazyDanych.zwrocBlogPoId(idBlog);
            return RedirectToAction("WyswietlBlog", new { id = idBlog });
        }
        [Authorize]
        public ActionResult UsunKomentarz(int id, int idPostu)
        {
            ObsługaBazyDanych.usunKomentarz(id);
            var post = ObsługaBazyDanych.zwrocPostPoId(idPostu);
            return View("WyswietlPost", post);
        }
        [Authorize]
        public ActionResult StworzKomentarz(int id)
        {
            TempData["PostCommentId"] = id;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult StworzKomentarz(Comments comment)
        {
            var idPostu = (int)TempData["PostCommentId"];
            
            if (ModelState.IsValid)
            {
                comment.User_ID = ObsługaBazyDanych.zwrocIdUzytkownikaPoUsername(User.Identity.Name);
                ObsługaBazyDanych.dodajKomentarz(idPostu, comment);
                return RedirectToAction("WyswietlPost", new { id = idPostu });
            }
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult StworzKomentarzJava(int id, string content)
        {
            Debug.WriteLine(id + content);

            Comments comment = new Comments();

            comment.content = content;
            comment.User_ID = ObsługaBazyDanych.zwrocIdUzytkownikaPoUsername(User.Identity.Name);
            ObsługaBazyDanych.dodajKomentarz(id, comment);

            return Json(new { id1 = id}, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult StworzKind()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult StworzKind(Kinds kind)
        {
            if(ModelState.IsValid)
            {
                ObsługaBazyDanych.dodajKind(kind);
                return RedirectToAction("ListaKind");
            }
            return View();
        }
        [Authorize]
        public ActionResult StworzTag()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult StworzTag(Tags tag)
        {
            if (ModelState.IsValid)
            {
                ObsługaBazyDanych.dodajTag(tag);
                return RedirectToAction("ListaTagow");
            }
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult stworztagasync(string tagName)
        {
            Debug.WriteLine(tagName);
            ObsługaBazyDanych.dodajTag(tagName);
            var tag = ObsługaBazyDanych.ZwrocTagPoNazwie(tagName);
            return Json(new { id = tag.Tag_ID, nazwa = tag.tagName  }, JsonRequestBehavior.AllowGet);

        }
        [Authorize]
        [HttpGet]
        public ActionResult komentarzUpVote(string userName,string commentID)
        {
            Debug.WriteLine(userName+" "+commentID+"pierwszyGet");
            ObsługaBazyDanych.DodajGlosDoKomentarza(userName, commentID);
            //ObsługaBazyDanych.bazadanych.VotesSet.Add();
            var comment = ObsługaBazyDanych.zwrocKomentarzPoID(Int32.Parse(commentID));
             return Json(new { upVotes = comment.upVotes, id = comment.Comment_ID, user= userName},JsonRequestBehavior.AllowGet);

        }
        [Authorize]
        [HttpGet]
        public ActionResult komentarzDownVote(string userName, string commentID)
        {
            Debug.WriteLine(userName + " " + commentID + "pierwszyGet");
            ObsługaBazyDanych.UsunGlosDoKomentarza(userName, commentID);
            //ObsługaBazyDanych.bazadanych.VotesSet.Add();
            var comment = ObsługaBazyDanych.zwrocKomentarzPoID(Int32.Parse(commentID));
            return Json(new { upVotes = comment.upVotes, id = comment.Comment_ID, user = userName }, JsonRequestBehavior.AllowGet);

        }
        [Authorize]
        [HttpGet]
        public ActionResult blogFollow(string userName, string blogID)
        {
            Debug.WriteLine(userName + " " + blogID + "pierwszyGet");
            ObsługaBazyDanych.DodajFollowDoBloga(userName, blogID);
            //ObsługaBazyDanych.bazadanych.VotesSet.Add();
            var blog = ObsługaBazyDanych.zwrocBlogPoId(Int32.Parse(blogID));
            return Json(new { followCount = blog.followCount, id = blog.Blog_ID, user = userName }, JsonRequestBehavior.AllowGet);

        }
        [Authorize]
        [HttpGet]
        public ActionResult blogUnFollow(string userName, string blogID)
        {
            Debug.WriteLine(userName + " " + blogID + "pierwszyGet");
            ObsługaBazyDanych.UsunFollowZBloga(userName, blogID);

            var blog = ObsługaBazyDanych.zwrocBlogPoId(Int32.Parse(blogID));
            return Json(new { followCount = blog.followCount, id = blog.Blog_ID, user = userName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult renderPostView(string id)
        {
            Debug.WriteLine("Witam po udanym poście"+id);
            return Json(new { id1 = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult commentPartialView(string commentId)
        {
            var lookupId = int.Parse(commentId);
            var model =  ObsługaBazyDanych.zwrocKomentarzPoID(lookupId);
            return PartialView("commentsPartial", model);
        }

        [HttpGet]
        public ActionResult commentListPartialView(string postID)
        {
            Debug.WriteLine(postID);
            var post = ObsługaBazyDanych.zwrocPostPoId(Int32.Parse(postID));
            return PartialView("commentListPartial", post);
        }
        [HttpGet]
        public ActionResult blogPartialView(string blogID)
        {
            var lookupId = int.Parse(blogID);
            var model = ObsługaBazyDanych.zwrocBlogPoId(lookupId);
            return PartialView("blogFollowButton", model);
        }

        public ActionResult ListaTagow()
        {
            return View(ObsługaBazyDanych.zwrocListeTag());
        }

        public ActionResult ListaKind()
        {
            return View(ObsługaBazyDanych.zwrocListeKind());
        }
        [Authorize]
        public ActionResult EdytujBlog(int id)
        {
            var blog = ObsługaBazyDanych.zwrocBlogPoId(id);
            return View(blog);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EdytujBlog(Blogs blog, FormCollection form)
        {
            if(ModelState.IsValid)
            {
                var test = form["KindsIds"];
                ObsługaBazyDanych.EdytujBlog(blog, test);
                return RedirectToAction("ListaBlogow");
            }
            return RedirectToAction("ListaBlogow");
        }
        [Authorize]
        public ActionResult EdytujPost(int id)
        {
            
            var post = ObsługaBazyDanych.zwrocPostPoId(id);
            TempData["BlogPostId"] = post.Blogs.Blog_ID;
            return View(post);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EdytujPost(Post post, FormCollection form)
        {
            var idBlogu = (int)TempData["BlogPostId"];
            if (ModelState.IsValid)
            {
                var test = form["TagsIds"];
                ObsługaBazyDanych.EdytujPost(post, test);
                return RedirectToAction("WyswietlBLog", new { id = idBlogu });
            }
            return RedirectToAction("WyswietlBLog", new { id = idBlogu });
        }
        [Authorize]
        public ActionResult UsunKind(int id)
        {
            if(ObsługaBazyDanych.sprawdzCzyUzytkownikJestAdminem(User.Identity.Name))
                ObsługaBazyDanych.UsunKind(id);
            return RedirectToAction("ListaKind");
        }
        [Authorize]
        public ActionResult UsunTag(int id)
        {
            if (ObsługaBazyDanych.sprawdzCzyUzytkownikJestAdminem(User.Identity.Name))
                ObsługaBazyDanych.UsunTag(id);
            return RedirectToAction("ListaTagow");
        }
        [Authorize]
        public ActionResult PostyFollowedBlogs()
        {
            var listapostow = ObsługaBazyDanych.zwrocPostyZFollowanychBlogow(User.Identity.Name);

            return View(listapostow);
        }

    }
}