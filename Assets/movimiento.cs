using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class movimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    public AudioClip sonidoGolpe;
    public Text mensaje;

    Rigidbody2D rb;
    bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var kb = Keyboard.current;
        float dir = 0f;
        if (kb.aKey.isPressed) dir = 1f;
        if (kb.leftArrowKey.isPressed) dir = -1f;
        rb.linearVelocity = new Vector2(dir * velocidad, rb.linearVelocity.y);

        if (kb.vKey.wasPressedThisFrame && enSuelo)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);

        if (transform.position.y < -6f && !mensaje.gameObject.activeSelf)
        {
            mensaje.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, -4f));
            mensaje.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.GetContact(0).normal.y < -0.5f)
            AudioSource.PlayClipAtPoint(sonidoGolpe, transform.position);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        enSuelo = col.GetContact(0).normal.y > 0.5f;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        enSuelo = false;
    }
}
