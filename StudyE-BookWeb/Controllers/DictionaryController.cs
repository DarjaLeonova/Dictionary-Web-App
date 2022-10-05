using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using StudyE_BookWeb.Data;
using StudyE_BookWeb.Models;

namespace StudyE_BookWeb.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DictionaryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Dictionary> objDefinitionList = _db.Definitions;
            return View(objDefinitionList);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string definitonSearch)
        {
            ViewData["GetDefinitions"] = definitonSearch;

            var definitionQuery = from x in _db.Definitions select x;

            if(!String.IsNullOrEmpty(definitonSearch))
            {
                definitionQuery = definitionQuery.Where(x => x.Name.Contains(definitonSearch) || x.Description.Contains(definitonSearch));
            }
            return View(await definitionQuery.AsNoTracking().ToListAsync());
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dictionary obj)
        {
            if(obj.Name == obj.Description.ToString())
            {
                ModelState.AddModelError("CustomError", "Description cannot exactly match the Name");
            }
            if(ModelState.IsValid)
            {
                _db.Definitions.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Definition created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var definitionFromDb = _db.Definitions.Find(id);

            if(definitionFromDb == null)
            {
                return NotFound();
            }
            return View(definitionFromDb);
        }

        //GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dictionary obj)
        {
            if (obj.Name == obj.Description.ToString())
            {
                ModelState.AddModelError("CustomError", "Description cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Definitions.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Definition updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var definitionFromDb = _db.Definitions.Find(id);

            if (definitionFromDb == null)
            {
                return NotFound();
            }
            return View(definitionFromDb);
        }

        //GET
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Definitions.Find(id);

            if(obj == null)
            {
                return NotFound();
            }

            _db.Definitions.Remove(obj);
              _db.SaveChanges();
            TempData["success"] = "Definition deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
