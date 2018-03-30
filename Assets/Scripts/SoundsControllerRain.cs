using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum soundsGameRain
{
    Fade

  
}




public class SoundsControllerRain : MonoBehaviour
{




    public AudioClip soundFade;





    public static SoundsControllerRain instance;




    // Use this for initialization
    void Start()
    {
        instance = this;




    }




    public static void PlaySound(soundsGameRain currentSound)
    {
        switch (currentSound)
        {
            case soundsGameRain.Fade:
                {
                    instance.GetComponent<AudioSource>().PlayOneShot(instance.soundFade);
                }
                break;
           
         

        }
    }






}