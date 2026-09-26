# Minimum Edit Distance (MED) Algorithm

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12-239120?logo=csharp&logoColor=white)
![NLP](https://img.shields.io/badge/NLP-Minimum%20Edit%20Distance-0A66C2)
![License](https://img.shields.io/badge/License-MIT-green)

A C#/.NET 8 console application that implements the **Minimum Edit Distance (MED)** algorithm using **dynamic programming**. The project demonstrates both lexical similarity search over a vocabulary and step by step transformation analysis between two words.

---

## Overview

Minimum Edit Distance, commonly associated with **Levenshtein distance**, measures the minimum number of single character operations required to transform one string into another.

The implementation supports three basic edit operations:

- **Insertion** – adding a character
- **Deletion** – removing a character
- **Substitution** – replacing one character with another

The application exposes these capabilities through an interactive console menu:

1. **Find the 5 closest words** to a user provided word from a vocabulary.
2. **Calculate and visualize MED** between two words, including the optimal transformation path and the individual edit operations.

---

## Features

### Part 1 — Top 5 Similar Words

Given an input word, the application:

1. Loads the vocabulary.
2. Calculates the Levenshtein distance between the input and every vocabulary entry.
3. Sorts the candidates by ascending edit distance.
4. Returns the **five closest words**.
5. Reports the execution time in milliseconds.

---

### Part 2 — MED Calculation & Transformation Path

The second mode compares two user provided words and displays:

- The final Minimum Edit Distance.
- The complete dynamic programming matrix.
- The optimal path through the matrix.
- The sequence of insertion, deletion, and substitution operations.
- Execution time.

Cells belonging to the selected optimal path are highlighted in the console to make the dynamic programming process easier to follow.

---

## Test Results

### Part 1 — Similar Word Search

<p align="center">
  <img src="https://github.com/tolgamertsaruhan/MED_Algorithm/blob/main/images-for-readme/part1-test-result.png" alt="Part 1 Test Result">
</p>

### Part 2 — MED Matrix and Transformation Steps

<p align="center">
  <img src="https://github.com/tolgamertsaruhan/MED_Algorithm/blob/main/images-for-readme/part2-test-result.png" alt="Part 2 Test Result">
</p>

---

## License

This project is licensed under the **MIT License**.

See the [`LICENSE`](LICENSE) file for details.
