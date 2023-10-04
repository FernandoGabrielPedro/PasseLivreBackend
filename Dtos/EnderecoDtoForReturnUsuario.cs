using PasseLivre.Api.Entities;

namespace PasseLivre.Api.Dtos;

public class EnderecoDtoForReturnUsuario{
    public int Id {get; set;}
    public string Cep {get; set;} = string.Empty;
    public string Estado {get; set;} = string.Empty;
    public string Cidade {get; set;} = string.Empty;
    public string Bairro {get; set;} = string.Empty;
    public string Logradouro {get; set;} = string.Empty;
    public string Complemento {get; set;} = string.Empty;

    public EnderecoDtoForReturnUsuario() {}
    public EnderecoDtoForReturnUsuario(Endereco endereco) {
        if(endereco == null) return;
        
        Id = endereco.Id;
        Cep = endereco.Cep;
        Estado = endereco.Estado;
        Cidade = endereco.Cidade;
        Bairro = endereco.Bairro;
        Logradouro = endereco.Logradouro;
        Complemento = endereco.Complemento;
    }
}