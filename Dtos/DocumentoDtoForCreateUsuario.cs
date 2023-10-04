namespace PasseLivre.Api.Dtos;

public class DocumentoDtoForCreateUsuario {
    public string Nome {get; set;} = string.Empty;
    public string Url {get; set;} = string.Empty;
    public bool IsPasse {get; set;}
}