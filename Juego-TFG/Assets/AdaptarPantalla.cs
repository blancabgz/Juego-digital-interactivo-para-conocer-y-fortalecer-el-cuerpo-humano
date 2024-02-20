using UnityEngine;
using UnityEngine.UI;

public class AdjustPanelWidth : MonoBehaviour
{
    private void Start()
    {
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();

        // Calcular el ancho total del contenido
        float totalWidth = CalculateTotalWidth(gridLayoutGroup);

        // Ajustar el ancho del panel
        RectTransform panelRectTransform = GetComponent<RectTransform>();
        panelRectTransform.sizeDelta = new Vector2(totalWidth, panelRectTransform.sizeDelta.y);
    }

    private float CalculateTotalWidth(GridLayoutGroup gridLayoutGroup)
    {
        // Calcular el ancho total de una fila
        float totalWidthForRow = (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x) * gridLayoutGroup.constraintCount + gridLayoutGroup.spacing.x;

        // Calcular el número de filas
        int rowCount = Mathf.CeilToInt((float)transform.childCount / gridLayoutGroup.constraintCount);

        // Calcular el ancho total del contenido
        float totalWidth = totalWidthForRow * rowCount;

        return totalWidth;
    }
}
