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

### GemLevel (Garden)
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
To push an ice block, move towards it and press ENTER.
It will slide in that direction at a constant speed. The ice block will stop
sliding when it hits the edge of the puzzle (indicated by raised snow terrain),
another ice block, or any obstacles in the way (wooden crate).
To solve a puzzle, an ice block must stop exactly in the position
indicated by the translucent red cube.
Solve all 4 ice puzzles to unlock the door and move on to the next scene

### PlatformLevel
Bowser tells you this is the final room.
Traverse across the level, avoiding the large water and lava puddles.
Stepping on either one causes death.
A hint tells you you can wall jump on red walls. Use your three well-times wall jumps
to get up to the upper platform and grab the blue mushroom.
The blue mushroom grants invincibility to water. Drop to the water puddle and grab
the red mushroom. This grants lava invincibility, so you can go through the lava puddle
to collect the orange mushroom. The final mushroom makes the star appear.
Collect the star but (surprise!) the star moves. Collect it again to win the game.

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

## Final Version Manifest
### Team member assets contributed
Emilio
* Materials/IcePuzzleLevel_Materials/*
* Models_and_Animations/IcePuzzleLevel/*
* Prefabs/IcePuzzleLevel/*
* Scenes/IcePuzzleLevel.unity
* Scenes/End.unity
* Sound assets
* Luna animation controller

Neja
* Assets/Materials/LevelX
* Asets/ModelsAndAnimations/LevelX
* Assets/Prefabs/LevelX
* Assets/Scenes/LevelX
* Assets/Materials/GemLevel
* Assets/ModelsAndAnimations/GemLevel
* Assets/Prefabs/GemLevel
* Assets/Scenes/GemLevel
* Luna - prefab model imported from asset store that we added root motion and and animator to move the character.
* Collectable gems that spin, with the model imported from unity asset store and added rotating animation, and a collectable script to it.Also added audio
* Purple exploding tiles that disappear 2 seconds after the player collides with it.
* Yellow moving tiles that move up and down that the player jumps on to reach the gems.
* 2 enemy agents (monsters), whose models were imported from the asset store, and then added audio to detect collisions with player and move randomly around the navmesh.
* 1 purple AI agent (big monster) that has a state machine moves between 2 waypoints in front of the door, when player gets close to it, the agent converses with the player to give them a clue on how to solve the game.
* Added in a grass material texture for the ground, and a material for the walls and doors as well.
* Health bar was implemented off of the asset store.

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
* Materials/Level1_Materials (Some of the materials I created in this folder used base/height/normal maps from other levels. When creating materials from borrowed textures, I did define some unique parameters such as tiling so that these textures would better suit my level. The materials and textures in Materials/Level1_Materials/Aquaset were imported from the free package 'Marble Design Materials' found on the Unity Asset Store. This package was published by the user 'Aquaset')
* Models_and_Animations/Level1_Models_and_Animations
* Physics_Materials/Level1_Physics_Materials
* Prefabs/Level1_Prefabs
* Materials/Joy_Materials/WalljumpWall.mat
* Physics_Materials/Joy_physics_materials/Wall_material.physicMaterial
* Physics_Materials/Joy_physics_materials/WalljumpWall_material.physicMaterial
* Scenes/Level1.unity (I wasn't the only person who worked on this scene, but I believe I did most of the work on getting this scene complete).
* Scenes/PlatformLevel.unity (I implemented the walljump mechanics as well as the walljump platforming challenge)

### Team member C# files contributed
Emilio (`Scripts/IcePuzzleLevel_Scripts`)
* `CentralAudioManager.cs`
* `ExitHandler.cs`
* `IceBlockController.cs`
* `IcePuzzleTriggerController.cs`
* `PuzzleCounter.cs`
* `Assets/Scripts/LevelX/BackgroundAudioManager.cs`
* `Assets/Scripts/LevelX/Player/RootMotionControl.cs`
* `Assets/playerLose.cs`
* `Assets/PlayerSoundManager.cs`

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
* All Scripts/GemLevel:
* `CharacterInputController.cs`
* `CoinManager.cs`
* `CollectableCoin.cs`
* `DoorOpener.cs`
* `EnemyFSM.cs`
* `EnemyWalking.cs`
* `ExploadingTile.cs`
* `LoadNextScene.cs`
* `MovingTile.cs`
* Player files:
* `PlayerMovement.cs`
* `HealthBarController.cs`
* `HealthBarHUDTester.cs`
* `PlayerStats.cs`
* `RootMotionControl.cs`
* Camera files:
* `ThirdPersonCamera.cs`

Benjamin (`Scripts/Level1_Scripts`)
* `DrawbridgeAnimator.cs`
* `DrawbridgeAudioPlayer.cs`
* `DrawbridgeTrigger.cs`
* `LoseHandler.cs`
* `SceneReset.cs`
* `WinHandler.cs`
* `Scripts/Level1_Scripts` (I did create all of the scripts in this folder, but other team members may have made some minor edits to some of the enclosed scripts)
* `Scripts/Scripts_Joy/Final_Joy/MushroomPickup.cs` (I did not create this script myself, but I did add some code to have the mushrooms spin along the y-axis)

Jingyan (Joy) (`Scripts/Scripts_Joy`)
*`Final_Joy/AudioManager.cs`
*`Final_Joy/CharacterJump.cs`
*`Final_Joy/DeathManager.cs`
*`Final_Joy/GameStateManager.cs`
*`Final_Joy/GapTrigger.cs`
*`Final_Joy/HintManager.cs`
*`Final_Joy/IceLakeTrigger.cs`
*`Final_Joy/JoyLoseHandler.cs`
*`Final_Joy/LavaLakeTrigger.cs`
*`Final_Joy/MushroomManager.cs`
*`Final_Joy/MushroomPickup.cs`
*`Final_Joy/PauseManager.cs`
*`Final_Joy/StarController.cs`
*`Audio/Joy/*`

Rushil
* Created Maze/Dungeon Level Scene
* Player Navigates through a dungeon while battling enemies to eventually reach the end of the dungeon where they move onto the next level
  * Created Gate which operates based on certain thresholds- i.e. player proximity, or level objectives completion
  * Contributed Ghost and Skeleton Movement Scripts
  * Contributed Fireball Attack functionality for player on dungeon level
  * Worked on various bugs and defects throughout the development process
* Made final trailer for game
* Scripts Contributed:
  * Scripts Assets/Scripts/Dungeon