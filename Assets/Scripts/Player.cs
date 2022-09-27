using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    GameManager gm;

    public int magia;
    public Sprite[] partesDelCuerpo;
    public int[] inventario;
    public int[] nivelDeItem;
    public int id;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Carga los items del inventario del personaje
        for (int i = 0; i < inventario.Length; i++)
        {
            inventario[i] = gm.inventariosPersonajes[id, i, 0];
            nivelDeItem[i] = gm.inventariosPersonajes[id, i, 1];
        }

        //Actualiza las estadisticas en base a los items equipados
        gm.VerStats(gameObject.GetComponent<Player>());
    }

}