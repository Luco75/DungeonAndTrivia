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

    


    void Start()
    {
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

[System.Serializable]
public class SaveData
{
    //Aca van todos los datos del GameManager que se necesitan guardar
}
