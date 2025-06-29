# RamblinGames_Summit
> Emilio Aponte, Neja Atapattu, Rushil More, Benjamin Skelton, Jingyan Xu

## Alpha Version Manifest
### Changelog
Emilio
* Created `IcePuzzleLevel` scene with 4 different Ice puzzles
* Created ice puzzle blocks that need to be pushed (for an extended period of time) by the player and then slide with constant speed until they hit the edge or another ice block
* Ice puzzle trigger objects (transparent, red) turn green and complete the ice puzzle when an ice block stops at its grid location
* When all 4 puzzles are completed the exit door opens, revealing an invisible box collider to start the next scene
* Added ice and snow textures
* Added snow terrain to separate the 4 ice puzzle areas
* Add sound effects
  * Slide sound for ice blocks
  * Sound effects for achievements (complete puzzle, finish level, etc.)
  * Door open sound effect
* Add wall and door texture/material

Neja
* Created a garden gem level.
* Player has partial root motion and animation. Currently implemented forward motion and jumping.
* Player has to collect all the gems whilst dodging the enemy and keeping health. Once all 7 gems have been collected the doors will slide open and there is a collider to move to the next scene. There are purple exploding tiles that disappear 3 seconds after collision. 3 Enemy monsters that keep guard of the garden and will decrease health on collision with player.
* Added audio for gem collection and enemy obstacles that will decrease health on the health bar.

### TODO
Emilio
* Improve player movement/player sprite/add push animation

Neja
* Game should restart when player reaches 0 health.
* Enemies should be able to collect the gems as well (maybe in the final version).
* Backward root motion has not been implemented. Turning left may also glitch.

## Game Proposal
### Gameplay Mechanics
Our game, Summit, is a 3D heist game where you play as one of the evil king Browser’s minions who is tasked with infiltrating Princess Orange’s tower to retrieve the evil king’s stolen treasure. Orange had previously looted Browser’s castle, injured his guards, and escaped with all the treasure. As one of the king’s most valuable minions, your mission is to storm Orange’s tower, reclaim the stolen treasure and take down the princess.

You will ascend the tower by completing each level(a room) and its unique challenge, with the final level consisting of a boss fight. For combat, each character will have a basic attack and a special ability with the ability to utilize powerups they have gathered throughout the rooms.

### Formal elements
Rules & Objectives:
* Clearing the level by defeating the enemy or completing the challenge in reach room
* Characters have limited health and dying restarts the current room
* To win, simply finish all the levels and beat the final boss.

The player will be able to select a character that they will control throughout the game as they fight through the levels. Each character will have unique abilities and distinct combat styles.

### Workplan
We foresee the need for a number of algorithm implementation decisions to be made, particularly when it comes to the enemies. We plan to have several levels with different enemy challenges, including a car racing level, in which we will need to implement complex enemy path planning algorithms.

For the game pitch we split into two teams where Rushil, Jingyan and Benjamin worked on the unity demo and the rest (Emilio and Neja) worked on writing the team pitch proposal document and creating the presentation slides. Moving forward, we will split up work individually or in pairs to implement specific functionalities required for each milestone, ensuring version control through unity’s platform or github. Additionally, we will have a few days dedicated to testing where each member will test specific functionality to ensure that features are working as expected.
