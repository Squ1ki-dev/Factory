Notes:
It is recommended to use the folllowing options:
	Edit > Project Settings > Time > Fixed Timestep = 0.015
	Edit > Project Settings > Physics > Min Penetration For Penalty = 0.01
	Edit > Project Settings > Physics > Solver Iteration Count = 75

Some users may have to manually add each scene to the build settings window for it to work properly.

Every script contains documentation for any notable instruction written. If you have any problems, feedback or requests contact me at: aquageneral@hotmail.com

Changelog:
v1.1
- Improved documentation and functionality of; “Menu.js” and “GlobalLogic.js”
- Fixed the sizing of the preview images in the Level Select screen
- Added a new level “06_LargeCastleWall”
- Levels have been slightly rebalanced

v1.1.2
- Rebalanced every level
- Fixed a bug that caused the level tips from not being displayed

v1.1.2.2
- Updated readme.txt

v1.5.0
- HUD is now tab aligned
- Automatic projectile labelling (no need to manually label the name of the projectile)
- Massive changes to levels
- Several new levels,
- Bomb Projectile
- Player can control camera zoom
- Simple destructible boxes have been added
- Level Tips can now be multi-line
- Victory Condition screen now appears only if all objects are stationary (and times-out after waiting too long)
- Added a camera prefab

v1.6.0
- Added sounds
- Added two new levels
- Major rebalancing work has been done
- Ball is now fired in the exact position of the mouse

v1.7.0
- Minor improvements to the direction the ball is shot in 
- Main block is now 25% lighter. Level 4 "Topple Trio" is now a bit easier in return.

v1.7.1
- Upgraded project to Unity v4.3.2
- Created an optimized block and sphere for mobile devices
- Created scenes "11_BonusMobileRuins" and "12_BonusMobileTroph" to show off mobile device optimizations
- Made MainMenuBlockMelt inherit from BlockMelt
- Imporvements to Main Menu animation behaviour
- Optimized the main menu block animation. It recycles blocks instead of spawning new ones every time
- Lots of other minor improvements to scripts and documentation

v2.0.0
- Ported project to C#
- Ported project to Unity 5
- Tweaked things to make things work like they did in PhysX 2.x (PhysX 3.x is so much faster and more stable, it's win-win).
- Project now uses Unity 5 lighting and materials
- Tweaks to rigidbody weights
- Improvements to code throughout the project
- Code simplification - unnecessary classes merged and/or removed
- Removed secondary directional light from all scenes