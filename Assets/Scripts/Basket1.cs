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

    // Start is called before the first frame update
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
                    leaf_true.Add(objToShow);
                    current_cage_leaf_index++; // 次のオブジェクトに進む
                }
            }

            Destroy(obj); // お茶の葉を削除
        }

        if (obj.CompareTag("Touched1") || obj.CompareTag("Ochita"))
        {
            Debug.Log(obj.name + "がトリガーに入りました。");

            // 籠にお茶の葉を入れて、スコアを更新
            num_sheet += 1;

            // 次の非表示の葉を表示
            if (current_cage_leaf_index < cage_leafs.Length)
            {
                Debug.Log("オブジェクトが現れるはず");
                obj.SetActive(true); // オブジェクトを表示
                obj.GetComponent<Collider>().enabled = false;
                obj.GetComponent<Rigidbody>().isKinematic = true;
                obj.tag = "NotMove";
                leaf_true.Add(obj);
            }
        }
    }

    // お茶の葉が籠から出たとき
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NotMove"))
        {
            Debug.Log(other.gameObject.name + "がトリガーから出ました。");

            // 1秒後にタグを変更
            StartCoroutine(ChangeTagWithDelay(other, 1f));

            // リストから出たオブジェクトを削除
            if (basketScript.leaf_true.Contains(other.gameObject))
            {
                // LINQのWhereメソッドを使ってオブジェクトを削除
                basketScript.leaf_true = basketScript.leaf_true.Where(obj => obj != other.gameObject).ToList();
                // もしくは、Removeメソッドを使う場合:
                // basketScript.leaf_true.Remove(other.gameObject);
            }

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

    private IEnumerator ChangeTagWithDelay(Collider other, float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した時間待機
        other.tag = "Ochita"; // タグを変更
    }
}
