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

    private Vector2 movimiento;

    private bool estaEnSuelo;

    private Animator animador;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    private void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        suelo = LayerMask.NameToLayer("Suelo");
    }
    private void Update()
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
        ComprobarEstaEnSuelo();
    }
    private void ComprobarEstaEnSuelo()
    {
        estaEnSuelo = cuerpo.IsTouchingLayers(suelo);
        animador.SetBool("estaSaltando", !estaEnSuelo);
    }
    private void FixedUpdate()
    {
        cuerpo.velocity = new Vector2(movimiento.x, cuerpo.velocity.y);
    }
    private void OnMover(InputValue entrada)
    {
        movimiento = new Vector2(entrada.Get<Vector2>().x * velocidad, entrada.Get<Vector2>().y);
    }
    private void OnSaltar()
    {
        if (estaEnSuelo)
        {
            cuerpo.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }
}
