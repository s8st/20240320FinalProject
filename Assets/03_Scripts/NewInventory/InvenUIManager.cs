using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvenUIManager : MonoBehaviour
{
    public static InvenUIManager Instance;

    public Canvas MainCanvas;

    [SerializeField] private List<InvenUIBase> uiList = new List<InvenUIBase>();

    public Dictionary<string, InvenUIBase> UI_List = new Dictionary<string, InvenUIBase>();
    public Dictionary<string, InvenUIBase> UI_Obj_List = new Dictionary<string, InvenUIBase>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Init();
    }

    void Init()
    {
        foreach (InvenUIBase ui in uiList)
        {
            UI_List.Add(ui.name, ui);
        }

        uiList.Clear();
    }

    public GameObject Show(string uiName)
    {
        if (!UI_List.ContainsKey(uiName))
            return null;

        InvenUIBase ui = UI_List[uiName];

        GameObject obj = ui.Show();

        return obj;
    }

    public void Hide(string uiName)
    {
        if (!UI_Obj_List.ContainsKey(uiName))
            return;

        InvenUIBase ui = UI_Obj_List[uiName];

        ui.Hide();
    }
}
