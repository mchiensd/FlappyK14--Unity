using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum soundsGame  
{
    die,
    hit,
    menu,
    point,
    wing,
    rain,
    cheer,
    onsound
}




public class SoundsController : MonoBehaviour
{




    public AudioClip soundDie;
    public AudioClip soundHit;
    public AudioClip soundMenu;
    public AudioClip soundPoint;
    public AudioClip soundWing;
    public AudioClip soundRain;
    public AudioClip soundCheer;
    public AudioClip soundOnSound;





    public static SoundsController instance;




    // Use this for initialization
    void Start()
    {
        instance = this;




    }




    public static void PlaySound(soundsGame currentSound)
    {
        switch (currentSound)
        {
            case soundsGame.die:
                {
                    instance.GetComponent<AudioSource>().PlayOneShot(instance.soundDie);
                }
                break;
            case soundsGame.hit:
                {
                    instance.GetComponent<AudioSource>().PlayOneShot(instance.soundHit);
                    instance.Invoke("PlaySoundDie", 0.3f);
                }
                break;
            case soundsGame.menu:
                {
                    instance.GetComponent<AudioSource>().PlayOneShot(instance.soundMenu);
                }
                break;
            case soundsGame.point:
                {
                    instance.GetComponent<AudioSource>().PlayOneShot(instance.soundPoint);
                }
                break;
            case soundsGame.wing:
                {
                    instance.GetComponent<AudioSource>().PlayOneShot(instance.soundWing);
                }
                break;
            case soundsGame.cheer:
                {
                    instance.GetComponent<AudioSource>().PlayOneShot(instance.soundCheer);
                }
                break;
            case soundsGame.onsound:
                {
                    instance.GetComponent<AudioSource>().PlayOneShot(instance.soundOnSound);
                }
                break;

        }
    }






}