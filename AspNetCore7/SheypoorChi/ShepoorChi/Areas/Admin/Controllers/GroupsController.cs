using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SheypoorChi.DataLayer.Context;
using SheypoorChi.DataLayer.Models;
using SheypoorChi.Core.Classes;

namespace ShepoorChi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupsController : Controller
    {
        private readonly DatabaseContext _context;

        public GroupsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Groups
        public async Task<IActionResult> Index()
        {
            return _context.Groups != null ?
                        View(await _context.Groups.ToListAsync()) :
                        Problem("Entity set 'DatabaseContext.Groups'  is null.");
        }

        // GET: Admin/Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Admin/Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group @group)
        {
            if (ModelState.IsValid)
            {
                //create svg icon
                group.Img =
                  await new ImageClass().CreateSvg(group.Img, group.GroupName);

                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Admin/Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }


            //get svg file content (svg code)
            ViewBag.GroupImg = group.Img;
            group.Img = await new ImageClass().GetSvgCode(group.Img);

            return View(@group);
        }

        // POST: Admin/Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Group @group,string oldFile)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                group.Img =
                  await new ImageClass().EditSvg(group.Img, 
                                            group.GroupName,
                                            oldFile);

                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
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
            return View(@group);
        }

        // GET: Admin/Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Admin/Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Groups == null)
            {
                return Problem("Entity set 'DatabaseContext.Groups'  is null.");
            }
            var @group = await _context.Groups.FindAsync(id);
            if (@group != null)
            {
                _context.Groups.Remove(@group);
                await _context.SaveChangesAsync();

                new ImageClass().DeleteSvg(group.Img);
            }

           
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return (_context.Groups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
