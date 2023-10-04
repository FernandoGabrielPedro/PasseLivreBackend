using PasseLivre.Api.Dtos;

namespace PasseLivre.Api.Entities;

public class Documento {
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Url {get; set;} = string.Empty;
    public bool IsPasse {get; set;}
    public int Id_Usuario {get; set;}
    public Usuario? Usuario {get; set;}

    public Documento() {}
    public Documento(DocumentoDtoForCreateUsuario documento) {
        Nome = documento.Nome;
        Url = documento.Url;
        IsPasse = documento.IsPasse;
    }
}