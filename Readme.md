
A dynamic, interactive and knowledge game developed in C#. Challenge your general knowledge while managing your lives and aiming for the highest score!

---

## 🎮 How to Play
1. **Launch:** Run the application via your terminal.
2. **Questions:** The game automatically shuffles the question pool for a unique experience every time.
3. **Lives:** You start with **3 lives** (❤️). Every wrong answer costs you one life!
4. **Input:** Type the number of your chosen answer (1-4) and press **Enter**.
5. **Game Over:** The game ends when you either finish all questions or run out of lives.

---

## ✨ Features
* **Randomized Gameplay:** Implements the **Fisher-Yates Shuffle** algorithm to ensure questions appear in a random order.
* **Heart-Based Life System:** Adds a layer of difficulty and "Game Over" logic.
* **Robust Input Handling:** Uses `.Trim()` and `int.TryParse()` to prevent crashes from accidental spaces or non-numeric input.
* **Object-Oriented Design:** Structured using a dedicated `Question` class for clean and maintainable code.
