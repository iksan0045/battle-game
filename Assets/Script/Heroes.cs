using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heroes : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackPower;
    [SerializeField] private float movementSpeed;
    [SerializeField] private string OpponentTags;
    [SerializeField] private GameObject projectile;


    float startTime;
    float coolDown;
    public LayerMask targetMele;
    Transform opponentPos;
    float distance;
    float direction;
    private float timeAttack;
    private string sideTag;
    public bool mele;
    public Animator anim;
    public float meleRange;
    public Transform attackPosition;

    void Start()
    {
        startTime = 2f;
        sideTag = gameObject.tag;
        coolDown = attackRate;
        timeAttack = attackRate;
        opponentPos = GameObject.FindGameObjectWithTag(OpponentTags).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime >= 0)
        {
            startTime -= Time.deltaTime;
        }
        else
        {
            if ( opponentPos != null )
            {
                opponentPos = GameObject.FindGameObjectWithTag(OpponentTags).transform;
                distance = Vector2.Distance(attackPosition.position,opponentPos.position);
            }
            else
            {
                anim.SetBool("walk",false);
                FindObjectOfType<GameManager>().Win(gameObject.name);
            }
            if ( distance >= attackRange && opponentPos != null)
            {
                Move();
            }
            
            if (distance < attackRange)
            {
                anim.SetBool("walk",false);
                if (coolDown >= 0)
                {
                    coolDown -= Time.deltaTime;
               
                }
                else
                {
                    Attack();
                    coolDown = attackRate;   
                }
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void Move()
    {
        anim.SetBool("walk",true);
        if (sideTag == "Kubu A")
        {
            direction = 1;
            transform.position += transform.right * movementSpeed * Time.deltaTime * direction;
        }
        if (sideTag == "Kubu B")
        {
            direction = -1;
            transform.position += transform.right * movementSpeed * Time.deltaTime * direction;
        }   
    }
    
    private void Attack()
    {
        if (opponentPos != null)
        {
            if (mele ==true)
            {
                Debug.Log("MELE");
                MeleAttack();
            }
            else
            {
                Debug.Log("RANGE");
                //wraith
                WraithAttack();
            }  
        }
        else
        {
            anim.SetBool("walk",false);
        } 
    }
    private void MeleAttack()
    {
        anim.SetTrigger("attack");
        Collider2D[] damage = Physics2D.OverlapCircleAll( attackPosition.position, attackRange, targetMele );

                for (int i = 0; i < damage.Length; i++)
                {
                    damage[i].gameObject.GetComponent<Heroes>().TakeDamage(attackPower);
                }
    }
    private void WraithAttack()
    {
        anim.SetTrigger("attack");
        Instantiate(projectile,attackPosition.position,attackPosition.rotation);

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPosition.position,meleRange);
    }
}
