﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol : MonoBehaviour {

    dusmanKontrol dusman;

    Rigidbody2D fizik;

    // Use this for initialization
    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman").GetComponent<dusmanKontrol>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon()*1000);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
