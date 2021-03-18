# Backlog

x ART: enemy-alpha sprite with layers
x ART: medium mob with animation
x ART: small mob with animation
x ART: enemy-delta with layers
x ART: enemy-echo with layers
x ART: UFO with animation
x ART: laser cannon with layers
x ART: missile and bomb
x CODE: Enemy setup and locomotion
x CODE: Player locomotion
x CODE: Fire missiles destroy enemies
x CODE: Drop bombs from enemies
  - CODE: Bombs destroy player
x CODE: Shields provide cover
  - Collisions with bombs destroy part of the shield
  - Collisions with missiles destroy part of the shield
  - Collisions with mobs destroy part of the shield
x CODE: Implement Game Over conditions
  * Game Over when player is out of lives
  * Game Over when mob reaches bottom or collides with player
  * Play again after Game Over
x ART: Animations for enemy players
x CODE: Implement waves and grid and enemy reset
x CODE: Enemies increase speed when there are fewer.
  - Probably step wise
  - Maybe with 3 levels. Normal, Medium, and 1-Left.
x CODE: Implement scoring
X SOUND: Background music
  -  matches enemy speed
* SOUND: SFX Missile firing and explosions
* SOUND: SFX Bomb dropping and explosions
* SOUND: Game over sound/music
* ART: Particle for enemy explosions
* ART: Particle for shield explosions
* ART: Particle for player explosion
* ART: Particle for missed bomb explosion
* ART: Redesign main menu
  - play background music as well
* INFRA: Build for WebGL and release
---
* ART: Background for the game play screen
* CODE: UFO spawn and locomotion
* CODE: UFO drops bombs
* SOUND: UFO sound
* CODE: Player 1up
  - SOUND: SFX for 1up
* CODE: Delay respawn if bomb is about to hit player
* ART: Cause animations to start at different times
* CODE: Damaged shield tiles and border tiles (rule tiles)
---
* CODE: Enemy on the edge has a 50/50 chance to drop bomb on change of direction.
* CODE: Enter wave restores some shields
