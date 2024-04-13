using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data;

namespace Logic
{
    private ISet<IObserver<IEnumerable<Ball>>> _obserwatorzy; // Zbiór obiektów typu IObserver, które będą otrzymywać aktualizacje, gdy stan symulacji ulegnie zmianie.
    private AbstractDataAPI _dane; // Referencja do obiektu typu AbstractDataAPI dostarczającego dane symulacji.
    private SimulationManager _menedżerSymulacji; // Obiekt typu SimulationManager zarządzający symulacją.
    private bool czyDziała = false; // Zmienna logiczna wskazująca, czy symulacja jest w trakcie działania czy nie.

    public SimulationController(AbstractDataAPI? dane = default)
    {
        _dane = dane ?? AbstractDataAPI.CreateInstance(); // Jeśli nie podano obiektu danych, utwórz domyślny.
        _menedżerSymulacji = new SimulationManager(new Window(_dane.SzerokośćOkna, _dane.WysokośćOkna), _dane.ŚrednicaPiłki); // Utwórz nowy obiekt typu SimulationManager z oknem i średnicą piłki dostarczanymi przez obiekt danych.
        _obserwatorzy = new HashSet<IObserver<IEnumerable<Ball>>>(); // Zainicjuj zbiór obserwatorów.
    }

}