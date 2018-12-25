using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class RealScoreManager
{
    public int scores { get; set; }
}

//TODO: rewrite!
public class ScoreManager : MonoBehaviour
{
    internal static RealScoreManager instance;

    public int scores
    {
        get => instance.scores;
        set => instance.scores = value;
    }

    void Awake()
    {
        if (instance == null)
            instance = new RealScoreManager();

        //if (instance == null)
        //    instance = this;
        //else if (instance == this) // Экземпляр объекта уже существует на сцене
        //    Destroy(gameObject);

        //// Теперь нам нужно указать, чтобы объект не уничтожался
        //// при переходе на другую сцену игры
        //DontDestroyOnLoad(gameObject);

        //// И запускаем собственно инициализатор
        //InitializeManager();


    }

    private void InitializeManager()
    {

    }
}
