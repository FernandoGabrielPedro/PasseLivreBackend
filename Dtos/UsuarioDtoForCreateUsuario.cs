using PasseLivre.Api.Entities;

namespace PasseLivre.Api.Dtos;

public class UsuarioDtoForCreateUsuario {
    public string Nome_Completo {get; set;} = string.Empty;
    public string Telefone {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public bool Passe_Categoria {get; set;}
    public bool Motociclista {get; set;}
    public List<DocumentoDtoForCreateUsuario> Documentos {get; set;} = new();
    public EnderecoDtoForCreateUsuario Endereco {get; set;} = new();

    public UsuarioDtoForCreateUsuario() {}
}

