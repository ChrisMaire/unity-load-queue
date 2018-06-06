# unity-load-queue

This is a test implementation of a Unity level loading system that queues start/awake calls, to reduce hitching while transitioning between levels.

How it works:
- The level loader starts:
  - unloads the current scene
  - loads a loading scene
  - starts loading the new scene.
- As the new scene loads, instead of running their start/awake calls, classes that inherit from Fun_Monobehaviour add themselves to lists on Fun_MonoBehaviourInitializer 
- When the level is fully loaded, it calls Run on the initializer, and starts running through all the now queued start calls.
- The Awake/Start coroutines are now spread across frames so animations continue to play.
- When all the Awake/Start calls are done, we advance, unload the loading screen, and fade in.

I don't really like the use of StartInit and AwakeInit; I'd like this system to be as invisible as possible.
