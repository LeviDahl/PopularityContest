using UnityEngine;
using System.Collections;
public enum MainUIState
    {
        MainMenu = 0,
        FBLogin = 1
    }
public class GameUIManager : MonoBehaviour {
    
    public delegate void OnStateBegin(MainUIState state);
    public delegate void OnStateEnd(MainUIState state);
    
    [SerializeField] MainUIState m_currentState = MainUIState.MainMenu;
	[SerializeField] MainUIState m_prevState = MainUIState.MainMenu;
    
    class UIState
    {
        public OnStateBegin m_begin;
        public OnStateEnd m_end;
        public UIState(){}
    }
    UIState[] m_UIStates;
	
    private static GameUIManager mInstance;
    
    void Awake()
    {
       
     
    }
  
    public GameUIManager()
    {
          int max = System.Enum.GetNames (typeof(MainUIState)).Length;
		m_UIStates = new UIState[max];
		for (int i = 0; i <max; i++) 
		{
			m_UIStates [i] = new UIState();
		}
        Debug.Log("Count of m_uistates" + m_UIStates.Length);
    }
    public static GameUIManager Instance
    {
        get
        {
            if (mInstance == null)
                 mInstance =(GameUIManager) FindObjectOfType(typeof(GameUIManager));
            return mInstance;
        }
    } 
	
	  public void RegisterForState(MainUIState registeredState, OnStateBegin begin, OnStateEnd end)
	{
		m_UIStates [(int)registeredState].m_begin += begin;
		m_UIStates [(int)registeredState].m_end += end;
	}
	public void UnRegisterForState(MainUIState registeredState, OnStateBegin begin, OnStateEnd end)
	{
		m_UIStates [(int)registeredState].m_begin -= begin;
		m_UIStates [(int)registeredState].m_end -= end;
	}
	public void TransitionToState(MainUIState toState)
	{
          Debug.Log("Current State: " + toState.ToString());
		m_prevState = m_currentState;
		if (m_UIStates [(int)m_prevState].m_end != null)
			m_UIStates [(int)m_prevState].m_end (m_prevState);
		m_currentState = toState;
		m_UIStates [(int)m_currentState].m_begin (m_currentState);
	}
    void KillAllDelegates()
	{
		for (int i = 0; i < m_UIStates.Length; i++) 
		{
			m_UIStates[i].m_begin = null;
			m_UIStates[i].m_end = null;
		}
	}
	void Start () {
	  
	}
	void Update () {
	
	}
     void OnApplicationQuit()
	{
		KillAllDelegates ();
	}
}
