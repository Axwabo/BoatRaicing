# BoatRaicing

A 3D Unity game where you race with boats on ice.

First one to the finish line wins.
After completing the map, you can spectate other bots.

Maps were made entirely out of primitive meshes (cubes, quads),
which limits the looks and physics.
Some ice is covered by wide bars, you could look at this as a stylistic choice.
I chose this path primarily to have to avoid using Blender.

This is pretty much my first "proper" physics-based Unity game.

# Controls

Press `Tab` to open the in-game menu.

The ice is slippery, so it's not the easiest to control the boat.
Try out the `Tutorial` in the main menu to get a feel for it.

`W/S` to go forwards/backwards.
This will apply force in the boat's facing direction.

`A/D` to turn left or right.

Rowing in the opposite direction (e.g. backwards when going forwards)
leads to slower deceleration than if you were to accelerate.

There is a terminal velocity that you can't exceed.

Move the mouse to look around, though you can't do full 360 rotations.
The camera's rotation doesn't affect the boat.

`Left Click` to change to the next boat when you're spectating.

# Known Issues

Due to floating-point limitations (or simply just because Unity is stupid),
sometimes you'll get stuck on seemingly nothing.
This is partially because colliders are box colliders, and they aren't 100%
seamlessly placed next ot each other. At the finish line, you might
even fly upwards if you hit some magical angle (quad MeshCollider moment).

The bots aren't exactly smart, they (try to) move along pre-defined target points.
Maybe treat this as if you were super skillful :3

The music can pop a little when it loops, I couldn't do much about it :(

Bouncing off the wall when going sideways applies torque in the wrong direction.
Yet again, I blame this wonderful game engine (it's actually not my fault).

Touchscreens aren't supported yet.

# Assets Used

Sound effects were recorded by me. I made the music as well.

Check out the `FLP` directory for some `FL Studio` projects.
I've been using the trial version for years, so you'll need an unlocked
FL installation to open the projects.

External assets:

- [Ice Shader Graph](https://www.youtube.com/watch?v=Gym5JWHgjkk)
- [Prototype Materials](https://assetstore.unity.com/packages/2d/textures-materials/gridbox-prototype-materials-129127)
- [Merry Sister](https://www.youtube.com/watch?v=z5b5Yp83YY0)

The rest of the assets are provided by Unity.
