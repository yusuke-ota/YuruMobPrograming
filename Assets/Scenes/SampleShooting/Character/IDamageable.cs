namespace Scenes.SampleShooting.Character
{
    /// <summary>
    ///     ダメージ処理を外部から呼び出すためのInterface。
    ///     具体的には、攻撃可能なオブジェクトから呼ぶことを想定。
    /// </summary>
    public interface IDamageable
    {
        void Damage();
    }
}