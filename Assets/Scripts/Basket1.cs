using System.Linq; // これを追加

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Basket1 : MonoBehaviour
{
    [SerializeField] int num_sheet = 0; // 現在のスコア
    [SerializeField] public GameObject[] cage_leafs; //籠に入れるお茶の葉
    public List<GameObject> leaf_true; // リスト

    private int current_cage_leaf_index = 0; //籠に入れるお茶の葉のインデックス

    private Basket1 basketScript;  // Basket1のインスタンスを宣言

    [SerializeField] private GameObject basket; // 籠オブジェクトを格納する変数
    [SerializeField] private int totalLeafNum; // 皿オブジェクトを格納する変数
    [SerializeField] AudioManager audiomanager;

    // Start is called before the first frame update
    private void Start()
    {
        // インスタンス化
        basketScript = GetComponent<Basket1>();  // Basket1スクリプトを取得
    }

    // お茶の葉が籠に入ったとき
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.CompareTag("Touched"))
        {
            Debug.Log(obj.name + "がトリガーに入りました。");

            // 籠にお茶の葉を入れて、スコアを更新
            num_sheet += 1;

            audiomanager.playYattaSound();
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
                    objToShow.tag = "NotMove";
                    // 重複を防ぐため、リストに追加する前に含まれていないか確認
                    if (!leaf_true.Contains(objToShow))
                    {
                        leaf_true.Add(objToShow);
                    }
                    current_cage_leaf_index++; // 次のオブジェクトに進む
                }
            }

            Destroy(obj); // お茶の葉を削除
        }

        if (obj.CompareTag("Ochita"))
        {
            Debug.Log(obj.name + "がトリガーに入りました。");


            // obj を basket の子オブジェクトにする
            //obj.transform.parent = basket.transform;

            // 籠にお茶の葉を入れて、スコアを更新
            num_sheet += 1;

            // 次の非表示の葉を表示
            // if (current_cage_leaf_index < cage_leafs.Length)
            // {
            //     Debug.Log("オブジェクトが現れるはず");
            //     obj.SetActive(true); // オブジェクトを表示
            //     obj.GetComponent<Collider>().enabled = false;
            //     obj.GetComponent<Rigidbody>().isKinematic = true;
            //     obj.tag = "NotMove";
            //     leaf_true.Add(obj);
            // }
            obj.SetActive(false);
            num_sheet = 0; // お茶の葉を削除
        }

        // if (obj.CompareTag("Ochita"))
        // {
        //     // 2秒遅延させて追加するコルーチンを開始
        //     StartCoroutine(DelayedAddLeaf(obj));
        // }
    }

    // お茶の葉が籠から出たとき
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NotMove"))
        {
            Debug.Log(other.gameObject.name + "がトリガーから出ました。");

            // 1秒後にタグを変更
            //StartCoroutine(ChangeTagWithDelay(other, 1f));
            
            // 籠から出たお茶の葉を非表示にしてスコアを減らす
            num_sheet -= 1;

            // スコア更新後の処理など
            Debug.Log("お茶が落ちた！！! Score: " + num_sheet);
        }
    }

    // スコアを取得
    public int GetScore()
    {
        return (int)num_sheet * 100/ totalLeafNum ;
    }


    // private IEnumerator ChangeTagWithDelay(Collider other, float delay)
    // {
    //     yield return new WaitForSeconds(delay); // 指定した時間待機
    //     other.tag = "Ochita"; // タグを変更
    // }


    // 2秒遅延させて葉を追加するコルーチン
    // private IEnumerator DelayedAddLeaf(GameObject obj)
    // {
    //     // 2秒待つ
    //     yield return new WaitForSeconds(2f);

    //     Debug.Log(obj.name + "がトリガーに入りました。");

    //     // 籠にお茶の葉を入れて、スコアを更新
    //     num_sheet += 1;

    //     // 次の非表示の葉を表示
    //     if (current_cage_leaf_index < cage_leafs.Length)
    //     {
    //         Debug.Log("オブジェクトが現れるはず");
    //         obj.SetActive(true); // オブジェクトを表示
    //         obj.GetComponent<Collider>().enabled = false;
    //         obj.GetComponent<Rigidbody>().isKinematic = true;
    //         obj.tag = "NotMove";
    //         leaf_true.Add(obj);
    //     }
    // }

    public int getNumLeaf()
    {
        return num_sheet;
    }
}