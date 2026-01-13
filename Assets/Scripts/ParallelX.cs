using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float animationSpeed = 0.3f;
    private MeshRenderer meshRenderer;
    private Material materialInstance;
    private Vector2 offsetDelta;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialInstance = meshRenderer.material;
    }

    private void Update()
    {
        offsetDelta.x = animationSpeed * Time.deltaTime;
        offsetDelta.y = 0;
        materialInstance.mainTextureOffset += offsetDelta;
    }

    private void OnDestroy()
    {
        if (materialInstance != null)
        {
            Destroy(materialInstance);
        }
    }
}
