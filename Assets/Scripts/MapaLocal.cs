using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaLocal : MonoBehaviour
{
    public GameObject mazmorra;
    public GameObject herreria;
    public GameObject taberna;
    public GameObject mercado;
    public GameObject suburbios;
    public GameObject biblioteca;
    public GameObject cementerio;
    public GameObject catedral;
    public GameObject jardin;

    public GameObject unoHerreria;
    public GameObject dosTaberna;
    public GameObject tresMercado;
    public GameObject cuatroSuburbios;
    public GameObject cincoBiblioteca;
    public GameObject seisCementerio;
    public GameObject sieteCatedral;
    public GameObject ochoJardin;

    // Start is called before the first frame update
    void Start()
    {
     herreria.SetActive(false);
     taberna.SetActive(false);
     mercado.SetActive(false);
     suburbios.SetActive(false);
     biblioteca.SetActive(false);
     cementerio.SetActive(false);
     catedral.SetActive(false);
     jardin.SetActive(false);   

     dosTaberna.SetActive(false);
     tresMercado.SetActive(false);
     cuatroSuburbios.SetActive(false);
     cincoBiblioteca.SetActive(false);
     seisCementerio.SetActive(false);
     sieteCatedral.SetActive(false);
     ochoJardin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConstruccionHerreria()
    {
        herreria.SetActive(true);
        dosTaberna.SetActive(true);
    }

        public void ConstruccionTaberna()
    {
        taberna.SetActive(true);
        tresMercado.SetActive(true);
    }
        public void ConstruccionMercado()
    {
        mercado.SetActive(true);
        cuatroSuburbios.SetActive(true);
    }
        public void ConstruccionSburbios()
    {
        suburbios.SetActive(true);
        cincoBiblioteca.SetActive(true);
    }
        public void ConstruccionBilioteca()
    {
        biblioteca.SetActive(true);
        seisCementerio.SetActive(true);
    }
        public void ConstruccionCementerio()
    {
        cementerio.SetActive(true);
        sieteCatedral.SetActive(true);
    }
        public void ConstruccionCatedral()
    {
        catedral.SetActive(true);
        ochoJardin.SetActive(true);
    }
        public void ConstruccionJardin()
    {
        jardin.SetActive(true);
    }
}
