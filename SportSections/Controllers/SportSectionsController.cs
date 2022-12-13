using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportSections;
using SportSections.Models;
using SportSections.ViewModels;

namespace SportSections.Controllers
{
    [Authorize(Roles = "director,assistent")]
    public class SportSectionsController : Controller
    {
        private readonly SportContext _context;
        private readonly IAutomation _automation;

        public SportSectionsController(SportContext context, IAutomation automation)
        {
            _context = context;
            _automation = automation;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewModel = new SportSectionsViewModel()
            {
                SportSections = await _context.SportSections.Include(s => s.Sport).ToListAsync()
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(SportSectionsViewModel viewModel)
        {
            var sportsmans = _context.SportSections.Include(s => s.Sport).AsQueryable();


            if (!string.IsNullOrEmpty(viewModel.SectionNameSearch))
            {
                sportsmans = sportsmans.Where(c => c.Name.Contains(viewModel.SectionNameSearch));
            }

            if (!string.IsNullOrEmpty(viewModel.SportNameSearch))
            {
                sportsmans = sportsmans.Where(c => c.Sport.Name.Contains(viewModel.SportNameSearch));
            }

            viewModel.SportSections = await sportsmans.ToListAsync();

            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> DetailsSp(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportSection = await _context.SportSections
                .Include(s => s.Sport)
                .Include(c => c.SectionRecords)
                    .ThenInclude(c => c.Sportsman)
                .FirstOrDefaultAsync(m => m.SportSectionId == id);
            if (sportSection == null)
            {
                return NotFound();
            }

            return View(sportSection);
        }

        [AllowAnonymous]
        public async Task<IActionResult> DetailsTr(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportSection = await _context.SportSections
                .Include(s => s.Sport)
                .Include(c => c.Coachings)
                    .ThenInclude(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.SportSectionId == id);
            if (sportSection == null)
            {
                return NotFound();
            }

            return View(sportSection);
        }

        // GET: SportSections/Create
        public IActionResult Create()
        {
            ViewData["SportId"] = new SelectList(_context.Sports, "SportId", "Name");
            return View();
        }

        // POST: SportSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportSectionId,Name,Address,Cost,RecommendedTraining,DoctorRecomended,SportId")] SportSection sportSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportId"] = new SelectList(_context.Sports, "SportId", "Name", sportSection.SportId);
            return View(sportSection);
        }

        // GET: SportSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportSection = await _context.SportSections.FindAsync(id);
            if (sportSection == null)
            {
                return NotFound();
            }
            ViewData["SportId"] = new SelectList(_context.Sports, "SportId", "Name", sportSection.SportId);
            return View(sportSection);
        }

        // POST: SportSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportSectionId,Name,Address,Cost,RecommendedTraining,DoctorRecomended,SportId")] SportSection sportSection)
        {
            if (id != sportSection.SportSectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportSectionExists(sportSection.SportSectionId))
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
            ViewData["SportId"] = new SelectList(_context.Sports, "SportId", "Name", sportSection.SportId);
            return View(sportSection);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sportSection = await _context.SportSections.FindAsync(id);
            _context.SportSections.Remove(sportSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportSectionExists(int id)
        {
            return _context.SportSections.Any(e => e.SportSectionId == id);
        }

        public IActionResult CreateDetailsSp(int sportSectionId)
        {
            var model = new SectionRecord()
            {
                SportSectionId = sportSectionId
            };

            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name");
            ViewData["SportsmanId"] = new SelectList(_context.Sportsmans, "SportsmanId", "Surname");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetailsSp(SectionRecord sectionRecord)
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


        public IActionResult EditDetailsSp(int id)
        {
            var sectionRecord = _context.SectionRecords.Find(id);
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
        public async Task<IActionResult> EditDetailsSp(int id, [Bind("SectionRecordId,SportsmanId,SportSectionId,RecordDate")] SectionRecord sectionRecord)
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


        public async Task<IActionResult> DeleteDetailsSp(int id)
        {
            var sectionRecord = await _context.SectionRecords.FindAsync(id);
            _context.SectionRecords.Remove(sectionRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult CreateDetailsTr(int sportSectionId)
        {
            var model = new Coaching()
            {
                SportSectionId = sportSectionId
            };

            ViewData["BestTrainer"] = _automation.GetBestTrainerForSection(sportSectionId);
            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name");
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Surname");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetailsTr([Bind("CoachingId,SportSectionId,TrainerId,StartTime,FinishTime")] Coaching coaching)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coaching);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BestTrainer"] = _automation.GetBestTrainerForSection(coaching.SportSectionId);
            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name", coaching.SportSectionId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Surname", coaching.TrainerId);
            return View(coaching);
        }

        // GET: Coachings/Edit/5
        public async Task<IActionResult> EditDetailsTr(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coaching = await _context.Coachings.FindAsync(id);
            if (coaching == null)
            {
                return NotFound();
            }
            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name", coaching.SportSectionId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Surname", coaching.TrainerId);
            return View(coaching);
        }

        // POST: Coachings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetailsTr(int id, [Bind("CoachingId,SportSectionId,TrainerId,StartTime,FinishTime")] Coaching coaching)
        {
            if (id != coaching.CoachingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coaching);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachingExists(coaching.CoachingId))
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
            ViewData["SportSectionId"] = new SelectList(_context.SportSections, "SportSectionId", "Name", coaching.SportSectionId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Surname", coaching.TrainerId);
            return View(coaching);
        }

        public async Task<IActionResult> DeleteDetailsTr(int id)
        {
            var coaching = await _context.Coachings.FindAsync(id);
            _context.Coachings.Remove(coaching);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachingExists(int id)
        {
            return _context.Coachings.Any(e => e.CoachingId == id);
        }
    }
}
