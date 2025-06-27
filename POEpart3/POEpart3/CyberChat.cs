using POEpart3.Dictionaries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
//References
//ChatGPT - www.chatgpt.com
namespace POEpart3
{
    //ChatGPT helped with the implementation of the CyberChat class and its methods into this project.
    public class CyberChat
    {
        
        private string name;
        private readonly ArrayList memory = new ArrayList();
        private static readonly Random _rng = new Random();

        
        public void PlaySound(string soundFilePath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(soundFilePath))
                {
                    player.Play();
                }
            }
            catch
            {
                
            }
        }

       
        public IEnumerable<string> GetLogoLines()
        {
            var logo = @"  ____            _                  _                 
            / ___|___  _ __ | |_ _ __ _   _   / \   ___ ___  ___ 
            | |   / _ \| '_ \| __| '__| | | | / _ \ / __/ __|/ _ \
            | |__| (_) | | | | |_| |  | |_| |/ ___ \\__ \__ \  __/
            \____\___/|_| |_|\__|_|   \__, /_/   \_\___/___/\___|
                           |___/                     
            Cybersecurity Awareness Bot
            ";
            return logo.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Where(l => l != "");
        }

        
        public void SetName(string userName) => name = userName;

        
        public string Ask(string rawQuestion)
        {
            
            var question = (rawQuestion ?? "").Trim().ToLower();
            question = Regex.Replace(question, @"[^\w\s]", "");

            if (string.IsNullOrWhiteSpace(question))
            {
                return "Sorry, I didn't catch that. Could you rephrase?";
            }
                
            var sentimentKey = MainResponces.sentimateDetection.Keys.FirstOrDefault(k => question.Contains(k));

            if (sentimentKey != null)
            {
                var apology = MainResponces.sentimateDetection[sentimentKey];

                var matchKey = MainResponces.responses.Keys.FirstOrDefault(k => question.Contains(k));
                    
                if (matchKey != null)
                {
                    
                    if (!memory.Contains(matchKey)) 
                    {
                        memory.Add(matchKey);
                    }

                    var options = MainResponces.responses[matchKey];
                    var detailed = options[_rng.Next(options.Length)];
                    return $"{apology}\nAnswer: {detailed}";
                }

                return apology;
            }

            if (question.Contains("recall") || question.Contains("remind me") || question.Contains("remember"))
            {
                var recallKey = MainResponces.responses.Keys.FirstOrDefault(k => question.Contains(k));

                if (recallKey != null && memory.Contains(recallKey))
                {
                    MainResponces.memoryValue.TryGetValue(recallKey, out var recallMsg);
                    return recallMsg;
                }

                if (recallKey != null)
                {
                    return $"We haven’t covered '{recallKey}' yet, but we can discuss it!";
                }
                    
                return "I don't understand what you'd like me to recall. Try again.";
            }

            
            var normalKey = MainResponces.responses.Keys.FirstOrDefault(k => question.Contains(k));
            if (normalKey != null)
            {
                if (!memory.Contains(normalKey)) 
                {
                    memory.Add(normalKey);
                }
                
                var options2 = MainResponces.responses[normalKey];
                return options2[_rng.Next(options2.Length)];
            }

            return $"I'm sorry {name}, I don't have an answer for that.";
        }
        public string[] GetFollowUpOptions(string key)
        {
            return MainResponces.followUp.TryGetValue(key, out var opts) ? opts : Array.Empty<string>();
        }
    }
}
