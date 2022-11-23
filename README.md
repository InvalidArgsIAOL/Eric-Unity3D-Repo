# Eric-Unity3D-Repo
Repo for Unity3D Projects.
Untitled ??? Game - Alpha WIP

While I initally wanted to design a hack-and-slash...
I'm having way too much fun with Unity-chan's model and animations.
I will likely transition this game to more stealth/escape/survival type game.

Please read this document and report any gamebreaking bugs/overtly odd behavior.

-------------------------------------------------------------------------------------------------------------
Controls:
Keyboard and Mouse implementation ONLY!
No controller support at this time. Request if wanted.

WASD for movement.
Mouse to move the camera.
Left click to slide. NOTE: This is a fixed direction slide!
Esc to Pause/Bring up the Menu. Esc to unpause the game.

Menu Controls: Mouse to click buttons.

-------------------------------------------------------------------------------------------------------------

Alpha Prototype Features:

Unity-chan. No, I'm not joking. I found this asset on the Unity Store and it was actually pretty awesome.

Player Control Functionality - Move/"attack"(slide)/Camera.
Player Animations - Movement, Slide, Tired(move continuously for 10 seconds, sliding resets this!)
Player idle animations - Wait ~10 seconds and you'll see Unity-chan do 1 of 3 different idle animations.

Test Map - Walk around and (hopefully) not fall off the edge of the terrain!
Game Objective - Walk to the exit point. Sadly, that's it for now, since the enemies can't attack yet!
Avoid enemies! They'll chase you(?) and stare at you menacingly.


Enemy AI Functionality
AI Jobs - Only two jobs are implemented, Guard and Patrol. Hunt will be added later.
AI Classes - Only one class is implemented, the Ranger.

Shared AI Behavior:
Enemies will stand and guard (idle state, currently actual guarding unimplemented). 
Enemies can adopt fixed patrol paths. (set manually in the scene)
Enemies have a Field of View - locked usually to 100-120 degrees. 
- Players outside of this angle cannot be detected. 
- Players behind obstacles cannot be detected.
- Enemies will lose track of the player if they hide behind an obstacle.
Enemies can track the player when the player is seen.
Enemy detection range will INCREASE if a player has been seen to prevent them from running away easily.
- Later, this will become the attack range.

Enemies will move towards the player when detected(?) - Currently unimplemented functionality.

Ranger AI Specifics:
The ranger will keep distance from the player if the player is detected.
Ideally, the ranger will also try to strafe around a player in a circular motion, but I'm too dumb to code that.
Any suggestions are welcome.
- The distance allows the ranger to attack the player more effectively.


Menu and Loading Screen. Because of the weight of this game (small), loading is incredibly fast.
Inside this folder will be a screenshot of the loading screen.

Pausing - You can pause the game by pressing Esc. Press Esc again to unpause the game.

-------------------------------------------------------------------------------------------------------------

Known Issues:

1. When the goal is reached by the player, the game continues to play.
A menu will appear that lets the player choose to quit or go back to the main menu.
Since the game isn't paused, trying to get to the menu will make the camera shift.

Priority - Very Low. I need to test things after the game ends.

2. Animations are somewhat wonky for the only enemy currently in the game.
Most notable example is when the enemy strafes away from the player.
The enemy may glide slightly before coming to a stop.

Priority - Low. I need to get more enemy types on the field.
I also need to give the Ranger an attack to hit the player.

3. Nothing happens when the player is detected.

Priority - High. I am working on implementing damage/projectiles(?)/raycast hit.

4. Unity-chan doesn't get tired if you slide. 

Priority - Low. Easily fixable, but it's just a cute animation. No stamina or anything.

5. AI instantly returns to their actions if the player hides behind objects

Priority - Medium. It's on my radar, but I need to add the attacking state first.

-------------------------------------------------------------------------------------------------------------

NOT Issues:

1. Player cannot control the direction of their dash mid-animation.
This is not an issue - it's incredibly weird to be able to turn during a slide.
You committed to the animation.

2. White walls in the play zone.
This is not an issue - using them as placeholders to test FOV.

3. Camera may snap when the game spawns.
This is not an issue - this happens sometimes because of the way my scene resets.


-------------------------------------------------------------------------------------------------------------
Upcoming Updates:

1. Two new enemy classes - Melee Range
2. Support for new Enemy AI behaviors
3. Basic attacks for enemies and players
4. Death. Yes, Unity-chan is probably going to die a bunch until I can figure out attack durations.
5. Abilities. For both the player and enemy. Grenades would be a good start.
6. Player selection - this way I can keep Unity-chan, but also bring a model with proper animations in.
7. Combo attacks. I have the logic setup for the player already. But delaying the enemy will be a challenge.
8. Improved FOV/LOS. Sadly, I don't know how to make player indicators... any suggestions welcome.
9. More fleshed out environment. Currently barren.
10. More player mobility options (e.g. Jump, Climb(?), Walk).
11. Improve and add additional options.


-------------------------------------------------------------------------------------------------------------
Possibly Updates
1. Climbing(!?). I've learned from a tutorial how to do this but I don't have the animations for it.
2. My own created animations(!?) - really unlikely at this time. It's becoming more busy.
Additionally, it's incredibly hard. I spent the greater part of a day just trying to make a walking animation.
Needs way more learning time.
3. AI BehaviorTree migration. Unlikely at this time until I can complete more core features.
4. Climbing(!?). I've learned from a tutorial how to do this but I don't have the animations for it.
I may juryrig it using poses but it won't look pretty.
5. Enemy strafing! I have no idea how to do this one - but I do know this requires a lot of circles and vectors...

