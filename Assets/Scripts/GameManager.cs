
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public TMPro.TextMeshProUGUI UIPoint;
    public TMPro.TextMeshProUGUI UIStage;
    public GameObject UIRestartBtn;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }


    public void NextStage()
    {
        // Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else
        { // Game Clear
            // Player Control Lock
            Time.timeScale = 0;
            // Result UI
            // Restart Button UI
            TMPro.TextMeshProUGUI btnText = UIRestartBtn.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            btnText.text = "Clear!";
            ViewBtn();
        }

        // Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 1, 1, 0.4f);
        }
        else
        {
            // All Health UI Off
            UIhealth[0].color = new Color(1, 1, 1, 0.4f);

            // Player Die Effect
            player.OnDie();

            // Retry Button
            UIRestartBtn.SetActive(true);

        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            // Player Reposition 
            if (health > 1)
            {
                PlayerReposition();
            }

            // Health down
            HealthDown();
        }

    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-8, 0, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void ViewBtn() {
        UIRestartBtn.SetActive(true);
    }
}
