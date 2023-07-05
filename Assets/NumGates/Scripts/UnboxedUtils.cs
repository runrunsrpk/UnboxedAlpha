using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public static class UnboxedUtils
    {
        public static Vector2 CalculateSpriteSize(GameObject target)
        {
            Vector2 size = Vector2.zero;

            SpriteRenderer renderer = target.GetComponentInChildren<SpriteRenderer>();

            if(renderer != null)
            {
                Camera camera = Camera.main;

                Vector3 boundsMin = renderer.bounds.min;
                Vector3 boundsMax = renderer.bounds.max;

                Vector3 screenMin = camera.WorldToScreenPoint(boundsMin);
                Vector3 screenMax = camera.WorldToScreenPoint(boundsMax);

                size = new Vector2(screenMax.x - screenMin.x, screenMax.y - screenMin.y);
            }

            return size;
        }

        public static Vector2 CalculateUISize(Transform target, float offset)
        {
            Vector2 size = Vector2.zero;

            if (target.TryGetComponent(out RectTransform rectTransform))
            {
                Canvas root = target.GetComponentInParent<Canvas>();
                RectTransform rootRectTransform = root.GetComponent<RectTransform>();
                Vector2 scale = new Vector2(rootRectTransform.localScale.x, rootRectTransform.localScale.y);

                float width = ( rectTransform.rect.width + offset ) * scale.x;
                float height = ( rectTransform.rect.height + offset ) * scale.y;
                size = new Vector2(width, height);
            }

            return size;
        }
    }
}

