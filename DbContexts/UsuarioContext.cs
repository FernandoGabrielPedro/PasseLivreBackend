using Microsoft.EntityFrameworkCore;
using PasseLivre.Api.Entities;
using static Microsoft.EntityFrameworkCore.DbContext;

namespace PasseLivre.Api.DbContexts;

public class UsuarioContext : DbContext
{
    public DbSet<Usuario> Usuario { get; set; } = null!;
    public DbSet<Documento> Documento { get; set; } = null!;
    public DbSet<Endereco> Endereco { get; set; } = null!;

    public UsuarioContext(DbContextOptions<UsuarioContext> options)
    : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var usuario = modelBuilder.Entity<Usuario>(); 

        usuario
            .ToTable("Usuario");

        usuario
            .HasMany(c => c.Documentos)
            .WithOne(a => a.Usuario)
            .HasForeignKey(a => a.Id_Usuario);

        usuario
            .HasOne(c => c.Endereco)
            .WithOne(e => e.Usuario)
            .HasForeignKey<Endereco>(e => e.Id_Usuario);

        usuario.Property(c => c.Nome_Completo)
            .HasMaxLength(80)
            .IsRequired();

        usuario.Property(c => c.Telefone)
            .HasMaxLength(15)
            .IsRequired();

        usuario.Property(c => c.Email)
            .HasMaxLength(80)
            .IsRequired();
        
        usuario.Property(c => c.Senha)
            .HasMaxLength(256)
            .IsRequired();
            
        usuario.Property(c => c.Cpf)
            .HasMaxLength(11)
            .IsRequired();

        usuario.Property(c => c.Status)
            .IsRequired();
        
        usuario.Property(c => c.Passe_Quantidade)
            .IsRequired();

        usuario.Property(c => c.Passe_Categoria)
            .IsRequired();
        
        usuario.Property(c => c.Motociclista)
            .IsRequired();

        usuario.Property(c => c.Data_Cadastro)
            .IsRequired();



        var documento = modelBuilder.Entity<Documento>();

        documento
            .ToTable("Documento");

        documento.Property(d => d.Nome)
            .HasMaxLength(80)
            .IsRequired();

        documento.Property(d => d.Url)
            .HasMaxLength(256)
            .IsRequired();

        documento.Property(d => d.IsPasse)
            .IsRequired();



        var endereco = modelBuilder.Entity<Endereco>();

        endereco
            .ToTable("Endereco");

        endereco.Property(e => e.Cep)
            .HasMaxLength(10)
            .IsRequired();

        endereco.Property(e => e.Estado)
            .HasMaxLength(2)
            .IsRequired();

        endereco.Property(e => e.Cidade)
            .HasMaxLength(50)
            .IsRequired();

        endereco.Property(e => e.Bairro)
            .HasMaxLength(50)
            .IsRequired();

        endereco.Property(e => e.Logradouro)
            .HasMaxLength(50)
            .IsRequired();

        endereco.Property(e => e.Complemento)
            .HasMaxLength(50)
            .IsRequired();



        base.OnModelCreating(modelBuilder);
    }

}