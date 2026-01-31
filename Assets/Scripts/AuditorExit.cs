using UnityEngine;

public class AuditorExit : MonoBehaviour
{
    public void EndAuditorPhase()
    {
        GameObject.FindAnyObjectByType<GamePhaseManager>().EndAuditorPhase();
    }
}
