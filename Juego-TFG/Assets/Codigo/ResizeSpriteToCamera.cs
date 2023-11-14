using UnityEngine;

public class AutoResizeSpriteToCamera : MonoBehaviour
{
    private void Start()
    {
        // Obtenemos el tamaño de la cámara
        Camera cam = Camera.main;


        if (cam != null)
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            Debug.Log(height);
            Debug.Log(width);

            // Obtenemos el componente SpriteRenderer del fondo
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // Establecemos el tamaño del sprite para que coincida con el tamaño de la cámara
                spriteRenderer.size = new Vector2(1, 1);
                Debug.Log(spriteRenderer.sprite.bounds.size.x);
                Debug.Log(spriteRenderer.sprite.bounds.size.y);
            }
        }
    }
}
