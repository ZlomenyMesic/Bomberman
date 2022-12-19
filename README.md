# Bomberman

A simple 2d game, remake of *Eric and the floaters*.
Everything was made in C#, using the Monogame framework.

## Gameplay

You are the Eric. Your goal is to get through as many
levels as possible and get a high score. Each level is
randomly generated. Every map is a grid consisting of 
strong and weak walls. In every level, there are two
floating balloons (floaters). These are your enemies.
They are a bit faster than you, and can kill you.
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
another level. After reaching level 3, there is a 1 in 3 chance
of generating a random upgrade (or downgrade). So far I have added
just one - wheelchair. Collecting a wheelchair will take you a
random amount of score between 160 and 1260. Your score cannot
be lower than zero.

## Key binds

W - move up  
A - move left  
S - move down  
D - move right  
Escape - close the game  
R - restart the game (not yet)
