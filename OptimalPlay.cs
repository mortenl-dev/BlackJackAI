public class OptimalPlay {
    public int bankCard;
    public int playerHand;

    char[,] blackjackStrategy =
    {
        // Hard Totals
        { 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H' }, // 5 to 8
        { 'H', 'D', 'D', 'D', 'D', 'H', 'H', 'H', 'H', 'H' }, // 9
        { 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'H', 'H' }, // 10
        { 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'H' }, // 11
        { 'H', 'H', 'S', 'S', 'S', 'H', 'H', 'H', 'H', 'H' }, // 12
        { 'S', 'S', 'S', 'S', 'S', 'H', 'H', 'H', 'H', 'H' }, // 13 to 16
        { 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S' }, // 17 to 21
        // Soft Totals
        { 'H', 'H', 'H', 'D', 'D', 'H', 'H', 'H', 'H', 'H' }, // 13 to 15
        { 'H', 'H', 'D', 'D', 'D', 'H', 'H', 'H', 'H', 'H' }, // 16 to 18
        { 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S' }, // 19 to 21
        // Pairs
        { 'P', 'P', 'P', 'P', 'P', 'P', 'H', 'H', 'H', 'H' }, // 2s and 3s
        { 'H', 'H', 'H', 'P', 'P', 'H', 'H', 'H', 'H', 'H' }, // 4s
        { 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'H', 'H' }, // 5s
        { 'P', 'P', 'P', 'P', 'P', 'H', 'H', 'H', 'H', 'H' }, // 6s
        { 'P', 'P', 'P', 'P', 'P', 'P', 'H', 'H', 'H', 'H' }, // 7s
        { 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P' }, // 8s
        { 'P', 'P', 'P', 'P', 'P', 'S', 'P', 'P', 'S', 'S' }, // 9s
        { 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S' }, // 10s and Aces
    };




}