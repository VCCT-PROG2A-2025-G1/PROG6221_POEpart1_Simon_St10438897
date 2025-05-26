
# Cybersecurity Awareness Bot

> **References:**  
> - ChatGPT assisted in writing this README.  
> - The GitHub CI workflow file `main.yml` was created with ChatGPT.

---

## Name: Cybersecurity Awareness Bot

This console app is designed to educate users about cybersecurity topics. It includes an audio greeting, ASCII art logo, keyword recognition, colored console messages, and a natural question-and-answer system. The program is modular and designed for an interactive user experience.

---

## Program Flow

1. Plays the introductory sound.
2. Displays ASCII art logo.
3. Prompts for and validates user name.
4. Enters a Q&A loop:
   - User asks questions.
   - App matches keywords.
   - Responds accordingly.
5. Allows multiple questions or exiting.

---

## Core Methods

### `PlaySound(string soundFilePath)`
- Plays a `.wav` file using `SoundPlayer`.
- Catches playback exceptions.

### `DisplayLogo()`
- Shows ASCII art using multiline string.

### `Greeting()`
- Prompts for user name.
- Validates input (no digits or blank).
- Displays greeting using colored text.

### `Cyperchat()`
Handles the full chat logic:
- Prompts and processes questions.
- Validates input (no digits, not empty).
- Normalizes with `Regex`.
- Matches predefined keywords.
- Provides random responses.
- Offers follow-up responses.
- Tracks discussed topics (memory).
- Supports user-triggered recall of covered/uncovered topics.
- Handles sentiment (e.g. “worried”, “frustrated”) and responds empathetically.
- Manages exit control and loop flow.

---

## Input Validation & Error Handling

| Input Type   | Validation Logic                            | Error Behavior |
|--------------|----------------------------------------------|----------------|
| Name         | Not empty, no digits                        | Red error + re-prompt |
| Questions    | Not empty, no digits                        | Red error + re-prompt |
| Yes/No       | Must be "yes" or "no" only              | Red error + re-prompt |
| Audio Error  | File missing/corrupt handled with message   | Program continues |

---

## Console Formatting

- **Green**: Prompts & success
- **Red**: Validation or input errors
- **Yellow**: When the app cannot answer a valid question
- `--------------------------------------------------` separators for readability

---

## How to Run

1. Download code from GitHub.
2. Extract the zipped file.
3. Open `POEpart2` folder
4. Open 'POEpart1.sln' with **Visual Studio 2022**.
4. Click **Start** in the IDE to run the program.

### Usage

1. Audio greeting plays.
2. Enter your name.
3. Ask a cybersecurity question.
4. Receive a response.
5. Choose to ask another question or exit.

---

## Example Sessions

### ✅ Correct Input Example

```
(Plays IntroSound.wav)
<ASCII Art Logo>
Please enter your name: Alice
Hello Alice! I'm your virtual assistant...
Please ask your question Alice: vpn
Answer: A VPN, or Virtual Private Network...
Would you like to ask another question Alice? (yes/no): yes
```

### ❌ Incorrect Input Example

```
Please enter your name: (blank)
[Error]: Please enter a valid name and don't include numbers.
Please enter your name: John
Please ask your question John: 123
John, Please enter a valid question and don't include numbers.
```

---

## Video Presentation

📺 [Watch here](https://youtu.be/hDKWbGg1cfo)
