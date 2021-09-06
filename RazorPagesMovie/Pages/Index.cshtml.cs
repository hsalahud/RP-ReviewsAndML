using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using MlClassification.Model;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public IndexModel(ILogger<IndexModel> logger, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;

        }

        public void OnGet()
        {

        }

        public IActionResult OnGetAnalyzeSentiment([FromQuery] string text)
        {
            Console.WriteLine(text);
            if (String.IsNullOrEmpty(text)) return Content("Negative");
            var input = new ModelInput { SentimentText = text };
            var prediction = _predictionEnginePool.Predict(input);
            var sentiment = Convert.ToBoolean(Convert.ToInt32(prediction.Prediction)) ? "Positive" : "Negative";
            return Content(sentiment);
        }
    }
}
