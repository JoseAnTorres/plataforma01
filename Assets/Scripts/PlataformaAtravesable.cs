using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class PlataformaAtravesable : MonoBehaviour
{
    private GameObject jugador;
    private BoxCollider2D plataforma;
    private float baseJugador;
    private float alturaJugador;
    private float cimaPlataforma;
    private bool haciendoAtravesable = false;
    private const float factorCorreccion = 0.1f;
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
       
        plataforma = GetComponent<BoxCollider2D>();
        alturaJugador = plataforma.bounds.size.y;
        var limites = plataforma.bounds;
        cimaPlataforma = limites.center.y + limites.size.y / 2.0f;
    }
    private void Update() {
        baseJugador = jugador.transform.position.y - alturaJugador / 2.0f;
        if (!haciendoAtravesable && baseJugador >= cimaPlataforma - factorCorreccion)
        {
            plataforma.isTrigger = false;
        } 
        if (baseJugador < cimaPlataforma - factorCorreccion)
        {
            plataforma.isTrigger = true;
            haciendoAtravesable = false;
        }
    }
    public void HacerAtravesable()
    {
        haciendoAtravesable = true;
        plataforma.isTrigger = true;
    }
}
