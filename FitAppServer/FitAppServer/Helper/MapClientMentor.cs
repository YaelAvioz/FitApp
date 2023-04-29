using System;
using System.Collections.Generic;
using System.Linq;

namespace FitAppServer.Helper
{
    public class MapClientMentor
    {
        // Dictionary to store the tag-based representations of clients and mentors
        // Mapping between client/mentor (string - id) and their tags/experties
        private Dictionary<string, List<string>> clientTags;
        private Dictionary<string, List<string>> mentorTags;

        // Dictionary to store the similarities between clients and mentors
        // The string is the client's id
        private Dictionary<string, List<(string, double)>> clientMentorSimilarities;

        // Constructor to initialize the class with client and mentor data
        public MapClientMentor(Dictionary<string, List<string>> clientTags, Dictionary<string, List<string>> mentorTags)
        {
            this.clientTags = clientTags;
            this.mentorTags = mentorTags;
            clientMentorSimilarities = new Dictionary<string, List<(string, double)>>();

            // Calculate the similarities between clients and mentors
            foreach (string clientId in clientTags.Keys)
            {
                List<(string, double)> similarities = new List<(string, double)>();
                foreach (string mentorId in mentorTags.Keys)
                {
                    double similarityScore = CalculateCosineSimilarity(clientTags[clientId], mentorTags[mentorId]);
                    similarities.Add((mentorId, similarityScore));
                }
                clientMentorSimilarities[clientId] = similarities.OrderByDescending(s => s.Item2).ToList();
            }
        }

        // Method to calculate cosine similarity between two vectors
        private double CalculateCosineSimilarity(List<string> vec1, List<string> vec2)
        {
            double dotProduct = vec1.Intersect(vec2).Count();
            double magnitude1 = Math.Sqrt(vec1.Count);
            double magnitude2 = Math.Sqrt(vec2.Count);

            return dotProduct / (magnitude1 * magnitude2);
        }

        // Method to rank mentors based on similarity scores
        private List<string> RankMentors(string clientId, int numMentors)
        {
            List<(string, double)> rankedMentors = clientMentorSimilarities[clientId].Take(numMentors).ToList();
            return rankedMentors.Select(r => r.Item1).ToList();
        }

        // Method to assign a mentor to a client
        public string AssignMentor(string clientId)
        {
            List<string> rankedMentors = RankMentors(clientId, 1);
            return rankedMentors.First();
        }
    }
}
