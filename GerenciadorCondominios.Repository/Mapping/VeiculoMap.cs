using GerenciadorCondominios.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.Repository.Mapping
{
    class VeiculoMap : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Nome).IsRequired().HasMaxLength(40);
            builder.Property(v => v.Marca).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Placa).IsRequired().HasMaxLength(20);
            builder.HasIndex(v => v.Placa).IsUnique();
            builder.Property(v => v.UsuarioId).IsRequired();

            builder.HasOne(v => v.Usuario).WithMany(v => v.Veiculos).HasForeignKey(v => v.UsuarioId);

            builder.ToTable("Veiculos");
        }
    }
}
