using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActorManager<T> where T : Actor
{
    private static ActorManager<T> _instnace;
    public static ActorManager<T> instnace
    {
        get
        {
            if (_instnace == null)
            {
                _instnace = new ActorManager<T>();
            }
            return _instnace;
        }
    }
    List<T> actors = new List<T>();

    public void RegisterActor(T actor)
    {
        actors.Add(actor);
    }
    public void UnregisterActor(T actor)
    {
        if (actors.Contains(actor))
        {
            actors.Remove(actor);
        }
    }
    public void ClearAllActor()
    {
        foreach (var actor in actors)
        {
            if (actor.gameObject.activeSelf)
            {
                actor.gameObject.SetActive(false);
            }
        }
    }
    public IReadOnlyList<T> GetActors()
    {
        return actors;
    }
}