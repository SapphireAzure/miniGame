using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMask : MonoBehaviour
{
    public Material LOSMaskMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, LOSMaskMaterial);
    }
}
