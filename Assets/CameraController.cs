using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float hareketHizi = 10f;

    [Header("Zoom Ayarları")]
    public float zoomHizi = 5f;
    public float minZoom = 3f;
    public float maxZoom = 15f;
    public float zoomYumusakligi = 8f;

    private Camera cam;
    private float hedefZoom;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        hedefZoom = cam.orthographicSize;
    }

    void LateUpdate()
    {
        WASDHareketi();
        ZoomHesapla();
        ZoomUygula();
    }

    private void WASDHareketi()
    {
        Vector3 hareket = Vector3.zero;

        // Yeni Input System ile tuş okuma
        if (Keyboard.current.wKey.isPressed) hareket.z += 1f;
        if (Keyboard.current.sKey.isPressed) hareket.z -= 1f;
        if (Keyboard.current.aKey.isPressed) hareket.x -= 1f;
        if (Keyboard.current.dKey.isPressed) hareket.x += 1f;

        transform.position += hareket * hareketHizi * Time.deltaTime;
    }

    private void ZoomHesapla()
    {
        // Yeni Input System ile scroll okuma
        float scrollInput = Mouse.current.scroll.ReadValue().y;

        if (scrollInput != 0f)
        {
            hedefZoom -= scrollInput * 0.01f * zoomHizi;
            hedefZoom = Mathf.Clamp(hedefZoom, minZoom, maxZoom);
        }
    }

    private void ZoomUygula()
    {
        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize,
            hedefZoom,
            Time.deltaTime * zoomYumusakligi
        );
    }
}