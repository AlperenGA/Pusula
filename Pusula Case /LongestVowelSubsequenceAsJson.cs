using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PusulaApp
{
    public class VowelSequenceResult
    {
        public string word { get; set; }
        public string sequence { get; set; }
        public int length { get; set; }
    }

    public static class LongestVowelSubsequenceSolver
    {
        public static string LongestVowelSubsequenceAsJson(List<string> words)
        {
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            var results = new List<VowelSequenceResult>();

            foreach (var word in words)
            {
                string maxSeq = "";
                string currentSeq = "";

                foreach (var ch in word)
                {
                    if (vowels.Contains(Char.ToLower(ch)))
                    {
                        currentSeq += ch;
                    }
                    else
                    {
                        if (currentSeq.Length > maxSeq.Length)
                        {
                            maxSeq = currentSeq;
                        }
                        currentSeq = "";
                    }
                }

                // Son kalan diziyi de kontrol et
                if (currentSeq.Length > maxSeq.Length)
                {
                    maxSeq = currentSeq;
                }

                results.Add(new VowelSequenceResult
                {
                    word = word,
                    sequence = maxSeq,
                    length = maxSeq.Length
                });
            }

            return JsonSerializer.Serialize(results);
        }
    }
}
