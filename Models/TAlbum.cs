using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TAlbum
    {
        public TAlbum()
        {
            TAlbumsTracks = new HashSet<TAlbumsTrack>();
            TAlbumsVinylTypes = new HashSet<TAlbumsVinylType>();
            IdArtists = new HashSet<TArtist>();
            IdGroups = new HashSet<TGroup>();
        }

        public int IdAlbum { get; set; }
        public int? IdLabel { get; set; }
        public string? Title { get; set; }

        public virtual TLabel? IdLabelNavigation { get; set; }
        public virtual ICollection<TAlbumsTrack> TAlbumsTracks { get; set; }
        public virtual ICollection<TAlbumsVinylType> TAlbumsVinylTypes { get; set; }

        public virtual ICollection<TArtist> IdArtists { get; set; }
        public virtual ICollection<TGroup> IdGroups { get; set; }
    }
}
