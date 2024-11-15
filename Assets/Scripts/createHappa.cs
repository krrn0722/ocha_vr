using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createHappa : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform dishTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createflyHappa()
    {
        Debug.Log("お茶を生成したよ！");
        // 生成するプレハブを指定
        GameObject obj = (GameObject)Resources.Load("happasPrefab");
        // プレハブを元にインスタンスを生成
        Vector3 pos = dishTransform.position + new Vector3(0, 0.2f, 0);
        GameObject instance = Instantiate(obj, pos, Quaternion.identity);
        // 生成したインスタンスを子オブジェクトに設定
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        instance.tag = "NotTouch";
        
        rb.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(12,25), Random.Range(-2, 2)), ForceMode.Impulse);
    }
}
