# Changelog

## [Sprint 5 - Week 2] - 2025-11-10 to 2025-11-16

### Added

- **Upgrade System Implementation:** (Carter)
  - Vision upgrade fully implemented (affects camera field of view)
  - Vigor upgrade fully implemented (invincibility timer on hit)
  - Dev console commands for applying mobility and vision upgrades
  - Godmode command for testing

- **Player Invincibility System:** (Carter)
  - Invincibility timer after taking damage
  - Visual feedback for invincible state
  - Godmode toggle for debugging

- **Screen Effects System:** (Chess)
  - ScreenEffectManager implementation for visual feedback
  - Enemy proximity vignette effect
  - Health/damage red vignette effect
  - Noise texture overlay for atmospheric effects

- **Jumpscare System:** (Chess)
  - Full jumpscare cutscene when caught by enemy
  - Jumpscare.cs script with timing and camera control
  - Integration with DayManager for death sequence

- **Audio Assets:** (Chess)
  - Death scream sound effect
  - Player squeak audio
  - Updated AudioManager with new sound triggers

- **Item Animations:** (Chris)
  - Animation controllers for Key, Needle, and Thread pickups
  - Idle animations for collectible items
  - Updated item models with animation support

- **Enemy Difficulty Scaling:** (Jake)
  - Dynamic enemy speed scaling based on current day
  - Final boss enemy spawns when key is picked up
  - Enemy behavior changes on final day
  - Enemies can traverse into tunnel sections
  - Additional enemy placed in rooms for increased difficulty

- **Level Design:** (Brice, Chess)
  - Final room layout completed and decorated
  - Item population across all rooms
  - Spring and Thread prefab placement
  - Gate repositioned for better gameplay flow

- **Lighting System Enhancements:** (Chris, Chess)
  - Updated lighting for new tunnel models
  - Light material variants (M_Light_Off for inactive lights)
  - Fog implementation for atmosphere
  - Room-specific lighting adjustments (especially room 5)
  - Light prefab system testing

- **UI Improvements:** (Brice, Chess, Carter)
  - Workbench UI visual implementation
  - Inventory display updates in real-time via UnityEvent
  - "Press [E] to Craft" prompt near workbenches
  - Vignette graphic assets (standard and red for damage)

### Changed

- **Upgrade System:** (Carter)
  - Mobility upgrade values rebalanced for better gameplay
  - UpgradeManager.cs moved to Upgrades folder for organization

- **Enemy System:** (Chess, Jake)
  - Enemy collision handling consolidated into Player.cs
  - EnemyNavigation refactored for better performance
  - Enemy collider fixes to prevent phasing issues

- **Lighting:** (Chris)
  - Tunnel models updated with improved geometry and lighting
  - Rust material textures updated (4K normal, metalness, color maps)
  - Brick tunnel normal maps enhanced
  - NavMesh regenerated for new geometry

- **Prefab Naming:** (Chess)
  - Needle prefabs renamed to Syringe for consistency

- **Scene Organization:** (Chess)
  - Final map cleanup and organization
  - Tunnel section reorganized

### Fixed

- **UI Bugs:** (Brice)
  - Fixed UI not being clickable bug
  - Removed black bars from vignette effect

- **Save System:** (Brice)
  - Fixed save system bugs with item persistence
  - Key pickup now properly triggers save

- **Dev Console:** (Carter)
  - Fixed mouse lock bug caused by dev console
  - Fixed null reference error on UIText after variable refactor
  - Corrected line endings for consistency

- **Lighting Issues:** (Chess, Chris)
  - Fixed lighting glitches and inconsistencies
  - Removed duplicate lighting objects
  - Corrected light placement in room 5

- **Enemy Bugs:** (Chess, Jake)
  - Fixed enemy nearby glitch
  - Fixed enemy collider issues preventing proper player detection

### Removed

- Reverted experimental lighting changes that caused issues
- Removed duplicate lighting objects from scene

## [Sprint 5 - Week 1] - 2025-11-03 to 2025-11-09

### Added

- **Workbench Visual Interaction:** (Chess)
  - "Press [E] to Craft" UI prompt system
  - Visual feedback when player is near workbench

- **Lighting Improvements:** (Chess, Chris)
  - Fog system implementation for atmosphere
  - Lighting prefab testing for standardization
  - Room-specific lighting adjustments

- **Enemy AI Enhancements:** (Jake)
  - Enemy speed increases on final day
  - Enemies can now patrol into tunnel areas
  - Enemy collider improvements
  - Enemies leave starting rooms after Day 1

- **Dev Console Commands:** (Carter)
  - Mouse lock toggle command for debugging
  - Console command improvements

### Changed

- **File Organization:** (Carter)
  - UpgradeManager.cs relocated to Upgrades folder

- **Lighting System:** (Chess, Chris)
  - Lighting testing and iterative improvements
  - Final map cleanup and scene organization

### Fixed

- **Save System:** (Brice)
  - Attempted fixes for save system persistence issues
  - Key pickup and ending sequence corrections

- **Dev Console:** (Carter)
  - Fixed mouse lock bug
  - Fixed null reference error on UIText

- **Level Design:** (Brice, Chess)
  - Gate positioning adjusted
  - Tunnel section corrections

## [Sprint 4 - Week 2] - 2025-10-27 to 2025-11-02

### Added

- **Dev Console System:** (Carter)
  - Full debug console implementation with command processing
  - Console game state with dedicated input handling
  - Endgame command with win/loss arguments for testing

- **Workbench/Crafting UI:** (Chess)
  - Workbench UI screen implementation
  - Visual card designs for upgrades (Mobility, Sight, Vigor)

- **Audio System:** (Chess)
  - Complete audio implementation for game events
  - Button hover and press sound effects
  - Workbench opening sound effect
  - Winning and losing ending sounds
  - Item pickup audio feedback
  - Enemy nearby audio

- **End Area System:** (Jake)
  - End game area with escape objective
  - Key-unlockable gate mechanic
  - Feedback text system for player prompts
  - Final room with victory trigger
  - Gate push-back mechanic to prevent phasing through

- **Lighting System:** (Chris)
  - Night skybox implementation
  - Lighting settings and profiles
  - Point light system
  - Reflection probes for improved visuals
  - Improved room lighting positions

- **Game Art:** (Chris)
  - Updated enemy rat model with detailed textures
  - Enemy idle animations (wiggle and breathe)
  - New room model with updated geometry
  - Updated tunnel models (T-junction, center, straight, turn variations)
  - Concrete siding material and textures
  - Metal siding material and textures
  - Wood plank material and textures
  - Updated brick tunnel textures

- **Level Design:** (Brice, Jake, Chris)
  - Room decoration and asset population
  - Additional enemy placement in rooms
  - Organized map layout improvements
  - Finished room 3 design

- **Settings Menu:** (Chess)
  - Settings menu UI created
  - Navigation between pause and settings screens

- **Upgrade System Implementation:** (Carter)
  - UpgradeManager stat application system implemented
  - Mobility upgrades now modify player speed and turning stats
  - Upgrade bonuses apply on crafting and save/load
  - PlayerMovement tracks base stats for additive upgrade bonuses

- **Workbench Visual UI Integration:** (Carter)
  - Connected console-based crafting to visual UI cards
  - Button interactability based on available resources
  - Level display (Lvl. 0, Lvl. 1, etc.) on upgrade tiles
  - UI updates automatically after crafting

- **Camera Settings Integration:** (Carter)
  - Connected sensitivity slider in Settings UI to ThirdPersonCamera
  - Active Sensitivity adjustment (0-200 range)
  - Slider initializes to current camera sensitivity value

### Changed

- **Inventory System:** (Carter)
  - Refactored to use `Dictionary<ItemsEnum, int>` instead of lists
  - Updated save system to handle dictionary-based inventory
  - Updated item collection to work with new inventory structure

- **Save System:** (Brice)
  - Extended to save player upgrades
  - Updated GameData structure for upgrade persistence

- **Enemy AI:** (Jake)
  - Updated enemy models with new assets
  - Fixed enemy pathing issues
  - Improved collision detection on attack trigger

- **Scene Updates:**
  - Repositioned rooms for better flow
  - Updated game scene with all new assets and systems

### Fixed

- **UI Bugs:** (Carter)
  - Fixed pause menu continue button error when reloading scene
  - UIManager now dynamically assigns onClick events to prevent missing references

- **Player Physics:** (Chris, Carter)
  - Fixed player falling through floor
  - Removed debug statement for gravity

- **Collision Issues:** (Jake)
  - Fixed enemy bugs with player interaction
  - Fixed gate phasing with push-back system

- **Save System:** (Carter)
  - Fixed item loading bug where "(Clone)" suffix prevented prefab loading
  - GameData now strips "(Clone)" from GameObject names before saving
  - Thread, Needle, and Spring items now persist correctly across sessions

- **Player Collision Handling:** (Carter)
  - Removed duplicate TakeHit component causing double collision triggers
  - Consolidated enemy collision handling into Player.cs
  - Fixed audio playing twice and day counter decrementing by 2 on enemy hit

### Removed

- Useless/redundant rooms from scene
- Old tunnel straight prefab references
- Deprecated factory assets

## [Sprint 4 - Week 1] - 2025-10-20 to 2025-10-26

### Added

- **Crafting/Upgrade System Foundation:** (Carter)
  - UpgradeManager singleton implementation
  - CraftingRecipe ScriptableObject system
  - Workbench interaction script
  - Recipe assets: Vision, Mobility, Vigor upgrades
  - Crafting console UI for displaying available recipes
  - Console-based crafting functionality with number key inputs

- **Player System:** (Carter)
  - Player class as component reference hub
  - Centralized access to Movement, Inventory, Camera, Upgrades
  - Player upgrade tracking with `Dictionary<UpgradesEnum, int>`

- **Workbench Asset:** (Carter)
  - Table 3D model with materials and textures
  - Workbench prefab with collision detection
  - Proximity-based crafting UI trigger

- **Enemy AI Enhancements:** (Jake)
  - Enemy patrol system implementation
  - AttackTrigger script for player collision detection
  - Patrolling enemies placed in multiple rooms

- **Map Development:** (Jake)
  - Phase 1 of new map layout completed
  - Organized map structure with labeled sections
  - Base room framework established

- **Level Population:** (Brice)
  - Decorative assets added to starting room
  - Multiple rooms filled with environment details
  - Item placement for gameplay testing

- **Audio Assets:** (Chess)
  - Imported menu background music
  - Imported atmosphere/ambient game audio
  - Audio integration into Menu and Game scenes
  - Updated AudioManager for new audio sources

### Changed

- **Inventory Architecture:** (Carter)
  - Converted from list-based to dictionary-based system
  - ItemsEnum now serves as dictionary key
  - Item quantities tracked as integer values

- **Save System:** (Carter)
  - Updated GameData to serialize dictionary inventory
  - Adapted save/load logic for new data structure

- **Input System:** (Carter)
  - Added CRAFTING game state to state machine
  - Console input integration for crafting interactions

- **Game State Management:** (Carter)
  - Added CONSOLE state for debug console
  - Input map swapping for crafting state

### Fixed

- Merge conflicts from feature branch integrations

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

- **Game Layers** (Carter)
   - Added `Player` layer to the game scene as part of camera clipping bug fix.

### Changed

- **Scene Updates:** (Chess)
  - Updated Game, GameOver, and Menu scenes with new UI elements
  - Improved visual presentation across game states

- **Game Art** (Chris)
  - Updated tunnels with higher detail models

### Fixed

- **Camera Bugs:** (Carter)
  - Fixed rat stutter bug caused by Update vs FixedUpdate timing mismatch
  - Fixed camera clipping into Rat model caused by raycast collision with Rat/Player object
    - Now ignores the new `Player` layer

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
