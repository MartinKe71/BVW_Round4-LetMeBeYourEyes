using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] List<PlayableDirector> _timelines;

    public bool PlayAtAwake = false;

    private Queue<PlayableDirector> _myQueue = new Queue<PlayableDirector>();

    PlayableDirector curTimeline = null;

    bool isPlaying;

    int PlayIndex;

    // Start is called before the first frame update
    void Start()
    {
        foreach(PlayableDirector timeline in _timelines)
        {
            _myQueue.Enqueue(timeline);
        }
        
        curTimeline = null;
        isPlaying = false;
        PlayIndex = 0;

        //Play the first timeline on awake
        if (PlayAtAwake) {
            PlayNext(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curTimeline != null) {
            if (curTimeline.state == PlayState.Playing) {
                isPlaying = true;
            } else {
                isPlaying = false;
            }
        }else {
            isPlaying = false;
        }

        if (Input.GetMouseButtonDown(0) && _myQueue.Count > 0 && !isPlaying)
        {
            curTimeline = _myQueue.Dequeue();
            curTimeline.Play();
            PlayIndex++;
        }
    }

    public bool PlayNext(int targetIndex){
        if (_myQueue.Count > 0 && !isPlaying && targetIndex == PlayIndex)
        {
            curTimeline = _myQueue.Dequeue();
            curTimeline.Play();
            PlayIndex++;
            return true;
        }
        return false;
    }
}
