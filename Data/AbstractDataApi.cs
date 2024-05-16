namespace Data
{
    // Definicja klasy AbstractDataApi jako abstrakcyjnej
    public abstract class AbstractDataApi
    {
        // Definicja abstrakcyjnych elementow dla wysokosci i szerokosci okna
        public abstract int getHeightOfWindow();
        public abstract int getWidthOfWindow();


        public abstract AbstractBallApi SpawnBalls(bool isWorking);

        // Zdefiniuj metodę statyczną, aby utworzyć wystąpienie klasy AbstractDataAPI.
        public static AbstractDataApi CreateInstance(int HeightOfWindow,int WidthOfWindow)
        {
            // Zwraca nową instancję klasy Data.
            return new Data(HeightOfWindow, WidthOfWindow);
        }
    }
}
