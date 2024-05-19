using System.Diagnostics;
using System.Numerics;

namespace Data
{
    //Definicja klasy Data jako wewnetrzenj i podklasy AbstractDataApi
    internal class Data : AbstractDataApi
    {
        private int Height;
        private int Width;

        public override int getWidthOfWindow()
        {
            return Width;
        }

        public override int getHeightOfWindow()
        {
            return Height;
        }
        public Data(int HeightOfWindow, int WidthOfWindow)
        {
            Height = HeightOfWindow;
            Width = WidthOfWindow;
        }

        public override AbstractBallApi SpawnBalls(bool isWorking)
        {
            int mass = 5;
            int radius = 10;  
            Random random = new Random();
            int positionX = random.Next(10, Width - 10);
            int positionY = random.Next(10, Height - 10);

            int varX = random.Next(-5, 5);
            int varY = random.Next(-5, 5);

            if (varX == 0)
            {
                varX = random.Next(1, 3) * 2 - 3;
            }
            if (varY == 0)
            {
                varY = random.Next(1, 3) * 2 - 3;
            }

            int velocityX = varX;
            int velocityY = varY;

            Vector2 position = new Vector2(positionX, positionY);

            return AbstractBallApi.CreateInstance(position, velocityX, velocityY, mass, radius, isWorking);
        }
    }
}
