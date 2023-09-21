using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ExperimentController : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector[] director;
    public bool played;
    private int index;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void PlayExperiment( int index)
    {
        director[index].Play();
    }


 

}
