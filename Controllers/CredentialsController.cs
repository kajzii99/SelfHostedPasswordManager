using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfHostedPasswordManager.Data;
using SelfHostedPasswordManager.Models;

namespace SelfHostedPasswordManager.Controllers
{
    [Authorize]
    public class CredentialsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CredentialsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Credentials
        public async Task<IActionResult> Index()
        {
            var currentApplicationUserId = GetCurrentApplicationUserId();

            var credentials = await _context.Credentials.Where(cred => cred.ApplicationUserId.Equals(currentApplicationUserId)).ToListAsync();

            return View(credentials);
        }

        // GET: credentials/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Credentials == null)
                return NotFound();

            var credential = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);

            if (credential == null || credential.ApplicationUserId != GetCurrentApplicationUserId())
                return NotFound();

            return View(credential);
        }

        // GET: credentials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: credentials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, [Bind("Id,Website,Username,Password,Notes")] Credential credential)
        {
            credential.ApplicationUserId = GetCurrentApplicationUserId();

            if (ValidateCredentialModel(credential))
            {
                _context.Credentials.Add(credential);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(credential);
        }

        // GET: credentials/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Credentials == null)
                return NotFound();

            var credential = await _context.Credentials.FindAsync(id);

            if (credential == null || credential.ApplicationUserId != GetCurrentApplicationUserId())
                return NotFound();

            return View(credential);
        }

        // POST: credentials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Website")] string website, [Bind("Username")] string username, [Bind("Password")] string password, [Bind("Notes")] string notes)
        {
            Credential credential = await _context.Credentials.FindAsync(id);

            if (credential == null)
                return NotFound();

            if (credential.ApplicationUserId != GetCurrentApplicationUserId())
                return NotFound();

            credential.Website = website;
            credential.Username = username;
            credential.Password = password;
            credential.Notes = notes;

            if (ValidateCredentialModel(credential))
            {
                try
                {
                    _context.Update(credential);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialExists(credential.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(credential);
        }

        // GET: credentials/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Credentials == null)
                return NotFound();

            var credential = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);

            if (credential == null || credential.ApplicationUserId != GetCurrentApplicationUserId())
                return NotFound();

            return View(credential);
        }

        // POST: credentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Credentials == null)
                return Problem("Entity set 'ApplicationDbContext.credentials'  is null.");

            var credential = await _context.Credentials.FindAsync(id);
            
            if (credential != null && credential.ApplicationUserId == GetCurrentApplicationUserId())
                _context.Credentials.Remove(credential);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialExists(string id)
        {
          return _context.Credentials.Any(e => e.Id == id);
        }

        private string GetCurrentApplicationUserId()
        {
            string applicationUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return applicationUserId;
        }

        private bool ValidateCredentialModel(Credential credential) //Maybe validate Website or Password separately?
        {
            if (credential == null)
                return false;

            bool validationResult = string.IsNullOrEmpty(credential.Id.ToString()) ||
                                    string.IsNullOrEmpty(credential.Username) ||
                                    string.IsNullOrEmpty(credential.Password) ||
                                    string.IsNullOrEmpty(credential.ApplicationUserId) ||
                                    string.IsNullOrEmpty(credential.Website);

            return !validationResult;
        }
    }
}
