using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PantallaDerrota : MonoBehaviour
{
    public TextMeshProUGUI textoTiempoFinal;
    public TextMeshProUGUI textoEnemigosEliminados;
    public TextMeshProUGUI textoNivelAlcanzado;

    void Start()
    {
        if (GameManager.instancia != null)
        {
            Action<GameManager> actualizarTextos = (gm) => {
                textoTiempoFinal.text = "Tiempo sobrevivido: " + (int)gm.tiempo + " segundos";
                textoEnemigosEliminados.text = "Enemigos eliminados: " + gm.enemigosEliminados;
                textoNivelAlcanzado.text = "Nivel alcanzado: " + gm.nivel;
            };

            actualizarTextos(GameManager.instancia);
        }
    }
}
