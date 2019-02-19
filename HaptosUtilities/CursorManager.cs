using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HaptosUtilities
{
    public class CursorManager : MonoBehaviour
    {
        //Custom cursor texture
        public Texture2D cursorTexture;

        void Awake()
        {
            Cursor.SetCursor(cursorTexture, new Vector2(16, 16), CursorMode.Auto);
            DisableCursor();
        }

        //Enable the cursor
        public void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Disable the cursor
        public void DisableCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
}
