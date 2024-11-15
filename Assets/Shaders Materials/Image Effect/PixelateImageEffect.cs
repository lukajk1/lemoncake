using UnityEngine;

[ExecuteInEditMode]
public class PixelateImageEffect : MonoBehaviour
{
    [SerializeField] private Material pixellateMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, pixellateMaterial);
    }
}
