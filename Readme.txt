References:
ChatGPT helped make this Readme
The 'main.yml' file on GitHub for CI was created by ChatGPT
---------------------------------------------------------------------

Name: Cybersecurity Awareness Bot

This console app is designed to educate the user with regards to certain cybersecurity topics. It has an initial start up audio greeting the user, has an ASCII art logo, the keyword recognition system and coloured text depending on the actions that the user commits. Making this console app an interactive experience.
---------------------------------------------------------------------

Overview of the program:

1. Plays the introductory sound.

2. Displays the ASCII art logo.

3. Prompts the user for their name (with validation).

4. Enters a Q&A loop where the user asks cybersecurity questions.

5. Provides answers based on predefined keywords.

6. Allows the user to continue asking questions or exit.
---------------------------------------------------------------------

The core logic resides in four methods within Program.cs:

Method: PlaySound

	Plays a WAV file at startup using SoundPlayer.

Method: DisplayLogo

	Writes ASCII art to the console for branding.

Method: Greeting

	Handles name prompt, input validation, and greeting text.

Method: Cyperchat

	Manages the main chat loop, question parsing, and answers.
---------------------------------------------------------------------

Detailed Components:

1. PlaySound(string soundFilePath)

	Purpose: Play an introductory WAV audio file.

	Implementation: Uses System.Media.SoundPlayer.

	Error Handling: Catches exceptions (e.g., missing file) and prints an error message without crashing.

2. DisplayLogo()

	Purpose: Show an ASCII art banner.

	Implementation: A multiline string literal (@"...") is printed to the console.

3. Greeting()

	Purpose: Obtain and validate the user’s name.

	Behavior:

		Call DisplayLogo().

		Loop until a non-empty, digit-free name is entered.

		Use Console.ForegroundColor for coloured prompts and error messages.

		Store the valid name in the static variable name.

4. Cyperchat()

	Purpose: Run the interactive Q&A session.

	Core Steps:

		Prompt for a question: "Please ask your question {name}:".

		Validate input: reject empty or numeric strings.

		Normalize using Regex.Replace(question, @"[^\w\s]", "") and ToLower().

		Lookup: find the first dictionary key contained in the normalized question.

		Respond:

			Match: display the corresponding value in green.

			No Match: show a yellow apology message.

		Loop Control: ask "Would you like to ask another question {name}? (yes/no)" and handle response.

All these component makes the user experience as good as it can be, it makes it so that the program is modular and if something needs to be changed, the whole program does not get shut down.
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Input Validation & Error Handling:

1. Name Input:

	Invalid Entries: Empty input or any input containing digits.

	Error Message: Displayed in red exactly as: '[Error]: Please enter a valid name and don't include numbers.'

	Behavior: After showing the error, the program re-displays the prompt until a valid name is entered.

2. Question Input:

	Invalid Entries: Empty input or any input containing digits.

	Error Message: Displayed in red as: '<name>, Please enter a valid question and don't include numbers.'

	Behavior: On error, the separator line is printed and the user is re-prompted for a question.

3. Continuation Prompt (yes/no)

	Invalid Entries: Any response other than yes or no, empty input, or any input containing digits.

	Error Message: Displayed in red as: '[Error]: <name>, Please enter 'yes' or 'no' without numbers.'

	Behavior: The separator is printed and the continue prompt repeats until a valid response is given.

4. Audio Playback Errors

	When: If IntroSound.wav is missing or cannot be played.

	Error Message: Printed in default console colour as: 'Error playing sound: <exception message>'

	Behavior: The program continues execution even if the sound fails.

All the errors are handled so that when the user makes a mistake, it does not shut down the whole program. 
----------------------------------------------------------------------------------------------------------------------------------------------

Console Formatting

Colours:

	Green: Successful prompts and answers.

	Red: Validation errors.

	Yellow: Apology messages.

Separators: Uses a line of dashes ("-") to visually segment sections.

These console formatting makes the user more involved in the process of getting the info as making a mistake clearly shows by red as an instinct
of something being incorrect. The Green shows that something was successful and the yellow means that the user did correctly enter values however
the system could not find an answer, thus, the user should know it was not their fault. And the '-' separators are great as it cleary shows the
user where they need to pay attention and enter what they need to enter.

-----------------------------------------------------------------------------------------------------------------------------------------------89

How to run and use:
Run:
1. Download code from GitHub.
2. Extract the zipped file.
3. Go into the folder and find 'POEpart1.csproj'.
4. Once you have found it, double click to open it.
5. If you are prompted to use a program to open, use 'Visual Studio 2022'.
6. Once it has been opened, at the top of the window, you should see a 'start' button, click that.

Use:
7. Now that the console window has popped up, a greeting voice should start,
   Then you will be prompted to enter your name.
8. Enter your name and enter it correctly, do not leave it empty or use numbers.
9. Once your name has been entered, you will be prompted to enter your question regarding cyber security.
10. Once you have entered your question you will either given an answer or not since the amount of words in the dictionary is not big.
11. If you want to ask another questin after asking one successful one, you will be given the opportunity too ask another or you can choose to leave the program.

---------------------------------------------------------------------
Example session of correct inputs:
(Plays IntroSound.wav)
<CYBER LOGO ASCII ART>
Please enter your name: Alice (this is the person name)
Hello, Alice! I'm your virtual assistant for online safety.
I'm here to help you with your online safety questions.
Please ask your question Alice: vpn
Answer: A VPN, or Virtual Private Network...
Would you like to ask another question Alice? (yes/no): yes
Please ask your question Alice: ransomware
Answer: Ransomware is a type of malware...
Would you like to ask another question Alice? (yes/no): no
Thank you for using the Cyber Chat Alice! Stay safe online!

Example session of incorrect inputs:
1. 	(Plays IntroSound.wav)
	<CYBER LOGO ASCII ART>
	Please enter your name: (user leaves blank or inputs a number)
	[Error]: Please enter a valid name and don't include numbers.
	Please enter your name: John
	Hello, John! I'm your virtual assistant for online safety.
	I'm here to help you with your online safety questions.
	Please ask your question John: (enters nothing or numbers)
	John, Please enter a valid question and don't include numbers.
	Please ask your question John: VPN
	Answer: A VPN, or Virtual Private Network...
	Would you like to ask another question {name}? (yes/no)
	Enter: (nothing entered or numbers entered)
	[Error]: John, Please enter a valid question and don't include numbers.
	Enter: yes
	(goes though the loop again)

2.	(now an example of an incorrect initial question)
	(Plays IntroSound.wav)
	<CYBER LOGO ASCII ART>
	Please enter your name: John
	Hello, John! I'm your virtual assistant for online safety.
	I'm here to help you with your online safety questions.
	Please ask your question John: vyuj (some random mumbo)
	I'm sorry John, I don't have an answer for that question.
	If you want to ask another question, please type 'yes'.
	If you want to exit, please type 'no'.
	Enter: (enters nothing)
	[Error]: John, Please enter a valid response (yes/no) and don't include numbers.
	Enter: no
	Thank you John for using the Cyber Chat! Stay safe online!

Video Presentation link: https://youtu.be/T8Ja4l9Nj8I
