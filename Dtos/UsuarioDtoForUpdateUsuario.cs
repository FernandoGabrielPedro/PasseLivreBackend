namespace PasseLivre.Api.Dtos;

public class UsuarioDtoForUpdateUsuario {
    public int Id {get; set;}
    public string Email {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty;
}