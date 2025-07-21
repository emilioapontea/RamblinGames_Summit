# RamblinGames_Summit
> Emilio Aponte, Neja Atapattu, Rushil More, Benjamin Skelton, Jingyan Xu

## Info
* Start scene: `Title.unity`

## How to play
* Use W,A,S, and D keys to move Luna through the different rooms.
* Each room has a different objective.
* Dodge enemies, collect items, solve puzzles to move on
* Bowser and Lakitu will talk to Luna to provide information
* Signs are posted throughout with hints
* If you ever lose all your hearts or die, you will restart the room.
* You can always use the pause menu to restart a room.

### Level1
Level 1 is an intro, just use the keys to move the character.
Trigger the drawbridge to get inside the tower and move on to the next level.
If you fall in the moat, you die.

### Maze (Dungeon)
Bowser tells you he has been here before. This room is a dungeon with ghost enemies.
The ghosts patrol the halls, and if they touch you you lose a heart.
If you fall in lava, you die. Bowser left a sign in the dungeon explaining how to use fireballs.
You can shoot fire to kill the ghosts.
If you reach the end, the gate opens and you move on to the next room.

### LevelX (Garden)
Player has to collect all the gems whilst dodging the enemy and
keeping health.
Once all 7 gems have been collected the doors will slide open
and after entering will move to the next scene (Ice puzzle level).
There are purple exploding tiles that disappear 3 seconds after
collision.
2 Enemy monsters that keep guard of the garden and will decrease
health on collision with player.
1 AI Enemy agent with a state machine that will patrol around the
garden to specific waypoints and also be on alert and then chase
the player (exclamation mark bubble appears) when the player gets
close (shown in the video).
Added audio for gem collection and enemy obstacles that will
decrease health on the health bar.
1 garden guard will accept gems as payment to open the door to the next room.
If you fall in the water, you die.

### IcePuzzleLevel
Move the character around to solve 4 separate ice block puzzles.
To push an ice block, move towards it for an extended period of time.
After the block experiences a prolonged impact force, it will slide
in that direction at a constant speed. The ice block will stop sliding
when it hits the edge of the puzzle (indicated by raised snow terrain),
another ice block, or any obstacles in the way (wooden crate).
To solve a puzzle, an ice block must stop exactly in the position
indicated by the translucent red cube.
Solve all 4 ice puzzles to unlock the door and move on to the next scene (End)

## Known problem areas
### `Title` scene
* May be issues with text scaling on different screen sizes

### `Level1` scene
* Luna animation not yet implemented

### `LevelX` scene
* Root motion on character does not have any backward motion implemented
* Blending between motions will be much improved by the final submission
* Character may glitch when turning left in grass puzzle level
* Currently grass puzzle level is the only level with root motion implemented, but the other levels will be adapted in the next submission
* Also will incorporate more interaction with the level surroundings in the next submission by creating additional models the character can interact with
* Will include a how to play screen before each level and be more obvious when the player has completed the level and what to do next.

### `IcePuzzleLevel` scene
* Walls can be walked through at certain spots due to uneven terrain
* Ice block push force threshold is inconsistent
* Luna animation note yet implemented

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

### Team member assets contributed
Emilio
* Materials/IcePuzzleLevel_Materials/*
* Models_and_Animations/IcePuzzleLevel/*
* Prefabs/IcePuzzleLevel/*
* Scenes/IcePuzzleLevel.unity
* Scenes/End.unity

Neja
* Assets/Materials/LevelX
* Asets/ModelsAndAnimations/LevelX
* Assets/Prefabs/LevelX
* Assets/Scenes/LevelX

Luna - prefab model imported from asset store that we added root motion and and animator to move the character.
Collectable gems that spin, with the model imported from unity asset store and added rotating animation, and a collectable script to it.Also added audio
Purple exploding tiles that disappear 2 seconds after the player collides with it.
Yellow moving tiles that move up and down that the player jumps on to reach the gems.
2 enemy agents (monsters), whose models were imported from the asset store, and then added audio to detect collisions with player and move randomly around the navmesh.
1 purple AI agent (big monster) that has a state machine and on default is on patrol between 4 different way points around the room, when player gets pretty close to it, the agent begins to chase the player with an exclamation mark bubble to show it is in chase mode.
Added in a grass material texture for the ground, and a material for the walls and doors as well.
Health bar was implemented off of the asset store

Benjamin
* Materials/Level1_Materials
* Models_and_Animations/Level1_Models_and_Animations
* Physics_Materials/Level1_Physics_Materials
* Prefabs/Level1_Prefabs
* Scenes/CharacterTest.unity
* Scenes/Controls.unity
* Scenes/Level1.unity
* Scenes/Title.unity

### Team member C# files contributed
Emilio (`Scripts/IcePuzzleLevel_Scripts`)
* `CentralAudioManager.cs`
* `ExitHandler.cs`
* `IceBlockController.cs`
* `IcePuzzleTriggerController.cs`
* `PuzzleCounter.cs`

Neja
* `CoinManager.cs`
* `CollectableCoin.cs`
* `DoorOpener.cs`
* `ElephantControl.cs`
* `EnemyFSM.cs`
* `EnemyWalking.cs`
* `ExploadingTile.cs`
* `LoadNextScene.cs`
* `MovingTile.cs`
* `PlayerMovement.cs`
* `HealthBarController.cs`
* `HealthBarHUDTester.cs`
* `PlayerStats.cs`
* `RootMotionControl.cs`

Benjamin (`Scripts/Level1_Scripts`)
* `DrawbridgeAnimator.cs`
* `DrawbridgeAudioPlayer.cs`
* `DrawbridgeTrigger.cs`
* `LoseHandler.cs`
* `SceneReset.cs`
* `WinHandler.cs`