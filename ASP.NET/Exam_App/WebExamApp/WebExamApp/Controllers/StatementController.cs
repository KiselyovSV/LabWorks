using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebExamApp.Data;
using WebExamApp.Models;

namespace WebExamApp.Controllers
{
    public class StatementController : Controller
    {
        private readonly StatementDbContext _context;

        public StatementController(StatementDbContext context)
        {
            _context = context;
        }

        private string? jsonResult;

        private string? txtResult;

        // GET: Statement
        public async Task<IActionResult> Index()
        {
            var statementDbContext = _context.Statement.Include(s => s.Evaluation).Include(s => s.Lesson).Include(s => s.Student);
            return View(await statementDbContext.ToListAsync());
        }

        // GET: Statement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statement = await _context.Statement
                .Include(s => s.Evaluation)
                .Include(s => s.Lesson)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statement == null)
            {
                return NotFound();
            }

            return View(statement);
        }

        // GET: Statement/Create
        [Authorize]
        public IActionResult Create()
        {
            var students = _context.Students.ToList();
            var items = GetStudentsNames();
            ViewData["EvaluationsNames"] = new SelectList(_context.Evaluations, "Id", "Name");
            ViewData["LessonsNames"] = new SelectList(_context.Lessons, "Id", "Name");
            ViewData["StudentsNames"] = items;
            return View();
        }

        // POST: Statement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,LessonId,EvaluationId,Date")] Statement statement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var items = GetStudentsNames();
            ViewData["EvaluationsNames"] = new SelectList(_context.Evaluations, "Id", "Name", statement.EvaluationId);
            ViewData["LessonsNames"] = new SelectList(_context.Lessons, "Id", "Name", statement.LessonId);
            ViewData["StudentsNames"] = new SelectList(items, "Value", "Text", statement.StudentId); 
            return View(statement);
        }

        // получаем List<SelectListItem> с Ф.И.О. студентов
        public List<SelectListItem>GetStudentsNames()
        {
            var students = _context.Students.ToList();
            var items = new List<SelectListItem>();
            foreach (var student in students)
            {
                string st = $"{student.LastName} {student.FirstName} {student.Patronymic}";
                items.Add(new SelectListItem() { Text = st, Value = student.Id.ToString() });
            }
            return items;
        }

        // GET: Statement/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statement = await _context.Statement.FindAsync(id);
            if (statement == null)
            {
                return NotFound();
            }
            var items = GetStudentsNames();
            ViewData["EvaluationsNames"] = new SelectList(_context.Evaluations, "Id", "Name", statement.EvaluationId);
            ViewData["LessonsNames"] = new SelectList(_context.Lessons, "Id", "Name", statement.LessonId);
            ViewData["StudentsNames"] = new SelectList(items, "Value", "Text", statement.StudentId);
            return View(statement);
        }

        // POST: Statement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,LessonId,EvaluationId,Date")] Statement statement)
        {
            if (id != statement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatementExists(statement.Id))
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
            var items = GetStudentsNames();
            ViewData["EvaluationsNames"] = new SelectList(_context.Evaluations, "Id", "Name", statement.EvaluationId);
            ViewData["LessonsNames"] = new SelectList(_context.Lessons, "Id", "Name", statement.LessonId);
            ViewData["StudentsNames"] = new SelectList(items, "Value", "Text", statement.StudentId); 
            return View(statement);
        }

        // GET: Statement/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statement = await _context.Statement
                .Include(s => s.Evaluation)
                .Include(s => s.Lesson)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statement == null)
            {
                return NotFound();
            }

            return View(statement);
        }

        // POST: Statement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statement = await _context.Statement.FindAsync(id);
            if (statement != null)
            {
                _context.Statement.Remove(statement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatementExists(int id)
        {
            return _context.Statement.Any(e => e.Id == id);
        }

        // операции с файлами:

        [Authorize]
        public IActionResult StudentsPrintJson()
        {
            var students = _context.Statement.Include(s => s.Student).Include(l => l.Lesson).Include(e => e.Evaluation).ToList();
            if (students.Count == 0)
            {
                ViewData ["Mes"] = ("Записи отсутствуют");
                //return HttpNotFound();
            }
            jsonResult = JsonConvert.SerializeObject(students, Formatting.Indented);
            SaveResult(jsonResult, "jsonResult.json");
            return GetJsonFile();
        }
        public IActionResult StudentsPrintTxt()
        {
            var students = _context.Statement.Include(s => s.Student).Include(l => l.Lesson).Include(e => e.Evaluation).ToList();
            if (students.Count == 0)
            {
                ViewData["Mes"] = ("Записи отсутствуют");
                //return HttpNotFound();
            }
            txtResult = GreateTxt(students);
            SaveResult(txtResult, "txtResult.txt");
            return GetTxtFile();
        }
        public void SaveResult(string content, string fileName)
        {
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"E:\\LabWorks\\ASP.NET\\Exam_App\\WebExamApp\\WebExamApp\\Files\\{fileName}");
            try
            {
                if (System.IO.File.Exists(file_path)) System.IO.File.Delete(file_path);
                using (var W = new StreamWriter(file_path))
                {
                    W.Write(content);
                    W.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public IActionResult GetJsonFile()
        {
            // Путь к файлу
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "E:/LabWorks/ASP.NET/Exam_App/WebExamApp/WebExamApp/Files/jsonResult.json");
            // Тип файла - content-type
            string file_type = "application/octet-stream"; //   или так: string file_type = "text/json"; "application/octet-stream" - это универсальный тип
                                                          
            string file_name = "jsonResult.json";          // Имя файла - необязательно
            return PhysicalFile(file_path, file_type, file_name);
        }

        public IActionResult GetTxtFile()
        {
            // Путь к файлу
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "E:/LabWorks/ASP.NET/Exam_App/WebExamApp/WebExamApp/Files/txtResult.txt");
            // Тип файла - content-type
            string file_type = "application/octet-stream"; //   или так: string file_type = "text/txt"; "application/octet-stream" - это универсальный тип
                                                           
            string file_name = "txtResult.txt";            // Имя файла - необязательно
            return PhysicalFile(file_path, file_type, file_name);
        }

        public string GreateTxt(List<Statement> list)
        {
            string text = "";
            foreach (Statement studs in list)
            {
                text += studs.ToString();
            }
            return text;
        }



    }
}
