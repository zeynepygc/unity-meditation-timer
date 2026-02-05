using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections;
using UnityEngine.EventSystems;

public class WindowManager : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    const int GWL_STYLE = -16;
    const int WS_BORDER = 0x00800000;
    const int WS_DLGFRAME = 0x00400000;
    const int WS_CAPTION = 0x00C00000;
    const uint SWP_SHOWWINDOW = 0x0040;
    const int WM_NCLBUTTONDOWN = 0xA1;
    const int HTCAPTION = 0x2;

    IntPtr windowHandle = IntPtr.Zero;

    [Tooltip("Width of the app window in pixels (client area).")]
    public int targetWidth = 310;
    [Tooltip("Height of the app window in pixels (client area).")]
    public int targetHeight = 400;

    void Awake()
    {
        // Force Unity's rendering resolution to the target size.
        // This makes Screen.width/height equal the window client size.
        Screen.SetResolution(targetWidth, targetHeight, false);
    }

    void Start()
    {
        StartCoroutine(ApplyFramelessCentered());
    }

    IEnumerator ApplyFramelessCentered()
    {
        // Wait a short time so the OS/Unity window is fully created.
        yield return new WaitForSeconds(0.15f);

        windowHandle = GetActiveWindow();
        if (windowHandle == IntPtr.Zero)
        {
            Debug.LogWarning("WindowManager: Could not get active window handle.");
            yield break;
        }

        // Remove caption/frame/borders
        int style = GetWindowLong(windowHandle, GWL_STYLE);
        style &= ~WS_BORDER;
        style &= ~WS_DLGFRAME;
        style &= ~WS_CAPTION;
        SetWindowLong(windowHandle, GWL_STYLE, style);

        // Center on the primary display
        int screenW = Display.main.systemWidth;
        int screenH = Display.main.systemHeight;
        int posX = (screenW - targetWidth) / 2;
        int posY = (screenH - targetHeight) / 2;

        SetWindowPos(windowHandle, IntPtr.Zero, posX, posY, targetWidth, targetHeight, SWP_SHOWWINDOW);
    }

    // Public helper you can call from UI (EventTrigger PointerDown) to start dragging
    public void BeginWindowDrag()
    {
        if (windowHandle == IntPtr.Zero) windowHandle = GetActiveWindow();
        if (windowHandle == IntPtr.Zero) return;

        ReleaseCapture();
        SendMessage(windowHandle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
    }

    // For convenience: if you want to drag by clicking anywhere not on a UI element,
    // uncomment the Update code below and ensure EventSystem exists.
    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            BeginWindowDrag();
        }
    }
    */
#endif
}

