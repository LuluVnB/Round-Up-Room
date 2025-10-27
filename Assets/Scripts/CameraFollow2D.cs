using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow2D : MonoBehaviour {
    [Header("Target")]
    public Transform target;

    [Header("Follow")]
    public float smoothTime = 0.15f;
    public Vector3 offset = new Vector3(0f, 0f, -10f); // keep camera behind on Z

    [Header("World Bounds")]
    [Tooltip("Any 2D collider that encloses your playable map (BoxCollider2D, PolygonCollider2D, etc.).")]
    public Collider2D worldBounds; // recommended: BoxCollider2D sized to your level

    private Camera _cam;
    private Vector3 _velocity = Vector3.zero;

    void Awake() {
        _cam = GetComponent<Camera>();
        _cam.orthographic = true; // 2D camera
        if (offset.z == 0f) offset.z = -10f; // ensure we sit behind the scene
    }

    void LateUpdate() {
        if (target == null) return;

        // Desired position before clamping
        Vector3 desired = target.position + offset;

        if (worldBounds != null) {
            // Camera half extents in world units
            float halfHeight = _cam.orthographicSize;
            float halfWidth = halfHeight * _cam.aspect;

            Bounds b = worldBounds.bounds;

            // Expand min by +extents and max by -extents so camera edges never cross the collider bounds
            float minX = b.min.x + halfWidth;
            float maxX = b.max.x - halfWidth;
            float minY = b.min.y + halfHeight;
            float maxY = b.max.y - halfHeight;

            // If the bounds are smaller than the camera view, lock to center on that axis
            if (minX > maxX) {
                float cx = (b.min.x + b.max.x) * 0.5f;
                minX = maxX = cx;
            }
            if (minY > maxY) {
                float cy = (b.min.y + b.max.y) * 0.5f;
                minY = maxY = cy;
            }

            desired.x = Mathf.Clamp(desired.x, minX, maxX);
            desired.y = Mathf.Clamp(desired.y, minY, maxY);
            // desired.z comes from offset (don’t clamp Z)
        }

        // Smoothly move camera
        Vector3 smoothed = Vector3.SmoothDamp(transform.position, desired, ref _velocity, smoothTime);
        transform.position = smoothed;
    }

#if UNITY_EDITOR
    // Optional: visualize current camera rect at runtime when selected
    void OnDrawGizmosSelected() {
        if (_cam == null || worldBounds == null) return;
        float halfHeight = _cam.orthographicSize;
        float halfWidth = halfHeight * _cam.aspect;
        Vector3 p = Application.isPlaying ? transform.position : (target ? target.position + offset : transform.position);
        Gizmos.color = new Color(0, 1, 1, 0.25f);
        Gizmos.DrawWireCube(new Vector3(p.x, p.y, 0f), new Vector3(halfWidth * 2f, halfHeight * 2f, 0f));
    }
#endif
}
