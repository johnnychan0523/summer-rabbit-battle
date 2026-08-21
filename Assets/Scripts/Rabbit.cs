using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // 在 Inspector 可調整速度
    public float moveSpeed = 5f;

    private GameObject currentFloor;   // 記錄當前踩到的地板
    private Rigidbody rb;              // 快取 Rigidbody

    [SerializeField] int Hp;

    [SerializeField] GameObject HpBar;

    [SerializeField] TMP_Text scoreText;

    AudioSource deathSound;

    [SerializeField]GameObject replayButton;

    int score;
    float scoreTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Hp = 10;
        score=0;
        scoreTime=0f;
        deathSound=GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 左鍵 → 向左 (-X)
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        // 右鍵 → 向右 (+X)
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        UpdateScore();
    }

    /// <summary>
    /// 與實體地板碰撞時
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // 只取第一個接觸點來判斷是否從「正上方」踩到
        Vector3 n = collision.GetContact(0).normal;
        bool hitFromTop = Vector3.Dot(n, Vector3.up) > 0.5f;   // 夾角小於約 60°

        if (hitFromTop && collision.gameObject.CompareTag("grass"))
        {
            currentFloor = collision.gameObject;
            ModiffyHp(1);
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
        else if (hitFromTop && collision.gameObject.CompareTag("ocean"))
        {
            currentFloor = collision.gameObject;
            ModiffyHp(-3);
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
        else if (collision.gameObject.CompareTag("ceiling"))   // 請確認 Tag 也寫 ceiling
        {

            // 範例：關閉腳下地板的 Collider（若有踩在地板上）
            if (currentFloor != null)
            {
                Collider col = currentFloor.GetComponent<Collider>();
                if (col != null) col.enabled = false;
                ModiffyHp(-3);
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }

    /// <summary>
    /// 穿過觸發區（掉落線）時
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathLine"))
        {
            Die();
        }
    }

    void ModiffyHp(int num)
    {
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10;
        }
        else if (Hp <= 0)
        {
            Hp = 0;
            Die();
        }
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (Hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);   // 顯示血格
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);  // 隱藏血格
            }
        }
    }
    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if(scoreTime>5f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text="夏季兔兔" + score.ToString() + "層大作戰";
        }
    }
    void Die()
    {
        deathSound.Play();
        Time.timeScale=0f;
        replayButton.SetActive(true);
    }
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
