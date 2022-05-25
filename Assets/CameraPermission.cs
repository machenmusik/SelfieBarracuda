using System.Collections;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class CameraPermission : MonoBehaviour
{
    void findWebCams()
    {
        foreach (var device in WebCamTexture.devices)
        {
            Debug.Log("Name: " + device.name);
        }
    }

    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("webcam found");

            if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
                Permission.RequestUserPermission(Permission.Camera);
            }

            findWebCams();

            foreach (Transform child in transform) { child.gameObject.SetActive(true); }
        }
        else
        {
            Debug.Log("webcam not found");
        }
    }
}
