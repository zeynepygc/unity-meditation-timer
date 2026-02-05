using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class WindowControls : MonoBehaviour
{
    // ----------------------------
    // Close the app
    // ----------------------------
    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in editor
#else
        Application.Quit(); // Quits the build
#endif
    }

    // ----------------------------
    // Minimize the app (Windows only)
    // ----------------------------
    public void MinimizeGame()
    {
#if UNITY_STANDALONE_WIN
        var windowHandle = GetActiveWindow();
        ShowWindow(windowHandle, 2); // 2 = Minimize
#endif
    }

#if UNITY_STANDALONE_WIN
    // Windows API imports for minimizing
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
#endif
}
