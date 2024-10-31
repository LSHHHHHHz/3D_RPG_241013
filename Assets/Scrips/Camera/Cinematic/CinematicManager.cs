using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private CinematicTrack[] cinematicTrack;
    public void ActionCinematic(int num)
    {
        cinematicTrack[num].gameObject.SetActive(true);
    }
}
