# Pug G

## Goals for 4/23
***Programming***

- Bath minigame implementation [in progress by Vincent]
- Finish design of levels 2 and 3 [in progress]
- implement the real art [in progress by Zac]

***Art***
- Various polish
- (Bath minigame music)
- (Level complete jingle)

*( ) means optional goal, may become mandatory for next meeting*


## Bugs

***A***

- Hitting a soap bar in the bath minigame will cause the player's sprite to vanish permanently and softlock the player

***B***

- The timer runs down and can restart you in the Level Complete screen
- The time remaining displayed on the Level Complete screen is not always correct (just saw 7:29)
- Enemies' ground check colliders act erratically unless they're placed directly under platform colliders as in Level 1 (extremely weird)
- Enemies can be scared off of the platforms they stand on (rare)
- Bath should send you back to daytime, not to the level
- Powerup uses no longer decrement past 1 (player can use them infinitely)
- Continue button on level complete screen should take the player back to the daytime level

***C***

- Level complete screen flashes at the end of its entry animation
- Meatloaf's run animation plays when you're running into a wall
- If you collide with a platform (but do not land on top of it) at the peak of your jump, your double jump will be reset
- Bath minigame should be zoomed in more, and shouldn't take as long to complete (make it timer based)
- Continue button on level complete screen should animate in after everything else

*About bug ranks*
- *A rank: Causes the game to become completely unplayable*
- *B rank: Negatively affects gameplay*
- *C rank: Visual error, doesn't affect gameplay at all*

## Completed goals

- camera tracking
- basic player movement
- player sprites
- enemy sprites
- other sprites
- Song 1
- audio manager
- score increasing
- title screen
- HP bar
- Enemy AI & mechanics
- platform mechanics
- timer
- checkpoints
- Title theme
- Daytime theme
- powerups
- level 1
- shop
