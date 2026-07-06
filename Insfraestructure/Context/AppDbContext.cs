using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DescuentosProducto> descuentos_productos { get; set; }

    public virtual DbSet<DetallePedido> detalle_pedidos { get; set; }

    public virtual DbSet<Laboratorio> laboratorios { get; set; }

    public virtual DbSet<Pedido> pedidos { get; set; }

    public virtual DbSet<Producto> productos { get; set; }

    public virtual DbSet<RefreshToken> refresh_tokens { get; set; }

    public virtual DbSet<StagingVendible> staging_vendibles { get; set; }

    public virtual DbSet<Usuario> usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in", "like", "ilike", "is", "match", "imatch", "isdistinct" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<DescuentosProducto>(entity =>
        {
            entity.HasKey(e => e.id).HasName("descuentos_producto_pkey");

            entity.ToTable("descuentos_producto");

            entity.HasIndex(e => e.producto_id, "idx_descuentos_producto");

            entity.HasIndex(e => new { e.producto_id, e.cantidad_minima }, "uq_producto_cantidad").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.cantidad_minima).HasPrecision(12, 2);
            entity.Property(e => e.precio_unitario).HasPrecision(12, 2);

            entity.HasOne(d => d.producto).WithMany(p => p.descuentos_productos)
                .HasForeignKey(d => d.producto_id)
                .HasConstraintName("fk_descuento_producto");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.id).HasName("detalle_pedido_pkey");

            entity.ToTable("detalle_pedido");

            entity.HasIndex(e => e.pedido_id, "idx_detalle_pedido");

            entity.HasIndex(e => e.producto_id, "idx_detalle_producto");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.cantidad).HasPrecision(12, 2);
            entity.Property(e => e.precio_unitario_aplicado).HasPrecision(12, 2);
            entity.Property(e => e.subtotal).HasPrecision(12, 2);

            entity.HasOne(d => d.pedido).WithMany(p => p.detalle_pedidos)
                .HasForeignKey(d => d.pedido_id)
                .HasConstraintName("fk_detalle_pedido");

            entity.HasOne(d => d.producto).WithMany(p => p.detalle_pedidos)
                .HasForeignKey(d => d.producto_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalle_producto");
        });

        modelBuilder.Entity<Laboratorio>(entity =>
        {
            entity.HasKey(e => e.id).HasName("laboratorios_pkey");

            entity.HasIndex(e => e.nombre, "laboratorios_nombre_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pedidos_pkey");

            entity.HasIndex(e => e.cliente_id, "idx_pedidos_cliente");

            entity.HasIndex(e => e.estado, "idx_pedidos_estado");

            entity.HasIndex(e => e.promotor_id, "idx_pedidos_promotor");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying");
            entity.Property(e => e.fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.total).HasPrecision(12, 2);

            entity.HasOne(d => d.cliente).WithMany(p => p.pedidoclientes)
                .HasForeignKey(d => d.cliente_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pedidos_cliente");

            entity.HasOne(d => d.promotor).WithMany(p => p.pedidopromotors)
                .HasForeignKey(d => d.promotor_id)
                .HasConstraintName("fk_pedidos_promotor");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.id).HasName("productos_pkey");

            entity.HasIndex(e => e.codigo, "idx_productos_codigo");

            entity.HasIndex(e => e.laboratorio_id, "idx_productos_laboratorio");

            entity.HasIndex(e => e.codigo, "productos_codigo_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.activo).HasDefaultValue(true);
            entity.Property(e => e.actualizado_en)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.codigo).HasMaxLength(100);
            entity.Property(e => e.medida).HasMaxLength(100);
            entity.Property(e => e.precio_venta).HasPrecision(12, 2);
            entity.Property(e => e.stock).HasPrecision(12, 2);

            entity.HasOne(d => d.laboratorio).WithMany(p => p.productos)
                .HasForeignKey(d => d.laboratorio_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_productos_laboratorio");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.id).HasName("refresh_tokens_pkey");

            entity.HasIndex(e => e.expiracion, "idx_refresh_expiracion");

            entity.HasIndex(e => e.revocado, "idx_refresh_revocado");

            entity.HasIndex(e => e.usuario_id, "idx_refresh_usuario");

            entity.HasIndex(e => e.token_hash, "refresh_tokens_token_hash_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.creado_en)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.dispositivo).HasMaxLength(255);
            entity.Property(e => e.expiracion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ip_address).HasMaxLength(100);
            entity.Property(e => e.revocado).HasDefaultValue(false);

            entity.HasOne(d => d.usuario).WithMany(p => p.refresh_tokens)
                .HasForeignKey(d => d.usuario_id)
                .HasConstraintName("fk_refresh_usuario");
        });

        modelBuilder.Entity<StagingVendible>(entity =>
        {
            entity.HasKey(e => e.id).HasName("staging_vendibles_pkey");

            entity.HasIndex(e => e.lote_carga, "idx_staging_lote");

            entity.HasIndex(e => e.procesado, "idx_staging_pendientes");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.cargado_en)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.lote_carga).HasMaxLength(100);
            entity.Property(e => e.procesado).HasDefaultValue(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.id).HasName("usuarios_pkey");

            entity.HasIndex(e => e.email, "idx_usuarios_email");

            entity.HasIndex(e => e.rol, "idx_usuarios_rol");

            entity.HasIndex(e => e.email, "usuarios_email_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.activo).HasDefaultValue(true);
            entity.Property(e => e.creado_en)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.email).HasMaxLength(255);
            entity.Property(e => e.farmacia).HasMaxLength(255);
            entity.Property(e => e.nombre).HasMaxLength(255);
            entity.Property(e => e.rol).HasMaxLength(20);
            entity.Property(e => e.telefono).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
