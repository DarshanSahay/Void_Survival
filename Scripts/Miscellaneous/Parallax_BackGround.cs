using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_BackGround : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    private void Update()
    {
        float temp = (cam.transform.position.y * (1 - parallaxEffect));
        float distance = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
        if(temp > startPos + length)
        {
            startPos += length;
        }else if(temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
