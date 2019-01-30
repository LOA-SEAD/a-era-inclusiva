using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaTitulo : MonoBehaviour
{
   public void IniciarJogo()
    {
        SceneManager.LoadScene("Scenes/MenuPrincipal");
    }
}
