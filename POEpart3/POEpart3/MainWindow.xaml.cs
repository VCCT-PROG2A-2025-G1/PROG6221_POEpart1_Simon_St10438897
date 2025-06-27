using POEpart3.Dictionaries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;    // ← for Tasks collection
using System.Linq;
using System.Text.RegularExpressions;    // ← added for timeframe parsing
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

//References
//ChatGPT - www.chatgpt.com
namespace POEpart3
{
    public partial class MainWindow : Window
    {
        enum ChatState { EnterName, AskQuestion, AwaitingReminder, AskMoreDetails, AskAnother }
        private readonly CyberChat _bot = new CyberChat();
        private string userName;
        private readonly Random _rng = new Random();
        private ChatState _state = ChatState.EnterName;
        private string _lastTopicKey;
        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();
        private TaskItem _pendingTask;
        private List<Question> _questions;
        private int _currentQuestionIndex = 0; 
        private int _score = 0;               
        private Question CurrentQuestion => _questions[_currentQuestionIndex];



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            PopulateQuestions();

            LoadQuestion();
            Loaded += MainWindow_Loaded;
        }
        //ChatGPT generated questions
        private void PopulateQuestions()
        {
            _questions = new List<Question>
            {
                new Question
                {
                    Text        = "What should you do if you receive an email asking for your password?",
                    Options     = new[] { "A) Reply with it", "B) Delete the email", "C) Report as phishing", "D) Ignore it" },
                    Answer      = "C",
                    Explanation = "Reporting phishing emails helps prevent scams.",
                    
                },
                new Question
                {
                    Text        = "Which of these is a sign of a phishing website?",
                    Options     = new[] { "A) A padlock icon in the address bar", "B) A misspelled domain name", "C) An HTTPS URL", "D) A custom favicon" },
                    Answer      = "B",
                    Explanation = "Attackers often spoof domains with small typos to trick users.",

                },
                new Question
                {
                    Text        = "Which practice helps create a strong password?",
                    Options     = new[] { "A) Using your birthdate", "B) Reusing the same password everywhere", "C) Using a mix of letters, numbers, and symbols", "D) Making it a common dictionary word" },
                    Answer      = "C",
                    Explanation = "A mix of uppercase/lowercase letters, numbers and symbols makes passwords harder to crack.",

                },
                new Question
                {
                    Text        = "What is two-factor authentication (2FA)?",
                    Options     = new[] { "A) Using two passwords at login", "B) Combining your password with a second verification (e.g. SMS code)", "C) Changing your password twice a week", "D) Sharing your login with two devices" },
                    Answer      = "B",
                    Explanation = "2FA adds a second form of proof (like a text code) on top of your password, improving security.",

                },
                new Question
                {
                    Text        = "Why should you avoid entering sensitive data on public Wi-Fi?",
                    Options     = new[] { "A) Public Wi-Fi is always slow", "B) Attackers can intercept your traffic on an unsecured network", "C) It drains your device battery faster", "D) It blocks secure websites" },
                    Answer      = "B",
                    Explanation = "Unencrypted public Wi-Fi can be monitored, allowing attackers to steal your data.",

                },
                new Question
                {
                    Text        = "What does HTTPS in a website’s address bar mean?",
                    Options     = new[] { "A) The site is hosted on HTTP only", "B) Your connection is encrypted with SSL/TLS", "C) It’s a government-run website", "D) It uses heavy graphics" },
                    Answer      = "B",
                    Explanation = "HTTPS (the ‘S’ for Secure) encrypts the data between your browser and the server.",

                },
                new Question
                {
                    Text        = "What’s the best defense against ransomware?",
                    Options     = new[] { "A) Paying the ransom immediately", "B) Keeping your software and OS up to date", "C) Disabling antivirus", "D) Opening all email attachments" },
                    Answer      = "B",
                    Explanation = "Regular updates patch vulnerabilities that ransomware often exploits.",

                },
                new Question
                {
                    Text        = "Before clicking a shortened URL from an unknown source, you should:",
                    Options     = new[] { "A) Click it right away", "B) Preview or expand the link first", "C) Forward it to friends", "D) Bookmark it for later" },
                    Answer      = "B",
                    Explanation = "Previewing a shortened link helps you verify its true destination before visiting.",

                },
                new Question
                {
                    Text        = "Which action helps protect your computer from malware?",
                    Options     = new[] { "A) Installing apps from unverified sites", "B) Disabling all software updates", "C) Keeping your operating system and applications up to date", "D) Using weak or default passwords" },
                    Answer      = "C",
                    Explanation = "Updates often include security patches that close malware entry points.",

                },
                new Question
                {
                    Text        = "What is social engineering?",
                    Options     = new[] { "A) Writing social media code", "B) Manipulating people into revealing confidential information", "C) Designing social networks", "D) Encrypting social data" },
                    Answer      = "B",
                    Explanation = "Social engineering exploits human trust to gain unauthorized access or information.",

                },
                new Question
                {
                    Text        = "Why should you always log out of shared or public computers?",
                    Options     = new[] { "A) To save the device’s battery", "B) To clear the browser history", "C) To prevent the next user from accessing your accounts", "D) To update the software automatically" },
                    Answer      = "C",
                    Explanation = "Logging out ensures other people can’t use your session or access your data.",

                },
                new Question
                {
                    Text        = "What is the main purpose of a VPN (Virtual Private Network)?",
                    Options     = new[] { "A) To slow down your internet speed", "B) To encrypt your internet traffic over untrusted networks", "C) To block all ads on websites", "D) To make your computer run faster" },
                    Answer      = "B",
                    Explanation = "A VPN encrypts data between your device and the VPN server, protecting it on public networks.",

                },

            };
        }
        private void LoadQuestion()
        {
            QuizProgressText.Text = $"Question {_currentQuestionIndex + 1} of {_questions.Count}";

            QuestionText.Text = CurrentQuestion.Text;

            FeedbackText.Text = "";

            OptionARadio.IsChecked = false;
            OptionBRadio.IsChecked = false;
            OptionCRadio.IsChecked = false;
            OptionDRadio.IsChecked = false;

            var options = CurrentQuestion.Options;

            OptionARadio.Content = options.Length > 0 ? options[0] : "";
            OptionBRadio.Content = options.Length > 1 ? options[1] : "";
            OptionCRadio.Content = options.Length > 2 ? options[2] : "";
            OptionDRadio.Content = options.Length > 3 ? options[3] : "";
            
            NextButton.Visibility = Visibility.Collapsed;
            SubmitButton.Visibility = Visibility.Visible;
            SubmitButton.IsEnabled = true;

        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //_bot.PlaySound("IntroSound.wav");

            // Show ASCII logo
            foreach (var line in _bot.GetLogoLines()) 
            {
                MessagesList.Items.Add(line);
            }
            
            MessagesList.Items.Add("Bot: Please enter your name:");
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        } 

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessInput();
            }    
        }

        private void ProcessInput()
        {
            var text = UserInput.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            
            MessagesList.Items.Add($"You: {text}");
            UserInput.Clear();

            switch (_state)
            {
                case ChatState.EnterName:
                    _bot.SetName(text);
                    userName = text;
                    MessagesList.Items.Add($"Bot: Hello, {text}! How can I help you today?");
                    _state = ChatState.AskQuestion;
                    break;

                case ChatState.AskQuestion:
                    if (text.StartsWith("add task ", StringComparison.OrdinalIgnoreCase))
                    {
                        var remainder = text.Substring(9).Trim();  
                        
                        var matchKey = MainResponces.responses.Keys.FirstOrDefault(k => remainder.ToLower().Contains(k));
                        if (matchKey == null)
                        {
                            MessagesList.Items.Add($"Bot: Sorry {userName}, I don't recognize that topic. Please try again.");
                            break;
                        }

                        
                        var title = remainder;

                        
                        var options = MainResponces.responses[matchKey];
                        var description = options[_rng.Next(options.Length)];

                        
                        var task = new TaskItem
                        {
                            Title = title,
                            Prompt = description,
                            Schedule = "",
                            Completed = false
                        };
                        Tasks.Add(task);
                        _pendingTask = task;

                        
                        MessagesList.Items.Add($"Bot: Task added with the description \"{description}\".");
                        MessagesList.Items.Add("Bot: Would you like a reminder? (no or e.g. 3 days)");
                        _state = ChatState.AwaitingReminder;
                        break;
                    }
                    if (text.Any(char.IsDigit))
                    {
                        MessagesList.Items.Add($"[Error]: {userName}, please enter a valid question and don't include numbers.");
                        MessagesList.Items.Add("Bot: Please re-enter your question.");
                        break;
                    }

                    var reply = _bot.Ask(text);
                    
                    if (reply.StartsWith($"I'm sorry {userName}"))
                    {
                        MessagesList.Items.Add($"Bot: {reply}");
                        MessagesList.Items.Add("Bot: If you want to ask another question, please type 'yes'.");
                        MessagesList.Items.Add("Bot: If you want to exit, please type 'no'.");
                        _state = ChatState.AskAnother;
                    }
                    else
                    {
                        
                        foreach (var line in reply.Split('\n'))
                        {
                            MessagesList.Items.Add($"Bot: {line}");
                        }
                            
                        _lastTopicKey = MainResponces.responses.Keys.FirstOrDefault(k => text.ToLower().Contains(k));

                        if (_lastTopicKey != null && MainResponces.followUp.ContainsKey(_lastTopicKey))
                        {
                            MessagesList.Items.Add($"Bot: Would you like to hear a more in detailed answer, {userName}? (yes/no)");
                            _state = ChatState.AskMoreDetails;
                        }
                    }
                    break;

                case ChatState.AwaitingReminder:
                    
                    if (text.Equals("no", StringComparison.OrdinalIgnoreCase) || text.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        MessagesList.Items.Add("Bot: Got it! Task added!");
                        _pendingTask = null;
                        _state = ChatState.AskQuestion;
                    }
                    else
                    {
                        
                        var m = Regex.Match(text, @"^(\d+)\s*(day|days|week|weeks|month|months|year|years)$", RegexOptions.IgnoreCase);
                        if (m.Success)
                        {
                            var count = m.Groups[1].Value;
                            var unit = m.Groups[2].Value.ToLower();

                            
                            _pendingTask.Schedule = $"{count} {unit}";
                            MessagesList.Items.Add($"Bot: Got it! I'll remind you in {count} {unit}.");
                            _pendingTask = null;
                            _state = ChatState.AskQuestion;
                        }
                        else
                        {
                            
                            MessagesList.Items.Add("[Error]: Please answer 'no' or give a time like '3 days'.");
                            MessagesList.Items.Add("Bot: Would you like a reminder? (yes/no or \"N days\")");
                        }
                    }
                    break;
                case ChatState.AskMoreDetails:
                    if (text.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    {
                        var details = MainResponces.followUp[_lastTopicKey][_rng.Next(MainResponces.followUp[_lastTopicKey].Length)];
                        MessagesList.Items.Add($"Bot: More details on {_lastTopicKey}: {details}");
                    }
                    
                    MessagesList.Items.Add($"Bot: Would you like to ask another question, {userName}? (yes/no)");
                    _state = ChatState.AskAnother;
                    break;

                case ChatState.AskAnother:
                    if (text.Equals("no", StringComparison.OrdinalIgnoreCase))
                    {
                        Application.Current.Shutdown();
                    }
                        
                    else if (text.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    {
                        MessagesList.Items.Add($"Bot: Great—what would you like to ask next, {userName}?");
                    }
                        
                    else
                    {
                        MessagesList.Items.Add($"[Error]: {userName}, please enter 'yes' or 'no' only.");
                        MessagesList.Items.Add("Bot: If you want to ask another question, please type 'yes'.");
                        MessagesList.Items.Add("Bot: If you want to exit, please type 'no'.");
                        return;
                    }

                    _state = ChatState.AskQuestion;
                    break;
            }

            MessagesList.ScrollIntoView(MessagesList.Items[MessagesList.Items.Count - 1]);
        }
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is Button btn && btn.Tag is TaskItem toRemove)
            {
                Tasks.Remove(toRemove);
            }
        }

        

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string selected = null;

            
            var rb = OptionsPanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

            if (rb != null)
            {
                selected = rb.Content.ToString().Substring(0, 1); 
            }
                
            if (string.IsNullOrEmpty(selected))
            {
                FeedbackText.Text = "Please select an answer before submitting.";
                return;
            }

            
            if (selected.Equals(CurrentQuestion.Answer, StringComparison.OrdinalIgnoreCase))
            {
                FeedbackText.Text = $"Correct! {CurrentQuestion.Explanation}";
                _score++;
            }
            else
            {
                FeedbackText.Text = $"Sorry—that’s not right. The correct answer is {CurrentQuestion.Answer}. {CurrentQuestion.Explanation}";
            }

            

            SubmitButton.IsEnabled = false;
            NextButton.Visibility = Visibility.Visible;
        }
        
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _currentQuestionIndex++;

            if (_currentQuestionIndex < _questions.Count)
            {
                LoadQuestion();
            }
            else
            {
                QuizProgressText.Text += $"\nQuiz complete! Your score: {_score} out of {_questions.Count}.";
                QuestionText.Visibility = Visibility.Collapsed;
                OptionsPanel.Visibility = Visibility.Collapsed;
                FeedbackText.Visibility = Visibility.Collapsed;
                SubmitButton.Visibility = Visibility.Collapsed;
                NextButton.Visibility = Visibility.Collapsed;
            }
        }

    }


}
