using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bomb1 : MonoBehaviour
{
    [SerializeField] GameObject bombObj;
    [SerializeField] private GameObject bomb_center;
    private Basket1 basketScript;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private AudioManager audiomanager;
    [SerializeField] private GameObject explosionFrefab;
    private Vector3 flyVec;
    [SerializeField] private GameObject dishObj;
    [SerializeField] private Basket1 basket1;
    [SerializeField] private createHappa createHappa;

    private void Start()
    {
        basketScript = FindObjectOfType<Basket1>(); // Basket1スクリプトのインスタンスを取得
    }

    private IEnumerator DelayCoroutine(float seconds, UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    public void Kobayashibomp()
    {  
        Debug.Log("関数ないです Bomb!");

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
                audiomanager.playGuuSound();
                Debug.Log("範囲内！！！！");
                List<GameObject> leavesCopy = new List<GameObject>(basketScript.leaf_true); // リストのコピーを作成
                foreach (GameObject leaf in leavesCopy)
                {
                    Debug.Log(leaf.name + " おちる！！！！");

                    // まずリストから削除
                    basketScript.leaf_true.Remove(leaf);

                    // leafのColliderとRigidbodyの設定を変更
                    leaf.GetComponent<Collider>().enabled = true;
                    leaf.GetComponent<Rigidbody>().isKinematic = false;

                    leaf.tag = "Ochita";

                    // basketオブジェクトの子オブジェクトから外す
                    leaf.transform.parent = null;

                    // 飛ばす方向のベクトルをランダムに設定して加える
                    // flyVec = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), UnityEngine.Random.Range(1.0f, 1.5f), UnityEngine.Random.Range(-2.0f, 2.0f));
                    // leaf.GetComponent<Rigidbody>().AddForce(flyVec * speed);
                }
            }
            for (int i = 0; i < basket1.getNumLeaf() ; i++)
            {
                createHappa.createflyHappa();
            }
            // お茶の葉の数をリセット
            //範囲内にあるお茶の葉をふっとばす

            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("NotTouch");
            foreach(GameObject obs in obstacles) {
                if(Vector3.Distance(bombObj.transform.position, obs.transform.position) < distance)
                {
                    obs.GetComponent<Collider>().enabled = true;
                    obs.GetComponent<Rigidbody>().isKinematic = false;
                    flyVec = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), UnityEngine.Random.Range(1.0f, 1.5f), UnityEngine.Random.Range(-2.0f, 2.0f));
                    obs.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2, 2), Random.Range(12,25), Random.Range(-2, 2)), ForceMode.Impulse);
                }
            }
        }));                

        // Debug.Log("Explode!");
        
    }
    

}
