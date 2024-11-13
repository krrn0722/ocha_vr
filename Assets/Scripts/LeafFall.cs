// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class LeafFall : MonoBehaviour
// {
//     [SerializeField] private Transform dish;
//     [SerializeField] private GameObject dishObj;
//     float coolTime = 0;
//     void Start()
//     {
        
//     }

//     void Update()
//     {
//         // zのばあいんも　
//         //  0 < 230, 310 < 360 
//         if(180 < dish.eulerAngles.x && dish.eulerAngles.x < 220 || 320 < dish.eulerAngles.x && dish.eulerAngles.x < 360)
//         {

//             // Debug.Log("おちる");
//         }
//         // if()

//         // 0 < a <180 全部おちる
//         else if(0 < dish.eulerAngles.x && dish.eulerAngles.x < 180)
//         {
//             if(coolTime > 3)
//             {
//                 var childCount = dish.childCount;
//                 if(childCount > 0)
//                 {
//                     Debug.Log(childCount+ "マイおちる");
//                     for (int i = 0; i < childCount; i++)
//                     {
//                         dishObj = dish.GetChild(i).gameObject;
//                         if(dishObj.tag == "NotMove")
//                         {
//                             dish.GetChild(i).parent = null;
                            

//                             dishObj.GetComponent<Collider>().enabled = true;
//                             dishObj.GetComponent<Rigidbody>().isKinematic = false;
//                         }

//                     }
//                 }
//                 coolTime = 0;
//             }

//             // num_sheet = 0;
//         }
//         coolTime += Time.deltaTime;
//     }
// }
