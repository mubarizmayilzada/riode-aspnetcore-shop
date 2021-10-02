using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Riode.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly RiodeDbContext db;
        readonly IConfiguration configuration;
        public HomeController(RiodeDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.CurrentDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ffffff");
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Faq()
        {
            var faqs = db.Faqs.Where(f => f.DeletedByUserId == null).ToList();
            return View(faqs);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe([Bind("Email")] Subscribe model)
        {
            if (ModelState.IsValid)
            {
                var current = db.Subscribes.FirstOrDefault(s => s.Email.Equals(model.Email));
                if (current != null && current.EmailConfirmed == true)
                {
                    return Json(new
                    {
                        error = false,
                        message = "siz artiq bu e-poct unvani ile daha onceden qqeydiyyatdan kecmisiniz"
                    });
                }
                else if (current != null && (current.EmailConfirmed ?? false == false))
                {
                    return Json(new
                    {
                        error = true,
                        message = "E-pocta gonderilmis linkle qeydiyyat tamamlanmayib"
                    });
                }
                db.Subscribes.Add(model);
                db.SaveChanges();

                string token = $"subscribetoken={model.Id}-{DateTime.Now:yyyyMMddHHmmss}";
                token = token.Encrypt();



                string path = $"{Request.Scheme}://{Request.Host}/subscribe-confirm?token={token}";

                var mailSended = configuration.SendEmail(model.Email, "Riode Newsletter subscribe", $"Zehmet olmasa <a href={path}>link</a> vasitesi ile abuneliyi tamamlayasiniz");

                if (mailSended == false)
                {

                    db.Database.RollbackTransaction();

                    return Json(new
                    {
                        error = false,
                        message = "E-mail göndərilən zaman xəta baş verdi. Bir az sonra yenidən yoxlayın!"
                    });
                }


            }
            return Json(new
            {
                error = "true",
                message = "sorgunun icrasi zamani xeta yarandi. biraz sonra yeniden yoxlayin "
            });
        }

        [HttpGet]
        [Route("subscribe-confirm")]
        public IActionResult SubscribeConfrim(string token)
        {
            token = token.Decrypt();
            Match match = Regex.Match(token, @"subscribetoken-(?<id>\d+)-(?<executeTimeStamp>\d{14})");

            if (match.Success)
            {

                int id = Convert.ToInt32(match.Groups["id"].Value);
                string executeTimeStamp = match.Groups["executeTimeStamp"].Value;

                var subscribe = db.Subscribes.FirstOrDefault(s => s.Id == id && s.DeletedByUserId == null);

                if (subscribe == null)
                {

                    ViewBag.Message = Tuple.Create(true, "Token xetasi");
                    goto end;
                }

                if ((subscribe.EmailConfirmed ?? false) == true)
                {

                    ViewBag.Message = Tuple.Create(true, "Artiq tesdiq edilib");
                    goto end;
                }

                subscribe.EmailConfirmed = true;
                subscribe.EmailConfirmedDate = DateTime.Now;
                db.SaveChanges();

                ViewBag.Message = Tuple.Create(false, "Abuneliyi tesdiq edildi");
            }
            else {

                ViewBag.Message = Tuple.Create(true, "Token xetasi");
                goto end;
            }

        end:
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactPost model)
        {
            ViewBag.CurrentDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ffffff");
            if (ModelState.IsValid)
            {
                db.ContactPosts.Add(model);
                db.SaveChanges();

                //ModelState.Clear();

                //ViewBag.Message = "Sizin müraciətiniz qeydə alındı. Tezliklə sizə geri dönüş edəcəyik!";
                //return View();
                return Json(new
                {
                    error = false,
                    message = "Sizin müraciətiniz qeydə alındı. Tezliklə sizə geri dönüş edəcəyik!"
                });
            }
            return Json(new
            {
                error = true,
                message = "Biraz sonra yeniden yoxlayin!"
            });
            //return View(model);
        }
    }
}
