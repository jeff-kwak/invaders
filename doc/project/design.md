# Invaders

Invaders is a Space Invaders clone written for the ["Make a Game Club"](https://itch.io/jam/make-a-game-club-01) game jam.

## Game Design

### Player Control
Player Controls a laser Cannon. The laser cannon can only move along the x-axis at the bottom of the screen. The player fires missiles, and has an unlimited number of missiles. The missiles only travel up. The fire rate should be limited. The player has 3 lives.

### Enemies
There are three primary types of enemies. They differ in size. They start the game in a grid like formation and move along the x-axis until they reach the edge. When they reach the edge they move a row towards the bottom of the screen. As the number of enemies decreases their speed increases (step wise). The final enemy moves quite fast.

### UFO
An UFO flies across the top of the screen. It's worth a lot of points.

### Bombs
The bottom row of enemies drops bombs that fall vertically down. When they strike the player the player explodes and loses a life.

### Missiles
Missiles are fired from the laser cannon. There is a rate limit. There is unlimited ammo (i.e., not tracked).

### Shields
There are three shield like buildings that absorb damage. When either an enemy, missile, or bomb collides with the shield a part of it is destroyed. The neighboring parts of the shield show damage.

#### User Interface

#### HUD
The number of lives is depicted at the bottom left of the screen under the player. The score is depicted at the top left.

#### Main Menu
There's a basic main menu with title, play, and quit.

## Graphics Needed

* Laser Cannon (the player)
* Missile
* Bombs. Squiggles with at least 2 cells of animation.
* Shield. Might be tile based with each tile having a un-damaged and a damaged state. There are probably 3 tiles: solid, 45deg right, 45deg left. Maybe this could be sprite based instead.
* Alien x 3 with at least 2 cells of animation
* UFO with at least 2 cells of animation

## Audio Requirements

* The background "music". Speed it up as the number of enemies decreases, and their rate of decent increases.
* Missile firing sound
* Bomb dropping sound
* Bomb explosion on ground sound
* Missile firing sound
* Enemy explosion sound
* Shield explosion
* Laser cannon explosion
* Extra life sound
* UFO sound


## Art Direction
Pixel art is the obvious choice. ~Instead we're going with a faux vector art style. Vibrant colors and glow effects. There will be particle effects that add juice.~

I could not execute on the faux vector art idea. Trying a cartoonish like figures.

https://graphicriver.net/item/space-invader-pack/17912020?irgwc=1&clickid=W9Z31izFcxyLUrSwUx0Mo3YgUkEQpz2duSWMXI0&iradid=275988&irpid=1257950&iradtype=ONLINE_TRACKING_LINK&irmptype=mediapartner&mp_value1=&utm_campaign=af_impact_radius_1257950&utm_medium=affiliate&utm_source=impact_radius

https://comp3interactive.itch.io/invaders-from-outerspace-full-project-asset-pack

https://cluly.itch.io/space-eaters