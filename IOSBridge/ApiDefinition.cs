using System;
using Foundation;
using ObjCRuntime;

namespace IOSBridge
{
    // Binding UnityFramework (native API)
    [BaseType(typeof(NSObject))]
    interface UnityFramework
    {
        [Static]
        [Export("GetInstance")]
        UnityFramework GetInstance();

        [Export("runEmbedded")]
        void RunEmbedded();

        [Export("showUnityWindow")]
        void ShowUnityWindow();

        [Export("sendMessageToGOWithName:methodName:message:")]
        void SendMessageToGOWithName(string goName, string methodName, string message);

    }

    [BaseType(typeof(NSObject))]
    interface Bridge
    {
        [Static]
        [Export("registerUnityContentReceiver:")]
        void RegisterUnityContentReceiver(IUnityContentReceiver receiver);
    }

    [Model]
    [Protocol]
    interface IUnityContentReceiver
    {
        [Export("onReceivedUnityContent:")]
        void OnReceivedUnityContent(string content);
    }
}
