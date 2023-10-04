using PasseLivre.Api.Dtos;

namespace PasseLivre.Api.Entities;

public class Usuario{
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
    public List<Documento> Documentos {get; set;} = new();
    public Endereco? Endereco {get; set;}

    // public User(UserDto userDto){
    //     this.Nome_Completo = userDto.Nome;
    //     this.Cpf = userDto.Cpf;
    //     this.Telefone = userDto.Telefone;
    //     this.Email = userDto.Email;
    //     this.Senha = userDto.Senha;
    //     this.Status = 0;
    //     this.Passe_Quantidade = 0;
    // }

    public Usuario() {}

    public Usuario(UsuarioDtoForCreateUsuario usuario) {
        Nome_Completo = usuario.Nome_Completo;
        Telefone = usuario.Telefone;
        Email = usuario.Email;
        Senha = usuario.Senha;
        Cpf = usuario.Cpf;
        Passe_Categoria = usuario.Passe_Categoria;
        Motociclista = usuario.Motociclista;

        Status = 1;
        Passe_Quantidade = 0;
        Data_Cadastro = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        Documentos = new();
        foreach(DocumentoDtoForCreateUsuario doc in usuario.Documentos) {
            Documentos.Add(new Documento(doc));
        }
        Endereco = new Endereco(usuario.Endereco);
    }
}