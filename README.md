# unity-load-queue

This is a test implementation of a Unity level loading system that queues start/awake calls, to reduce hitching while transitioning between levels.

How it works:
- The level loader starts:
  - unloads the current scene
  - loads a loading scene
  - starts loading the new scene.
- As the new scene loads asyncronously:
  - classes that inherit from Fun_Monobehaviour have a custom yield instruction that just waits for a value to be set on them that halts their start from running
  -Fun_Monobehaviour queues those yield instructions up in Fun_MonoBehaviourInitializer.
- When the level is fully loaded, it calls Run on the initializer, and starts running through all the now queued start calls.
- The Start coroutines are now spread across frames so animations continue to play.
- When all the Start calls are done, we advance, unload the loading screen, and fade in.

Note that this has no solution for spreading out Awakes now; because Awake can't be a coroutine, there's not a nice easy solution like there was for start.
