using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject Object;
    public Material material;
    private float timer;
    public float beat = (60 / 90);
    Renderer rend;
    Color currentColor;
    Color originalColor;
    private float beatCount;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.8 * beat || timer < 0.1f)
         {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rend.material.color = Color.green;
            }
         }
       if(timer>beat)
        {
            if(rend.material.color == originalColor)
            {
                rend.material.color = Color.red;
                currentColor = rend.material.color;
            }
            else
            {
                rend.material.color = Color.blue;
                originalColor = rend.material.color;
            }
            timer -= beat;
            beatCount = beatCount +1;
        }
        timer += Time.deltaTime;
    }
}
