using UnityEngine;
using UnityEngine.UI;

public class BlinkOutline : MonoBehaviour
{
    public Outline outline;

    [Header("Cor do highlight")]
    public Color highlightColor = new Color(1f, 0.85f, 0f, 1f);

    [Header("Piscar")]
    public float speed = 3f;
    public float minAlpha = 0.25f;
    public float maxAlpha = 1f;

    [Header("Tamanho do contorno")]
    public Vector2 minDistance = new Vector2(3f, -3f);
    public Vector2 maxDistance = new Vector2(7f, -7f);

    private void Awake()
    {
        if (outline == null)
        {
            outline = GetComponent<Outline>();
        }
    }

    private void Update()
    {
        if (outline == null)
        {
            return;
        }

        float pulse = (Mathf.Sin(Time.time * speed) + 1f) / 2f;

        Color color = highlightColor;
        color.a = Mathf.Lerp(minAlpha, maxAlpha, pulse);

        outline.effectColor = color;
        outline.effectDistance = Vector2.Lerp(minDistance, maxDistance, pulse);
    }
}