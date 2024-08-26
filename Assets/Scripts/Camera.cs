using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject cat;
    void Start()
    {
        cat = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 catPos = cat.transform.position;
        transform.position = new Vector3(transform.position.x, catPos.y, transform.position.z);
    }
}
