using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject bombObj;
    [SerializeField] private GameObject bomb_center;
    [SerializeField] private GameObject dishObj;
    private GameObject leafObj;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private AudioManager audiomanager;
    [SerializeField] private GameObject explosionFrefab;
    private Vector3 flyVec;

    private IEnumerator DelayCoroutine(float seconds, UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    public void Kobayashibomp()
    {  
        Debug.Log("関数ないです Bomb!");
        // 音鳴らす
        // StartCoroutine(
        //     DelayCoroutine(3f, () => {
        //     //範囲内に皿があったら吹っ飛ばす
        //     if(Vector3.Distance(bomb.transform.position, dish.transform.position) < distance)
        //     {
        //         var childCount = dish.childCount;
        //         for (int i = 0; i < childCount; i++)
        //         {
        //             dishObj = dish.GetChild(i).gameObject;
        //             dishObj.GetComponent<Rigidbody>().AddForce(Vector3.up * speed);

        //         }
        //     }
        //     //範囲内にあるお茶の葉をふっとばす
        //     });        
        // );

        StartCoroutine(DelayCoroutine(2f, () => {
            Debug.Log("ぶっとばす！！！！");
            //エフェクトだす
            Vector3 explosionPosition = bomb_center.transform.position;
            GameObject explosion = Instantiate(explosionFrefab, explosionPosition, Quaternion.identity);
            Destroy(explosion, 1.0f);
            audiomanager.playExplodeSound();
            //範囲内に皿があったら吹っ飛ばす
            if(Vector3.Distance(bombObj.transform.position, dishObj.transform.position) < distance)
            {
                Debug.Log("範囲内！！！！");
                var childCount = dishObj.GetComponent<Transform>().childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Debug.Log(i +"おちる！！！！");
                    leafObj = dishObj.GetComponent<Transform>().GetChild(i).gameObject;
                    leafObj.GetComponent<Collider>().enabled = true;
                    leafObj.GetComponent<Rigidbody>().isKinematic = false;
 
                    flyVec =  new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(0.5f,1.0f),UnityEngine.Random.Range(-1.0f, 1.0f));
                    leafObj.GetComponent<Rigidbody>().AddForce(flyVec * speed);
                }
            }
            //範囲内にあるお茶の葉をふっとばす

            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("NotTouch");
            foreach(GameObject obs in obstacles) {
                if(Vector3.Distance(bombObj.transform.position, obs.transform.position) < distance)
                {
                    obs.GetComponent<Collider>().enabled = true;
                    obs.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }));                

        // Debug.Log("Explode!");
        
    }
    

}
