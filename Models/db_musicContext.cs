using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace projet_boutique_vinyle.Models
{
    public partial class db_musicContext : DbContext
    {
        public db_musicContext()
        {
        }

        public db_musicContext(DbContextOptions<db_musicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAlbum> TAlbums { get; set; } = null!;
        public virtual DbSet<TAlbumsTrack> TAlbumsTracks { get; set; } = null!;
        public virtual DbSet<TAlbumsVinylType> TAlbumsVinylTypes { get; set; } = null!;
        public virtual DbSet<TArtist> TArtists { get; set; } = null!;
        public virtual DbSet<TArtistsGroup> TArtistsGroups { get; set; } = null!;
        public virtual DbSet<TGenre> TGenres { get; set; } = null!;
        public virtual DbSet<TGroup> TGroups { get; set; } = null!;
        public virtual DbSet<TLabel> TLabels { get; set; } = null!;
        public virtual DbSet<TTrack> TTracks { get; set; } = null!;
        public virtual DbSet<TVinylType> TVinylTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=db_music;User Id=postgres;Password=Rodrigue1103;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAlbum>(entity =>
            {
                entity.HasKey(e => e.IdAlbum)
                    .HasName("t_albums_pkey");

                entity.ToTable("t_albums");

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.IdLabel).HasColumnName("id_label");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.HasOne(d => d.IdLabelNavigation)
                    .WithMany(p => p.TAlbums)
                    .HasForeignKey(d => d.IdLabel)
                    .HasConstraintName("t_albums_id_label_fkey");

                entity.HasMany(d => d.IdArtists)
                    .WithMany(p => p.IdAlbums)
                    .UsingEntity<Dictionary<string, object>>(
                        "TAlbumArtist",
                        l => l.HasOne<TArtist>().WithMany().HasForeignKey("IdArtist").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("t_album_artists_id_artist_fkey"),
                        r => r.HasOne<TAlbum>().WithMany().HasForeignKey("IdAlbum").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("t_album_artists_id_album_fkey"),
                        j =>
                        {
                            j.HasKey("IdAlbum", "IdArtist").HasName("t_album_artists_pkey");

                            j.ToTable("t_album_artists");

                            j.IndexerProperty<int>("IdAlbum").HasColumnName("id_album");

                            j.IndexerProperty<int>("IdArtist").HasColumnName("id_artist");
                        });

                entity.HasMany(d => d.IdGroups)
                    .WithMany(p => p.IdAlbums)
                    .UsingEntity<Dictionary<string, object>>(
                        "TAlbumGroup",
                        l => l.HasOne<TGroup>().WithMany().HasForeignKey("IdGroup").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("t_album_groups_id_group_fkey"),
                        r => r.HasOne<TAlbum>().WithMany().HasForeignKey("IdAlbum").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("t_album_groups_id_album_fkey"),
                        j =>
                        {
                            j.HasKey("IdAlbum", "IdGroup").HasName("t_album_groups_pkey");

                            j.ToTable("t_album_groups");

                            j.IndexerProperty<int>("IdAlbum").HasColumnName("id_album");

                            j.IndexerProperty<int>("IdGroup").HasColumnName("id_group");
                        });
            });

            modelBuilder.Entity<TAlbumsTrack>(entity =>
            {
                entity.HasKey(e => new { e.IdAlbum, e.IdTracks })
                    .HasName("t_albums_tracks_pkey");

                entity.ToTable("t_albums_tracks");

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.IdTracks).HasColumnName("id_tracks");

                entity.Property(e => e.TrackNumber).HasColumnName("track_number");

                entity.HasOne(d => d.IdAlbumNavigation)
                    .WithMany(p => p.TAlbumsTracks)
                    .HasForeignKey(d => d.IdAlbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_albums_tracks_id_album_fkey");

                entity.HasOne(d => d.IdTracksNavigation)
                    .WithMany(p => p.TAlbumsTracks)
                    .HasForeignKey(d => d.IdTracks)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_albums_tracks_id_tracks_fkey");
            });

            modelBuilder.Entity<TAlbumsVinylType>(entity =>
            {
                entity.HasKey(e => new { e.IdAlbum, e.IdVinylType })
                    .HasName("t_albums_vinyl_types_pkey");

                entity.ToTable("t_albums_vinyl_types");

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.IdVinylType).HasColumnName("id_vinyl_type");

                entity.Property(e => e.Picture)
                    .HasColumnType("character varying")
                    .HasColumnName("picture");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdAlbumNavigation)
                    .WithMany(p => p.TAlbumsVinylTypes)
                    .HasForeignKey(d => d.IdAlbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_albums_vinyl_types_id_album_fkey");

                entity.HasOne(d => d.IdVinylTypeNavigation)
                    .WithMany(p => p.TAlbumsVinylTypes)
                    .HasForeignKey(d => d.IdVinylType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_albums_vinyl_types_id_vinyl_type_fkey");
            });

            modelBuilder.Entity<TArtist>(entity =>
            {
                entity.HasKey(e => e.IdArtist)
                    .HasName("t_artists_pkey");

                entity.ToTable("t_artists");

                entity.Property(e => e.IdArtist).HasColumnName("id_artist");

                entity.Property(e => e.Birtdate).HasColumnName("birtdate");

                entity.Property(e => e.Deathdate).HasColumnName("deathdate");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(20)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(20)
                    .HasColumnName("lastname");

                entity.Property(e => e.Pseudonym)
                    .HasMaxLength(20)
                    .HasColumnName("pseudonym");
            });

            modelBuilder.Entity<TArtistsGroup>(entity =>
            {
                entity.HasKey(e => new { e.IdArtist, e.IdGroup })
                    .HasName("t_artists_groups_pkey");

                entity.ToTable("t_artists_groups");

                entity.Property(e => e.IdArtist).HasColumnName("id_artist");

                entity.Property(e => e.IdGroup).HasColumnName("id_group");

                entity.Property(e => e.IsMember).HasColumnName("is_member");

                entity.HasOne(d => d.IdArtistNavigation)
                    .WithMany(p => p.TArtistsGroups)
                    .HasForeignKey(d => d.IdArtist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_artists_groups_id_artist_fkey");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.TArtistsGroups)
                    .HasForeignKey(d => d.IdGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_artists_groups_id_group_fkey");
            });

            modelBuilder.Entity<TGenre>(entity =>
            {
                entity.HasKey(e => e.IdGenre)
                    .HasName("t_genres_pkey");

                entity.ToTable("t_genres");

                entity.Property(e => e.IdGenre).HasColumnName("id_genre");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TGroup>(entity =>
            {
                entity.HasKey(e => e.IdGroup)
                    .HasName("t_groups_pkey");

                entity.ToTable("t_groups");

                entity.Property(e => e.IdGroup).HasColumnName("id_group");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TLabel>(entity =>
            {
                entity.HasKey(e => e.IdLabel)
                    .HasName("t_label_pkey");

                entity.ToTable("t_label");

                entity.Property(e => e.IdLabel).HasColumnName("id_label");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TTrack>(entity =>
            {
                entity.HasKey(e => e.IdTracks)
                    .HasName("t_tracks_pkey");

                entity.ToTable("t_tracks");

                entity.Property(e => e.IdTracks).HasColumnName("id_tracks");

                entity.Property(e => e.DurationSec).HasColumnName("duration_sec");

                entity.Property(e => e.TrackName)
                    .HasMaxLength(50)
                    .HasColumnName("track_name");

                entity.HasMany(d => d.IdGenres)
                    .WithMany(p => p.IdTracks)
                    .UsingEntity<Dictionary<string, object>>(
                        "TTracksGenre",
                        l => l.HasOne<TGenre>().WithMany().HasForeignKey("IdGenre").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("t_tracks_genres_id_genre_fkey"),
                        r => r.HasOne<TTrack>().WithMany().HasForeignKey("IdTracks").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("t_tracks_genres_id_tracks_fkey"),
                        j =>
                        {
                            j.HasKey("IdTracks", "IdGenre").HasName("t_tracks_genres_pkey");

                            j.ToTable("t_tracks_genres");

                            j.IndexerProperty<int>("IdTracks").HasColumnName("id_tracks");

                            j.IndexerProperty<int>("IdGenre").HasColumnName("id_genre");
                        });
            });

            modelBuilder.Entity<TVinylType>(entity =>
            {
                entity.HasKey(e => e.IdVinylType)
                    .HasName("t_vinyl_types_pkey");

                entity.ToTable("t_vinyl_types");

                entity.Property(e => e.IdVinylType).HasColumnName("id_vinyl_type");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
