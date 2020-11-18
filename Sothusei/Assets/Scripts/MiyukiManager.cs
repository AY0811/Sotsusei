using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool hitfuton = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackCounter = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        Movement();
    }

    void Attack()
    {
        
        if (hitfuton)
        {
            Debug.Log("布団に攻撃");
            animator.SetTrigger("IsAttack");
            Debug.Log("攻撃");
            Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                int at = 1;
                hitEnemy.GetComponent<EnemyManager>().OnDamage(at);
            }
            counterObject.GetComponent<GameCount>().countUp();
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); 　//方向キーの横
        Debug.Log(x);
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
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Enemy")
        {
            hitfuton = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Enemy")
        {
            hitfuton = false;
        }
    }
}
