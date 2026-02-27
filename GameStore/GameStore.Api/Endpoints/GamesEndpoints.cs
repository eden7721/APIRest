using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpoinName = "Get Game";
    private static readonly List<GameDto> games = [
        new (
            1,
            "Terraria",
            "Plataforms",
            9.99M,
            new DateOnly(2011, 5, 16)),
        new (
            2,
            "The Binding of Isaac",
            "Roguelike",
            9.99M,
            new DateOnly(2011, 9, 28)),
        new (
            3,
            "Red Dead Redemption 2",
            "Action",
            19.99M,
            new DateOnly(2018, 10, 26)),

    ];

    public static void GetGameEndPoint(this WebApplication app)
    {
        int counter;
        var group = app.MapGroup("/games");

        // GET  /games
        group.MapGet("/", () => games)
            .WithName("AllGames");

        // GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            var gameFound = games.Find(game => game.Id == id);

            return gameFound is null ? Results.NotFound() : Results.Ok(gameFound);
        }).WithName(GetGameEndpoinName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                counter = games.Any() ? games.Max(game => game.Id) + 1 : 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpoinName, new { id = game.Id }, game);
            //return Results.CreatedAtRoute("AllGames", null, games);
        });

        // PUT /games/3
        group.MapPut("/{id}", (int id, UpdatedGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
    }
}
