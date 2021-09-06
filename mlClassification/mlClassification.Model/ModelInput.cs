//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace MlClassification.Model
{
    public class ModelInput
    {
        [ColumnName("col0"), LoadColumn(0)]
        public string SentimentText { get; set; }


        [ColumnName("col1"), LoadColumn(1)]
        public string Sentiment { get; set; }


    }
}
