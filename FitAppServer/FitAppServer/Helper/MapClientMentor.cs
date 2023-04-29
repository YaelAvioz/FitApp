using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FitAppServer.Helper
{
    public class MapClientMentor
    {
        // Dictionary to store the tag-based representations of clients and mentors
        // Mapping between client/mentor (id/name) and their tags/experties
        private Dictionary<string, List<string>> clientTags;
        private Dictionary<string, List<string>> mentorTags;
        private Dictionary<string, double> tagsWeights;

        // Eating conditions and allergies are first - most important
        private List<string> highestPriority = new List<string>()
        {
            "Veganism",
            "Vegetarianism",
            "Gluten-free",
            "Dairy-Free",
            "Plant-based nutrition"
        };
        // Sport and eating methods are also important, but less
        private List<string> highPriority = new List<string>()
        {
            "Calorie-counting",
            "HIIT",
            "Meditation",
            "Tabata",
            "Intermittent fasting",
            "Eating habits",
            "Coaching"
        };

        // Dictionary to store the similarities between clients and mentors
        // The string is the client's id
        private Dictionary<string, List<(string, double)>> clientMentorSimilarities;

        // Constructor to initialize the class with client and mentor data
        public MapClientMentor(Dictionary<string, List<string>> clientTags, Dictionary<string, List<string>> mentorTags)
        {
            this.clientTags = clientTags;
            this.mentorTags = mentorTags;

            this.tagsWeights = new Dictionary<string, double>();
            foreach (List<string> tagsList in mentorTags.Values)
            {
                foreach (string tag in tagsList)
                {
                    if (!tagsWeights.ContainsKey(tag))
                    {
                        if (highestPriority.Contains(tag))
                        {
                        tagsWeights.Add(tag, 4.0);
                        } 
                        else if (highPriority.Contains(tag))
                        {
                            tagsWeights.Add(tag, 2.0);
                        } 
                        else
                        {
                            tagsWeights.Add(tag, 1.0);
                        }
                    }
                }
            }

            clientMentorSimilarities = new Dictionary<string, List<(string, double)>>();

            // Calculate the similarities between clients and mentors
            foreach (string clientId in clientTags.Keys)
            {
                List<(string, double)> similarities = new List<(string, double)>();
                foreach (string mentorId in mentorTags.Keys)
                {
                    double similarityScore = CalculateCosineSimilarity(clientTags[clientId], mentorTags[mentorId], tagsWeights);
                    similarities.Add((mentorId, similarityScore));
                }
                clientMentorSimilarities[clientId] = similarities.OrderByDescending(s => s.Item2).ToList();
            }
        }

        // Calculating the similarity while taking into consideration priorities
        private double CalculateCosineSimilarity(List<string> vec1, List<string> vec2, Dictionary<string, double> tagWeights)
        {
            var commonTags = vec1.Intersect(vec2);

            double dotProduct = commonTags.Sum(tag => tagWeights.ContainsKey(tag) ? tagWeights[tag] : 1.0);
            double magnitude1 = Math.Sqrt(vec1.Sum(tag => Math.Pow(tagWeights.ContainsKey(tag) ? tagWeights[tag] : 1.0, 2)));
            double magnitude2 = Math.Sqrt(vec2.Sum(tag => Math.Pow(tagWeights.ContainsKey(tag) ? tagWeights[tag] : 1.0, 2)));

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
