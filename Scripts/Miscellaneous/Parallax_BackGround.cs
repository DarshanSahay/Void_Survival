using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_BackGround : MonoBehaviour
{
    public Material bgMat;
    public MeshRenderer _mRenderer;
    Vector2 matOffset;

    private void Start()
    {
        _mRenderer = GetComponent<MeshRenderer>();
        bgMat = _mRenderer.material;
        matOffset = bgMat.mainTextureOffset;
    }

    // Update is called once per frame
    private void Update()
    {
        matOffset.y += Time.deltaTime / 10f;
        bgMat.mainTextureOffset = matOffset;
    }
}
