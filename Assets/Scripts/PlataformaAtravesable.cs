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
    private Color colorGizmos = Color.red;
    private bool haciendoAtravesable = false;
    private const float factorCorreccion = 0.1f;
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        alturaJugador = jugador.GetComponent<SpriteRenderer>().bounds.size.y;
        plataforma = GetComponent<BoxCollider2D>();
        var limites = GetComponent<SpriteRenderer>().bounds;
        cimaPlataforma = limites.center.y + limites.size.y / 2.0f;
    }
    private void Update() {
        baseJugador = jugador.transform.position.y - alturaJugador / 2.0f;
        if (!haciendoAtravesable && baseJugador >= cimaPlataforma - factorCorreccion)
        {
            plataforma.isTrigger = false;
            colorGizmos = Color.green;
        } 
        if (baseJugador < cimaPlataforma - factorCorreccion)
        {
            plataforma.isTrigger = true;
            colorGizmos = Color.red;
            haciendoAtravesable = false;
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = colorGizmos;
        Gizmos.DrawCube((Vector3)transform.position, plataforma.bounds.size);
    }
    public void HacerAtravesable()
    {
        haciendoAtravesable = true;
        plataforma.isTrigger = true;
        colorGizmos = Color.red;
    }
}
