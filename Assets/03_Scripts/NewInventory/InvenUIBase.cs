using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenUIBase : MonoBehaviour
{
    [HideInInspector]
    public Canvas canvas;

    public GameObject Show()
    {
        canvas = InvenUIManager.Instance.MainCanvas;

        GameObject obj = Instantiate(gameObject, canvas.transform);
        obj.name = obj.name.Replace("(Clone)", "");

        InvenUIManager.Instance.UI_Obj_List.Add(obj.name, obj.GetComponent<InvenUIBase>());

        return obj;
    }

    public void Hide()
    {
        InvenUIManager.Instance.UI_Obj_List.Remove(name);

        Destroy(gameObject);
    }
}
