using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class dusmanKontrol : MonoBehaviour
{

    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirKereAl = true;
    Vector3 aradakiMesafe;

    int aradakiMesafeSayaci = 0;
    int hiz = 5;


    bool ileriGeri = true;
    GameObject karakter;
    public GameObject kursun;
    float atesZamani = 0;

    RaycastHit2D ray;

    public LayerMask layerMask;
    public Sprite onTaraf;
    public Sprite arkaTaraf;

    SpriteRenderer spriteRenderer;


    void Start()
    {


        karakter = GameObject.FindGameObjectWithTag("Player");
        gidilecekNoktalar = new GameObject[transform.childCount];
        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        beniGorduMu(); 
        if(ray.collider.tag == "Player"){
            // Ates etmeli
            hiz = 8;
            spriteRenderer.sprite = onTaraf;
            atesEt();

        }else{
            hiz = 4;
            spriteRenderer.sprite = arkaTaraf;

        }
        noktalaraGit();
    }

    void atesEt(){
        atesZamani += Time.deltaTime;

        if (atesZamani > Random.Range(0.2f,1)){
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamani = 0;
        }
    }

    void beniGorduMu(){
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000, layerMask);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }

    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;

        if (mesafe < 0.5f)
        {

            aradakiMesafeyiBirKereAl = true;
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ileriGeri = false;

            }
            else if (aradakiMesafeSayaci == 0)
            {
                ileriGeri = true;
            }
            if (ileriGeri)
            {
                aradakiMesafeSayaci++;
            }
            else
            {
                aradakiMesafeSayaci--;
            }
        }

    }

    public Vector2 getYon () {
        return (karakter.transform.position - transform.position).normalized;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.GetChild(i).transform.position, 1);
        }

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);

        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(dusmanKontrol))]
[System.Serializable]

class dusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        dusmanKontrol script = (dusmanKontrol)target;
        if (GUILayout.Button("URET"))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerMask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

    }



}
#endif
