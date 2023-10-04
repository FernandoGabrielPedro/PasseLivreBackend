using PasseLivre.Api.Dtos;

namespace PasseLivre.Api.Entities;

public class Endereco{
    public int Id {get; set;}
    public string Cep {get; set;} = string.Empty;
    public string Estado {get; set;} = string.Empty;
    public string Cidade {get; set;} = string.Empty;
    public string Bairro {get; set;} = string.Empty;
    public string Logradouro {get; set;} = string.Empty;
    public string Complemento {get; set;} = string.Empty;
    public int Id_Usuario {get; set;}
    public Usuario? Usuario {get; set;}

    public Endereco() {}
    public Endereco(EnderecoDtoForCreateUsuario endereco) {
        Cep = endereco.Cep;
        Estado = endereco.Estado;
        Cidade = endereco.Cidade;
        Bairro = endereco.Bairro;
        Logradouro = endereco.Logradouro;
        Complemento = endereco.Complemento;
    }
}