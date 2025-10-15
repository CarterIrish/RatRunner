# Rat Runner

A survival horror game about a rat exploring dangerous sewers, gathering resources, and upgrading its abilities to escape.

**Genre:** Survival Scavenger / Horror / Rougelike

**Platform:** PC (Unity 2023.2.20f1)  
**Team:** Alt+F5  
**Course:** [IGME.320 - Game Design & Devlopment II]  
**Development Period:** September 2025 - Present

## About

Rat Runner combines tense exploration with roguelike mechanics in an eerie sewer environment. Players control a rat navigating a dark, maze-like sewer system, collecting items and upgrades while avoiding or outrunning enemies. Death returns you to your colony base where you can purchase permanent upgrades for future runs.

### Design Pillars
- **Tense Exploration** - Navigate unknown maze environments under pressure
- **Eerie Atmosphere** - Dark, quiet sewers with lurking threats
- **Survival Mechanics** - Death loop with persistent upgrades

## Current Features
- First-person player movement and camera controls
- Enemy AI with pursuit behavior
- Item collection and inventory system
- Door-key puzzle mechanics
- Game state management (Pause, Playing, Game Over)
- NavMesh-based pathfinding

## Team
- **Maya** - Concept Art/Design, UI/UX Programming
- **Brice** - Items & Inventory Programming
- **Jake** - Player & Enemy Programming  
- **Carter** - GameManager, Map Creation
- **Chris** - 3D Graphics & Assets

## Development

[Link to High Concept Document](https://docs.google.com/document/d/e/2PACX-1vRN4v44V8mjBcOPSkB2Yl0jgf-J6qtahjTJwznlAHGf2UYgckZKTMoXCYlzLRgnN6s2SkB-l_eS0BuG/pub)

---

# Changelog

## [Sprint 3 - Week 2] - 2025-10-13 to 2025-10-19

### Added

- **Save/Load System:** (Brice)
  - Complete save and load functionality with binary file storage
  - Automatic save after each day
  - Item position saving and loading system
  - Game data parsing and restoration on load
  - Resources folder structure with prefabs for Key and Cheese items

- **Camera System:**
  - CameraFollow.cs script (Jake) integrated into camera profile structure (Carter)

- **UI Assets:** (Chess)
  - Game Over screen artwork
  - Pause screen artwork
 
- **UI Feedback** (Jake)
  - Day progression notification system - UI element displays when day increases

### Changed

- **Scene Updates:** (Chess)
  - Updated Game, GameOver, and Menu scenes with new UI elements
  - Improved visual presentation across game states

- **Game Art** (Chris)
  - Updated tunnels with higher detail models

### Fixed

- **Camera Bugs:** (Carter)
  - Fixed rat stutter bug caused by Update vs FixedUpdate timing mismatch

- **Enemy AI Bugs:** (Jake)
  - Fixed bug where enemy wouldn't target player after loading a saved game

- **Save System Bugs:** (Brice)
  - Resolved item duplication issue when loading saved games
  - Items now properly track picked-up state

## [Sprint 3 - Week 1] - 2025-10-06 to 2025-10-12

### Added

- **Day System:** (Jake)
  - Day Manager with basic functionality for tracking game progression
  - Day-based gameplay loop foundation

- **Save System Foundation:** (Brice)
  - Initial save feature for inventory and day count
  - Binary file serialization for game data
  - GameData and SaveSystem scripts

- **Camera System:** (Carter)
  - Camera Manager for handling multiple camera profiles
  - Three camera profiles: First-person, Third-person, and Fixed
  - Third-person camera with mouse-based controls
  - Input Actions integration for camera controls
  - Improved zoom responsiveness on fixed camera
  - Camera switching framework

- **Pause System:** (Chess)
  - Pause menu UI implementation
  - Continue button functionality

- **Menu/UI Assets:** (Chess)
  - Title screen artwork and implementation
  - Custom typography (Green Fuz and Magiera Script fonts)
  - Updated GameOver and Menu scenes

### Changed

- **Code Architecture:** (Carter)
  - Refactored pause UI code from GameManager to UIManager
  - Decoupled game state management from UI handling
  - Updated input handling across multiple scripts for new camera system

- **Player Tag System:** (Carter)
  - Added player tag to ProjectSettings for better entity identification

### Fixed

- **UI Bugs:** (Carter)
  - Fixed pause menu UI bug preventing proper menu interaction
  - Resolved missing Unity scene update issues

## [Sprint 2 - Week 2] - 2025-09-26 to 2025-10-03

### Added

- **Player Systems:**
  - Controllable rat model integrated into game scene (Jake)
  - NavMesh support for AI pathfinding (Jake)
  - Player tag for entity identification (Jake)
  
- **Enemy Systems:**
  - Enemy AI system that follows and chases the player (Jake)
  
- **Item & Puzzle Systems:**
  - LockedDoor script for handling key-based door unlocking (Carter)
  - Event-based item pickup system with collision handling (Carter)
  - Door unlocking mechanic - doors deactivate when player collects key (Brice)
  - Key-inventory integration for puzzle progression (Jake)
  - Updated key asset model (Brice)
  
- **Level Design:**
  - Playtest map created from reference sketch (Carter)
  - Basic start room asset - requires polish (Carter)
  - Updated maze demo environment (Carter)

- **UI/Game State:**
  - Game Over state and temporary transition screen (Carter)
  - Code documentation for input manager callbacks (Carter)

- **Game Art:** (Chris)
   - Crated tunnel assets
      - T-connector, cross-junction, straight pipe, left & right bends
   - Created rat asset
   - Created cheese asset 
   - Created thread & shard assets
   - Created ket asset
   - Created needle asset

### Changed

- Texture tiling fixed on ground materials - set to 1 for proper scaling (Chris)
- Rearranged player hierarchy in scene structure 
- Disabled player movement commands when UI is active (Carter)
- Improved input handling with UI map swaps (Carter)
- Refactored inventory system to use event listeners instad of direct class/object references (Carter)
- Updated enemy nav to integrate with event system (Carter)

### Fixed

- Player tipping/rotation bug during movement (Jake)
- Pause state bug that prevented proper game pausing (Carter)
- Merge conflicts from enemy feature branch integration (Jake)
- Fixed memory leak in LockedDoor event listener cleanup (Carter)
- Removed redundant scene load in TempGameOver script (Carter)

### Removed

- Redundant code from game state management (Carter)

### Build

- First playable build created for testing

## [Sprint 2 - Week 1] - 2025-09-24 to 2025-09-25

### Added

- #### **Core Systems:**

  - Game State Machine for managing game flow (Pause, Playing, GameOver states) (Carter)
  - Game Manager singleton for centralized game control (Carter)
  - UI Manager for interface handling (Carter)

- #### **Player Systems:**

  - Basic player movement with WASD/Arrow key controls (Jake)
  - First-person camera controller (Jake)
  - Temporary player assets for testing (Jake)

- #### **Inventory System:**

  - Item pickup mechanics (Brice)
  - Inventory management system (Brice)
  - Item interaction framework (Brice)

- #### **Project Infrastructure:**

  - `.gitignore` file for Unity project (Carter)
  - Scripts folder organization (Carter)
  - Feature branch workflow (dev branch for staging) (Carter)
  - Initial Unity project setup (2023.2.20f1 LTS) (Carter)
