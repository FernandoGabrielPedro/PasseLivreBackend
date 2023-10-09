using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasseLivre.Api.DbContexts;
using PasseLivre.Api.Dtos;
using PasseLivre.Api.Entities;

namespace PasseLivre.Api.Controllers;


[ApiController]
[Route("api/user")]
public class UsuarioController : ControllerBase
{

    private readonly UsuarioContext _context;

    public UsuarioController(UsuarioContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<UsuarioDtoForReturnUsuario>> GetUser(int userId)
    {
        var users = _context.Usuario.Include(u => u.Documentos).Include(u => u.Endereco).OrderBy(u => u.Id).ToList();

        List<UsuarioDtoForReturnUsuario> usersToReturn = new();
        foreach(Usuario user in users) {
            usersToReturn.Add(new UsuarioDtoForReturnUsuario(user));
        }

        return Ok(usersToReturn);
    }

    [HttpGet("{userId}")]
    public ActionResult<UsuarioDtoForReturnUsuario> GetUserById(int userId)
    {
        var user = _context.Usuario.Include(u => u.Documentos).Include(u => u.Endereco).FirstOrDefault(c => c.Id == userId);

        if(user == null) return NotFound();

        UsuarioDtoForReturnUsuario userToReturn = new UsuarioDtoForReturnUsuario(user);

        return Ok(userToReturn);
    }

    [HttpGet("simple/{userId}")]
    public ActionResult<UsuarioDtoForReturnUsuarioSimples> GetUserSimplesById(int userId)
    {
        var user = _context.Usuario.Include(u => u.Documentos).Include(u => u.Endereco).FirstOrDefault(c => c.Id == userId);

        if(user == null) return NotFound();

        UsuarioDtoForReturnUsuarioSimples userToReturn = new UsuarioDtoForReturnUsuarioSimples(user);

        return Ok(userToReturn);
    }

    [HttpPost]
    public ActionResult<UsuarioDtoForReturnUsuario> CreateUser([FromBody] UsuarioDtoForCreateUsuario userDto){

        var databaseUsers = _context.Usuario.OrderBy(u => u.Id).ToList();
        foreach(Usuario user in databaseUsers) {
            if(userDto.Email.Equals(user.Email)) return BadRequest();
            if(userDto.Cpf.Equals(user.Cpf)) return BadRequest();
        }
        
        Usuario userEntity = new Usuario(userDto);

        // userEntity.Documents = new();
        // foreach(DocumentDto doc in userDto.Documents) {
        //     userEntity.Documents.Add(new Document {
        //         Nome = doc.Nome,
        //         Url = doc.Url,
        //         IsPasse = doc.IsPasse,
        //         DataEnvio = DateTime.UtcNow
        //     });
        // }

        _context.Usuario.Add(userEntity);
        _context.SaveChanges();

        UsuarioDtoForReturnUsuario usuarioForReturn = new UsuarioDtoForReturnUsuario(userEntity);

        return Ok(usuarioForReturn); //ta errado, mas to com preguiça
    }

    [HttpPut("{userId}")]
    public ActionResult EditUserById(int userId, UsuarioDtoForUpdateUsuario userDtoForEdit)
    {
        var user = _context.Usuario.FirstOrDefault(c => c.Id == userId);

        if(user == null) return NotFound();
        if(userDtoForEdit.Id != userId) return BadRequest();

        user.Email = userDtoForEdit.Email;
        user.Senha = userDtoForEdit.Senha;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{userId}")]
    public ActionResult DeleteUserById(int userId)
    {
        var user = _context.Usuario.Include(u => u.Documentos).Include(u => u.Endereco).FirstOrDefault(c => c.Id == userId);

        if(user == null) return NotFound();

        foreach(Documento doc in user.Documentos) {
            _context.Documento.Remove(doc);
        }
        if(user.Endereco != null) _context.Endereco.Remove(user.Endereco);
        _context.Usuario.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost("login")]
    public ActionResult<int> LoginUser([FromBody] LoginDto login) {
        var user = _context.Usuario.Include(u => u.Documentos).Include(u => u.Endereco).FirstOrDefault(c => c.Email == login.email);

        if(user == null) return NotFound();
        if(user.Senha != login.password) return NotFound();
        if(user.Status < 2) return StatusCode(500);

        return Ok(user.Id);
    }

    [HttpPost("solicitarPasse/{userId}")]
    public ActionResult SolicitarPasse(int userId, List<DocumentoDtoForSolicitarPasse> documentos) {
        var user = _context.Usuario.Include(u => u.Documentos).Include(u => u.Endereco).FirstOrDefault(c => c.Id == userId);
        if(user == null) return NotFound();
        if(user.Status != 2) return BadRequest();

        user.Status = 3;

        foreach(Documento doc in user.Documentos) {
            if(doc.IsPasse) user.Documentos.Remove(doc);
        }
        foreach(DocumentoDtoForSolicitarPasse doc in documentos) {
            user.Documentos.Add(new Documento(doc));
        }
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost("usarPasse/{userId}")]
    public ActionResult UsarPasse(int userId) {
        var user = _context.Usuario.FirstOrDefault(c => c.Id == userId);
        if(user == null) return NotFound();
        if(user.Status != 4) return BadRequest();
        if(user.Passe_Quantidade < 1) return BadRequest();

        user.Passe_Quantidade--;
        _context.SaveChanges();

        return NoContent();
    }
}
