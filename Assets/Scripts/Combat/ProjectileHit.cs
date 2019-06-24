using UnityEngine;
using System.Linq;
using System.Collections;

public class ProjectileHit : MonoBehaviour
{
    public float splashRadius = 0;

    public AnimationCurve attenuationCurve = AnimationCurve.Linear(0, 1, 1, 0);

    protected virtual void OnDirectHit(Collider other) { }
    protected virtual void OnSplashHit(Collider other, float influenceFactor) { }

    public void OnCollidedWith(Collider other, string[] allowedTags)
    {
        OnDirectHit(other);

        if (splashRadius <= 0)
            return;

        foreach (var acceptableTarget in Physics.OverlapSphere(transform.position, splashRadius).Where(x => allowedTags.Contains(x.tag) && x != other))
        {
            var distance = (transform.position - acceptableTarget.transform.position).magnitude;
            OnSplashHit(acceptableTarget, attenuationCurve.Evaluate(Mathf.Clamp01(distance / splashRadius)));
        }

    }

}
