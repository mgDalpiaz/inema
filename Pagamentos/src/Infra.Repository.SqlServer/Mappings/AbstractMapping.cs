using Core.Shared.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.SqlServer
{
    public abstract class AbstractMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            #region [ Code ]

            #region [ Default Identity ]

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Id)
               .IsRequired()
               .HasColumnName("Id")
               .ValueGeneratedOnAdd();

            builder.Property(c => c.IntegrationCode)
                .HasColumnType("varchar(120)")
                .HasMaxLength(120);

            builder.HasIndex(x => x.IntegrationCode);

            #endregion

            #region [ Control State to Entity ]

            builder.Property(c => c.InsertedAt)
                .IsRequired(false)
                .HasColumnName("InsertedAt")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("LastUpdatedAt");

            builder.Property(c => c.RemovedAt)
                .HasColumnName("RemovedAt");

            builder.Property(c => c.InsertedUser)
                .IsRequired(false)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .HasColumnName("CreatedUser");

            builder.Property(c => c.InsertedUserId)
                .IsRequired(false)
                .HasColumnName("CreatedUserId");

            builder.Property(c => c.UpdatedUser)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .HasColumnName("LastUpdatedUser");

            builder.Property(c => c.UpdatedUserId)
                .HasColumnName("LastUpdatedUserId");

            #endregion

            #endregion
        }
    }
}
