using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterLevelPoints : MonoBehaviour
{
    #region Variables
    [SerializeField] private int countObjects;
    private GameObject[] listaFrutas;
    private GameObject[] listaEnemigos;
    #endregion

    #region Unity Methods
    /// <summary>
    /// Find all object with Fruit tag and count how many are
    /// </summary>
    private void Awake()
    {
        listaFrutas = GameObject.FindGameObjectsWithTag("Fruit");
        for(int i = 0; i < listaFrutas.Length; i++)
        {
            countObjects += listaFrutas[i].GetComponent<FruitController>().getPuntos();
        }
        listaEnemigos = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < listaEnemigos.Length; i++)
        {
            countObjects += listaEnemigos[i].GetComponent<EnemyController>().getPoints();
        }
    }

    /// <summary>
    /// Set the level's points
    /// </summary>
    private void Start()
    {
        GameManager.instance.setTotalLevel(countObjects);
    }
    #endregion
}
