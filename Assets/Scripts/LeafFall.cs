using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafFall : MonoBehaviour
{
    [SerializeField] private Transform dish;
    [SerializeField] private GameObject dishObj;
    void Start()
    {
        
    }

    void Update()
    {
        // zのばあいんも　
        //  0 < 230, 310 < 360 
        if(180 < dish.eulerAngles.x && dish.eulerAngles.x < 220 || 320 < dish.eulerAngles.x && dish.eulerAngles.x < 360)
        {

            Debug.Log("おちる");
        }
        // if()

        // 0 < a <180 全部おちる
        else if(0 < dish.eulerAngles.x && dish.eulerAngles.x < 180)
        {
            var childCount = dish.childCount;
            for (int i = 0; i < childCount; i++)
            {
                dish.GetChild(i).parent = null;
                dishObj = dish.GetChild(i).gameObject;

                dishObj.GetComponent<Collider>().enabled = true;
                dishObj.GetComponent<Rigidbody>().isKinematic = false;
            }

            // num_sheet = 0;
        }
    }
}
