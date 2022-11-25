using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vocals : MonoBehaviour
{
    private AudioSource source;

    public static Vocals instance; 

	private void Awake()
	{
        instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

	public void Say(AudioObject clip)
	{
		if (source.isPlaying)
		{
			source.Stop();
		}
		source.PlayOneShot(clip.clip);
		 
		UI.instance.SetSubtitle(clip.subtitle, clip.clip.length);
	}
	
#if UNITY_EDITOR
	private void Update()
	{
		if (source.isPlaying && Keyboard.current.fKey.isPressed)
			source.Stop();
	}
#endif
}
