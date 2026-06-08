# 🏰 Tower Defense

> ⚠️ **Note:** It is a personal learning project built to explore game development with Godot and C#, but it is not yet fully functional or complete.

## 📌 About The Project
This is a 2D Tower Defense game built from scratch using the **Godot Engine** and **C#**. The goal of this project is to implement a clean, modular architecture for a classic strategy game, focusing on object-oriented programming in game development.

### ✨ Key Features (Under the Hood)
* **Resource-Driven Data**: Uses custom Godot Resources (`TowerData` and `EnemyData`) to define stats, making it extremely easy to tweak, balance, and add new types of towers or enemies.
* **Tower Tiers & Factions**: Data structures are already prepared for multiple tower categories, including *Historic*, *Mythic*, *Whispered*, and *Forgotten* tiers.
* **Placement System**: Includes a dedicated `PlacementSystem` to handle grid-based positioning, validations, and mechanics for deploying towers onto the battlefield.
* **Modular Effect System**: Supports dynamic status effects applied to enemies, such as `SlowEffect` and custom combat behaviors, alongside specific death triggers (`DeathEffect`).
* **In-Game Shop UI**: Features a `ShopManager` to handle player gold, tower selection, and purchasing logic.

## 🛠️ Built With
* **Godot Engine** (With .NET/C# support)
* **C# / .NET**

## 🚀 How to Open & Explore
Since this is a development repository, you will need the Godot Engine installed with .NET support to open it.

1. Clone the repository:
   ```bash
   git clone [https://github.com/AgentOutsiders/tower-defense.git](https://github.com/AgentOutsiders/tower-defense.git)
