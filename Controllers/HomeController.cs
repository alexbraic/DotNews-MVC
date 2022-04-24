
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNews.Data;
using DotNews.Models;

namespace DotNews.Controllers

{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var report = from s in _context.Report
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                report = report.Where(s => s.Id.ToString().Contains(searchString)
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


        public IActionResult Team()
        {
            return View();
        }
    }
}