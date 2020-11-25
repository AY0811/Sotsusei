using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCount : MonoBehaviour
{
    public Text textField;
    public Text timerText;
    int count;
    int result;
    //　トータル制限時間
    private float totalTime;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;
    //　前回Update時の秒数
    private float oldSeconds;

    public Text ClearText;
    public Text gameOverText; //ゲームオーバーの文字
    private bool gameOver = false; //ゲームオーバー判定
    public GameObject gameOverLayer;
    public MiyukiManager miyukiManager;
    public EnemyManager enemyManager;


    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        textField.text = count + "回";
        totalTime = seconds;
        oldSeconds = 0f;

        //StartCoroutine("Clear");

    }

    // Update is called once per frame
    void Update()
    {
        /**
        if (Input.GetKeyDown(KeyCode.Space))
        { count++;
            textField.text = count + "回";
            Debug.Log("増えたよ");
        }
        */
        //　制限時間が0秒以下なら何もしない
        if (totalTime <= 0f)
        {
            return;
        }
        //　一旦トータルの制限時間を計測；
        totalTime = seconds;
        totalTime -= Time.deltaTime;

        //　再設定
        seconds = totalTime;

        //　タイマー表示用UIテキストに時間を表示する
        if ((int)seconds != (int)oldSeconds)
        {

            timerText.text = ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;
        //　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
        if (totalTime <= 0f)
        {
            Debug.Log("制限時間終了");
            StartCoroutine("TimeGameOver");
        }
    }

    public void countUp()
    {
       count++;
       textField.text = count + "回";
    }

    IEnumerator TimeGameOver()
    {
      gameOverText.enabled = true;
      gameOverLayer.SetActive(true);
      miyukiManager.isPlaying = false;
      enemyManager.isPlaying = false;
      yield return new WaitForSeconds(1.0f);
    }

    /*IEnumerator Clear()
    {
        yield return new WaitForSeconds(30);

        // Time.timeScale = 0f;
        ClearText.enabled = true;

        if (this.count >= 15)
        {//15点以上獲得
            ClearText.enabled = true;
        }
        else//失敗
        {
            gameOverText.enabled = true;
            gameOverLayer.SetActive(true);
        }

    }*/
}