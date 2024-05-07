using UnityEngine;

public static class SocialShareHelper
{
    public static void ShareText(string text, string subject)
    {
        // Crea el mensaje de compartir
        string shareMessage = text;

        // Llama a la función de uso compartido de acuerdo a la plataforma
        #if UNITY_ANDROID
        NativeShare(shareMessage, subject);
        #elif UNITY_IOS
        NativeShareiOS(shareMessage, subject);
        #endif
    }

    static void NativeShare(string shareMessage, string subject)
    {
        // Crea un intent para compartir en Android
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

        // Inicia la actividad de uso compartido
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Compartir mensaje");
        currentActivity.Call("startActivity", chooser);
    }

    static void NativeShareiOS(string shareMessage, string subject)
    {
        // Llama a la función de uso compartido de iOS
        // Esta función debe ser implementada específicamente para iOS
    }
}
