![Splash screen](https://i.imgur.com/3un2Qq8.png)


# ShanghaiEXE Randomizer mod
Fork of Shanghai EXE (as of SHNecro's 8/22/23 commit) that faucilitates freeplay, adds new and unused content, randomization and some new features.

Randomization is on by default, but can be disabled by chaning the seed to 0 and turning off the Randomizer start in the key config tool.

## Features
BMG/PMD randomization

Optional (but recommended) Open world/postgame start

Items obtained during the course of the story are redistributed to new BMD's

Difficulty selection (in Alice's PC, the red Sprite)

Added super chip trader

Number Trader items are now spread around new BMD's

Style are now aproximently 4 times faster to aquire

Added in hidden Gauntlet fights that reward new chips

Added new Mega and Dark chips, and a few previously unobtainable chip codes

Added refights for bosses that previously lacked them (Okuu, Kikuri, etc)

Relaxed a couple of the Heaven (post game) door requirements to lower grinding in a randomizer setting

Added previously unseen bosses (Orin, Youmu, Yuyuko, Flandre, Mima)

Added new condition to get to secret final bosses (Get the 4 new navi dark chips)

Spoiler log for randomizer, if you're stuck

Capcom copyrighted stuff obliterated from orbit (If i missed anything, please report)

### Misc technical changes
Default renderer is now OpenGL, as some modern hardware has issues with DX9. DX9 can be selected from keyconfig as usual.

Default window size is 4x instead of 1x

### Future goals
Region locking/ progression blocking (ie you can't take the metro everywhere from the start)

Overworld hidden item randomization (Currently not implemented as overworld items share the same functions as progressing side quests, will need a re-write)

More readable spoiler log

Clean up the randomization process so that there's not just 2 copies of the maps sitting in the installation folder (i'm a bit stupid)

Diffrent title screen, proper implimentation of completion stars

Archapeligo (online multiworld) Support

### Maybes
Encounter randomizer? (Requires re-writing how maps and events are handled)

Grimour Style? (Referenced in in game text but no code exists, will probably be hub style)

CirnoBX and ShanghaiDS (partialy done in code, shanghai moreso)

Unique boss music?

### Known issues
I can't get the map editor to compile, so the included one is just the binary from the old version

Spoiler log lists internal names of add-ons, not in game names (most of them are romanized versions of the BN equivalents)

Story mode randomized is largely untested, HIGHLY recomend using the new start if doing randomizer

Items obtained during the course of the story are redistributed to new BMD's, so story mode is theoreticly harder

Sumireko chip hunt sidequest's Blindleaf P NPC normally distributes an unlimited amount of the chip, so it's been moved to a GMD next to where he normally spawns to avoid screwing with the randomiser

Some fire trail effects can hit enemy reguardless of team (mima and druidman, some versions of dragon)

Madman (1hp gauntlet) dosn't have a reifght, but I'm not sure if anyone would even want one

I'm not finishing up Online PVP. Don't ask.

Omake room causes crashes (It's content has been distributed out to the rest of the game anyway, so it shouldn't matter much)

### Legal

This version of the game is based off of SHNecro's continuation of the game. He dosn't maintain a public presence and seems to be MIA, so if he makes himself known I'll work with him to keep this licensed apropriately.

Due to the orgional game getting shut down, reverse engineered and then updated by an anon it's legal history is hard to track, so we're going be assume it's abandonware.

This game and code are licensed STRICTLY under a CC-NC license. You can do whatever you want with it, but if you try to make money off of it we'll kill you in real roblox.
