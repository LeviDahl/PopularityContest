using UnityEngine;
using System.Collections;

public class MainUIAccessor : MonoBehaviour {
   public GameObject m_mainMenuPrefab;
   GameObject m_mainMenuObject;
   public GameObject m_FBLoginPrefab;
   GameObject m_FBLoginObject;
    
    private static MainUIAccessor mInstance;
    
    void Awake()
    {
        if (mInstance != null)
        {
           DestroyImmediate(mInstance);
           return;
        }
        mInstance = this;
       
    }
  
    public static MainUIAccessor Instance
    {
        get
        {
            return mInstance;
        }
    } 
	// Use this for initialization
	void Start () {
	   GameUIManager.Instance.RegisterForState(MainUIState.MainMenu, OnEnterMainMenu, OnExitMainMenu);
        GameUIManager.Instance.RegisterForState(MainUIState.FBLogin, OnEnterFBLogin, OnExitFBLogin);
       GameUIManager.Instance.TransitionToState(MainUIState.MainMenu);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnEnterMainMenu(MainUIState state)
    {
        m_mainMenuObject = GameObject.Instantiate(m_mainMenuPrefab) as GameObject;
        m_mainMenuObject.transform.SetParent(gameObject.transform);
        m_mainMenuObject.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        m_mainMenuObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    void OnExitMainMenu(MainUIState state)
    {
        Destroy(m_mainMenuObject);
    }
      void OnEnterFBLogin(MainUIState state)
    {
        m_FBLoginObject = GameObject.Instantiate(m_FBLoginPrefab) as GameObject;
        m_FBLoginObject.transform.parent = gameObject.transform;
        m_FBLoginObject.transform.localScale = Vector3.one;            
    }
    void OnExitFBLogin(MainUIState state)
    {
        Destroy(m_FBLoginObject);
    }
}
