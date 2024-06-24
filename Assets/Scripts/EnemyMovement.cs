using System;
using System.Collections;
using System.Drawing;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 5f;
    private Rigidbody2D rigit;
    private Transform currentPoint;

    [SerializeField]
    private float speed;
    private float xPosition;
    private bool isTurn = false;

    private float spawnTimer = 0f;
    [SerializeField]
    private float spawnInterval = 10f;

    [SerializeField]
    public float dameEnemy = 10f;
    [SerializeField]
    public float maxHealthEnemy = 50f;
    public float HealthEnemy { get; set; }
    [SerializeField]
    GameObject healthbarPre;

    [SerializeField]
    Canvas healCanvas;

    //GameObject healthbar;
    public GameObject Healthbar { get; set; }
    public Animator Aim { get; set; }
    //public bool UnMove { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Aim = GetComponent<Animator>();
        rigit = GetComponent<Rigidbody2D>();
        xPosition = rigit.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0;
            speed *= 1.05f;
        }
        if (rigit.transform.localScale.x > 0)
        {
            rigit.velocity = new Vector2(speed, 0);
        }
        if (rigit.transform.localScale.x <= 0)
        {
            rigit.velocity = new Vector2(-speed, 0);
        }
        if (Math.Abs(xPosition - transform.position.x) >= maxDistance && !isTurn)
        {
            isTurn = true;
            flip();
        }
        if (Math.Abs(xPosition - transform.position.x) < maxDistance)
        {
            isTurn = false;
        }

        //if (rigit.transform.position.x - currentPoint.position.x > maxXLimit)
        //{
        //    flip();
        //}
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            flip();
        }
    }

    public void createHealthEnemy()
    {
        if (healthbarPre != null)
        {
            Healthbar = Instantiate(healthbarPre, healCanvas.transform);
            Healthbar.SetActive(true);
            HBController health = Healthbar.AddComponent<HBController>();
            health.Offset = new Vector3(0, 1, 0);
            health.Target = gameObject.transform;
        }
        HealthEnemy = maxHealthEnemy;
    }


    public void Die()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        Aim.SetTrigger("Die");
    }
    public void setEnemyInactive()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }
    //IEnumerator SetInactiveAfterAnimation()
    //{
        
    //    UnMove = true;
    //    gameObject.GetComponent<Rigidbody2D>().simulated=false;
    //    // Wait for the length of the death animation
    //    yield return new WaitForSeconds(Aim.GetCurrentAnimatorStateInfo(0).length);
    //    gameObject.SetActive(false);
    //    UnMove = false;
    //    gameObject.GetComponent<Rigidbody2D>().simulated = true;
    //}
}