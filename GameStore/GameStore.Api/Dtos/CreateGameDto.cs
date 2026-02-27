using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record CreateGameDto(
    [Required][StringLength(50, ErrorMessage = "El campo no puede superar los 50 caracteres.")]string Name,
    [Required][StringLength(50, ErrorMessage = "El campo no puede superar los 50 caracteres.")]string Genre,
    [Required][Range(1,100, ErrorMessage = "El precio no puede superar 100")]decimal Price,
    DateOnly ReleaseDate
);
