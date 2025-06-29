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

Benjamin
- Created 4 scenes: title screen, controls screen, character testing scene, and Level 1 (tower entrance)
- The title screen and controls screen are composed of a UI canvas with a few text elements, and buttons to proceed to a set destination scene.
- The character test scene is not meant to be part of the game, but it contains a small play area with a ramp, fences, and a pillar to test character movement and collision.
- I have created a player character to be used for the CharacterTest and Level1 scenes. This player consists of a cylinder collider (no actual model yet) with various features. 
- The player character can be moved in four directions using the W/A/S/D keys, and can turn in place using the O and P keys. Turning the character also moves the camera so that it remains pointed at the character's back. The character's controls should seem consistent as the camera rotates (e.g. pressing the W key always causes the player to move in the direction the camera is facing).
- The player character can jump by pressing the spacebar. Like many platformer games, the player's jump height increases the longer spacebar is held down for.
- The player character has the ability to die by falling in the moat surrounding the tower, causing the game to stop time, and display a message prompting the user to press the R key to reset the level. The R key can only be used to reset the level after the player has died.
- The 'Level1' scene is intended to be the tower entrance. This area is mainly here to allow the player to experiment with the controls in a safe area. I tried to add a bit of atmosphere by adding a drawbridge, and some lighting effects in the tower entrance room.
- Level 1 has a drawbridge which makes use of an invisible trigger zone to detect that the player character is near the drawbridge. The drawbridge uses mecanim to lower the bridge when the player moves into the trigger zone, and to raise the bridge when the player leaves the trigger zone.
- A pause screen can be accessed in the 'Level1' and 'CharacterTest' scenes by pressing the Escape key. The pause menu stops the game while visible, and has a button to reset the current scene, and another button to terminate the game. Pressing the Escape key while the pause menu is open will close the pause menu and resume time in the scene.
- Reaching an invisible win area within the 'Level1' scene (placed at some stairs leading further up the tower) will cause a level complete message to appear. This message prompts the player to press the Enter key to move onto the next level (which can be set in the inspector).
- I have added a few sound effects to the 'Level1' scene.
  1. The player character plays an effect upon jumping.
  2. The player character plays an effect upon landing.
  3. The player character plays an effect upon dying.
  4. The drawbridge plays an effect upon a player character entering or exiting the trigger zone, signaling that the drawbridge is going to be raised or lowered.

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
