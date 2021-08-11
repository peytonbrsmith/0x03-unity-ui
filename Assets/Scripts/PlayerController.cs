using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Image endScreen;
    public Text endText;
    public Text scoreText;
    public Text healthText;
    public float speed = 5.0f;
    private int score = 0;
    public int health = 5;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        SetHealthText();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);
    }

    void Update()
    {
        if (health <= 0)
        {
            endScreen.gameObject.SetActive(true);
            endText.text = "Game Over!";
            StartCoroutine(LoadScene(3.0F));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            score++;
            // Debug.Log($"Score: {score}");
            SetScoreText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            // Debug.Log($"Health: {health}");
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            SetWin();
            // Debug.Log("You win!");
        }
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("maze");
    }

    void SetWin()
    {
        endScreen.gameObject.SetActive(true);
        endScreen.color = Color.green;
        endText.text = "You Win!";
        endText.color = Color.black;
        StartCoroutine(LoadScene(3.0F));
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }
}
