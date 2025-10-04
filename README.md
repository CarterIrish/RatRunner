## \[Sprint 2 \- Week 2\] \- 2025-09-26 to Present

### Added

- Enemy AI system that follows and chases the player (\#4)  
- Game Over state and temporary transition screen  
- Player tag for entity identification  
- Code documentation for input manager callbacks

### Changed

- Rearranged player hierarchy in scene structure  
- Disabled player movement commands when UI is active  
- Improved input handling with UI map swaps

### Fixed

- Player tipping/rotation bug during movement  
- Pause state bug that prevented proper game pausing  
- Merge conflicts from enemy feature branch integration

### Removed

- Redundant code from game state management

## \[Sprint 2 \- Week 1\] \- 2025-09-24 to 2025-09-25

### Added

- #### **Core Systems:**

  - Game State Machine for managing game flow (Pause, Playing, GameOver states)  
  - Game Manager singleton for centralized game control  
  - UI Manager for interface handling

### 

- #### **Player Systems:**

  - Basic player movement with WASD/Arrow key controls (\#2)  
  - First-person camera controller  
  - Temporary player assets for testing

  ### 

- #### **Inventory System:**

  - Item pickup mechanics  
  - Inventory management system  
  - Item interaction framework

  ### 

- #### **Project Infrastructure:**

  - `.gitignore` file for Unity project  
  - Scripts folder organization  
  - Feature branch workflow (dev branch for staging)  
  - Initial Unity project setup (2023.2.20f1 LTS)
