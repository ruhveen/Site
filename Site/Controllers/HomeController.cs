using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Site.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using Site.Helpers;
using System.Web.Script.Serialization;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }

        public HomeController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SSETest()
        {
            return View();
        }

        public ActionResult NodeJSSocketIO()
        {
            return View();
        }

        public ActionResult ReadWriteCache()
        {
            return View();
        }
        public ActionResult UnderConstruction()
        {
            return View();
        }

        public ActionResult EmploymentHistory()
        {
            return View();
        }
        //public ActionResult Matrix()
        //{
        //    return View(MatrixCreator.New);
        //}
        public string SolveMatrix()
        {
            var str = Request.Form["res"];

            JavaScriptSerializer js = new JavaScriptSerializer();
            int[][] resss = js.Deserialize<int[][]>(str);


            return js.Serialize(MatrixCreator.Solve(resss)) ;
        }
        public ActionResult Friends(bool privateFriends = false)
        {
            string currentUserId = User.Identity.GetUserId();
            FacebookLists lists = new FacebookLists();

            if (currentUserId != null)
            {

                var user = UserManager.FindById<ApplicationUser>(currentUserId);


                var fb = new FacebookClient(user.AccessToken);
                dynamic myInfo = fb.Get("/me/friends");
                var friendsList = new List<FacebookViewModel>();
                foreach (dynamic friend in myInfo.data)
                {
                    friendsList.Add(new FacebookViewModel() { Name = friend.name, ImageURL = @"https://graph.facebook.com/" + friend.id + "/picture?type=small" });
                    //Response.Write("Name: " + friend.name + "<br/>Facebook id: " + friend.id + "<br/><br/>");
                }

                
                if (!privateFriends)
                {


                    List<string> lines1 = new List<string>();
                    using (StreamReader sr = new StreamReader(@"C:\Users\Guy\Documents\fbfriendsunicodePLAY.txt", System.Text.Encoding.UTF8, true))
                    {

                        while (!sr.EndOfStream)
                        {
                            string currLine = sr.ReadLine();
                            if (currLine == "Friends")
                            {
                                currLine = sr.ReadLine();
                            }
                            lines1.Add(currLine);
                            currLine = sr.ReadLine();
                            if (currLine != string.Empty)
                            {
                                currLine = sr.ReadLine();
                            }
                        }
                    }

                    var deletedMe = lines1.Where(k => !friendsList.Select(p => p.Name).Contains(k.Replace("\r", ""))).Distinct();

                    List<FacebookViewModel> friendsLists = new List<FacebookViewModel>() { new FacebookViewModel() { Name = "Bla" } };
                    lists = new FacebookLists() { OldList = friendsList, NewList = friendsList, DeletedMe = deletedMe.Select(k => new FacebookViewModel() { Name = k }).ToList() };
                }
                else
                {
                    lists = new FacebookLists() { OldList = friendsList };
                }
            }
            return View(lists);
        }
        private static List<string> GetLines(StreamReader sr)
        {
            return sr.ReadToEnd().Split('\n').Where(x => x != " ").ToList();
        }
    }
}