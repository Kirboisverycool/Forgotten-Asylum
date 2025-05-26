using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProccesingInteracting : MonoBehaviour
{
    Volume volume;
    DepthOfField dof;
    FilmGrain fm;
    [SerializeField] List<GameObject> uiToHide;
    private void Start()
    {
        volume = GetComponent<Volume>();
       
        volume.profile.TryGet(out dof);
        volume.profile.TryGet(out fm);
    }

    public void ToggleBlur(bool state)
    {
        dof.active = state;
        for(int i =0; i < uiToHide.Count; i ++) 
        {
            uiToHide[i].SetActive(!state);
        }

    }
    public void ToggleFilmGrain(bool state)
    {
        fm.active = state;
    }
}
