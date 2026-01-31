using UnityEngine;

public class HeroTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject.FindAnyObjectByType<GamePhaseManager>().EndHeroPhase();
            gameObject.SetActive(false);
        }
    }
}
