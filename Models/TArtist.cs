using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TArtist
    {
        public TArtist()
        {
            TArtistsGroups = new HashSet<TArtistsGroup>();
            IdAlbums = new HashSet<TAlbum>();
        }

        public int IdArtist { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? Pseudonym { get; set; }
        public DateOnly? Birtdate { get; set; }
        public DateOnly? Deathdate { get; set; }

        public virtual ICollection<TArtistsGroup> TArtistsGroups { get; set; }

        public virtual ICollection<TAlbum> IdAlbums { get; set; }
    }
}
