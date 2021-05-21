# sensoricFramework

## How to use
This Framework uses a sender-receiver pattern to trigger a sensoric event.
Drag and drop [sensoricFramework.unitypackage](/sensoricFramework.unitypackage) into your Unity Project.
An example can be found here: [vrTest_sensoricFramework](https://github.com/Ultimatonium/vrTest_sensoricFramework)

### SensoricManager
Singleton Object which handles the whole communication between a trigger and the hardware.
Has to be attached to any GameObject (only once).

### SensoricReceiver
An object which defines each "body"-part. For example, the head can receive olfactory or hand thermal events.
Has to be attached to each GameObject which should trigger a sensoric event.

### SensoricSender
An abstract class that is implemented into specific ones (e.g. TactileSender, ThermalSender, OlfactorySender). Each of these senders emits a specific sensoric event. 
They can even be future specified for specific hardware features(e.g. TactileSender->BHapticsSender or OlfactorySender->CiliaSender)
Has to be attached to each GameObject which should emit a sensoric event.

### SensoricDevice
An abstract class that implements each communication with the plugin or hardware (e.g. BHapticsDevice, CiliaDevice)
Has to be attached to any GameObject (only once).

### SensoricSenderModifier
A GameObject with SensoricSender could have one or more SensoricSenderModifier on which the sensoric values may get changed on personal needs.

## Contains
* [bHaptics Haptic Plugin](https://assetstore.unity.com/packages/tools/integration/bhaptics-haptic-plugin-76647) (included in the framework)
* [CiliaUnityPlugin](https://hapticsol.com/software) (included in the framework)
* [SteamVR Plugin](https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647) (for testing only - not part of the framework)

## Supported Hardware
* [bHaptics](https://www.bhaptics.com/)
* [ThermoReal](http://thermoreal.com/)
* [Cilia](https://hapticsol.com/)

## Documentation
Documentation is generated with [Doxygen](https://www.doxygen.nl/index.html) and can be found in the folder [html](/html). Just open the [index.html](/html/index.html) in your cloned repository.

![class diagram](/sensoricFramework/Assets/sensoricFramework/sensoricFramework.jpg)
