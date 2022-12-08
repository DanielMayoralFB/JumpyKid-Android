using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boton : MonoBehaviour
{
    #region Variables
    [SerializeField] private string Escena;
    [SerializeField] private Color[] colores;
    [SerializeField] private Image botonImage;
    [SerializeField] private bool selected = false;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        botonImage = GetComponent<Image>();
        botonImage.color = colores[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            botonImage.color = colores[1];
        }
        else
        {
            botonImage.color = colores[0];
        }

        if(Input.GetKey("enter") && selected)
        {
            SceneManager.LoadScene(Escena);
        }
    }
    #endregion

    #region Getters y Setters
    public bool getIsSelected() { return selected; }
    public void setSelected(bool newValue) { selected = newValue; }
    #endregion
}
