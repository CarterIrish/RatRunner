# Rat Runner

Survive, evolve, and escape the sewers of madness.

## Overview

Rat Runner is a fast-paced survival horror game where you play as the lone sane survivor of a scientist’s failed experiments. Explore a maze-like sewer system, collect items, evolve new abilities, and outsmart the infected rats hunting you. Your goal is simple: survive and escape!

## Genre
Survival Scavenger / Horror / Roguelike

## Gameplay

Use WASD to navigate the sewers and E to craft items. Pick up resources by moving over them, manage limited time, and avoid enemy rats at all costs. With only three days to break free, every decision matters.

## Features

**Enemy AI & Navigation:** Implemented enemy behaviors using Unity’s NavMesh system, including patrolling, player detection, and dynamic speed scaling. Enemies navigate baked NavMesh surfaces and switch between patrol and pursuit states based on player proximity.

**Save & Load System:** Developed a secure save/load feature that records player progress at the end of each in-game day using file I/O and binary serialization to prevent tampering with game data.

**First-Person Movement & Camera Controls:** Implemented smooth first-person controller movement and camera look mechanics, allowing players to freely navigate the environment with responsive and immersive controls.

**Item Collection & Inventory System:** Designed an item pickup and inventory system that lets players gather resources, track collected items, and use them for crafting or progression throughout the game.

**Door–Key Puzzle Mechanics:** Built interactive puzzles where players must locate keys and unlock doors to advance, adding exploration, and tension to the gameplay loop.

**Game State Management:**  Developed a system to manage all core game states including Playing, Paused, and Game Over, ensuring consistent transitions, UI updates, and smooth flow throughout the player experience.

## Story

You are the final successful test subject of a disgraced geneticist obsessed with unlocking immortality. Deep beneath the city, his abandoned experiments roam the sewers, failed, unstable, and feral. As the only one who survived with your mind intact, you must escape the labyrinth he created before you become just another experiment gone wrong.

## Motivation

Rat Runner began as a passion project, an opportunity for our team to challenge ourselves, learn new systems, and build a polished, fully playable game we could be proud of. With Halloween coming up, we aimed to create a horror experience that was atmospheric, tense, and fun. Many of us had never developed a horror game before, so we dove into research, experimentation, and plenty of iteration.  
Our goal was to push ourselves as developers and designers to create a unique experience that shows our growth, creativity, and collaboration.

## Tech Stack

| Layer | Technologies Used |
|-------|--------------------|
| Engine | Unity |
| Software | Visual Studio, GitHub |
| Programming Language | C# |
| Project Management | Trello |

## Controls

**Move:** WASD  
**Craft:** E  
**Change Camera:** C  
**Options Menu:** Escape

## Installation

Download the ZIP file from our <a href="https://chessmix.itch.io/rat-runner" target="_blank">Itch.io</a> page, unzip it, and launch the executable to begin playing.

## Credits

### Team Members

Technical Artist & Audio Engineer — Maya Teng  
Game Programmer & Audio Engineer — Brice Woodburn  
Game Programmer — Jake Shapiro  
Game Programmer — Carter Irish  
3D Artist — Chris Wells

### Assets Used

<a href="https://assetstore.unity.com/packages/3d/props/industrial/abandoned-factory-lite-62597" target="_blank">Abandoned Factory (Lite)</a>

## Documentation

[Link to Changelog Document](changelog.md)

[Link to High Concept Document](https://docs.google.com/document/d/e/2PACX-1vRN4v44V8mjBcOPSkB2Yl0jgf-J6qtahjTJwznlAHGf2UYgckZKTMoXCYlzLRgnN6s2SkB-l_eS0BuG/pub)