using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : Singleton<Cursor>
{
    [SerializeField] private Texture2D cursor;

    private void Start()
    {
        UnityEngine.Cursor.SetCursor(cursor, Vector3.zero, CursorMode.Auto);
    }
}
