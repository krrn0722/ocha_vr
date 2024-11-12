using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Basket : MonoBehaviour
{
    [SerializeField] GameObject basket; // 籠のオブジェクト
    [SerializeField] int num_sheet = 0; // 現在のスコア

    private GameObject obj;
    private Rigidbody rb;

    private List<Collider> waitKinematicColliderList = new List<Collider>();

    int findedIndex;
     // お茶の葉が籠に入ったとき
    private void OnTriggerEnter(Collider other)
    {
        obj = other.gameObject;
        if (obj.CompareTag("Touched"))
        {
            
            rb = obj.GetComponent<Rigidbody>();
            
            Debug.Log(other.gameObject.name + "がトリガーに入りました。");  
            // waitKinematicColliderList.Add(other);
            obj.tag = "WaitKinematicLeaf";
            StartCoroutine(DelayCoroutine(0.4f, () => {
                
                // WaitLineamaticLeafのタグがついていれば
                if(obj.CompareTag("WaitKinematicLeaf"))
                {
                    // キネマティックにして籠に入れる
                    obj.transform.parent = basket.transform;
                    other.enabled = false;
                    rb.isKinematic = true;
                    obj.tag = "NotMove";
                    num_sheet += 1;
                    Debug.Log("Tea Leaf entered! Score: " + num_sheet);
                }
            }));
        }
    }
    private IEnumerator DelayCoroutine(float seconds, UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    // お茶の葉が籠から出たとき
    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        if (obj.CompareTag("WaitKinematicLeaf"))
        {
            
            other.gameObject.tag = "Touched";

            Debug.Log(other.gameObject.name + "がトリガーから出ました。");  
            Debug.Log("Tea Leaf exited! Score: " + num_sheet);
        }
    }

    //スコアを計算すべき
    public int GetScore()
    {
        return num_sheet;
    }

    
}
