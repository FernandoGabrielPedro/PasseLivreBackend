using PasseLivre.Api.Entities;

namespace PasseLivre.Api.Dtos;

public class UsuarioDtoForReturnUsuarioSimples{
    public int Id {get; set;}
    public string Nome_Completo {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty;
    public int Passe_Quantidade {get; set;}

    
    public UsuarioDtoForReturnUsuarioSimples() {}
    public UsuarioDtoForReturnUsuarioSimples(Usuario usuario) {
        Id = usuario.Id;
        Nome_Completo = usuario.Nome_Completo;
        Email = usuario.Email;
        Senha = usuario.Senha;
        Passe_Quantidade = usuario.Passe_Quantidade;
    }
}