# Cybersecurity Awareness Bot (POE Part 3)

> **References:**
>
> * This README file was generated by ChatGPT.
> * Portions of this file and application logic were scaffolded with assistance from ChatGPT (OpenAI).
> * CI workflow (`.github/workflows/main.yml`) generated with ChatGPT guidance.

---

## Overview

A WPF desktop application designed to educate users on cybersecurity best practices through four integrated modules:

* **Chat**: Interactive conversation with keyword-based Q\&A, sentiment detection, and in-chat commands.
* **Tasks**: GUI task manager for viewing, deleting, and completing cybersecurity tasks with optional reminders (added via the Chat tab using the `add task` command; no in-tab task creation).
* **Quiz**: 10-question multiple-choice cybersecurity quiz featuring immediate feedback, score tracking, and a Next button to advance.
* **Activity Log**: Timestamped record of the last 10 key actions (task added, reminder set, quiz start/completion).

---

## Features & Application Flow

1. **Startup**

   * Plays `IntroSound.wav`.
   * Renders ASCII art logo in the Chat tab.

2. **Chat Tab**

   * Prompts for and validates the user’s name.
   * Handles free-form questions via keyword matching and `CyberChat` engine.
   * Sentiment detection for words like "confused" or "worried."
   * **Add Task**: `add task <topic>` creates a `TaskItem` with dictionary description.
   * **Reminder Prompt**: After adding, user enters `no` or `<N> days/weeks/months/years` to set `Schedule`.

3. **Tasks Tab**

   * Displays an `ObservableCollection<TaskItem>` in a `DataGrid`.
   * Columns: Title, Description, Reminder, Completed (checkbox), Delete (button).
   * Checking **Completed** or clicking **Delete** updates the collection dynamically.

4. **Quiz Tab**

   * Presents 10 curated multiple-choice questions covering phishing, password safety, safe browsing, social engineering, etc.
   * Radio buttons for options A–D.
   * **Submit** shows immediate correctness feedback and explanation.
   * **Next** button appears to load the subsequent question.
   * Final score displayed upon completion.

5. **Activity Log Tab**

   * Binds to an `ObservableCollection<LogEntry>`.
   * Columns: Time (short format) and Description.
   * Logs events: task added, reminder set, quiz started, quiz completed.
   * Shows only the 10 most recent entries.

---

## Requirements

* **.NET 6.0 SDK** or later
* **Visual Studio 2022** (or newer) on Windows

---

## Setup & Execution

```bash
# Clone the repository
git clone https://github.com/VCCT-PROG2A-2025-G1/PROG6221_POEpart1_Simon_St10438897.git
cd CybersecurityAwarenessBot
```

1. Open `POEpart3.sln` in Visual Studio.
2. Restore NuGet packages and build the solution.
3. Set **POEpart3** as the Startup Project.
4. Run (F5) to launch the WPF application.

---

## Code Structure

* **MainWindow\.xaml / MainWindow\.xaml.cs**: Hosts the `TabControl` and implements UI logic for Chat, Tasks, Quiz, and Activity Log.
* **CyberChat.cs**: Encapsulates chatbot logic (keyword matching, sentiment, memory, responses).
* **Dictionaries**: Static response and follow-up dictionaries for chat and tasks.
* **Models**: `TaskItem`, `Question`, `LogEntry` represent core data.

---



## Demo Video

📺 [Video Presentation](https://youtu.be/IL3Xvdfpg6c)




