namespace GameStore.Api.Dtos;

// A DTO is a contract between the client and server since it represent
// a shared agreement about how data will be tranferrend an used.
public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
