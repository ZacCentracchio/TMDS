using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DemoShake : MonoBehaviour
{
    public void Shake(float duration, float strength)
    {
        // Note that the third parameter is the number of vibrato, which controls how much the shake will "jump". 
        // Feel free to adjust this value to get the desired effect.
        transform.DOShakePosition(duration, new Vector3(strength, strength, 0f), 15);
    }
    /*
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        Quaternion originalRot = transform.localRotation;
        float elapsed = 0.0f;
        float resetTime = 0.5f; // Adjust as needed for smooth reset

        while (elapsed < duration)
        {
            float randomMagnitude = Random.Range(magnitude - 1.5f, magnitude + 1.5f);
            float randomDuration = Random.Range(duration - 0.05f, duration + 0.05f);

            float x = Random.Range(-1f, 1f) * randomMagnitude;
            float y = Random.Range(-1f, 1f) * randomMagnitude;

            Vector3 newPosition = originalPos + new Vector3(x, y, 0f);
            Quaternion newRotation = Quaternion.Euler(0f, 0f, Random.Range(-5f, 5f) * randomMagnitude); // Adjust rotation range as needed

            transform.localPosition = newPosition;
            transform.localRotation = newRotation;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Smoothly reset to original position and rotation
        float resetElapsed = 0f;
        Vector3 targetPos = originalPos;
        Quaternion targetRot = originalRot;
        while (resetElapsed < resetTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, resetElapsed / resetTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, resetElapsed / resetTime);
            resetElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos; // Ensure exact reset
        transform.localRotation = originalRot; // Ensure exact reset
    }
    */

}
