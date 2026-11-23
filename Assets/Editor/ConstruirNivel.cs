using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class ConstruirNivel
{
    const string Base = "Assets/brackeys_platformer_assets/";

    [MenuItem("Examen/Construir Nivel")]
    static void Construir()
    {
        foreach (var go in Object.FindObjectsByType<Transform>(FindObjectsSortMode.None))
            if (go != null && (go.name == "Jugador" || go.name == "Canvas" || go.name.StartsWith("Plataforma")))
                Object.DestroyImmediate(go.gameObject);

        var plataforma = Sprite("sprites/platforms.png", "platforms_0");
        var knight = Sprite("sprites/knight.png", "knight_0");

        Plataforma(plataforma, -4f, -3f, 12f);
        Plataforma(plataforma, 4.2f, -3f, 12f);
        Plataforma(plataforma, -4f, -0.6f, 6f);
        Plataforma(plataforma, 4.2f, -0.6f, 6f);

        var canvas = new GameObject("Canvas", typeof(Canvas));
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var texto = new GameObject("Mensaje", typeof(Text)).GetComponent<Text>();
        texto.transform.SetParent(canvas.transform, false);
        texto.rectTransform.sizeDelta = new Vector2(600, 200);
        texto.text = "chale";
        texto.font = AssetDatabase.LoadAssetAtPath<Font>(Base + "fonts/PixelOperator8-Bold.ttf");
        texto.fontSize = 80;
        texto.alignment = TextAnchor.MiddleCenter;
        texto.color = Color.red;
        texto.gameObject.SetActive(false);

        var jugador = new GameObject("Jugador");
        jugador.transform.position = new Vector3(-5f, -2f);
        jugador.transform.localScale = new Vector3(4f, 4f, 1f);
        jugador.AddComponent<SpriteRenderer>().sprite = knight;
        jugador.AddComponent<Rigidbody2D>().freezeRotation = true;
        jugador.GetComponent<Rigidbody2D>().gravityScale = 3f;
        jugador.AddComponent<BoxCollider2D>();
        var mov = jugador.AddComponent<movimiento>();
        mov.sonidoGolpe = AssetDatabase.LoadAssetAtPath<AudioClip>(Base + "sounds/hurt.wav");
        mov.mensaje = texto;
    }

    static void Plataforma(Sprite sprite, float x, float y, float ancho)
    {
        var go = new GameObject("Plataforma");
        go.transform.position = new Vector3(x, y);
        go.transform.localScale = new Vector3(ancho, 5f, 1f);
        go.AddComponent<SpriteRenderer>().sprite = sprite;
        go.AddComponent<BoxCollider2D>();
    }

    static Sprite Sprite(string ruta, string nombre) =>
        AssetDatabase.LoadAllAssetsAtPath(Base + ruta).OfType<Sprite>().First(s => s.name == nombre);
}
