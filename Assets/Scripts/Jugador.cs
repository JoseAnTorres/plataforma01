using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Jugador : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float fuerzaSalto;

    private Rigidbody2D cuerpo;
    private LayerMask suelo;
    private Collider2D armadura;

    private Vector2 movimiento;

    private bool estaEnSuelo;

    private Animator animador;
    private SpriteRenderer sprite;
    private Collider2D plataformaActiva;
    // Start is called before the first frame update
    private void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        armadura = GetComponent<CapsuleCollider2D>();
        suelo = LayerMask.GetMask("Suelo");
    }
    private void Update()
    {
        if (estaEnSuelo)
        {
            switch (cuerpo.velocity.x)
            {
                case > 0:
                    sprite.flipX = false;
                    animador.SetBool("estaCorriendo", true);
                    break;
                case < 0:
                    sprite.flipX = true;
                    animador.SetBool("estaCorriendo", true);
                    break;
                default:
                    animador.SetBool("estaCorriendo", false);
                    break;
            }
        }
        estaEnSueloLayer();
    }

    private void estaEnSueloLayer()
    {
        estaEnSuelo = armadura.IsTouchingLayers(suelo);
        animador.SetBool("estaSaltando", !estaEnSuelo);
        if (!estaEnSuelo)
        {
            plataformaActiva = null;
        }
    }
    /*
    private void ComprobarEstaEnSuelo()
    {
        estaEnSuelo = cuerpo.IsTouchingLayers(suelo);
        animador.SetBool("estaSaltando", !estaEnSuelo);
    }
    */
    private void FixedUpdate()
    {
        if (estaEnSuelo) {
            cuerpo.velocity = new Vector2(movimiento.x, cuerpo.velocity.y);
        }
        
    }
    private void OnMover(InputValue entrada)
    {
        movimiento = new Vector2(entrada.Get<Vector2>().x * velocidad, entrada.Get<Vector2>().y);
        if(movimiento.y < 0)
        {
            IntentarAtravesar();
        }
    }
    private void OnSaltar()
    {
        if (estaEnSuelo)
        {
            cuerpo.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }
    private void IntentarAtravesar()
    {
        if (plataformaActiva != null)
        {
            plataformaActiva.GetComponent<PlataformaAtravesable>().HacerAtravesable();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PlataformaAtravesable")
        { 
            plataformaActiva = other.collider;
        }
    }
}
