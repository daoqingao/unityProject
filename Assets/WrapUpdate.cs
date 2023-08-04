using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WrapUpdate : MonoBehaviour
    {
        private void Update()
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            Vector3 moveAdj = Vector3.zero;
            if (viewportPos.x < 0)
            {
                moveAdj.x += 1;
            }
            else if (viewportPos.x > 1)
            {
                moveAdj.x -= 1;
            }
            else if (viewportPos.y < 0)
            {
                moveAdj.y += 1;
            }
            else if (viewportPos.y > 1)
            {
                moveAdj.y -= 1;
            }

            transform.position = Camera.main.ViewportToWorldPoint(viewportPos + moveAdj);
        }
    }
