using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qwerty1.Models;
using qwerty1.Templates;


namespace qwerty1.Controllers
{
    public class SystemUsersController : Controller
    {
        private readonly CrudOpContext _context;

        public SystemUsersController(CrudOpContext context)
        {
            _context = context;
        }

        // GET: SystemUsers
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString("Role")!="Admin")
            {

                return RedirectToAction("StaffDashBoard", "SystemUsers");

            }


            return View(await _context.SystemUser.ToListAsync());
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(SystemUser _U)
        {

            SystemUser U = _context.SystemUser.Where(abc => abc.Username == _U.Username && abc.Password == _U.Password).FirstOrDefault();
            string password = "Jabbar_53644566";
            if (U == null)
            {
                ViewBag.Message = "Invalid Credentials";
                return View(_U);
            }
            string token = new System.Random().Next(1000, 9999).ToString();
            HttpContext.Session.SetString("Role", U.Role);
            HttpContext.Session.SetString("Username", U.Username);
            HttpContext.Session.SetString("Code", token);
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("mianabubakar131998@gmail.com");
            mailMessage.From = new MailAddress("abdul.jabbar53644566@gmail.com");
            mailMessage.Subject = "TwoStepVerify";
            mailMessage.CC.Add("abdul.jabbar53644566@gmail.com");
            mailMessage.Body = string.Format(EmailTemplates.WelcomeEmail, _U.Username, _U.Username, token);
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("abdul.jabbar53644566@gmail.com",password);
            try
            {
                smtpClient.Send(mailMessage);
               
            }
            catch
            {

            }
            return RedirectToAction(nameof(TwoStepVerification));
            
        }
        [HttpGet]
        public IActionResult TwoStepVerification()
        {

            return View();
        } 
        [HttpPost]
        public IActionResult TwoStepVerification(string token)
        {
            if(token == HttpContext.Session.GetString("Code") && HttpContext.Session.GetString("Role") == "Admin")
            {
                return RedirectToAction(nameof(DashBoard));
            }
            else if (token == HttpContext.Session.GetString("Code") && HttpContext.Session.GetString("Role") == "Staff")
            {
                return RedirectToAction(nameof(StaffDashBoard));
            }
            else
            {
                return View();
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult DashBoard()
        {
            return View();
        }
        public IActionResult StaffDashBoard()
        {
            return View();
        }


        // GET: SystemUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemUser = await _context.SystemUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemUser == null)
            {
                return NotFound();
            }

            return View(systemUser);
        }

        // GET: SystemUsers/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult newDashBoard()
        {
          if(HttpContext.Session.GetString("UserName") !=null)
            {
                return View();
            }


            return RedirectToAction(nameof(Login));
            
          
        } 

        // POST: SystemUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Role,Status,Email")] SystemUser systemUser)
        {
            if (ModelState.IsValid)
            {
                systemUser.Status = "Active";
                _context.Add(systemUser);
                await _context.SaveChangesAsync();


                MailMessage oEmail = new MailMessage();
                oEmail.From = new MailAddress("abdul.jabbar53644566@gmail.com");
                oEmail.To.Add(systemUser.Email);
                oEmail.Subject = "wellcome to QWERTY";
                oEmail.Body = string.Format(EmailTemplates.WelcomeEmail1, systemUser.Username, systemUser.Username, systemUser.Password);
                oEmail.IsBodyHtml = true;
                oEmail.CC.Add("mianabubakar131998@gmail.com");



                SmtpClient oSMTP = new SmtpClient();
                oSMTP.Credentials=new NetworkCredential("abdul.jabbar53644566@gmail.com", "Jabbar_53644566");
                oSMTP.Host = "smtp.gmail.com";
                oSMTP.Port = 587;
                oSMTP.EnableSsl = true;



                try {

                    oSMTP.Send(oEmail);
                }
                catch{
                    

                }





                return RedirectToAction(nameof(Index));
            }


            return View(systemUser);

        }

        // GET: SystemUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemUser = await _context.SystemUser.FindAsync(id);
            if (systemUser == null)
            {
                return NotFound();
            }
            return View(systemUser);
        }

        // POST: SystemUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Role,Status")] SystemUser systemUser)
        {
            if (id != systemUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemUserExists(systemUser.Id))
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
            return View(systemUser);
        }

        // GET: SystemUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemUser = await _context.SystemUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemUser == null)
            {
                return NotFound();
            }

            return View(systemUser);
        }

        // POST: SystemUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemUser = await _context.SystemUser.FindAsync(id);
            _context.SystemUser.Remove(systemUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemUserExists(int id)
        {
            return _context.SystemUser.Any(e => e.Id == id);
        }
    }
}
