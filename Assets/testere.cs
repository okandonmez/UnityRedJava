using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class testere : MonoBehaviour
{

    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirKereAl = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayaci = 0;
    bool ileriGeri = true;

    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++){
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        transform.Rotate(0, 0, 5);
        noktalaraGit();
    }

    void noktalaraGit(){
        if (aradakiMesafeyiBirKereAl){
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * 10;

        if (mesafe < 0.5f){

            aradakiMesafeyiBirKereAl = true;
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length-1){
                ileriGeri = false;
                
            }else if(aradakiMesafeSayaci == 0){
                ileriGeri = true;
            }
            if(ileriGeri){
                aradakiMesafeSayaci++;
            }
            else {
                aradakiMesafeSayaci--;
            }
        }

    }

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
        for (int i = 0; i < transform.childCount; i ++){
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.GetChild(i).transform.position, 1);
        }

        for (int i = 0; i < transform.childCount-1; i++){
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);

        }
	}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(testere))]
[System.Serializable]

class testereEditor : Editor {
	public override void OnInspectorGUI()
	{
        testere script = (testere)target;
        if (GUILayout.Button("URET")){
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
	}



}
#endif
