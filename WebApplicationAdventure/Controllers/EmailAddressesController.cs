using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationAdventure.DataLayer;

namespace WebApplicationAdventure.Controllers
{
    public class EmailAddressesController : Controller
    {
        private readonly AdventureworksContext _context;

        public EmailAddressesController(AdventureworksContext context)
        {
            _context = context;    
        }

        // GET: EmailAddresses
        public async Task<IActionResult> Index()
        {
            var adventureworksContext = _context.EmailAddress.Include(e => e.BusinessEntity).Take(10);
            return View(await adventureworksContext.ToListAsync());
        }

        // GET: EmailAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.EmailAddress
                .Include(e => e.BusinessEntity)
                .SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (emailAddress == null)
            {
                return NotFound();
            }

            return View(emailAddress);
        }

        // GET: EmailAddresses/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.Person, "BusinessEntityId", "FirstName");
            return View();
        }

        // POST: EmailAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,EmailAddressId,EmailAddress1,Rowguid,ModifiedDate")] EmailAddress emailAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Person, "BusinessEntityId", "FirstName", emailAddress.BusinessEntityId);
            return View(emailAddress);
        }

        // GET: EmailAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.EmailAddress.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (emailAddress == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Person, "BusinessEntityId", "FirstName", emailAddress.BusinessEntityId);
            return View(emailAddress);
        }

        // POST: EmailAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,EmailAddressId,EmailAddress1,Rowguid,ModifiedDate")] EmailAddress emailAddress)
        {
            if (id != emailAddress.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailAddressExists(emailAddress.BusinessEntityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Person, "BusinessEntityId", "FirstName", emailAddress.BusinessEntityId);
            return View(emailAddress);
        }

        // GET: EmailAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.EmailAddress
                .Include(e => e.BusinessEntity)
                .SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (emailAddress == null)
            {
                return NotFound();
            }

            return View(emailAddress);
        }

        // POST: EmailAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailAddress = await _context.EmailAddress.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            _context.EmailAddress.Remove(emailAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EmailAddressExists(int id)
        {
            return _context.EmailAddress.Any(e => e.BusinessEntityId == id);
        }
    }
}
