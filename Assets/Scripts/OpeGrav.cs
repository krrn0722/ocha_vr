using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeGrav : MonoBehaviour
{
    // public GameObject obj;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onGrav()
    {
        rb.isKinematic = false;
    }

    public void addTag()
    {
        if (this.tag == "Ochita")
        {
            this.tag = "Touched1";  // タグを "Touched1" に変更
        }
        else
        {
            this.tag = "Touched";   // それ以外は "Touched" に変更
        }
    }
}
