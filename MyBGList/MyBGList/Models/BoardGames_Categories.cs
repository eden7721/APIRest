namespace MyBGList.Models
{
    public class BoardGames_Categories
    {
        public int CategoryId { get; set; }
        public int BoardGameId { get; set; }

        public DateTime CreatedDate { get; set; }
        public Category? Category { get; set; }
        public BoardGame? BoardGame { get; set; }

    }
}
