#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNews.Data;
using DotNews.Models;
using DotNews.Services;

namespace DotNews.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //api
        private readonly ReportsService _reportsService;


        public ReportsController(ApplicationDbContext context, ReportsService reportsService /*(<- api)*/)
        {
            _context = context;

            //api
            _reportsService = reportsService;
        }


        //new methods---------------------------------------------------
        // GET: Reports - gets the API data
        public async Task<IActionResult> ApiIndex()
        {
            //api
            return View(await _reportsService.GetReportList());
        }

        // GET: Reports - from the MVC DB, for the DotNews page (Feed View)
        public async Task<IActionResult> Feed()
        {
            return View(await _context.Report.ToListAsync());
        }


        public async Task<IActionResult> Search(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var report = from s in _context.Report
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                report = report.Where(s => s.CreatedBy.ToString().Contains(searchString)
                                    || s.Category.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    report = report.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    report = report.OrderBy(s => s.Category);
                    break;
                case "date_desc":
                    report = report.OrderByDescending(s => s.Category);
                    break;
                default:
                    report = report.OrderBy(s => s.Title);
                    break;
            }

            return View(await report.AsNoTracking().ToListAsync());

        }
        private bool ReportsExists(int id)

        {
            return _context.Report.Any(e => e.Id == id);
        }
        

        //new methods---------------------------------------------------


        // GET: Reports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Report.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            // Get the comments to show in the Details page ------------------------------------
            var reportComments = new ReportComments();
            var comments = await _context.Comment.ToListAsync();
                
            reportComments.Report = (Report)report;
            reportComments.Comments = comments;

            return View(reportComments);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Body,CreatedBy,CreatedDate,Category")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Body,CreatedBy,CreatedDate,LastUpdatedDate,Category")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Report.FindAsync(id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
