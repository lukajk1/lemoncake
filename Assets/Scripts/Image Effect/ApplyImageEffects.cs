using UnityEngine;

[ExecuteInEditMode]
public class ApplyImageEffects : MonoBehaviour
{
    [SerializeField] private Material pixellateMaterial;
    [SerializeField] private Material fogMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture temp = RenderTexture.GetTemporary(source.width, source.height);

        Graphics.Blit(source, temp, fogMaterial);

        Graphics.Blit(temp, destination, pixellateMaterial);

        RenderTexture.ReleaseTemporary(temp);
    }

}
