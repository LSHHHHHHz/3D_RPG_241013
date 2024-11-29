using System.Collections;
using UnityEngine;

public class CinematicActorWall : CinematicActor
{
    public Player player;
    public Transform[] walls;
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3 && Input.GetMouseButtonDown(1))
        {
            WallUP();
        }
    }
    public override void PerformAction()
    {
        StartCoroutine(MoveWallsUp());
    }
    public void WallUP()
    {
        StopAllCoroutines();
        StartCoroutine(MoveWallsUp());
    }
    private IEnumerator MoveWallsUp()
    {
        float duration = 1f;
        float targetHeight = 5f;
        Vector3[] startPositions = new Vector3[walls.Length];
        Vector3[] endPositions = new Vector3[walls.Length];

        for (int i = 0; i < walls.Length; i++)
        {
            startPositions[i] = walls[i].position;
            endPositions[i] = startPositions[i] + new Vector3(0, targetHeight, 0);
        }

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].position = Vector3.Lerp(startPositions[i], endPositions[i], t);
            }

            yield return null;
        }
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].position = endPositions[i];
        }
    }
}
