using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MqdSessionStateStoreProvider.Demo.Models;

namespace MqdSessionStateStoreProvider.Demo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            LogHelper.WriteLine("------------------ Index 1 ------------------");

            LogHelper.WriteLine("------------------ Index 2 ------------------\r\n");

            return View();
        }

        public ActionResult CreateSession()
        {
            LogHelper.WriteLine("------------------ CreateSession 1 ------------------");

            Session["user"] = new UserInfoModel()
            {
                Id = 1,
                Name = "Mqd",
                Pwd = "123456",
                Username = "AAtest001"
            };

            LogHelper.WriteLine("------------------ CreateSession 2 ------------------\r\n");

            return View();
        }

        public ActionResult CreateSession1()
        {
            LogHelper.WriteLine("------------------ CreateSession1 1 ------------------");

            Session["user1"] = new UserInfoModel()
            {
                Id = 2,
                Name = "Mqd2",
                Pwd = "654321",
                Username = "AAtest002"
            };

            LogHelper.WriteLine("------------------ CreateSession1 2 ------------------\r\n");

            return View();
        }

        public ActionResult ReadSession()
        {
            LogHelper.WriteLine("------------------ ReadSession 1 ------------------");

            UserInfoModel userinfo = Session["user"] as UserInfoModel;
            if (userinfo != null)
            {
                ViewBag.user = JsonConvert.SerializeObject(userinfo);
            }

            LogHelper.WriteLine("------------------ ReadSession 2 ------------------\r\n");

            return View();
        }

        public ActionResult ReadSession1()
        {
            LogHelper.WriteLine("------------------ ReadSession1 1 ------------------");

            UserInfoModel userinfo = Session["user1"] as UserInfoModel;
            if (userinfo != null)
            {
                ViewBag.user1 = JsonConvert.SerializeObject(userinfo);
            }

            LogHelper.WriteLine("------------------ ReadSession1 2 ------------------\r\n");

            return View();
        }

        public ActionResult ClearSession()
        {
            LogHelper.WriteLine("------------------ ClearSession 1 ------------------");

            Session.Clear();

            LogHelper.WriteLine("------------------ ClearSession 2 ------------------\r\n");

            return View();
        }

        public ActionResult RemoveSessionItem()
        {
            LogHelper.WriteLine("------------------ RemoveSessionItem 1 ------------------");

            Session.Remove("user");

            LogHelper.WriteLine("------------------ RemoveSessionItem 2 ------------------\r\n");

            return View();
        }

        public ActionResult RemoveSessionItem1()
        {
            LogHelper.WriteLine("------------------ RemoveSessionItem1 1 ------------------");

            Session.Remove("user1");

            LogHelper.WriteLine("------------------ RemoveSessionItem1 2 ------------------\r\n");

            return View();
        }

        public ActionResult RemoveAll()
        {
            LogHelper.WriteLine("------------------ RemoveAll 1 ------------------");

            Session.RemoveAll();

            LogHelper.WriteLine("------------------ RemoveAll 2 ------------------\r\n");

            return View();
        }

        public ActionResult Abandon()
        {
            LogHelper.WriteLine("------------------ Abandon 1 ------------------");

            Session.Abandon();

            LogHelper.WriteLine("------------------ Abandon 2 ------------------\r\n");

            return View();
        }

        public ActionResult ReadSessioId()
        {
            LogHelper.WriteLine("------------------ ReadSessioId 1 ------------------");

            string id = Session.SessionID;
            ViewBag.SessionId = id;

            LogHelper.WriteLine("------------------ ReadSessioId 2 ------------------\r\n");

            return View();
        }
    }
}