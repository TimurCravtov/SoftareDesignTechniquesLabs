namespace Laboratory.GameEntities.Ammo;

public interface IBulletPoolManager
{
    Bullet GetBullet();
    void ReturnBullet(Bullet bullet);
}