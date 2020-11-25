using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiyukiManager : MonoBehaviour
{
    public float moveSpeed = 10f;
    Rigidbody2D rb;
    Animator animator;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    int at = 0;
    public float attackTime = 0;
    public float attackCounter = 0;
    public GameObject counterObject;

    //　HP
    [SerializeField]
    private int hp;
    //　LifeGaugeスクリプト
    [SerializeField]
    private LifeGauge lifeGauge;

    bool hitfuton = false;

    public GameObject player; //主人公
    public Text gameOverText; //ゲームオーバーの文字
    public bool gameOver = false; //ゲームオーバー判定
    public GameObject gameOverLayer;

    public bool isPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackCounter = attackTime;

        //　体力の初期化
        hp = 3;
        //　体力ゲージに反映
        lifeGauge.SetLifeGauge(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            Movement();
            PlayerDamage(at);
            Damage2(at);

            //ライフが0以下になった時、
            if (hp <= 0)
            {
                GameOver();
            }
        
        }
        else
        {
            //ゲームオーバー判定がtrueの時、
            if (gameOver)
            {
                //ゲームオーバーの文字を表示
                gameOverText.enabled = true;
                gameOverLayer.SetActive(true);
                /**画面をクリックすると
                if (Input.GetMouseButtonDown(0))
                {
                    //タイトルへ戻る
                    Application.LoadLevel("Title");
                }*/
            }
        }
        
    }

    void Attack()
    {

        if (hitfuton)
        {
            //Debug.Log("布団に攻撃");
            animator.SetTrigger("IsAttack");
            Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                int at = 1;
                hitEnemy.GetComponent<EnemyManager>().Damage((float)at);
            }
            counterObject.GetComponent<GameCount>().countUp();
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void OnDamege(int power)
    {
        hp -= power;

        animator.SetTrigger("PlHurt");

        if (hp <= 0)
        {
            PlayerDie();
        }
    }

    //　ダメージ処理メソッド（全削除＆HP分作成）
    public void PlayerDamage(int power)
    {
        hp -= power;
        //　0より下の数値にならないようにする
        hp = Mathf.Max(0, hp);

        if (hp >= 0)
        {
            lifeGauge.SetLifeGauge(hp);
        }
    }
    //　ダメージ処理メソッド（ダメージ数分だけアイコンを削除）
   public void Damage2(int power)
    {
        hp -= power;
        if (hp < 0)
        {
            //　ダメージ調整
            power = Mathf.Abs(hp + power);
            hp = 0;
        }
        if (power > 0)
        {
            lifeGauge.SetLifeGauge2(power);
        }
    }

    void PlayerDie()

    {
        hp = 0;
        animator.SetTrigger("PlayerDie");
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); 　//方向キーの横
        // Debug.Log(x);
        //　右向きだったら
        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // 左向きだったら
        if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Enemy")
        {
            hitfuton = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Enemy")
        {
            hitfuton = false;
        }
    }

    public void GameOver()
    {
        gameOver = true;
        //Destroy(player);
    }
}
