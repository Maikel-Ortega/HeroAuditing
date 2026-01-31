using System.Buffers;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static GameFSM;

public class GamePhaseManager : MonoBehaviour
{

    public GameST_Hero st_Hero = new GameST_Hero();
    public GameST_Auditor st_Auditor = new GameST_Auditor();
    public GameST_Resolution st_Resolution = new GameST_Resolution();
    public GameFSM fsm;

    public GameObject heroGameObject;
    public GameObject auditorGameObject;
    public GameObject resolutionPanelGameObject;

    private void Start()
    {
        fsm = new GameFSM(this, st_Hero);
    }

    public void Update()
    {
        fsm.DoUpdate(Time.deltaTime);
    }

    public void EndHeroPhase()
    {
        fsm.ChangeState(st_Auditor);
    }

    public void EndAuditorPhase()
    {
        fsm.ChangeState(st_Resolution);
    }

    public void EndResolutionPhase()
    {
        Debug.Log("GAME END! Roll credits");
    }

    public void MakeRandomChanges()
    {
        var mgr = FindFirstObjectByType<GameStateManager>();
        mgr.SaveGameState();
        mgr.MakeRandomChanges(1);
    }

    public List<ScriptableID> GetRealContradictions()
    {
        var mgr = FindFirstObjectByType<GameStateManager>();
        List<ScriptableID> differences = mgr.GetDifferencesFromSavedState();
        foreach (var item in differences)
        {
            Debug.Log("Contradiction detected is" + mgr.FindById(item));
        }
        return differences;
    }
}

public class GameFSM
{

    public GameFSMState currentState;
    private GamePhaseManager owner;
    public GameFSM(GamePhaseManager _owner, GameFSMState _initialState)
    {
        owner = _owner;
        currentState = _initialState;
        currentState.OnEnter(this);
    }

    public void DoUpdate(float deltaTime)
    {
        currentState.OnUpdate(this, deltaTime);
    }

    public void ChangeState(GameFSMState newState)
    {
        currentState.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }


    public class GameFSMState
    {
        public GameFSMState()
        {
        }

        public virtual void OnEnter(GameFSM fsm)
        {
        }

        public virtual void OnUpdate(GameFSM fsm, float deltaTime)
        {
        }

        public virtual void OnExit(GameFSM fsm)
        {
        }
    }

    #region GAME_STATES
    public class GameST_Hero : GameFSMState
    {
        public override void OnEnter(GameFSM fsm)
        {
            base.OnEnter(fsm);
            Debug.Log("Start Hero Phase");
            fsm.owner.auditorGameObject.SetActive(false);
            fsm.owner.heroGameObject.SetActive(true);

        }

        public override void OnUpdate(GameFSM fsm, float deltaTime)
        {
            base.OnUpdate(fsm, deltaTime);

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                fsm.owner.EndHeroPhase();
            }
        }

        public override void OnExit(GameFSM fsm)
        {
            base.OnExit(fsm);
            fsm.owner.heroGameObject.SetActive(false);

        }
    }

    public class GameST_Auditor : GameFSMState
    {
        public override void OnEnter(GameFSM fsm)
        {
            base.OnEnter(fsm);

            Debug.Log("Start Auditor Phase");
            fsm.owner.auditorGameObject.SetActive(true);

            fsm.owner.MakeRandomChanges();

            // Desactivar todos los goblins vivos
            foreach(var gob in GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID))
            {
                if (gob.entityData.blackboard.GetBool("ALIVE"))
                    gob.SetDialogueState(); 
            }
        }

        public override void OnUpdate(GameFSM fsm, float deltaTime)
        {
            base.OnUpdate(fsm, deltaTime);
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                fsm.owner.EndAuditorPhase();
            }

        }

        public override void OnExit(GameFSM fsm)
        {
            base.OnExit(fsm);
        }
    }

    public class GameST_Resolution : GameFSMState
    {
        public override void OnEnter(GameFSM fsm)
        {
            base.OnEnter(fsm);
            Debug.Log("Start Resolution Phase");
            fsm.owner.resolutionPanelGameObject.SetActive(true); 

        }

        public override void OnUpdate(GameFSM fsm, float deltaTime)
        {
            base.OnUpdate(fsm, deltaTime);
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                fsm.owner.EndResolutionPhase();
            }
        }

        public override void OnExit(GameFSM fsm)
        {
            base.OnExit(fsm);

            fsm.owner.resolutionPanelGameObject.SetActive(false);
        }
    }

    #endregion
}