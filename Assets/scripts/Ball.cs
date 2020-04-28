using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb2;
    public float moveSpeed = 10;
    public bool direction;
    public Text scoreText;
    public Transform player;
    public Vector2 restartPos = Vector2.zero;
    public int Score { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        int x = PlayerPrefs.GetInt("sahne");

        if (x == 0)//NewGame
        {
            Debug.Log("New game tıklandı.");
            string path = "kayit";
            //kayıt.txt adında bir dosya mevcut değilse
            if (File.Exists(path) != true)
            {
                Debug.Log("Kayıt dosyası yoktu oluşturuldu");
                //İlk oynayışta "kayıt.txt" dosyası yoksa oluşturulması için.
                PlayerData data = new PlayerData();
                SaveLoadManager.Save(path, data);
            }

            Invoke("ballStart", 2);
            //Topa yaklaştığında yeniden yüklenen oyunda en alttaki if e girmesi için
            PlayerPrefs.SetInt("sahne", 2);
        }
        else if(x == 1)//LoadLastGame
        {
            string path = "kayit";
            if (File.Exists(path) == true)
            {
                Debug.Log("Dosya mevcut ve Menuden LoadLastGame secildi.");
                //Dosyadan en son kaydedilen oyun bilgileri çekildi
                PlayerData data = new PlayerData();
                data = SaveLoadManager.Load(path);
                Score = data.score;
                scoreText.text = Score.ToString();
                restartPos = new Vector2(player.position.x, data.posy);
                player.transform.position = restartPos;
                //Topa yaklaştığında yeniden yüklenen oyunda en alttaki if e girmesi için
                PlayerPrefs.SetInt("sahne", 2);
                Invoke("ballStart", 2);

            }
            else
            {
                //dosya mevcut değil önceden kayıtlı oyun yok
                Debug.Log("Daha önceden kayıtlı oyun bulunamadı!!!");
                SceneManager.LoadScene(0);
            }   
        }
        else if (x == 2)//Topa yaklaşınca yenilenme
        {
            //Debug.Log("Topa yeterince yaklaşıldı ve skor korunup oyuncu konumu güncellendi.");
            
            Score = PlayerPrefs.GetInt("skor");
            scoreText.text = Score.ToString();
            restartPos = new Vector2(player.position.x, -4f);
            player.transform.position = restartPos;
            ballStart();
        }

    }

    private void ballStart()
    {
        //Topun harekete başlaması
        rb2 = GetComponent<Rigidbody2D>();
        rb2.velocity = new Vector2(1, 0) * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float mesafe = Mathf.Abs(transform.position.y - player.position.y);
        
        if (mesafe < 3)
        {
            //Skorun korunması için
            PlayerPrefs.SetInt("skor", Score);
            SceneManager.LoadScene(1);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TagManager tag = collision.gameObject.GetComponent<TagManager>();
        GetComponent<AudioSource>().Play();
        if (tag == null) return;
        //direction topla bombanın çarpısı sonucu topun yön değiştirmemesi için kullanılıyor.
        if (tag.myTag == Tag.SOL_DUVAR) direction = true;
        else if (tag.myTag == Tag.SAG_DUVAR) direction = false;
    }

    public void skorYap()
    {
        player oyuncu = GameObject.FindObjectOfType<player>();
        Score++;
        scoreText.text = Score.ToString();
        Vector2 newPos = Vector2.zero;
        if (Score % 5 == 0)
        {
            //Her 5 skorda player topa yaklaşıyor
            newPos = new Vector2(oyuncu.lastPos.position.x, oyuncu.lastPos.position.y + 1f);
            oyuncu.transform.position = newPos;
        }


        string path = "kayit";
        //Skor, dosyadaki kayıtlı skordan büyükse yeni bilgileri kaydet
        if (Score > SaveLoadManager.Load(path).score)
        {
            //Debug.Log("Kayıtlı skor geçildi, skor ve konum guncellendi.");
            PlayerData data = new PlayerData();
            data.score = Score;
            data.posy = oyuncu.transform.position.y;
            SaveLoadManager.Save(path, data);
        }

    }
}
