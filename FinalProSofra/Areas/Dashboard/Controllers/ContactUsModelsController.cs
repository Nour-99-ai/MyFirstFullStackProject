using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProSofra.Models;
using FinalProSofra.data;

namespace FinalProSofra.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ContactUsModelsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactUsModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/ContactUsModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.contactUsModels.ToListAsync());
        }

        // GET: Dashboard/ContactUsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUsModel = await _context.contactUsModels
                .FirstOrDefaultAsync(m => m.ContactUsId == id);
            if (contactUsModel == null)
            {
                return NotFound();
            }

            return View(contactUsModel);
        }

        // GET: Dashboard/ContactUsModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/ContactUsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactUsId,FirstName,LastName,Email,PhoneNumber,Message")] ContactUsModel contactUsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactUsModel);
        }

        // GET: Dashboard/ContactUsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUsModel = await _context.contactUsModels.FindAsync(id);
            if (contactUsModel == null)
            {
                return NotFound();
            }
            return View(contactUsModel);
        }

        // POST: Dashboard/ContactUsModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactUsId,FirstName,LastName,Email,PhoneNumber,Message")] ContactUsModel contactUsModel)
        {
            if (id != contactUsModel.ContactUsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactUsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsModelExists(contactUsModel.ContactUsId))
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
            return View(contactUsModel);
        }

        // GET: Dashboard/ContactUsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUsModel = await _context.contactUsModels
                .FirstOrDefaultAsync(m => m.ContactUsId == id);
            if (contactUsModel == null)
            {
                return NotFound();
            }

            return View(contactUsModel);
        }

        // POST: Dashboard/ContactUsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactUsModel = await _context.contactUsModels.FindAsync(id);
            if (contactUsModel != null)
            {
                _context.contactUsModels.Remove(contactUsModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsModelExists(int id)
        {
            return _context.contactUsModels.Any(e => e.ContactUsId == id);
        }
    }
}
