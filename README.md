# Bomberman

A simple 2d game, remake of *Eric and the floaters*.
Everything was made in C# .NET Framework, using Monogame.

## Gameplay

You are the Eric. Your goal is to get through as many
levels as possible and collect score. Each level is
a randomly generated map. Every map is a grid consisting of 
strong and weak walls. In every level, there are two
floating balloons (floaters). These are your enemies.
They are 33% faster than you, and can kill you.
You can also place bombs (only one at a time),
which explode after shortly after being placed. The explosion size
is 5 x 5 blocks. The bomb can kill you, but you can also use
it to kill floaters. Killing a floater will earn you
a random amount of score between 10 and 200.
The bomb will also destroy any weak walls in it's explosion.

## Items and upgrades

If a weak wall is destroyed, there is a chance of uncovering
a hidden item. All hidden items can only generate inside weak walls.
In every map, there is a treasure chest
and an exit portal generated at the start of the game.
Collecting the treasure will earn you a random score between
160 and 1260. Entering the exit portal will teleport you to
the next level. There is also a small chance that other items will generate,
those are: Wheelchair - gives you slowness for 3 seconds and Trap - can kill you or a floater.

## Key binds

WASD - move (up, left, down, right)  
B - place a bomb  
Esc - quit the game  
R - restart the game (not yet)

## Download

You can install this game easily with my [installer](https://github.com/ZlomenyMesic/Bomberman/blob/main/BombermanSetup.msi).
The game needs an installer because it consists of many .dll files, images and audio files.
All textures and sound effects can be found [here](https://github.com/ZlomenyMesic/Bomberman/blob/main/files/content),
and the source code can be found [here](https://github.com/ZlomenyMesic/Bomberman/blob/main/files/code).
If the installer doesn't work, right click the file, go to Properties, click the Unblock button and click OK or Apply.
