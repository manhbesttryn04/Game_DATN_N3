using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VatPham {
    public int ID { get; set; }	// Ma
    public string Name { get; set; } // Ten
    public int Quality { get; set; } // so luong

    public bool Revice { get; set; } // Nhan

    public float TimeLive { get; set; } // Thoi gian song
    public float Eat { get; set; }  // Nhan ve
    public VatPham()
    {
        ID = -1;
        Name = "";
        Quality = 0;
        Revice = false;
    }

    public VatPham(int id,string name, int quality,bool revice)
    {
        this.ID = id;
        this.Name = name;
        this.Quality = quality;
        this.Revice = revice;
    }

    public VatPham(int id, string name, float eat, float timeLive)
    {
        this.ID = id;
        this.Name = name;
        this.Eat = eat;
        this.TimeLive = timeLive;
    }
    public VatPham(int id, string name, float eat)
    {
        this.ID = id;
        this.Name = name;
        this.Eat = eat;
    }
}
