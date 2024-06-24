using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private GameObject attackPoint;

    [SerializeField]
    private float radius;

    [SerializeField]
    private LayerMask enemies;

    public static PlayerController scene1;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float jumpForce = 0.2f;
    //[SerializeField]
    //public float timePlay=10;
    private Rigidbody2D rigit;
    //private int Point = 0;
    [SerializeField]
    public Text textPoint;

    private bool grounded;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    private float Move;
    private Animator aim;
    private Text Point;
    int poin=0;
    //private float countdownTime;
    //private bool isCounting = true;

    //public Image imageCanvas;
    //public Vector2[] points;
    //public string Name { get; set; }
    public string TimePlay { get; set; }

    [SerializeField]
    public float damePlayer = 30f;
    [SerializeField]
    public float maxHealthPlayer = 100f;
    public float healthPlayer { get; set; }
    [SerializeField]
    private Slider healthbarPre;

    [SerializeField]
    private Canvas healCanvas;

    
    public string Status { get; set; }
    //public bool UnMove { get; set; }
    Slider Healthbar;
    //EnemySpawn enemySpawn = new EnemySpawn();
    // Start is called before the first frame update

    private void Awake()
    {
        scene1 = this;
    }
    void Start()
    {
        //DisplayPoints();
        aim = GetComponent<Animator>();
        rigit = gameObject.GetComponent<Rigidbody2D>();
        //StartCountdown(timePlay);

        if (healthbarPre != null)
        {
            //Healthbar = Instantiate(healthbarPre, healCanvas.transform);
            Healthbar= healthbarPre;
            Healthbar.gameObject.SetActive(true);
            //Healthbar.interactable = false;
            HBController health = Healthbar.gameObject.AddComponent<HBController>();
            health.Offset = new Vector3(0, 1, 0);
            health.Target = gameObject.transform;
        }
        healthPlayer = maxHealthPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
        Move = Input.GetAxisRaw("Horizontal");
    if(Time.timeScale != 0) { 
        if (Input.GetKey(KeyCode.RightArrow) && !aim.GetBool("isAttacking"))
        {
            transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime,
            transform.position.y,
            transform.position.z);
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !aim.GetBool("isAttacking"))
        {
            transform.position = new Vector3(
            transform.position.x - speed * Time.deltaTime,
            transform.position.y,
            transform.position.z);
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
            }
        }



        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded() && !aim.GetBool("isAttacking"))
        {
            //rigit.velocity=new Vector2(Move*speed,rigit.velocity.y);
            rigit.AddForce( new Vector2(rigit.velocity.x,10 * jumpForce));
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
                aim.SetBool("isAttacking", true);
                aim.SetBool("isRunning", false);
        }

        if(Move!=0)
        {
            aim.SetBool("isRunning", true);

        }
        else
        {
            aim.SetBool("isRunning", false);
        }
        
        aim.SetBool("isJumping",!isGrounded());
      }
    }

    public void endAttack()
    {
        aim.SetBool("isAttacking", false);
    }

    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        foreach(Collider2D enemyGameobject in enemy)
        {
            float healthE = enemyGameobject.gameObject.GetComponent<EnemyMovement>().HealthEnemy -= damePlayer;
            float maxhealthE = enemyGameobject.gameObject.GetComponent<EnemyMovement>().maxHealthEnemy;
            
            GameObject healthBarE = enemyGameobject.gameObject.GetComponent<EnemyMovement>().Healthbar;
            if (healthE <= 0)
            {
                Destroy(healthBarE);
                poin += 1;
                textPoint.text = "Point: " + poin.ToString();
                //StartCoroutine(WaitForAnimationEnemy(collision));
                enemyGameobject.gameObject.GetComponent<EnemyMovement>().Die();

            }else
            healthBarE.GetComponent<HBController>().SetHealth(healthE, maxhealthE);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize,0,-transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag.Equals("Enemy"))
    //    {
    //        collision.gameObject.SetActive(false);
    //        poin += 1;
    //        textPoint.text = "Point: " + poin.ToString();
    //    }


    //}

    //================va chạm với enemy tùy thuộc vị trí sẽ mất máu
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (gameObject.transform.position.x < collision.gameObject.transform.position.x
    //        && collision.gameObject.CompareTag("Enemy"))
    //    {
    //        if (gameObject.transform.localScale.x > 0)
    //        {
    //            //Player chém enemy
    //            float healthE=collision.gameObject.GetComponent<EnemyMovement>().HealthEnemy -= damePlayer;
    //            float maxhealthE= collision.gameObject.GetComponent<EnemyMovement>().maxHealthEnemy;

    //            GameObject healthBarE = collision.gameObject.GetComponent<EnemyMovement>().Healthbar;
    //            if (healthE <= 0)
    //            {
    //                healthBarE.gameObject.SetActive(false);
    //                poin += 1;
    //                textPoint.text = "Point: " + poin.ToString();
    //                collision.gameObject.GetComponent<EnemyMovement>().Die();

    //            }
    //            healthBarE.GetComponent<HBController>().SetHealth(healthE, maxhealthE);
    //        }
    //        else
    //        {
    //            //Enemy chạm Player
    //            healthPlayer -= collision.gameObject.GetComponent<EnemyMovement>().dameEnemy;
    //            if (healthPlayer <= 0)
    //            {
    //                Healthbar.gameObject.SetActive(false);
    //                aim.SetBool("isDead", true);
    //                    Debug.Log("Gameover");
    //                    Status = "You died";
    //                    int timPlay = UIController.ui.maxTimePlay - UIController.ui.TotalTime;
    //                    TimePlay = timPlay.ToString();
    //                StartCoroutine(WaitForAnimationAndLoadScene());
    //            }
    //            Healthbar.GetComponent<HBController>().SetHealth(healthPlayer, maxHealthPlayer);
    //        }

    //    }
    //    if (gameObject.transform.position.x > collision.gameObject.transform.position.x
    //        && collision.gameObject.CompareTag("Enemy"))
    //    {
    //        if (gameObject.transform.localScale.x > 0)
    //        {
    //            //Enemy chạm Player
    //            healthPlayer -= collision.gameObject.GetComponent<EnemyMovement>().dameEnemy;
    //            if (healthPlayer <= 0)
    //            {
    //                Healthbar.gameObject.SetActive(false);
    //                aim.SetBool("isDead", true);
    //                    //gameObject.SetActive(false);
    //                    Debug.Log("Gameover");
    //                    Status = "You died";
    //                    int timPlay = UIController.ui.maxTimePlay - UIController.ui.TotalTime;
    //                    TimePlay = timPlay.ToString();
    //                StartCoroutine(WaitForAnimationAndLoadScene());
    //            }
    //            Healthbar.GetComponent<HBController>().SetHealth(healthPlayer, maxHealthPlayer);
    //        }
    //        else
    //        {

    //            float healthE = collision.gameObject.GetComponent<EnemyMovement>().HealthEnemy -= damePlayer;
    //            float maxhealthE = collision.gameObject.GetComponent<EnemyMovement>().maxHealthEnemy;

    //            GameObject healthBarE = collision.gameObject.GetComponent<EnemyMovement>().Healthbar;
    //            if (healthE <= 0)
    //            {
    //                healthBarE.gameObject.SetActive(false);
    //                poin += 1;
    //                textPoint.text = "Point: " + poin.ToString();
    //                collision.gameObject.GetComponent<EnemyMovement>().Die();
    //            }
    //            healthBarE.GetComponent<HBController>().SetHealth(healthE, maxhealthE);

    //        }

    //    }
    //}



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
                healthPlayer -= collision.gameObject.GetComponent<EnemyMovement>().dameEnemy;
                if (healthPlayer <= 0)
                {
                    Healthbar.gameObject.SetActive(false);
                    aim.SetBool("isDead", true);
                    
                    //StartCoroutine(WaitForAnimationAndLoadScene());
                }
                Healthbar.GetComponent<HBController>().SetHealth(healthPlayer, maxHealthPlayer);
        }
    }

    public void playerDie()
    {
        Debug.Log("Gameover");
        Status = "You died";
        int timPlay = UIController.ui.maxTimePlay - UIController.ui.TotalTime;
        TimePlay = timPlay.ToString();
        SceneManager.LoadScene(2);
    }
    //private IEnumerator WaitForAnimationAndLoadScene()
    //{
    //    // Lấy AnimatorStateInfo cho layer 0
    //    AnimatorStateInfo stateInfo = aim.GetCurrentAnimatorStateInfo(0);

    //    // Chờ cho đến khi hoạt ảnh kết thúc
    //    while (stateInfo.normalizedTime < 2.0f && !stateInfo.IsName("isDead"))
    //    {
    //        UnMove = true;
    //        yield return null; // Chờ đến frame tiếp theo
    //        stateInfo = aim.GetCurrentAnimatorStateInfo(0); // Cập nhật stateInfo
    //    }
    //    // Chuyển cảnh sau khi hoạt ảnh kết thúc
    //    SceneManager.LoadScene(2);
    //}

    //private IEnumerator WaitForAnimationAndLoadScene()
    //{
    //    UnMove = true;
    //    yield return new WaitForSeconds(aim.GetCurrentAnimatorStateInfo(0).length);
    //    SceneManager.LoadScene(2);
    //}

    //private IEnumerator WaitForAnimationEnemy(Collision2D collision)
    //{
    //    collision.gameObject.GetComponent<EnemyMovement>().Aim.SetBool("isDeadEne",true);
    //    // Lấy Animator component từ enemy
    //    Animator enemyAnimator = collision.gameObject.GetComponent<EnemyMovement>().Aim;

    //    // Chờ cho đến khi hoạt ảnh "isDeadEne" kết thúc
    //    AnimatorStateInfo stateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);

    //    // Chờ cho đến khi normalizedTime >= 1.0f, tức là hoạt ảnh đã chạy hết
    //    //while (stateInfo.normalizedTime < 7.0f)
    //    //{
    //    //    collision.gameObject.GetComponent<EnemyMovement>().UnMove=true;
    //    //    yield return null; // Chờ đến frame tiếp theo
    //    //    stateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0); // Cập nhật stateInfo
    //    //}

    //    yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);

    //    // Tắt enemy sau khi hoạt ảnh kết thúc
    //    collision.gameObject.SetActive(false);
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag.Equals("Ground"))
    //    {
    //        grounded = false;
    //    }
    //}


    //void DisplayPoints()
    //{
    //    foreach (Vector2 point in points)
    //    {
    //        // Calculate the position on the Image Canvas
    //        Vector2 canvasPosition = new Vector2(
    //            point.x / imageCanvas.rectTransform.rect.width,
    //            point.y / imageCanvas.rectTransform.rect.height
    //        );

    //        // Create a new UI GameObject (e.g., a circle) at the calculated position
    //        GameObject newPoint = new GameObject("Point");
    //        newPoint.transform.SetParent(imageCanvas.transform, false);
    //        RectTransform rt = newPoint.AddComponent<RectTransform>();
    //        rt.anchorMin = canvasPosition;
    //        rt.anchorMax = canvasPosition;
    //        rt.sizeDelta = new Vector2(10, 10); // Adjust size as needed

    //    }
    //}

    //public void StartCountdown(float seconds)
    //{
    //    countdownTime = seconds;
    //    isCounting = true;
    //    StartCoroutine(CountdownCoroutine());
    //}
    //private IEnumerator CountdownCoroutine()
    //{
    //    while (countdownTime > 0)
    //    {
    //        countdownTime -= Time.deltaTime;
    //        UpdateTimeDisplay();
    //        yield return null;
    //    }

    //    // Countdown finished
    //    isCounting = false;
    //    UpdateTimeDisplay();
    //}

    //private void UpdateTimeDisplay()
    //{
    //    if (time != null)
    //    {
    //        int hours = Mathf.FloorToInt(countdownTime / 3600);
    //        int minutes = Mathf.FloorToInt((countdownTime - hours * 3600) / 60);
    //        int seconds = Mathf.FloorToInt(countdownTime - hours * 3600 - minutes * 60);

    //        time.text = "Time: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    //    }

    //}
}
