using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    // Klasa abstrakcyjna implementująca interfejsy IObserver oraz IObservable z parametrami generycznymi.
    public abstract class AbstractModelApi : IObserver<IEnumerable<IBall>>, IObservable<IEnumerable<IBallModel>>
    {
        // Metody abstrakcyjne, które muszą zostać zaimplementowane przez klasy dziedziczące:
        public abstract void SpawnBalls(int numberOfBalls);

        public abstract void Start();

        public abstract void Stop();

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(IEnumerable<IBall> balls);

        public abstract IDisposable Subscribe(IObserver<IEnumerable<IBallModel>> observer);

        // Metoda statyczna tworząca instancję klasy Model, przekazująca instancję logiki, jeśli jest dostępna.
        public static AbstractModelApi CreateInstance(AbstractLogicApi? logic = default)
        {
            return new Model(logic ?? AbstractLogicApi.CreateInstance());
        }
    }

    public interface IBallModel
    {
        Vector2 Velocity { get; }
        Vector2 Position { get; }
        int Radius { get; }
        int Diameter { get; }
    }
}
