﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.ProjectOxford.Text.Core;
using Microsoft.ProjectOxford.Text.Sentiment;

namespace MobileAzureDevDays.Services
{
    static class TextAnalysis
    {
        const string _sentimentAPIKey = "b75051a2b0554d8892f79434d9f59fcf";

        readonly static Lazy<SentimentClient> _sentimentClientHolder = new Lazy<SentimentClient>(() => new SentimentClient(_sentimentAPIKey));

        static SentimentClient SentimentClient => _sentimentClientHolder.Value;

        public static async Task<float?> GetSentiment(string text)
        {
            var sentimentDocument = new SentimentDocument { Id = "1", Text = text };
            var sentimentRequest = new SentimentRequest { Documents = new List<IDocument> { { sentimentDocument } } };

            var sentimentResults = await SentimentClient.GetSentimentAsync(sentimentRequest);
            var documentResult = sentimentResults.Documents.FirstOrDefault();

            return documentResult?.Score;
        }
    }
}
