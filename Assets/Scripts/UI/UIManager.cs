using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonobehaviour<UIManager>
{
    private Transform _uiRoot;
    //配置路径字典
    private Dictionary<string,string> pathDict;
    //预制体缓存字典
    private Dictionary<string ,GameObject> prefabDict;
    //已打开界面缓存字典
    public Dictionary<string,BasePanel> panelDict;

    public Transform UIRoot
    {
        get
        {
            if(_uiRoot == null)
            {
                if(GameObject.Find("Canvas"))
                {
                    _uiRoot = GameObject.Find("Canvas").transform;
                }
                else
                {
                    _uiRoot = new GameObject("Canvas").transform;
                }
            };
            return _uiRoot;
        }
    }
    
    private UIManager()
    {
        InitDicts();
    }

    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();

        pathDict = new Dictionary<string, string>()
        {
            {UIConst.LevelPanel,"LevelPanel"},
        };
    }

    public BasePanel GetPanel(string name)
    {
        BasePanel panel = null;
        if (panelDict.TryGetValue(name, out panel))
        {
            return panel;
        }
        return null;
    }

    public BasePanel OpenPanel(string name)
    {
        //检查界面是否打开
        BasePanel panel = null;
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("界面已打开：" + name);
            return null;
        }

        //检查路径是否配置
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.Log("界面名称错误，或者未配置路径：" + name);
            return null;
        }

        //使用缓存的预制件
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realpath = "Prefab/UI/" + path;
            panelPrefab = Resources.Load<GameObject>(realpath) as GameObject;
            prefabDict.Add(name, panelPrefab);
        }

        //打开界面
        GameObject panelObject = GameObject.Instantiate(panelPrefab,UIRoot,false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;

    }

    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("界面未打开："+ name);
            return false;
        }

        panel.ClosePanel();
        return true;
    }
}

public class UIConst
{
    public const string LevelPanel = "LevelPanel";
}