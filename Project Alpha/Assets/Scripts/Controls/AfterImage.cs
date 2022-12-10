using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AfterImage : MonoBehaviour
{
    [Tooltip("Number of sprites per distance unit.")]
    [SerializeField] private float rate = 2f;
    [SerializeField] private float lifeTime = 0.2f;
 
    private SpriteRenderer _baseRenderer;
    private bool _isActive = false;
    private float _interval;
    private Vector2 _previousPos;
 
    private void Start()
    {
        _baseRenderer = GetComponent<SpriteRenderer>();
        _interval = 1f / rate;
    }
 
    private void Update()
    {
        if (_isActive && Vector2.Distance(_previousPos, transform.position) > _interval)
        {
            SpawnTrailPart();
            _previousPos = transform.position;
        }
    }
 
    /// <summary>
    /// Call this function to start/stop the trail.
    /// </summary>
    public void Activate(bool shouldActivate)
    {
        if (_isActive == shouldActivate)
        {
            return;
        }
        
        _isActive = shouldActivate;
        if (_isActive)
        {
            _previousPos = transform.position;
        }
    }
 
    private void SpawnTrailPart()
    {
        var trailPart = new GameObject(gameObject.name + " trail part");
 
        // Sprite renderer
        var trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        CopySpriteRenderer(trailPartRenderer, _baseRenderer);
 
        // Transform
        trailPart.transform.position = transform.position;
        trailPart.transform.rotation = transform.rotation;
        trailPart.transform.localScale = transform.lossyScale;
 
        // Fade & Destroy
        StartCoroutine(FadeTrailPart(trailPartRenderer));
    }
 
    private IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        var fadeSpeed = 1 / lifeTime;
 
        while(trailPartRenderer.color.a > 0)
        {
            var color = trailPartRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            trailPartRenderer.color = color;
 
            yield return new WaitForEndOfFrame();
        }
 
        Destroy(trailPartRenderer.gameObject);
    }
 
    private static void CopySpriteRenderer(SpriteRenderer copy, SpriteRenderer original)
    {
        copy.sprite = original.sprite;
        copy.flipX = original.flipX;
        copy.flipY = original.flipY;
        copy.sortingLayerID = original.sortingLayerID;
        copy.sortingLayerName = original.sortingLayerName;
        copy.sortingOrder = original.sortingOrder;
    }
}
