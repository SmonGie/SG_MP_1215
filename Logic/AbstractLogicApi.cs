using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    //Zdefiniuj klase AbstractLogicApi jako publiczna i zaimplementuj interfejs IObservable dla IEnumerable obiektow typu Ball
    public abstract class AbstractLogicApi : IObservable<IEnumerable<IBall>>
    {
        //Zdefiniuj abstrakcyjna wartosc dla IEnumerable obiektow typu Ball
        internal abstract IEnumerable<Ball> Balls { get; }  //Definicje abstraktyjnych metod
        public abstract void Simulation();
        public abstract void Start();
        public abstract void Stop();
        public abstract void SpawnBalls(int ballNumber);
        public abstract IDisposable Subscribe(IObserver<IEnumerable<IBall>> observer);
        //Definicja metody statycznej ktora tworzy instancje klasy AbstractLogicApi, z opcjonalnymi parametrami
        public static AbstractLogicApi CreateInstance(AbstractDataApi? data = default)
        {
            //zwroc nowa instancje klasy SimulationController przekazywanej przez instancje AbstractDataApi albo stworz nowa jesli nie jest zapewniona
            return new SimulationController(data ?? AbstractDataApi.CreateInstance());
        }
    }
    public interface IBall
    {
        Vector2 Velocity { get; }
        Vector2 Position { get; }
        int Radius { get; }
        int Diameter { get; }
    }
}