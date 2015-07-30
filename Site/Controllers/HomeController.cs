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
    //[RequireHttps]
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
        public ActionResult Matrix()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return View("~/Views/Home/Matrix.cshtml","",js.Serialize(MatrixCreator.New));
            //return View("~/Views/Home/Matrix.cshtml", 4);
        
        }
        public string NewMatrix()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(MatrixCreator.New);
        }
        public string SolveMatrix()
        {
            var str = Request.Form["res"];

            JavaScriptSerializer js = new JavaScriptSerializer();
            int[][] resss = js.Deserialize<int[][]>(str);

            int[][] helper = new int[resss.Length][];

            for (int row = 0; row < helper.Length; row++)
            {
                helper[row] = new int[helper.Length];
            }

            helper[0][0] = 1;
            MatrixCreator.solveMaze(resss,0,0,new bool[8,8],helper);
            return js.Serialize(helper);
        }
        public ActionResult Friends(bool privateFriends = false)
        {
            string currentUserId = User.Identity.GetUserId();
            FacebookLists lists = new FacebookLists();

            if (currentUserId != null)
            {
                
                var user = UserManager.FindById<ApplicationUser,string>(currentUserId);


                var fb = new FacebookClient(user.AccessToken);
                
                int counter = 0;
                var friendsList = new List<FacebookViewModel>();
                while (counter < 10)
                {
                    dynamic myInfo = fb.Get("/me/friends?limit=100&offset=" + (counter * 100).ToString());
                    
                    foreach (dynamic friend in myInfo.data)
                    {
                        friendsList.Add(new FacebookViewModel() { Name = friend.name, ImageURL = @"https://graph.facebook.com/" + friend.id + "/picture?type=small" });
                        //Response.Write("Name: " + friend.name + "<br/>Facebook id: " + friend.id + "<br/><br/>");
                    }
                    counter++;
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
        public ActionResult TicTacToe()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return View("~/Views/Home/TicTacToe.cshtml", "");
            //return View("~/Views/Home/Matrix.cshtml", 4);

        }
    }
}