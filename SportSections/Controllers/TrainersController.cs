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
    public class TrainersController : Controller
    {
        private readonly SportContext _context;

        public TrainersController(SportContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TrainersViewModel()
            {
                Trainers = await _context.Trainers.ToListAsync()
            };


            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(TrainersViewModel viewModel)
        {
            var trainers = _context.Trainers.Include(c => c.Coachings)
                                     .Where(c => c.Coachings.Count() >= viewModel.MinimumSectionCount &&
                                                 c.Coachings.Count() <= viewModel.MaximumSectionCount);


            if (!string.IsNullOrEmpty(viewModel.SurnameSearch))
            {
                trainers = trainers.Where(c => c.Surname.Contains(viewModel.SurnameSearch));
            }

            trainers = trainers.OrderBy(c => c.Surname);

            if (viewModel.SurnameOrder == OrderByAlphabet.Descending)
            {
                trainers = trainers.Reverse();
            }

            viewModel.Trainers = await trainers.ToListAsync();

            return View(viewModel);
        }


        [AllowAnonymous]
        public async Task<IActionResult> DetailsTr(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .Include(c => c.Coachings)
                    .ThenInclude(c => c.SportSection)
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainer == null)
            {
                return NotFound();
            }

            var viewModel = new TrainerDetails()
            {
                Trainer = trainer,
                TrainerId = Convert.ToInt32(id)
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> DetailsTr(TrainerDetails viewModel)
        {
            var trainer = await _context.Trainers
                .Include(c => c.Coachings.Where(k => k.StartTime >= viewModel.MinDateTime && k.FinishTime <= viewModel.MaxDateTime))
                    .ThenInclude(c => c.SportSection)
                .FirstOrDefaultAsync(m => m.TrainerId == viewModel.TrainerId);
            if (trainer == null)
            {
                return NotFound();
            }

            viewModel.Trainer = trainer;

            return View(viewModel);
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainerId,Name,Surname,LastName,Age,Gender,WorkExperiance,Address,Salary")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainerId,Name,Surname,LastName,Age,Gender,WorkExperiance,Address,Salary")] Trainer trainer)
        {
            if (id != trainer.TrainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.TrainerId))
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
            return View(trainer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainers.Any(e => e.TrainerId == id);
        }


        public IActionResult CreateDetailsTr(int trainerId)
        {
            var model = new Coaching()
            {
                TrainerId = trainerId
            };

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
