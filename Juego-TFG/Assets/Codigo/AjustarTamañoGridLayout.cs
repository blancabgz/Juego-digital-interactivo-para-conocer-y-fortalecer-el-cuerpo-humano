using UnityEngine;
using UnityEngine.UI;

public class AutoAdjustGridLayoutCellSize : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;

    void Start()
    {
        // Obtener el RectTransform del contenedor
        RectTransform containerRect = GetComponent<RectTransform>();

        // Obtener el número de columnas y filas en el GridLayoutGroup
        int colCount = gridLayoutGroup.constraintCount;

        // Calcular el ancho y alto disponibles para las celdas, teniendo en cuenta el padding y el spacing
        float availableWidth = containerRect.rect.width - (gridLayoutGroup.padding.left + gridLayoutGroup.padding.right) - (gridLayoutGroup.spacing.x * (colCount - 1));
        float width = availableWidth / colCount;
        float height = width; // Hacer que las celdas sean cuadradas, pero puedes ajustar esto según tus necesidades

        // Establecer el tamaño de la celda en el GridLayoutGroup
        gridLayoutGroup.cellSize = new Vector2(width, height);
    }
}
