using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using BlogProjekt.Models;

namespace BlogProjekt.KlasyPomocnicze
{
    public static class ObsługaBazyDanych
    {
        public static BlogBazaContainer bazadanych = new BlogBazaContainer();

        public static List<Users> zwrocListeUzytkownikow()
        {
            return bazadanych.UsersSet.ToList();
        }

        public static void dodajNowegoUzytkownika(Users user)
        {
            bazadanych.UsersSet.Add(user);
            bazadanych.SaveChanges();
            Debug.WriteLine("test");
        }

        public static Users sprawdzCzyUzytkownikIsniteje(string email, string password)
        {
            Users tmpUser = null;
            tmpUser = bazadanych.UsersSet.FirstOrDefault(user => user.email == email && user.password == password);
            return tmpUser;
        }

        public static int zwrocIdUzytkownikaPoUsername(string username)
        {
            return bazadanych.UsersSet.FirstOrDefault(user => user.username == username).User_ID;
        }

        public static bool sprawdzCzyUzytkownikJestAdminem(string username)
        {
            Users tmpUser = null;
            tmpUser = bazadanych.UsersSet.FirstOrDefault(user => user.username == username);
            if (tmpUser != null)
                return tmpUser.isAdmin;
            return false;
        }


        public static void dodajNowyBlog(String username, Blogs blog,string test)
        {
            Users tmpUser = bazadanych.UsersSet.FirstOrDefault(user => user.username == username);
            blog.UsersUser_ID = tmpUser.User_ID;
            blog.Users = tmpUser;
            blog.status = "Następnym razem jak rafonix wymyśli takie pole to go zabije pozdrawiam serdecznie";

            string[] tmpArray = test.Split(',');
            foreach (var i in tmpArray)
             {
                if(i != ",")
                {
                    int id = Int32.Parse(i.ToString());
                    blog.Kinds.Add(bazadanych.KindsSet.FirstOrDefault(kind => kind.Kind_ID == id));
                }
             }
            //tmpUser.Blogs.Add(blog);
            try
            {
                bazadanych.BlogsSet.Add(blog);
                bazadanych.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
            }
        }

        public static List<Blogs> zwrocListeBlogow()
        {
            return bazadanych.BlogsSet.ToList();
        }

        public static List<Blogs> zwrocListeBlogowUzytkownika(string username)
        {
            return bazadanych.BlogsSet.Where(blog => blog.Users.username == username).ToList();
        }

        public static Blogs zwrocBlogPoId(int id)
        {
            return bazadanych.BlogsSet.FirstOrDefault(blog => blog.Blog_ID == id);
        }

        public static void usunBlog(int id)
        {
            var tmpBlog = bazadanych.BlogsSet.FirstOrDefault(blog => blog.Blog_ID == id);
            foreach(var post in tmpBlog.Post.ToList())
            {
                usunPost(post);
            }
            //TODO:
            //wyczyszczenie ze wszystkich tabel powiazan z danym blogiem. A wiec tablica postow(a wiec i komentarzy ale to w metodzie usunPost), kids-blogs tam gdzie byl ten blog (kindsa z Kinds nie), i z followowanych też tam gdzie wystąpil blog. 
            bazadanych.BlogsSet.Remove(tmpBlog);
            

            bazadanych.SaveChanges();
        }

        public static List<Post> zwrocListePostow()
        {
            return bazadanych.PostSet.ToList();
        }

        public static void dodajPost(int id, Post post, string test)
        {
            Debug.WriteLine(test);
            string[] tmpArray = test.Split(',');
            foreach(var i in tmpArray)
            {
                if(i != ",")
                {
                    int id1 = Int32.Parse(i.ToString());
                    post.Tags.Add(bazadanych.TagsSet.FirstOrDefault(tag => tag.Tag_ID == id1));
                }
            }
            Blogs tmpBlog = bazadanych.BlogsSet.FirstOrDefault(blog => blog.Blog_ID == id);
            post.dataAktualizacji = DateTime.Now;
            post.dataStworzenia = DateTime.Now;
            post.image_content_type = "nop";
            post.image_file_size = 10;
            post.image_upload_Date = DateTime.Now;
            post.status = "Jak rafonix kiedys znowu wymysli takie pole to go zabije";
 
            //post.Blogs = tmpBlog;
            //post.BlogsBlog_ID = tmpBlog.Blog_ID;
            
            try
            {
                tmpBlog.Post.Add(post);
                //bazadanych.PostSet.Add(post);
                bazadanych.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
            }
        }

        public static void usunPost(int id)
        {
            var tmpPost = bazadanych.PostSet.FirstOrDefault(post => post.Post_ID == id);
            foreach (var comment in tmpPost.Comments.ToList())
            {
                usunKomentarz(comment);
            }
            bazadanych.PostSet.Remove(tmpPost);
            bazadanych.SaveChanges();
        }
        public static void usunPost(Post post)
        {
            foreach (var comment in post.Comments.ToList())
            {
                usunKomentarz(comment);
            }
            bazadanych.PostSet.Remove(post);
            bazadanych.SaveChanges();
        }

        public static Post zwrocPostPoId(int id)
        {
            return bazadanych.PostSet.FirstOrDefault(post => post.Post_ID == id);
        }

        public static void dodajKomentarz(int id, Comments comment)
        {
            Post tmpPost = bazadanych.PostSet.FirstOrDefault(post => post.Post_ID == id);
            comment.dataAktualizacji = DateTime.Now;
            comment.dataIGodzina = DateTime.Now;
            comment.dataStworzenia = DateTime.Now;
            comment.upVotes = 0;
            
            try
            {
                tmpPost.Comments.Add(comment);
                bazadanych.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
            }
        }

        public static void usunKomentarz(int id)
        {
            var tmpKomentarz = bazadanych.CommentsSet.FirstOrDefault(comments => comments.Comment_ID == id);
            var vps = bazadanych.VotesSet.Where(vote => vote.CommentsComment_ID == id).ToList();

            foreach(var tmpVote in vps)
            {
                bazadanych.VotesSet.Remove(tmpVote);
            }
            bazadanych.CommentsSet.Remove(tmpKomentarz);
            bazadanych.SaveChanges();
        }
        public static void usunKomentarz(Comments comment)
        {
            var vps = bazadanych.VotesSet.Where(vote => vote.CommentsComment_ID == comment.Comment_ID).ToList();

            foreach (var tmpVote in vps)
            {
                bazadanych.VotesSet.Remove(tmpVote);
            }
            bazadanych.CommentsSet.Remove(comment);
            bazadanych.SaveChanges();
        }

        public static void dodajKind(Kinds kind)
        {
            kind.dataAktualizacji = DateTime.Now;
            kind.dataStworzenia = DateTime.Now;

            bazadanych.KindsSet.Add(kind);
            bazadanych.SaveChanges();

        }

        public static List<Kinds> zwrocListeKind()
        {
            return bazadanych.KindsSet.ToList();
        }

        public static void dodajTag(Tags tag)
        {
            tag.dataAktualizacji = DateTime.Now;
            tag.dataStworzenia = DateTime.Now;

            bazadanych.TagsSet.Add(tag);
            bazadanych.SaveChanges();
        }

        public static void dodajTag(string tagName)
        {
            Tags tmpTag = new Tags();
            tmpTag.dataAktualizacji = DateTime.Now;
            tmpTag.dataStworzenia = DateTime.Now;
            tmpTag.tagName = tagName;

            bazadanych.TagsSet.Add(tmpTag);
            bazadanych.SaveChanges();
        }

        public static Tags ZwrocTagPoNazwie(string nazwa)
        {
            return bazadanych.TagsSet.FirstOrDefault(tag => tag.tagName == nazwa);
        }
        public static Tags ZwrocTagPoId(int id)
        {
            return bazadanych.TagsSet.FirstOrDefault(tag => tag.Tag_ID == id);
        }

        public static List<Tags> zwrocListeTag()
        {
            return bazadanych.TagsSet.ToList();
        }

        public static int ilePostowPodTagiem(Tags tag)
        {
            return tag.Post.Count();
        }

        public static int ileBlogowPodRodzajem(Kinds kind)
        {
            return kind.Blogs.Count();
        }


        public static Comments zwrocKomentarzPoID(int id)
        {
            return bazadanych.CommentsSet.FirstOrDefault(comment => comment.Comment_ID == id);
        }

        public static void DodajGlosDoKomentarza(string userName, string commentID)
        {
            Votes vote = new Votes();
            vote.UsersUser_ID = zwrocIdUzytkownikaPoUsername(userName);
            vote.CommentsComment_ID = Int32.Parse(commentID);
            bazadanych.VotesSet.Add(vote);

            var comment = zwrocKomentarzPoID(Int32.Parse(commentID));
            comment.upVotes++;

            bazadanych.SaveChanges();
        }

        public static void DodajFollowDoBloga(string userName, string blogID)
        {
            Follows follow = new Follows();
            follow.UsersUser_ID = zwrocIdUzytkownikaPoUsername(userName);
            follow.BlogsBlog_ID = Int32.Parse(blogID);
            bazadanych.FollowsSet.Add(follow);

            var blog = zwrocBlogPoId(Int32.Parse(blogID));
            blog.followCount++;

            bazadanych.SaveChanges();

        }

        public static void UsunGlosDoKomentarza(string userName, string commentID)
        {
            var userid = zwrocIdUzytkownikaPoUsername(userName);
            var commentid = Int32.Parse(commentID);
            var tmpVote = bazadanych.VotesSet.FirstOrDefault(vote => vote.UsersUser_ID == userid && vote.CommentsComment_ID == commentid);
            bazadanych.VotesSet.Remove(tmpVote);

            var comment = zwrocKomentarzPoID(Int32.Parse(commentID));
            comment.upVotes--;

            bazadanych.SaveChanges();
        }

        public static void UsunFollowZBloga(string userName, string blogID)
        {
            var userid = zwrocIdUzytkownikaPoUsername(userName);
            var blogid = Int32.Parse(blogID);

            var tmpFollow = bazadanych.FollowsSet.FirstOrDefault(follow => follow.UsersUser_ID == userid && follow.BlogsBlog_ID == blogid);
            bazadanych.FollowsSet.Remove(tmpFollow);

            var blog = zwrocBlogPoId(blogid);
            blog.followCount--;

            bazadanych.SaveChanges();
        }

        public static bool czyUzytkownikGlosowalNaKomentarz(string username, int commentID)
        {
            var id = zwrocIdUzytkownikaPoUsername(username);
            if(bazadanych.VotesSet.Any(vote => vote.CommentsComment_ID == commentID && vote.UsersUser_ID == id ))
            {
                return true;
            }
            return false;
        }

        public static bool czyUzytkownikMaFollowNaBlogu(string username, int blogID)
        {
            var id = zwrocIdUzytkownikaPoUsername(username);
            if(bazadanych.FollowsSet.Any(follow => follow.BlogsBlog_ID == blogID && follow.UsersUser_ID == id))
            {
                return true;
            }
            return false;
        }

        public static void EdytujBlog(Blogs blog, string test)
        {
            var tmpBlog = bazadanych.BlogsSet.FirstOrDefault(blog1 => blog1.Blog_ID == blog.Blog_ID);
            tmpBlog.dataAktualizacji = DateTime.Now;
            tmpBlog.nazwa = blog.nazwa;

            string[] tmpArray = test.Split(',');
            foreach (var i in tmpArray)
            {
                if (i != ",")
                {
                    int id = Int32.Parse(i.ToString());
                    blog.Kinds.Add(bazadanych.KindsSet.FirstOrDefault(kind => kind.Kind_ID == id));
                }
            }

            tmpBlog.Kinds = blog.Kinds;

            bazadanych.SaveChanges();

        }
        public static void EdytujPost(Post post, string test)
        {
            var tmpPost = bazadanych.PostSet.FirstOrDefault(post1 => post1.Post_ID == post.Post_ID);
            tmpPost.dataAktualizacji = DateTime.Now;
            tmpPost.ifTop = post.ifTop;
            tmpPost.image_file_name = post.image_file_name;
            tmpPost.img_route = post.img_route;
            if (tmpPost.img_route is null) tmpPost.img_route = "htttp?";
            tmpPost.title = post.title;
            tmpPost.text_content = post.text_content;

            string[] tmpArray = test.Split(',');
            foreach (var i in tmpArray)
            {
                if (i != ",")
                {
                    int id = Int32.Parse(i.ToString());
                    post.Tags.Add(bazadanych.TagsSet.FirstOrDefault(tags => tags.Tag_ID == id));
                }
            }

            tmpPost.Tags = post.Tags;
            bazadanych.SaveChanges();

        }

        public static Kinds ZnajdzKindPoId(int id)
        {
            return bazadanych.KindsSet.FirstOrDefault(item => item.Kind_ID == id);
        }
        public static Tags ZnajdzTagPoId(int id)
        {
            return bazadanych.TagsSet.FirstOrDefault(item => item.Tag_ID == id);
        }
        public static void UsunKind(int id)
        {
            var kind = ZnajdzKindPoId(id);

            bazadanych.KindsSet.Remove(kind);
            bazadanych.SaveChanges();
        }

        public static void UsunTag(int id)
        {
            var tag = ZnajdzTagPoId(id);

            bazadanych.TagsSet.Remove(tag);
            bazadanych.SaveChanges();
        }

        public static List<Blogs> zwrocBlogiDanegoKind(int id)
        {
            return bazadanych.BlogsSet.Where(blog => blog.Kinds.Any(kind => kind.Kind_ID == id)).ToList();
        }

        public static List<Post> zwrocPostyDanegoTagu(int id)
        {
            return bazadanych.PostSet.Where(post => post.Tags.Any(tag => tag.Tag_ID == id)).ToList();
        }

        public static List<Post> zwrocPostyZFollowanychBlogow(string username)
        {
            Users user = bazadanych.UsersSet.FirstOrDefault(user1 => user1.username == username);
            List<Follows> userFollows = user.Follows.ToList();
            List<Post> followedBlogPosts = new List<Post>();
            foreach(var follow in userFollows)
            {
                followedBlogPosts.AddRange(follow.Blogs.Post.ToList());
            }

            return followedBlogPosts;
        }
    }
}