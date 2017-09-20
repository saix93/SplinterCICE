using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECursorType
{
    None,
    Attack,
    Forbidden,
    Interact
}

[CreateAssetMenu(fileName = "CursorManager", menuName = "CursorManager", order = 0)]
public class CursorManager : ScriptableObject
{
    [SerializeField]
    Texture2D attackCursor;
    [SerializeField]
    Texture2D forbiddenCursor;
    [SerializeField]
    Texture2D interactCursor;

    ECursorType cursorType;

    public void SetCursorType(ECursorType newCursorType)
    {
        if (cursorType != newCursorType)
        {
            cursorType = newCursorType;
            Texture2D newCursor = null;
            Vector2 newCursorWidth;

            switch (cursorType)
            {
                case ECursorType.Attack:
                    newCursor = attackCursor;
                    break;
                case ECursorType.Forbidden:
                    newCursor = forbiddenCursor;
                    break;
                case ECursorType.Interact:
                    newCursor = interactCursor;
                    break;
            }
            newCursorWidth = newCursor != null ? new Vector2(newCursor.width / 2, newCursor.height / 2) : Vector2.zero;

            Cursor.SetCursor(newCursor, newCursorWidth, CursorMode.Auto);
        }
    }
}
