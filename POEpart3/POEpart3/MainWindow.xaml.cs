using POEpart3.Dictionaries;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace POEpart3
{
    public partial class MainWindow : Window
    {
        enum ChatState { EnterName, AskQuestion, AskMoreDetails, AskAnother }
        private readonly CyberChat _bot = new CyberChat();
        private string userName;
        private readonly Random _rng = new Random();
        private ChatState _state = ChatState.EnterName;
        private string _lastTopicKey;


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
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
    }
}
