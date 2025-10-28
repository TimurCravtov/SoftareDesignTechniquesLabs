// namespace Laboratory.Game;
//
// using Characters.Enemies;
//
// public class RoomLevel()
// {
//     public RoomEnemySource EnemySource;
// }
//
// public class RoomEnemySource
// {
//     private List<RoomEnemyWave> _waves;
//
//     public void StartReleasing()
//     {
//         
//     }
// }
//
// public class RoomEnemyWave
// {
//     private IEnemy _enemy;
// }
//
// public abstract class NextWaveReleaser
// {
//     public abstract bool Release();
// }
//
// public class InstantReleaser: NextWaveReleaser
// {
//     public override bool Release()
//     {
//         return true;
//     }
// }
//
// public class DelayReleaser() : NextWaveReleaser
// {
//     
//     private TimeSpan _delay;
//     public DelayReleaser(TimeSpan delay)
//     {
//         this._delay = delay;
//     }
//     
//     public override bool Release()
//     {
//         return true;
//     }
// }
//
// public class AllKilledInPreviousReleaser() : NextWaveReleaser
// {
//     public override bool Release()
//     {
//         return true;
//     }
// }
