namespace Laboratory.GameEntities.Ammo;

public class BulletPoolManager: IBulletPoolManager
{
    private int POOL_SIZE = 50;
    private readonly Queue<Bullet> _bulletPool = new Queue<Bullet>();

    public BulletPoolManager()
    {
        foreach (int i in Enumerable.Range(0, POOL_SIZE))
        {
            Bullet bullet = new Bullet();
            bullet.Deactivate();
            _bulletPool.Enqueue(bullet);
        }        
    }
    
    public Bullet GetBullet()
    {
        return _bulletPool.Count > 0 ? _bulletPool.Dequeue() : new Bullet();
    }
    
    public void ReturnBullet(Bullet bullet)
    {
        if (_bulletPool.Count < POOL_SIZE) 
        {
            _bulletPool.Enqueue(bullet);
        }
    }
}