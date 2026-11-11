
using SolOricle.Core.Contracts;

namespace SolOricle.Api.Services
{
    public class AnalysisService : IAnalysisService
    {
        public string DetectEmotion(string content)
        {
            content = content.ToLower(); // expand upon later if time allows with more sophisticated NLP techniques

            if (content.Contains("happy") || content.Contains("great") || content.Contains("excited"))
                return "Positive";

            if (content.Contains("sad") || content.Contains("depressed") || content.Contains("lonely"))
                return "Negative";

            if (content.Contains("angry") || content.Contains("frustrated"))
                return "Angry";

            if (content.Contains("anxious") || content.Contains("worried"))
                return "Anxious";

            return "Neutral";
        }
    }
}