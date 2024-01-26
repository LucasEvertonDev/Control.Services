using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Infra.Data.EntitiesConfiguration
{
    public class MapPerfilUsuarioAcessosConfiguration : IEntityTypeConfiguration<MapPerfilUsuariosAcessos>
    {
        public void Configure(EntityTypeBuilder<MapPerfilUsuariosAcessos> builder)
        {
            builder.ToTable("MapPerfilUsuariosAcessos");
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.PerfilUsuario)
                .WithMany(perfilUsuario => perfilUsuario.MapPerfilUsuariosAcessos)
                .HasForeignKey(m => m.PerfilUsuarioId);

            builder.HasOne(m => m.Acesso)
                .WithMany(acesso => acesso.MapPerfilUsuariosAcessos)
                .HasForeignKey(m => m.AcessoId);
        }
    }
}
