using Microsoft.AspNetCore.Mvc;
using ResumeAnalyzer.Services;

namespace ResumeAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly AIService _aiService;

        public HomeController(AIService aiService)
        {
            _aiService = aiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AnalyzeResume(IFormFile resumeFile, string jobDescription)
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                ModelState.AddModelError("", "Please upload a valid resume file.");
                return View("Index");
            }

            // Save the uploaded file temporarily
            var filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                await resumeFile.CopyToAsync(stream);
            }

            // Extract text from the resume file
            string resumeText = FileParser.ExtractText(filePath);

            // Analyze resume using AI
            string aiFeedback = await _aiService.AnalyzeResumeAsync(resumeText, jobDescription);

            // Pass the analysis results to the view
            ViewData["AnalysisResult"] = aiFeedback;

            return View("AnalysisResult");
        }
    }
}
