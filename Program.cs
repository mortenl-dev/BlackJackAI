
public class BlackJack {
    public static void Main () {
        BlackJack Game = new BlackJack();
        List<int> Numbers = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12,13,1,2,3,4,5,6,7,8,9,10,11,12,13};
        Dictionary<string, List<int>> Deck = new Dictionary<string, List<int>>(){
            {"Spades", Numbers},
            {"Clubs", Numbers},
            {"Hearts", Numbers},
            {"Diamonds", Numbers},
            };
        Game.Round();
    }

    public void Round() {
        bool[] Seats = {false,false,false,false,false};
        int c = 0;
        Console.WriteLine("Please choose how many seats to play on:");
        foreach (bool b in Seats) {
            if (b == false) c++;
        }
        Console.WriteLine($"{c} seats are currently empty.");
        Console.WriteLine("How many would you like to fill?");
        try {
            string choice = Console.ReadLine()!;
            int i = Int32.Parse(choice);
            Console.WriteLine(i.ToString());
        }
        catch (FormatException f) {
            Console.WriteLine("Please try again.");
        }
    }
}

