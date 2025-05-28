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
            Action<GameManager> actualizarTextos = (juego) => {
                textoTiempoFinal.text = "Tiempo sobrevivido: " + (int)juego.tiempo + " segundos";
                textoEnemigosEliminados.text = "Enemigos eliminados: " + juego.enemigosEliminados;
                textoNivelAlcanzado.text = "Nivel alcanzado: " + juego.nivel;
            };

            actualizarTextos(GameManager.instancia);
        }

    }
}
