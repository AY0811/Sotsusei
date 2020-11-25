using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Animator animator;
    public int hp = 15;
    public float upForce = 100.0f;
    public float xForce = 30.0f;
    public float jumpWaitMin = 3;
    public float jumpWaitMax = 7;
    public GameObject playerObject;
    Vector3 rightScale;
    Vector3 leftScale;

    public float life;
    public float maxLife;

    protected EnemyGauge enemyGauge;

    public Transform attackPoint;
    public float attackRadius;
    public float attackTime = 0;
    bool hitplayer = false;
    public LayerMask playerLayer;

    public bool isPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("Attack");

        rightScale.x = gameObject.transform.localScale.x * -1.0f;
        rightScale.y = gameObject.transform.localScale.y;
        rightScale.z = gameObject.transform.localScale.z;

        leftScale.x = gameObject.transform.localScale.x;
        leftScale.y = gameObject.transform.localScale.y;
        leftScale.z = gameObject.transform.localScale.z;

        enemyGauge = GameObject.FindObjectOfType<EnemyGauge>();
        enemyGauge.SetPlayer(this);

    }


    // Update is called once per frame
    void Update()
    {
        /**if (Input.GetKeyDown(KeyCode.Return))
        {
            OnAttack();
            Debug.Log("布団攻撃");
        }*/

        if (playerObject.transform.position.x < gameObject.transform.position.x)
        {
            gameObject.transform.localScale = leftScale;
        }
        else
        {
            gameObject.transform.localScale = rightScale;
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(jumpWaitMin, jumpWaitMax));
            OnAttack();

            /*
            if (playerObject.transform.position.x < gameObject.transform.position.x)
            {
                // animator.SetTrigger("Jump");
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce + Vector2.right * xForce * -1.0f);
                
            }
            else
            {
                // animator.SetTrigger("Jump");
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce + Vector2.right * xForce);
            }
            */

        }
    }

    public void Damage(float power)
    {
        enemyGauge.GaugeReduction(power);
        life -= power;

        animator.SetTrigger("IsHurt");

        if (life < 0.0f)
        {
            Die();
        }
    }

    void Die()

    {
        hp = 0;
        animator.SetTrigger("Die");
    }

    void OnAttack()
    {

        if (hitplayer)
        {
            if (playerObject.transform.position.x < gameObject.transform.position.x)
            {
                // animator.SetTrigger("Jump");
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce + Vector2.right * xForce * -1.0f);

            }
            else
            {
                // animator.SetTrigger("Jump");
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce + Vector2.right * xForce);
            }

            animator.SetTrigger("EnAttack");
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
            foreach (Collider2D hitPlayer in hitPlayers)
            {
                int at = 1;
                //Debug.Log(hitPlayer.gameObject.name + "に攻撃できたよ");
                hitPlayer.GetComponent<MiyukiManager>().OnDamege(at);
            }
            hitplayer = false;
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "miyuki01")
        {
            hitplayer = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "miyuki01")
        {
            // hitplayer = false;
        }
    }
}