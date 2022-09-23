using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

public class GameManager : MonoBehaviour
{
    private FileStream saveFile;
    private SaveData saveData;
    private string ruta;
    private string nombreDelArchivo;

    private Preguntas preguntas;
    private Menus menus;
    private Items items;

    public int[,] inventariosPersonajes;
    public List<int> inventarioJugador = new List<int>();


    void Start()
    {
        inventariosPersonajes = new int[8, 8];

        Iniciar();

        int r = Random.Range(0, preguntas.lista.Count);

        print("Pregunta: " + preguntas.lista[r].texto);
        print("Opcion A: " + preguntas.lista[r].v);
        print("Opcion B: " + preguntas.lista[r].f1);
        print("Opcion C: " + preguntas.lista[r].f2);
        print("Opcion D: " + preguntas.lista[r].f3);
    }

    void Update()
    {
        
    }



    
    /*Programar todo lo que NO tenga que ver guardar los datos arriba de esto*/

    void Iniciar()
    {
        string idioma = "";

        if (Application.systemLanguage == SystemLanguage.Spanish)
        {
            idioma = "ES";
        }
        else
        {
            idioma = "EN";
        }

        ruta = Application.persistentDataPath;
        nombreDelArchivo = "/saveData.dat";

        if (CheckSave())
        {
            LoadGame();
        }

        preguntas = Preguntas.Load(idioma);
        menus = Menus.Load(idioma);
        items = Items.Load();
    }

    bool CheckSave()
    {
        if (File.Exists(ruta + nombreDelArchivo))
        {
            Debug.Log("Se ha encontrado un archivo de guardado");
            return true;
        }
        else
        {
            Debug.Log("No existe archivo de guardado");
            return false;
            
        }
    }

    public void SaveGame()
    {
        ReadAppData();
        BinaryFormatter bf = new BinaryFormatter();
        saveFile = File.Create(ruta + nombreDelArchivo);
        bf.Serialize(saveFile, saveData);
        saveFile.Close();

        Debug.Log("Save Created :" + saveFile.Name);
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        saveFile = File.Open(ruta + nombreDelArchivo, FileMode.Open);
        saveData = (SaveData)bf.Deserialize(saveFile);
        saveFile.Close();
        WriteAppData();

        Debug.Log("Save Loaded :" + saveFile.Name);
    }

    public void ReadAppData()
    {
        //Aca copia desde el GameManager a save
    }

    public void WriteAppData()
    {

        //Aca copia desde el save al GameManager
    }

    public void VerStats(Player p)
    {
        //Va recorriendo estadistica por estadistica y le va sumando todos los valores correspondientes de cada item del inventario. Si el item no da un plus en esa estadistica le va a sumar un 0.

        for (int i = 0; i < p.inventario.Length; i++)
        {
            p.salud += int.Parse(items.lista[p.inventario[i]].salud);
            p.daño += int.Parse(items.lista[p.inventario[i]].daño);
            p.magia += int.Parse(items.lista[p.inventario[i]].magia);
            p.prob_Bloquear += int.Parse(items.lista[p.inventario[i]].p_bloqueo);
            p.prob_Evadir += int.Parse(items.lista[p.inventario[i]].p_evadir);
            p.prob_Critico += int.Parse(items.lista[p.inventario[i]].p_critico);
            p.dam_Critico += int.Parse(items.lista[p.inventario[i]].d_critico);
        }
    }

}

public class Pregunta
{
    [XmlElement("texto")]
    public string texto;
    [XmlElement("v")]
    public string v;
    [XmlElement("f1")]
    public string f1;
    [XmlElement("f2")]
    public string f2;
    [XmlElement("f3")]
    public string f3;
}

[XmlRoot("allq")]
public class Preguntas
{
    [XmlArray("preguntas")]
    [XmlArrayItem("pregunta")]
    public List<Pregunta> lista;

    public static Preguntas Load(string language)
    {
        TextAsset _xml = Resources.Load<TextAsset>("Textos/" + "preguntas_" + language);
        XmlSerializer serializador = new XmlSerializer(typeof(Preguntas));
        StringReader reader = new StringReader(_xml.text);
        Preguntas lista = serializador.Deserialize(reader) as Preguntas;
        reader.Close();
        return lista;
    }
}

public class TodosLosMenus
{
    [XmlElement("texto")]
    public string texto;
}

[XmlRoot("allm")]
public class Menus
{
    [XmlArray("menus")]
    [XmlArrayItem("menu")]
    public List<TodosLosMenus> lista;

    public static Menus Load(string language)
    {
        TextAsset _xml = Resources.Load<TextAsset>("Textos/" + "menus_" + language);
        XmlSerializer serializador = new XmlSerializer(typeof(Menus));
        StringReader reader = new StringReader(_xml.text);
        Menus lista = serializador.Deserialize(reader) as Menus;
        reader.Close();
        return lista;
    }
}



public class TodosLosItems
{
    [XmlElement("nombre")]
    public string nombre;
    [XmlElement("calidad")]
    public string calidad;
    [XmlElement("salud")]
    public string salud;
    [XmlElement("daño")]
    public string daño;
    [XmlElement("magia")]
    public string magia;
    [XmlElement("p_bloqueo")]
    public string p_bloqueo;
    [XmlElement("p_evadir")]
    public string p_evadir;
    [XmlElement("p_critico")]
    public string p_critico;
    [XmlElement("d_critico")]
    public string d_critico;
}

[XmlRoot("alli")]
public class Items
{
    [XmlArray("items")]
    [XmlArrayItem("item")]
    public List<TodosLosItems> lista;

    public static Items Load()
    {
        TextAsset _xml = Resources.Load<TextAsset>("Base De Datos Items");
        XmlSerializer serializador = new XmlSerializer(typeof(Items));
        StringReader reader = new StringReader(_xml.text);
        Items lista = serializador.Deserialize(reader) as Items;
        reader.Close();
        return lista;
    }
}



[System.Serializable]
public class SaveData
{
    //Aca van todos los datos del GameManager que se necesitan guardar
}
