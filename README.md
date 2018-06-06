# unity-load-queue

This is a test implementation of a Unity level loading system that queues start/awake calls, to reduce hitching while transitioning between levels.

How it works:
- The level loader starts:
  - unloads the current scene
  - loads a loading scene
  - starts loading the new scene.
- As the new scene loads asyncronously:
  - classes that inherit from Fun_Monobehaviour put their startup logic that would normally go in Start/Awake into StartInit and AwakeInit (if you have suggestions for better naming please...lemme know).
  -Fun_Monobehaviour queues those init calls up in Fun_MonoBehaviourInitializer.
- When the level is fully loaded, it calls Run on the initializer, and starts running through all the now queued awake/start calls.
- The now-queued AwakeInit and StartInit calls are spread across frames so animations continue to play.
- When all the AwakeInit and StartInit calls are done, we advance, unload the loading screen, and fade in.
