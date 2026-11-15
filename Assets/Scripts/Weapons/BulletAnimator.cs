using UnityEngine;

public class BulletAnimator : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetDestroyAnimationTime()
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Death")
            {
                return clip.length;
            }
        }
        return 0;
    }
    public void DoDestroyAnimation()
    {
        animator.SetBool("Destroyed", true);
    }
}
