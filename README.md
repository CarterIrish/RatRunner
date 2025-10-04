## [Sprint 2 - Week 2] - 2025-09-26 to 2025-10-03

### Added

- **Player Systems:**
  - Controllable rat model integrated into game scene (Jake)
  - NavMesh support for AI pathfinding (Jake)
  - Player tag for entity identification (Jake)
  
- **Enemy Systems:**
  - Enemy AI system that follows and chases the player (Jake)
  
- **Item & Puzzle Systems:**
  - Door unlocking mechanic - doors deactivate when player collects key (Brice)
  - Key-inventory integration for puzzle progression (Jake)
  - Updated key asset model (Brice)
  
- **Level Design:**
  - Playtest map created from reference sketch (Carter)
  - Basic start room - requires polish (Carter)
  - Testing tunnels with default materials (Carter)
  - Updated maze demo environment (Carter)

- **UI/Game State:**
  - Game Over state and temporary transition screen (Carter)
  - Code documentation for input manager callbacks (Carter)

### Changed

- Texture tiling fixed on ground materials - set to 1 for proper scaling (Chris)
- Rearranged player hierarchy in scene structure 
- Disabled player movement commands when UI is active (Carter)
- Improved input handling with UI map swaps (Carter)

### Fixed

- Player tipping/rotation bug during movement (Jake)
- Pause state bug that prevented proper game pausing (Carter)
- Merge conflicts from enemy feature branch integration (Jake)

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
