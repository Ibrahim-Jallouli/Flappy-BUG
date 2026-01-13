using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public float speed = 5f;
    public float gap = 3f;

    private float leftEdge;
    private static Camera cachedCamera;
    private Transform cachedTransform;

    private void Awake()
    {
        cachedTransform = transform;
        if (cachedCamera == null)
            cachedCamera = Camera.main;
    }

    private void Start()
    {
        leftEdge = cachedCamera.ScreenToWorldPoint(Vector3.zero).x - 1f;
        top.position += Vector3.up * gap / 2;
        bottom.position += Vector3.down * gap / 2;
        
        GameManager.RegisterPipe(this);
    }

    private void OnDestroy()
    {
        GameManager.UnregisterPipe(this);
    }

    private void Update()
    {
        cachedTransform.position += speed * Time.deltaTime * Vector3.left;

        if (cachedTransform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }
}
