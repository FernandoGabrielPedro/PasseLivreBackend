using PasseLivre.Api.Entities;

namespace PasseLivre.Api.Dtos;

public class DocumentoDtoForReturnUsuario {
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Url {get; set;} = string.Empty;
    public bool IsPasse {get; set;}


    public DocumentoDtoForReturnUsuario() {}

    public DocumentoDtoForReturnUsuario(Documento documento) {
        if(documento == null) return;
        
        Id = documento.Id;
        Nome = documento.Nome;
        Url = documento.Url;
        IsPasse = documento.IsPasse;
    }
}