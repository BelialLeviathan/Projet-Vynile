using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TGroup
    {
        public TGroup()
        {
            TArtistsGroups = new HashSet<TArtistsGroup>();
            IdAlbums = new HashSet<TAlbum>();
        }

        public int IdGroup { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<TArtistsGroup> TArtistsGroups { get; set; }

        public virtual ICollection<TAlbum> IdAlbums { get; set; }
    }
}
