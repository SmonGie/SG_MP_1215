using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    //Definicja klasy Data jako wewnetrzenj i podklasy AbstractDataApi
    internal class Data : AbstractDataApi
    {
        //nadpisanie wartosci BallRadius, wartoscia 20
        public override int BallRadius => 20;
        //nadpisanie wartosci HeightWindow, wartoscia 500
        public override int HeightWindow => 500;
        //nadpisanie wartosci WidthWindow, wartoscia 800
        public override int WidthWindow => 800;

     
        
    }
}
