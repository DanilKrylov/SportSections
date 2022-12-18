using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using SportSections;
using SportSections.Models;
using SportSections.ViewModels;

namespace SportSections.Controllers
{
    [Authorize(Roles = "director,assistent")]
    public class SportsmansController : Controller
    {
        private readonly SportContext _context;

        public SportsmansController(SportContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewModel = new SportsmansViewModel()
            {
                Sportsmans = await _context.Sportsmans.ToListAsync()
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(SportsmansViewModel viewModel)
        {
            var sportsmans = _context.Sportsmans.Include(c => c.SectionRecords)
                                     .Where(c => c.SectionRecords.Count() >= viewModel.MinimumSectionCount &&
                                                 c.SectionRecords.Count() <= viewModel.MaximumSectionCount);


            if (!string.IsNullOrEmpty(viewModel.SurnameSearch))
            {
                sportsmans = sportsmans.Where(c => c.Surname.Contains(viewModel.SurnameSearch));
            }

            sportsmans = sportsmans.OrderBy(c => c.Surname);

            if(viewModel.SurnameOrder == OrderByAlphabet.Descending)
            {
                sportsmans = sportsmans.Reverse();
            }

            viewModel.Sportsmans = await sportsmans.ToListAsync();

            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportsman = await _context.Sportsmans
                .Include(c => c.SectionRecords)
                    .ThenInclude(c => c.SportSection)
                .FirstOrDefaultAsync(m => m.SportsmanId == id);
            if (sportsman == null)
            {
                return NotFound();
            }


            return View(sportsman);
        }


        // GET: Sportsmen/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportsmanId,Name,Surname,LastName,Age,Gender,Address")] Sportsman sportsman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportsman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sportsman);
        }

        // GET: Sportsmen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportsman = await _context.Sportsmans.FindAsync(id);
            if (sportsman == null)
            {
                return NotFound();
            }
            return View(sportsman);
        }

        // POST: Sportsmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportsmanId,Name,Surname,LastName,Age,Gender,Address")] Sportsman sportsman)
        {
            if (id != sportsman.SportsmanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportsman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportsmanExists(sportsman.SportsmanId))
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
            return View(sportsman);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sportsman = await _context.Sportsmans.FindAsync(id);
            _context.Sportsmans.Remove(sportsman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportsmanExists(int id)
        {
            return _context.Sportsmans.Any(e => e.SportsmanId == id);
        }

        public IActionResult CreateDetails(int sportsmanId)
        {
            var model = new SectionRecord()
            {
                SportsmanId = sportsmanId
            };

            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name");
            ViewData["SportsmanId"] = new SelectList(_context.Sportsmans, "SportsmanId", "Surname");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetails(SectionRecord sectionRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectionRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name", sectionRecord.SportSectionId);
            ViewData["SportsmanId"] = new SelectList(_context.Sportsmans, "SportsmanId", "Surname", sectionRecord.SportsmanId);
            return View(sectionRecord);
        }


        public IActionResult EditDetails(int id)
        {
            var sectionRecord =  _context.SectionRecords.Find(id);
            if (sectionRecord == null)
            {
                return NotFound();
            }

            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name");
            ViewData["SportsmanId"] = new SelectList(_context.Sportsmans, "SportsmanId", "Surname");

            return View(sectionRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetails(int id, [Bind("SectionRecordId,SportsmanId,SportSectionId,RecordDate")] SectionRecord sectionRecord)
        {
            if (id != sectionRecord.SectionRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectionRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                     return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name", sectionRecord.SportSectionId);
            ViewData["SportsmanId"] = new SelectList(_context.Sportsmans, "SportsmanId", "Surname", sectionRecord.SportsmanId);
            return View(sectionRecord);
        }


        public async Task<IActionResult> DeleteDetails(int id)
        {
            var sectionRecord = await _context.SectionRecords.FindAsync(id);
            _context.SectionRecords.Remove(sectionRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
