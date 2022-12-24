using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class ProjectileGraphicsController : ProjectileComponent<ProjectileGraphicsControllerData>
    {
        private SpriteRenderer _spriteRenderer;

        public override void SetReferences()
        {
            base.SetReferences();
            _spriteRenderer = transform.Find("Graphics").GetComponent<SpriteRenderer>();
            Data = Projectile.Data.GetComponentData<ProjectileGraphicsControllerData>();
            _spriteRenderer.sprite = Data.ProjectileSprite;
        }
    }

    public class ProjectileGraphicsControllerData : ProjectileComponentData
    {
        public ProjectileGraphicsControllerData()
        {
            ComponentDependencies.Add(typeof(ProjectileGraphicsController));
        }

        public Sprite ProjectileSprite;
    }
}