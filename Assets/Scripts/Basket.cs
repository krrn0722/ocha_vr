// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class Basket : MonoBehaviour
// {
//     [SerializeField] GameObject basket; // 籠のオブジェクト
//     [SerializeField] int num_sheet = 0; // 現在のスコア
//     [SerializeField] private GameObject[] cage_leafs; //籠に入れるお茶の葉
//     private int current_cage_leaf_index = 0; //籠に入れるお茶の葉のインデックス

//     private GameObject obj;
//     private Rigidbody rb;

//     private List<Collider> waitKinematicColliderList = new List<Collider>();

//     int findedIndex;

//     private void Start(){
//         foreach(GameObject cage_leaf in cage_leafs){
//             if(cage_leaf != null){
//                 cage_leaf.SetActive(false);
//             }
//         }
//     }

    
//      // お茶の葉が籠に入ったとき
//     private void OnTriggerEnter(Collider other)
//     {
//         obj = other.gameObject;
//         if (obj.CompareTag("Touched"))
//         {
            
//             //rb = obj.GetComponent<Rigidbody>();
            
//             Debug.Log(other.gameObject.name + "がトリガーに入りました。");  
//             // waitKinematicColliderList.Add(other);
//             // obj.tag = "WaitKinematicLeaf";
//             // StartCoroutine(DelayCoroutine(0.4f, () => {
                
//             //     // WaitLineamaticLeafのタグがついていれば
//             //     if(obj.CompareTag("WaitKinematicLeaf"))
//             //     {
//             //         // キネマティックにして籠に入れる
//             //         obj.transform.parent = basket.transform;
//             //         other.enabled = false;
//             //         rb.isKinematic = true;
//             //         obj.tag = "NotMove";
//             //         num_sheet += 1;
//             //         Debug.Log("Tea Leaf entered! Score: " + num_sheet);
//             //     }
//             // }));

//             Destroy(obj);

//             num_sheet += 1;

//             if(current_cage_leaf_index < cage_leafs.Length){
//                 GameObject objToShow = cage_leafs[current_cage_leaf_index];
//                 if(objToShow != null){
//                     objToShow.SetActive(true);
//                     current_cage_leaf_index++; //次のオブジェクトに進む
//                 }
//             }

//         }
//     }

//     // private IEnumerator DelayCoroutine(float seconds, UnityAction callback)
//     // {
//     //     yield return new WaitForSeconds(seconds);
//     //     callback?.Invoke();
//     // }

//     // お茶の葉が籠から出たとき
//     // private void OnTriggerExit(Collider other)
//     // {
//     //     var obj = other.gameObject;
//     //     if (obj.CompareTag("WaitKinematicLeaf"))
//     //     {
            
//     //         other.gameObject.tag = "Touched";

//     //         Debug.Log(other.gameObject.name + "がトリガーから出ました。");  
//     //         Debug.Log("Tea Leaf exited! Score: " + num_sheet);
//     //     }
//     // }

//     //スコアを計算すべき
//     public int GetScore()
//     {
//         return num_sheet;
//     }

    
// }
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Basket : MonoBehaviour
{
    [SerializeField] int num_sheet = 0; // 現在のスコア
    [SerializeField] private GameObject[] cage_leafs; //籠に入れるお茶の葉
    private int current_cage_leaf_index = 0; //籠に入れるお茶の葉のインデックス

    private GameObject obj;

    private void Start()
    {
        // ゲーム開始時に全てのオブジェクトを非表示にする
        foreach(GameObject cage_leaf in cage_leafs)
        {
            if(cage_leaf != null)
            {
                cage_leaf.SetActive(false);
            }
        }
    }

    // お茶の葉が籠に入ったとき
    private void OnTriggerEnter(Collider other)
    {
        obj = other.gameObject;
        if (obj.CompareTag("Touched"))
        {
            Debug.Log(other.gameObject.name + "がトリガーに入りました。");

            // 籠にお茶の葉を入れて、スコアを更新
            num_sheet += 1;

            // 次の非表示の葉を表示
            if (current_cage_leaf_index < cage_leafs.Length)
            {
                GameObject objToShow = cage_leafs[current_cage_leaf_index];
                if (objToShow != null)
                {
                    Debug.Log("オブジェクトが現れるはず");
                    objToShow.SetActive(true); // オブジェクトを表示
                    objToShow.GetComponent<Collider>().enabled = false;
                    objToShow.GetComponent<Rigidbody>().isKinematic = true;
                    current_cage_leaf_index++; // 次のオブジェクトに進む
                }
            }

            Destroy(obj); // お茶の葉を削除
        }
    }

    // お茶の葉が籠から出たとき
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NotMove"))
        {
            Debug.Log(other.gameObject.name + "がトリガーから出ました。");

            other.tag = "Touched";
            
            // 籠から出たお茶の葉を非表示にしてスコアを減らす
            num_sheet -= 1;

            // スコア更新後の処理など
            Debug.Log("Tea Leaf exited! Score: " + num_sheet);
        }
    }

    // スコアを取得
    public int GetScore()
    {
        return num_sheet;
    }
}
