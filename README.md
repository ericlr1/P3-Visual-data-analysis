# Visual Data Analysis Tool

## 📖 Overview
This tool was developed as part of a data analysis and visualization project designed for Unity. It enables developers to collect, analyze, and visualize game data directly within the Unity editor, providing powerful insights into player behavior and helping optimize the user experience.

## ✨ Key Features
1. **Data Structure and Sampling Strategy**:
   - Player events (e.g., jumps, interactions, damage taken, deaths) are recorded in specific tables.
   - Each event includes key information such as session ID, player position, and event timestamp.

2. **Data Import and Export**:
   - Data is serialized from Unity and stored in a MySQL database via PHP scripts.
   - A generic system handles sending and receiving data in JSON format, utilizing the `Newtonsoft.Json` library for processing in Unity.

3. **Event Tracking**:
   - Dedicated scripts (e.g., `PlayerDataStruct.cs`, `GetPlayerData.cs`) gather information without altering existing game code.
   - Automatically logs player events based on specific conditions.

4. **Data Visualization**:
   - Unity tool for generating heatmaps and player paths.
   - Interactive editor controls allow:
     - Grid size and density adjustments.
     - Color palette selection.
     - Filtering by age, country, and gender.
     - Visualization of specific events.

5. **Final Results**:
   - Generates customizable visualizations that simplify detailed analysis of player behavior patterns.

## 🛠️ Technical Stack
The tool integrates the following technologies:
- **Unity** for creating and visualizing the data.
- **MySQL** for storing and managing event data.
- **PHP** scripts for server-side data handling.

## 👥 Contributors
- [Francesc Teruel Rodríguez](https://github.com/francesctr4)
- [Eric Luque Romero](https://github.com/ericlr1)
- [Mario García Sutil](https://github.com/mariogs5)
- [Marcel Sunyer Caldú](https://github.com/MarcelSunyer)

