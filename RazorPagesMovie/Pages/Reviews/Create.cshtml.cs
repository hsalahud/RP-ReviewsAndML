using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.Extensions.ML;
using MlClassification.Model;

namespace RazorPagesMovie.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;


        public CreateModel(RazorPagesMovie.Data.RazorPagesMovieContext context, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _context = context;
            _predictionEnginePool = predictionEnginePool;
        }

        public IList<Review> Reviews { get; set; }

        public IActionResult OnGet()
        {
            ViewData["MovieTitle"] = new SelectList(_context.Movie, "Title", "Title");
            //ViewData["MovieTitle"] = new SelectList(_context.Movie, "Title", "Title");
            return Page();
        }

        //public void OnGet()
        //{

        //}


        [BindProperty]
        public Review Review { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int movieId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Review.MovieID = movieId;
            _context.Review.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetAnalyzeSentiment([FromQuery] string text)
        {
            if (String.IsNullOrEmpty(text)) return Content("Neutral");
            var input = new ModelInput { SentimentText = text };
            var prediction = _predictionEnginePool.Predict(input);
            var sentiment = Convert.ToBoolean(Convert.ToInt32(prediction.Prediction)) ? "Positive" : "Negative";
            return Content(sentiment);
        }
    }
}
