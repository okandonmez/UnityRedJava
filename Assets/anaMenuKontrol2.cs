using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anaMenuKontrol2 : MonoBehaviour {

    GameObject level1, level2, level3;
    GameObject kilit1, kilit2, kilit3;
    GameObject leveller;
    GameObject[] kilitler;

    void Start () {
        
        level1 = GameObject.Find("level1");
        level2 = GameObject.Find("level2");
        level3 = GameObject.Find("level3");

        leveller = GameObject.Find("leveller");

        kilit1 = GameObject.Find("kilit1");
        kilit2 = GameObject.Find("kilit2");
        kilit3 = GameObject.Find("kilit3");

        kilitler = new GameObject[3];
        kilitler[0] = kilit1;
        kilitler[1] = kilit2;
        kilitler[2] = kilit3;


        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);

        kilit1.SetActive(false);
        kilit2.SetActive(false);
        kilit3.SetActive(false);


        if (PlayerPrefs.GetInt("kacincilevel") == 1){
            leveller.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }

        if (PlayerPrefs.GetInt("kacincilevel") == 2)
        {
            leveller.transform.GetChild(0).GetComponent<Button>().interactable = true;
            leveller.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }

        if (PlayerPrefs.GetInt("kacincilevel") == 3)
        {
            leveller.transform.GetChild(0).GetComponent<Button>().interactable = true;
            leveller.transform.GetChild(1).GetComponent<Button>().interactable = true;
            leveller.transform.GetChild(2).GetComponent<Button>().interactable = true;
        }



	}
	
    public void buttonSec(int gelenButon) {
        if (gelenButon == 1) {
            SceneManager.LoadScene(1);
        }
        else if (gelenButon == 2){
            level1.SetActive(true);
            level2.SetActive(true);
            level3.SetActive(true);

            int level = PlayerPrefs.GetInt("kacincilevel");

            if(level == 3){
                
            }
            else if(level == 2){
                kilitler[2].SetActive(true);
            }
            else if (level == 1)
            {
                kilitler[1].SetActive(true);
                kilitler[2].SetActive(true);
            }

           

        }
        else if (gelenButon == 3) {
            Application.Quit();
        }
    }

    public void goToScene (int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}
