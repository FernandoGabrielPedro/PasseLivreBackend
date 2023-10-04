using PasseLivre.Api.Entities;

namespace PasseLivre.Api.Dtos;

public class UsuarioDtoForReturnUsuario{
    public int Id {get; set;}
    public string Nome_Completo {get; set;} = string.Empty;
    public string Telefone {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public int Status {get; set;}
    public int Passe_Quantidade {get; set;}
    public bool Passe_Categoria {get; set;}
    public bool Motociclista {get; set;}
    public DateOnly Data_Cadastro {get; set;}
    public List<DocumentoDtoForReturnUsuario> Documentos {get; set;} = new();
    public EnderecoDtoForReturnUsuario? Endereco {get; set;}

    
    public UsuarioDtoForReturnUsuario(Usuario usuario) {
        Id = usuario.Id;
        Nome_Completo = usuario.Nome_Completo;
        Telefone = usuario.Telefone;
        Email = usuario.Email;
        Senha = usuario.Senha;
        Cpf = usuario.Cpf;
        Status = usuario.Status;
        Passe_Quantidade = usuario.Passe_Quantidade;
        Passe_Categoria = usuario.Passe_Categoria;
        Motociclista = usuario.Motociclista;
        Data_Cadastro = usuario.Data_Cadastro;

        Endereco = new EnderecoDtoForReturnUsuario(usuario.Endereco!);

        Documentos = new();
        foreach(Documento doc in usuario.Documentos) {
            Documentos.Add(new DocumentoDtoForReturnUsuario(doc));
        }
    }
}