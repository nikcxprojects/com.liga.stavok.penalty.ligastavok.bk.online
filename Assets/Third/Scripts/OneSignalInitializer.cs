using OneSignalSDK;
using UnityEngine;

public class OneSignalInitializer : MonoBehaviour
{
    private void Start() => OneSignal.Default.Initialize("65611dab-3c06-4b40-b8ac-dfa054ce5494");
}
