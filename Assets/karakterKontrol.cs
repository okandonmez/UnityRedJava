using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class karakterKontrol : MonoBehaviour {

    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;

    public Text canText;

    public Image blackPattern;

    int can = 20;
    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;

    GameObject kamera;

    SpriteRenderer spriteRenderer;

    float horizontal = 0;
    float vertical = 0;
    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;
    float alphaSayaci = 0;
    float anaMenuyeDonZamani = 0;

    Vector3 vector;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;

    Rigidbody2D fizik;

    bool birKereZipla = true;


	void Start () {
        Time.timeScale = 1;

        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("kacincilevel", sceneIndex);

        kameraIlkPos = kamera.transform.position - transform.position;

        canText.text = "CAN   " + can;
	}

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.Space)){
            if (birKereZipla) {
                fizik.AddForce(new Vector2(0, 600));
                birKereZipla = false;
            }
        }
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
        birKereZipla = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "kursun"){
            can--;
            canText.text = "CAN   " + can;

        }

        if (collision.gameObject.tag == "dusman")
        {
            can -= 10;
            canText.text = "CAN   " + can;

        }

        if (collision.gameObject.tag == "testere")
        {
            can -= 10;
            canText.text = "CAN   " + can;

        }

        int mevcutLevel = SceneManager.GetActiveScene().buildIndex;
        mevcutLevel++;
        if (collision.gameObject.tag == "levelAtla")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
	}

    void FixedUpdate () {
        
        karakterHareket();
        Animasyon();

        if (can <= 0) {
            Time.timeScale = 0.4f;
            canText.enabled = false;
            alphaSayaci += 0.03f;
            blackPattern.color = new Color(0,0,0, alphaSayaci);
            anaMenuyeDonZamani += Time.deltaTime;
            if (anaMenuyeDonZamani > 1) {
                SceneManager.LoadScene(0);
            }
        }
	}

	private void LateUpdate(){
        kameraKontrol();
	}

	void karakterHareket(){
        horizontal = Input.GetAxisRaw("Horizontal");
        vector = new Vector3(horizontal*10, fizik.velocity.y, 0);
        fizik.velocity = vector;
    }

    void Animasyon(){
        if (birKereZipla) {
            if (horizontal == 0)
            {
                // Idle
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.05f)
                {
                    spriteRenderer.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    beklemeAnimZaman = 0;

                }

            }
            else if (horizontal > 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;

                }

                transform.localScale = new Vector3(1, 1, 1);

            }
            else if (horizontal < 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;

                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        else {
            if (fizik.velocity.y > 0) {
                spriteRenderer.sprite = ziplamaAnim[0]; 
            }

            else {
                spriteRenderer.sprite = ziplamaAnim[1];
            }

            if (horizontal > 0) {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void kameraKontrol () {
        kameraSonPos = kameraIlkPos + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position,kameraSonPos, 0.08f);

    }
}
