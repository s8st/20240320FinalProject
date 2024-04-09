using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Status
{
    public int atk;
    public int def;
    public int str;
    public int crt;

    public Status(int atk, int def, int str, int crt)
    {
        this.atk = atk;
        this.def = def;
        this.str = str;
        this.crt = crt;
    }
}

[System.Serializable]
public class UserData
{
    public string Name;
    public int Level;
    public int NowExp;
    public int FullExp;
    public Status Stat;
    public List<InvenItem> Inventory = new List<InvenItem>();

    public Status GetAllStat()
    {
        Status status = new Status(Stat.atk, Stat.def, Stat.str, Stat.crt);

        foreach (InvenItem item in Inventory)
        {
            if (item.IsEquipped)
            {
                status.atk += item.Data.Stat.atk;
                status.def += item.Data.Stat.def;
                status.str += item.Data.Stat.str;
                status.crt += item.Data.Stat.crt;
            }
        }

        return status;
    }
}

[System.Serializable]
public class InvenItem
{
    public bool IsEquipped;
    public InvenItemData Data;
}

public class InvenManager : MonoBehaviour
{
    public static InvenManager Instance;

    public UserData User;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

     //   LoadUserData("Bubble");
    }

    private void Start()
    {
        InvenUIManager.Instance.Show("PopupMain");
    }

    public void SaveUserData()
    {
        string data = JsonUtility.ToJson(User, true);

        string path = Path.Combine(Application.dataPath, User.Name + ".json");

        File.WriteAllText(path, data);

        Debug.Log("저장 완료 : " + path);
    }

    //public void LoadUserData(string userName)
    //{
    //    string path = Path.Combine(Application.dataPath, userName + ".json");

    //    string data = File.ReadAllText(path);

    //    User = JsonUtility.FromJson<UserData>(data);

    //    Debug.Log("로드 완료 : " + path);
    //}
}
